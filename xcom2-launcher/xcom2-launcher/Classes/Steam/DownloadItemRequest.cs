using Steamworks;

namespace XCOM2Launcher
{
    public class DownloadItemRequest
    {
        private Callback<DownloadItemResult_t> callback;
        private ulong id;

        public DownloadItemRequest(ulong id, Callback<DownloadItemResult_t>.DispatchDelegate callback)
        {
            this.id = id;
            this.callback = Callback<DownloadItemResult_t>.Create(callback);
        }

        public void Send()
        {
            SteamUGC.DownloadItem(new PublishedFileId_t(id), true);
        }
    }
}