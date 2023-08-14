using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Steamworks;

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

        static Workshop()
        {
            SteamManager.EnsureInitialized();
            _downloadItemCallback = Callback<DownloadItemResult_t>.Create(result => OnItemDownloaded?.Invoke(null, new DownloadItemEventArgs() { Result = result}));
        }
        
        public static ulong[] GetSubscribedItems()
        {
            var num = SteamUGC.GetNumSubscribedItems();
            var ids = new PublishedFileId_t[num];
            SteamUGC.GetSubscribedItems(ids, num);

            return ids.Select(t => t.m_PublishedFileId).ToArray();
        }

        public static void Subscribe(ulong id)
        {
            SteamUGC.SubscribeItem(id.ToPublishedFileID());
        }

        public static void Unsubscribe(ulong id)
        {
            SteamUGC.UnsubscribeItem(id.ToPublishedFileID());
        }

        /// <summary>
        /// Returns the UGC Details for the specified workshop id.
        /// </summary>
        /// <param name="id">Workshop id</param>
        /// <param name="getFullDescription">Sets whether to return the full description for the item. If set to false, the description is truncated at 255 bytes.</param>
        /// <returns>The requested data or the default struct (check for m_eResult == EResultNone), if the request failed.</returns>
        public static async Task<SteamUGCDetails> GetDetailsAsync(ulong id, bool getFullDescription = false)
        {
            var result = await GetDetailsAsync(new List<ulong> {id}, getFullDescription).ConfigureAwait(false);
            return result?.FirstOrDefault() ?? new SteamUGCDetails(new SteamUGCDetails_t(), Array.Empty<ulong>());
        }

        /// <summary>
        /// Returns a list of UGC Details for the specified workshop id's.
        /// </summary>
        /// <param name="identifiers">Workshop id's</param>
        /// <param name="getFullDescription">Sets whether to return the full description for the item. If set to false, the description is truncated at 255 bytes.</param>
        /// <returns>The requested data or null, if the request failed.</returns>
        public static async Task<List<SteamUGCDetails>> GetDetailsAsync(List<ulong> identifiers, bool getFullDescription = false)
        {
            if (identifiers == null)
                throw new ArgumentNullException(nameof(identifiers));

            if (identifiers.Count > MAX_UGC_RESULTS)
                throw new ArgumentException($"Max allowed number of identifiers is {MAX_UGC_RESULTS}.");

            if (!SteamManager.IsSteamRunning()) return null;

            var idList = identifiers
                .Where(x => x > 0)
                .Distinct()
                .Select(x => new PublishedFileId_t(x))
                .ToArray();
            if (idList.Length == 0) return new List<SteamUGCDetails>();

                var queryHandle = SteamUGC.CreateQueryUGCDetailsRequest(idList, (uint)idList.Length);
                SteamUGC.SetReturnLongDescription(queryHandle, getFullDescription);
                SteamUGC.SetReturnChildren(queryHandle, true); // required, otherwise m_unNumChildren will always be 0
            
                var apiCall = SteamUGC.SendQueryUGCRequest(queryHandle);

                var results = await SteamManager.QueryResultAsync<SteamUGCQueryCompleted_t, List<SteamUGCDetails>>(apiCall,
                    (result, ioFailure) =>
                    {
                        var details = new List<SteamUGCDetails>();

                        for (uint i = 0; i < result.m_unNumResultsReturned; i++)
                        {
                            // Retrieve Value
                            if (!SteamUGC.GetQueryUGCResult(queryHandle, i, out var detail))
                            {
                                return new List<SteamUGCDetails>();
                            }
                            
                            var childFileIds = new PublishedFileId_t[detail.m_unNumChildren];
                            var childIds = Array.Empty<ulong>();
                            var success = SteamUGC.GetQueryUGCChildren(queryHandle, i, childFileIds, (uint)childFileIds.Length);
                            if (success)
                            {
                                childIds = childFileIds.Select(x => x.m_PublishedFileId).ToArray();
                            }

                            details.Add(new SteamUGCDetails(detail, childIds));
                        }

                        SteamUGC.ReleaseQueryUGCRequest(queryHandle);
                        return details;
                    }).ConfigureAwait(false);
                return results;
        }

        public static EItemState GetDownloadStatus(ulong id)
        {
            return (EItemState)SteamUGC.GetItemState(new PublishedFileId_t(id));
        }

        public static InstallInfo GetInstallInfo(ulong id)
        {
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
            SteamUGC.DownloadItem(new PublishedFileId_t(id), true);
        }

        #endregion

        public static string GetUsername(ulong steamID)
        {
            var work_done = new System.Threading.ManualResetEventSlim(false);
            using (Callback<PersonaStateChange_t>.Create(result => { work_done.Set(); }))
            {
                bool success = SteamFriends.RequestUserInformation(new CSteamID(steamID), true);
                if (success)
                {
                    work_done.Wait(5000);
                    work_done.Reset();
                    return SteamFriends.GetFriendPersonaName(new CSteamID(steamID)) ?? string.Empty;
                }

                return string.Empty;
            }
        }
    }
}