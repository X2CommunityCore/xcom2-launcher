using Steamworks;

namespace XCOM2Launcher.Steam
{
    public class SteamUGCDetails
    {
        public SteamUGCDetails(SteamUGCDetails_t details, ulong[] children)
        {
            Details = details;
            Children = children;
        }

        public SteamUGCDetails_t Details { get; }
        public ulong[] Children { get; }
    }
}