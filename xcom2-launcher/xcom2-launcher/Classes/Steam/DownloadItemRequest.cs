using Steamworks;
using XCOM2Launcher.Classes.Steam;

namespace XCOM2Launcher.Steam
{
    public class DownloadItemRequest
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly Callback<DownloadItemResult_t> _callback;
        private readonly ulong _id;

        public DownloadItemRequest(ulong id, Callback<DownloadItemResult_t>.DispatchDelegate callback)
        {
            _id = id;
            _callback = Callback<DownloadItemResult_t>.Create(callback);
        }

        public void Send()
        {
            SteamAPIWrapper.Init();
            SteamUGC.DownloadItem(new PublishedFileId_t(_id), true);
        }
    }
}