using Steamworks;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace XCOM2Launcher.Steam
{
    public static class SteamManager
    {
        private static volatile bool _initialized;
        private static readonly object _initlock = new object();
        private static readonly System.Timers.Timer _timer = new System.Timers.Timer(50);
        private static bool _shutdown;

        static SteamManager()
        {
            _timer.Elapsed += (sender, args) =>
            {
                SteamAPI.RunCallbacks();
            };
        }
        
        public static bool IsSteamRunning()
        {
            return SteamAPI.IsSteamRunning();
        }
        
        public static bool EnsureInitialized()
        {
            // we can avoid taking locks when _initialized is already true
            // ReSharper disable once InconsistentlySynchronizedField
            if (_initialized) return true;
            
            lock (_initlock)
            {
                if (_initialized) return true;
                if (_shutdown) return false;
                if (!SteamAPI.IsSteamRunning()) return false;
                
                var steamInitialized = SteamAPI.Init();
                if (steamInitialized)
                {
                    _timer.Start();
                    _initialized = true;
                }
            }

            return true;
        }

        public static void Shutdown()
        {
            lock (_initlock)
            {
                if (!_initialized) return;
                if (_shutdown) return;
                
                // stop timer before shutdown to avoid exceptions from steam
                _timer.Stop();
                
                SteamAPI.Shutdown();
                _shutdown = true;
                _initialized = false;
            }
        }

        // A thread-safe global cache to prevent the Garbage Collector from killing pending callbacks
        private static readonly ConcurrentDictionary<Guid, object> _pendingReferences = new ConcurrentDictionary<Guid, object>();

        public static Task<TResult> QueryResultAsync<TCompleted, TResult>(SteamAPICall_t apiCall, Func<TCompleted, bool, TResult> onCompleted)
             where TCompleted : struct, ISteamCallbackIdentity
        {
            var tcs = new TaskCompletionSource<TResult>(TaskCreationOptions.RunContinuationsAsynchronously);

            // Generate a unique token for this specific async operation
            var operationId = Guid.NewGuid();

            CallResult<TCompleted> callResult = null;

            callResult = CallResult<TCompleted>.Create((completed, bIoFailure) =>
            {
                try
                {
                    var result = onCompleted(completed, bIoFailure);
                    tcs.SetResult(result);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
                finally
                {
                    // Clean up native allocations and drop the global reference to let the GC collect it
                    callResult?.Dispose();
                    _pendingReferences.TryRemove(operationId, out _);
                }
            });

            // Attach the API call handle to Steamworks
            callResult.Set(apiCall);

            // Lock the reference globally so the GC doesn't wipe it out while waiting for Steam's backend response
            _pendingReferences[operationId] = callResult;

            return tcs.Task;
        }
    }
}
