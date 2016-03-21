using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCOM2Launcher.Steam
{
    internal class ItemDetailsRequest
    {
        private CallResult<SteamUGCQueryCompleted_t> OnSteamUGCQueryCompletedCallResult;

        private ulong id;

        private UGCQueryHandle_t m_UGCQueryHandle;


        public bool Finished { get; private set; }
        public bool Cancelled { get; private set; }

        public SteamUGCDetails_t Result { get; private set; }

        public ItemDetailsRequest(ulong id)
        {
            this.id = id;
        }

        public ItemDetailsRequest Send()
        {
            this.OnSteamUGCQueryCompletedCallResult = CallResult<SteamUGCQueryCompleted_t>.Create(OnSteamUGCQueryCompleted);
            m_UGCQueryHandle = SteamUGC.CreateQueryUGCDetailsRequest(new PublishedFileId_t[] { new PublishedFileId_t(id) }, 1);
            SteamAPICall_t api_call = SteamUGC.SendQueryUGCRequest(m_UGCQueryHandle);
            OnSteamUGCQueryCompletedCallResult.Set(api_call);

            return this;
        }

        public bool Cancel()
        {
            Cancelled = true;
            return !Finished;
        }

        public ItemDetailsRequest waitForResult()
        {
            // Wait for Response
            while (!Finished && !Cancelled)
            {
                Thread.Sleep(10);
                SteamAPI.RunCallbacks();
            }

            if (Cancelled)
                return this;

            // Retrieve Value
            var result = new SteamUGCDetails_t();
            bool ret = SteamUGC.GetQueryUGCResult(m_UGCQueryHandle, 0, out result);
            

            Result = result;
            return this;
        }

        public string GetPreviewURL()
        {
            if (!Finished)
                return null;

            string url = "";

            SteamUGC.GetQueryUGCPreviewURL(m_UGCQueryHandle, 0, out url, 1000);
            return url;
        }

        public void OnSteamUGCQueryCompleted(SteamUGCQueryCompleted_t pCallback, bool bIOFailure)
        {
            Finished = true;
        }
    }
}
