using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Steamworks;

namespace XCOM2Launcher.Classes.Steam
{
    /// <summary>
    /// A hacky attempt to avoid race conditions caused by accessing the native Steam API from multiple threads by serializing access to it
    /// </summary>
    public static class SteamAPIWrapper
    {
        private static readonly object Mutex = new object();

        public static void RunCallbacks()
        {
            lock (Mutex)
            {
                SteamAPI.RunCallbacks();
            }
        }

        public static void Shutdown()
        {
            lock (Mutex)
            {
                SteamAPI.Shutdown();
            }
        }

        public static bool Init()
        {
            lock (Mutex)
            {
                return SteamAPI.Init();
            }
        }

        public static bool InitSafe()
        {
            lock (Mutex)
            {
                return SteamAPI.InitSafe();
            }
        }

        public static bool RestartAppIfNecessary(AppId_t unOwnAppID)
        {
            lock (Mutex)
            {
                return SteamAPI.RestartAppIfNecessary(unOwnAppID);
            }
        }


    }
}
