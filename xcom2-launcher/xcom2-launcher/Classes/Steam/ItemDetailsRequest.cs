using System.Threading;
using Steamworks;
using XCOM2Launcher.Classes.Steam;

namespace XCOM2Launcher.Steam
{
    public class ItemDetailsRequest
    {
        private CallResult<SteamUGCQueryCompleted_t> _onQueryCompleted;

        private UGCQueryHandle_t _queryHandle;

        public ItemDetailsRequest(ulong id)
        {
            ID = id;
        }


        public ulong ID { get; }

        public bool Success { get; private set; }
        public bool Finished { get; private set; }
        public bool Cancelled { get; private set; }


        public SteamUGCDetails_t Result { get; private set; }

        public ItemDetailsRequest Send()
        {
            _onQueryCompleted = CallResult<SteamUGCQueryCompleted_t>.Create(QueryCompleted);
            _queryHandle = SteamUGC.CreateQueryUGCDetailsRequest(new[] {ID.ToPublishedFileID()}, 1);

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

            // Retrieve Value
            SteamUGCDetails_t result;
            Success = SteamUGC.GetQueryUGCResult(_queryHandle, 0, out result);

            Result = result;
            return this;
        }

        public string GetPreviewURL()
        {
            if (!Finished)
                return null;

            string url;

            SteamUGC.GetQueryUGCPreviewURL(_queryHandle, 0, out url, 1000);
            return url;
        }

        private void QueryCompleted(SteamUGCQueryCompleted_t pCallback, bool bIOFailure)
        {
            Finished = true;
        }
    }
}