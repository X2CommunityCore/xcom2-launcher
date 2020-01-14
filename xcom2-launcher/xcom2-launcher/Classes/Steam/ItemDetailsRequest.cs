using System.Collections.Generic;
using System.Threading;
using Steamworks;
using XCOM2Launcher.Classes.Steam;

namespace XCOM2Launcher.Steam
{
    public class ItemDetailsRequest
    {
        private CallResult<SteamUGCQueryCompleted_t> _onQueryCompleted;
        private UGCQueryHandle_t _queryHandle;

        public List<ulong> Identifiers { get; }
        public bool GetFullDescription { get; }
        public bool Success { get; private set; }
        public bool Finished { get; private set; }
        public bool Cancelled { get; private set; }
        public List<SteamUGCDetails_t> Result { get; private set; }

        public ItemDetailsRequest(ulong id, bool GetDesc = false) : this(new List<ulong>() { id }, GetDesc) { 
        }

        public ItemDetailsRequest(List<ulong> identifiers, bool GetDesc = false)
        {
            Identifiers = new List<ulong>();
            Identifiers.AddRange(identifiers);
            GetFullDescription = GetDesc;
        }

        public ItemDetailsRequest Send()
        {
            SteamAPIWrapper.Init();

            PublishedFileId_t[] idList = Identifiers.ConvertAll(id => new PublishedFileId_t(id)).ToArray();
            
            _onQueryCompleted = CallResult<SteamUGCQueryCompleted_t>.Create(QueryCompleted);
            _queryHandle = SteamUGC.CreateQueryUGCDetailsRequest(idList, (uint)idList.Length);
            
            SteamUGC.SetReturnLongDescription(_queryHandle, GetFullDescription);
            SteamUGC.SetReturnChildren(_queryHandle, true);     // required, otherwise m_unNumChildren will always be 0

            var apiCall = SteamUGC.SendQueryUGCRequest(_queryHandle);
            _onQueryCompleted.Set(apiCall);

            return this;
        }

        public bool Cancel()
        {
            Cancelled = true;
            return !Finished;
        }

        public ItemDetailsRequest WaitForResult()
        {
            // Wait for Response
            while (!Finished && !Cancelled)
            {
                Thread.Sleep(10);
                SteamAPIWrapper.RunCallbacks();
            }

            if (Cancelled)
                return this;

            Result = new List<SteamUGCDetails_t>();

            for (uint i = 0; i < Identifiers.Count; i++)
            {
                // Retrieve Value
                Success = SteamUGC.GetQueryUGCResult(_queryHandle, i, out var result);
                Result.Add(result);
            }

            SteamUGC.ReleaseQueryUGCRequest(_queryHandle);
            return this;
        }

        public string GetPreviewURL()
        {
            if (!Finished)
                return null;

            SteamUGC.GetQueryUGCPreviewURL(_queryHandle, 0, out var url, 1000);
            return url;
        }

        private void QueryCompleted(SteamUGCQueryCompleted_t pCallback, bool bIOFailure)
        {
            Finished = true;
        }
    }
}