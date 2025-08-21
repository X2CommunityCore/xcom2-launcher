using System;
using System.Threading.Tasks;
using Steamworks;

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

        public static Task<TResult> QueryResultAsync<TCompleted, TResult>(SteamAPICall_t apiCall, Func<TCompleted, bool, TResult> onCompleted)
        {
            var tcs = new TaskCompletionSource<TResult>(TaskCreationOptions.RunContinuationsAsynchronously);

            var callResult = CallResult<TCompleted>.Create(OnCompleted);
            callResult.Set(apiCall);

            return tcs.Task;

            void OnCompleted(TCompleted completed, bool bIoFailure)
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
            }
        }
    }
}