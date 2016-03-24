using Steamworks;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using XCOM2Launcher.Classes.Steam;

namespace XCOM2Launcher.Steam
{
    public static class Workshop
    {

        //static private bool CallbackServiceRunning = false;
        //static private Thread t;
        //public static void StartCallbackService()
        //{
        //    if (CallbackServiceRunning)
        //        return;

        //    ThreadStart s = new ThreadStart(() =>
        //    {
        //        while (true)
        //        {
        //            SteamAPI.RunCallbacks();
        //            Thread.Sleep(100);
        //        }
        //    });

        //    t = new Thread(s);
        //    t.Start();
        //}

        //public static void StopCallbackService()
        //{
        //    t.Abort();
        //}

        public static ulong[] GetSubscribedItems()
        {
            var num = SteamUGC.GetNumSubscribedItems();
            var IDs = new PublishedFileId_t[num];
            SteamUGC.GetSubscribedItems(IDs, num);

            return IDs.Select(t => t.m_PublishedFileId).ToArray();
        }


        public static SteamUGCDetails_t GetDetails(ulong id)
        {
            var request = new ItemDetailsRequest(id);

            request.Send().waitForResult();

            return request.Result;
        }


        public static Steamworks.EItemState GetDownloadStatus(ulong id)
        {
            SteamAPIWrapper.Init();
            return (Steamworks.EItemState)SteamUGC.GetItemState(new PublishedFileId_t(id));
        }

        public static InstallInfo GetInstallInfo(ulong id)
        {
            ulong punSizeOnDisk = 0;
            string pchFolder = null;
            uint punTimeStamp = 0;

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
            ulong punBytesProcessed = 0;
            ulong punBytesTotal = 0;

            Steamworks.SteamUGC.GetItemDownloadInfo(new PublishedFileId_t(id), out punBytesProcessed, out punBytesTotal);

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

        private static Callback<DownloadItemResult_t> m_DownloadItemResult;
        public delegate void DownloadItemHandler(object sender, DownloadItemEventArgs e);
        public static event DownloadItemHandler OnItemDownloaded;
        public static void DownloadItem(ulong id)
        {
            m_DownloadItemResult = Callback<DownloadItemResult_t>.Create(ItemDownloaded);
            SteamUGC.DownloadItem(new PublishedFileId_t(id), true);
        }

        private static void ItemDownloaded(DownloadItemResult_t result)
        {
            // Make sure someone is listening to event
            if (OnItemDownloaded == null) return;

            DownloadItemEventArgs args = new DownloadItemEventArgs { Result = result };
            OnItemDownloaded(null, args);
        }

        internal static string GetUsername(ulong m_ulSteamIDOwner)
        {
            var userid = new Steamworks.CSteamID(m_ulSteamIDOwner);
            var m_Friend = SteamFriends.GetFriendByIndex(0, EFriendFlags.k_EFriendFlagImmediate);

            return SteamFriends.GetPlayerNickname(m_Friend);
        }
        #endregion
    }



    public class UpdateInfo
    {
        public ulong ItemID { get; set; }
        public ulong BytesProcessed { get; set; }
        public ulong BytesTotal { get; set; }
        public float Process
        {
            get
            {
                if (BytesTotal == 0)
                    return float.NaN;

                return BytesProcessed / BytesTotal;
            }
        }
    }

    public class InstallInfo
    {
        public ulong ItemID { get; set; }
        public ulong SizeOnDisk { get; set; }
        public string Folder { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}