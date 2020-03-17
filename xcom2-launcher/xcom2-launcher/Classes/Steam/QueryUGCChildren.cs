using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Steamworks;
using XCOM2Launcher.Classes.Steam;

namespace XCOM2Launcher.Steam
{
    public class QueryUGCChildren
    {
        private CallResult<SteamUGCQueryCompleted_t> _onQueryCompleted;
        private UGCQueryHandle_t _queryHandle;
        private readonly uint DependencyCount;
        
        public ulong Id { get; }
        public bool Success { get; private set; }
        public bool Finished { get; private set; }
        public bool Cancelled { get; private set; }
        public List<ulong> Result { get; private set; }

        public QueryUGCChildren(ulong parentId, uint dependencyCount)
        {
            Id = parentId;
            DependencyCount = dependencyCount;
            Result = new List<ulong>();
        }

        public QueryUGCChildren Send()
        {
            if (DependencyCount == 0)
                return this;

            SteamAPIWrapper.Init();

            _onQueryCompleted = CallResult<SteamUGCQueryCompleted_t>.Create(QueryCompleted);
            _queryHandle = SteamUGC.CreateQueryUGCDetailsRequest(new[] {Id.ToPublishedFileID()}, 1);
            
            //SteamUGC.SetReturnLongDescription(_queryHandle, GetFullDescription);
            SteamUGC.SetReturnChildren(_queryHandle, true);

            var apiCall = SteamUGC.SendQueryUGCRequest(_queryHandle);
            _onQueryCompleted.Set(apiCall);

            return this;
        }

        public bool Cancel()
        {
            Cancelled = true;
            return !Finished;
        }

        public QueryUGCChildren WaitForResult()
        {
            if (DependencyCount == 0)
                return this;

            // Wait for Response
            while (!Finished && !Cancelled)
            {
                Thread.Sleep(10);
                SteamAPIWrapper.RunCallbacks();
            }

            if (Cancelled)
                return this;

            var idList = new PublishedFileId_t[DependencyCount];
            Success = SteamUGC.GetQueryUGCChildren(_queryHandle, 0, idList, (uint)idList.Length);
            SteamUGC.ReleaseQueryUGCRequest(_queryHandle);
            Result = idList.ToList().ConvertAll(item => item.m_PublishedFileId);
         
            return this;
        }

        private void QueryCompleted(SteamUGCQueryCompleted_t pCallback, bool bIOFailure)
        {
            Finished = true;
        }
    }
}