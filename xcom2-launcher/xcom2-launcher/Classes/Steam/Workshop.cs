using System;
using System.Linq;
using System.Threading;
using Steamworks;
using XCOM2Launcher.Classes.Steam;

namespace XCOM2Launcher.Steam
{
    public static class Workshop
    {

        public static ulong[] GetSubscribedItems()
        {
            SteamAPIWrapper.Init();

            var num = SteamUGC.GetNumSubscribedItems();
            var ids = new PublishedFileId_t[num];
            SteamUGC.GetSubscribedItems(ids, num);

            return ids.Select(t => t.m_PublishedFileId).ToArray();
        }

        public static void Unsubscribe(ulong id)
        {
            SteamAPIWrapper.Init();

            SteamUGC.UnsubscribeItem(id.ToPublishedFileID());
            SteamAPIWrapper.RunCallbacks();
        }

        public static SteamUGCDetails_t GetDetails(ulong id)
        {
            var request = new ItemDetailsRequest(id);

            request.Send().WaitForResult();

            return request.Result;
        }


        public static EItemState GetDownloadStatus(ulong id)
        {
            SteamAPIWrapper.Init();
            return (EItemState)SteamUGC.GetItemState(new PublishedFileId_t(id));
        }

        public static InstallInfo GetInstallInfo(ulong id)
        {
            SteamAPIWrapper.Init();

            ulong punSizeOnDisk;
            string pchFolder;
            uint punTimeStamp;

            SteamUGC.GetItemInstallInfo(new PublishedFileId_t(id), out punSizeOnDisk, out pchFolder, 256, out punTimeStamp);

            return new InstallInfo
            {
                ItemID = id,
                SizeOnDisk = punSizeOnDisk,
                Folder = pchFolder,
                TimeStamp = new DateTime(punTimeStamp * 10)
            };
        }

        public static UpdateInfo GetDownloadInfo(ulong id)
        {
            SteamAPIWrapper.Init();

            ulong punBytesProcessed;
            ulong punBytesTotal;

            SteamUGC.GetItemDownloadInfo(new PublishedFileId_t(id), out punBytesProcessed, out punBytesTotal);

            return new UpdateInfo
            {
                ItemID = id,
                BytesProcessed = punBytesProcessed,
                BytesTotal = punBytesTotal
            };
        }


        #region Download Item
        public class DownloadItemEventArgs : EventArgs
        {
            public DownloadItemResult_t Result { get; set; }
        }

        // ReSharper disable once NotAccessedField.Local
        private static Callback<DownloadItemResult_t> _downloadItemCallback;
        public delegate void DownloadItemHandler(object sender, DownloadItemEventArgs e);
        public static event DownloadItemHandler OnItemDownloaded;
        public static void DownloadItem(ulong id)
        {
            _downloadItemCallback = Callback<DownloadItemResult_t>.Create(ItemDownloaded);
            SteamUGC.DownloadItem(new PublishedFileId_t(id), true);
        }

        private static void ItemDownloaded(DownloadItemResult_t result)
        {
            // Make sure someone is listening to event
            if (OnItemDownloaded == null) return;

            DownloadItemEventArgs args = new DownloadItemEventArgs { Result = result };
            OnItemDownloaded(null, args);
        }

        #endregion

        public static string GetUsername(ulong steamID)
        {
            // todo
            return SteamFriends.GetPlayerNickname(new CSteamID(steamID));
        }
    }
}