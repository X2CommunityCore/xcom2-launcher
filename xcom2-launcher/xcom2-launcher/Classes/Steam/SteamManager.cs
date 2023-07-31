using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Steamworks;

namespace XCOM2Launcher.Steam
{
    public static class SteamManager
    {
        private static bool _initialized;
        private static readonly object _initlock = new object();
        private static readonly System.Timers.Timer _timer = new System.Timers.Timer(5);

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
            if (_initialized) return true;
            if (!SteamAPI.IsSteamRunning()) return false;

            lock (_initlock)
            {
                if (_initialized) return true;

                SteamAPI.Init();
                _timer.Start();
                _initialized = true;
            }

            return true;
        }

        public static void Shutdown()
        {
            if (!_initialized) return;
            
            lock (_initlock)
            {
                if (!_initialized) return;
                
                // stop timer before shutdown to avoid exceptions from steam
                _timer.Stop();
                
                SteamAPI.Shutdown();
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