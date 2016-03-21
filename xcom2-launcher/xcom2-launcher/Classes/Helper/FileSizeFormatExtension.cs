using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace XCOM2Launcher.Helper
{
    public static class FileSizeFormatExtension 
    {
        [DllImport("Shlwapi.dll", CharSet = CharSet.Auto)]
        private static extern long StrFormatByteSize(long fileSize, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder buffer, int bufferSize);


        /// <summary>
        /// Converts a numeric value into a string that represents the number expressed as a size value in bytes, kilobytes, megabytes, or gigabytes, depending on the size.
        /// </summary>
        /// <param name="filelength">The numeric value to be converted.</param>
        /// <returns>the converted string</returns>
        public static string FormatAsFileSize(this long filesize)
        {
            StringBuilder sb = new StringBuilder(11);
            StrFormatByteSize(filesize, sb, sb.Capacity);
            return sb.ToString();
        }
    }
}
