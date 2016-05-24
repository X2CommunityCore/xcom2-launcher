using Steamworks;
using System.Collections.Generic;
using System;

namespace XCOM2Launcher.Steam
{
    public static class StaticExtension
    {
        public static PublishedFileId_t ToPublishedFileID(this ulong id) => new PublishedFileId_t(id);
    }


    public class UpdateInfo
    {
        public ulong ItemID { get; set; }
        public ulong BytesProcessed { get; set; }
        public ulong BytesTotal { get; set; }
        public double Process
        {
            get
            {
                if (BytesTotal == 0)
                    return double.NaN;

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