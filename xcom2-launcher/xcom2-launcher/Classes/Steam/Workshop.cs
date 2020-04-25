using System;
using System.Collections.Generic;
using System.Linq;
using Steamworks;
using XCOM2Launcher.Classes.Steam;

namespace XCOM2Launcher.Steam
{
    public static class Workshop
    {
        public const string APPID_FILENAME = "steam_appid.txt";

        /// <summary>
        /// According to Steamworks API constant kNumUGCResultsPerPage.
        /// The maximum number of results that you'll receive for a query result.
        /// </summary>
        public const int MAX_UGC_RESULTS = 50; // according to 

        public static ulong[] GetSubscribedItems()
        {
            SteamAPIWrapper.Init();

            var num = SteamUGC.GetNumSubscribedItems();
            var ids = new PublishedFileId_t[num];
            SteamUGC.GetSubscribedItems(ids, num);

            return ids.Select(t => t.m_PublishedFileId).ToArray();
        }

        public static void Subscribe(ulong id)
        {
            SteamAPIWrapper.Init();

            SteamUGC.SubscribeItem(id.ToPublishedFileID());
        }

        public static void Unsubscribe(ulong id)
        {
            SteamAPIWrapper.Init();

            SteamUGC.UnsubscribeItem(id.ToPublishedFileID());
        }

        public static SteamUGCDetails_t GetDetails(ulong id, bool GetDesc = false)
        {
            var result = GetDetails(new List<ulong> {id}, GetDesc);
            return result.FirstOrDefault();
        }

        public static List<SteamUGCDetails_t> GetDetails(List<ulong> identifiers, bool GetDesc = false)
        {
            if (identifiers == null)
                throw new ArgumentNullException(nameof(identifiers));

            if (identifiers.Count > MAX_UGC_RESULTS)
                throw new ArgumentException($"Max allowed number of identifiers is {MAX_UGC_RESULTS}.");

            var request = new ItemDetailsRequest(identifiers, GetDesc);
            request.Send().WaitForResult();

            return request.Success ? request.Result : null;
        }

        public static List<ulong> GetDependencies(ulong workShopId, uint dependencyCount)
        {
            if (dependencyCount <= 0)
            {
                return new List<ulong>();
            }

            QueryUGCChildren request = new QueryUGCChildren(workShopId, dependencyCount);
            request.Send().WaitForResult();
            
            return request.Success ? request.Result : null;
        }

        public static List<ulong> GetDependencies(SteamUGCDetails_t details)
        {
            return GetDependencies(details.m_nPublishedFileId.m_PublishedFileId, details.m_unNumChildren);
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
            System.Threading.ManualResetEvent work_done = new System.Threading.ManualResetEvent(false);
            var _profileCallback = Callback<PersonaStateChange_t>.Create(delegate (PersonaStateChange_t result)
            {
                work_done.Set();
            });
            bool success = SteamFriends.RequestUserInformation(new CSteamID(steamID), true);
            
            if (success)
            {
                work_done.WaitOne(5000);
                work_done.Reset();
            }

            return SteamFriends.GetFriendPersonaName(new CSteamID(steamID));
        }
    }
}