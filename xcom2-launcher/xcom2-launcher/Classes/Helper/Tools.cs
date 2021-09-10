using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace XCOM2Launcher.Helper {
    internal static class Tools {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(nameof(Tools));
        
        public static void StartProcess(string fileName, string arguments = "")
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Process.Start(fileName, arguments);
            }
            catch (Win32Exception ex)
            {
                Log.Warn($"Unable start process '{fileName}' {arguments}", ex);
                MessageBox.Show("Unable to open target process. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                Cursor.Current = Cursors.Default;
            }

        }

        public static void HandleNavigateWebBrowserControl(object sender, WebBrowserNavigatingEventArgs args)
        {
            string url = args.Url.ToString();

            if (url.Equals("about:blank", System.StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }
                
            //cancel the current event
            args.Cancel = true;

            // open URLs in the user's default browser
            //Process.Start(e.Url.ToString());
            StartProcess(args.Url.ToString());
        }


        /// <summary>
        /// Writes the text <paramref name="content"/> to the file specified in <paramref name="path"/>.
        /// Used FileOptions.WriteThrough to write directly to disk.
        /// Creates a backup file with .bak extension.
        /// Creates a temporary file with .tmp extension that will be used to atomically replace the target file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        public static void WriteTextToFileSave(string path, string content)
        {
            var backupFileName = path + ".bak";
            var tempFileName = path + ".tmp";

            var data = Encoding.UTF8.GetBytes(content);

            // Write data to temporary file
            using (var tempFile = File.Create(tempFileName, 4096, FileOptions.WriteThrough))
            {
                tempFile.Write(data, 0, data.Length);
            }

            // delete any existing backups
            if (File.Exists(backupFileName))
            {
                File.Delete(backupFileName);
            }

            // replace the content
            if (File.Exists(path))
            {
                File.Replace(tempFileName, path, backupFileName);
            }
            else
            {
                File.Move(tempFileName, path);
            }
        }

        public static string GetRtfEscapedString(string s)
        {
            var sb = new StringBuilder();
            
            foreach (var c in s)
            {
                if(c == '\\' || c == '{' || c == '}')
                {
                    // \, { and } are RTF control chars and need to be escaped
                    sb.Append(@"\" + c);
                }
                else if (c <= 0x7f)
                {
                    sb.Append(c);
                }
                else
                {
                    // In RTF, everything char above 7-bit ASCII needs to be escaped
                    sb.Append("\\u" + Convert.ToUInt32(c) + "?");
                }
            }

            return sb.ToString();
        }

        public static bool CompareDirectories(string dir1, string dir2)
        {
            if (dir1 == null || dir2 == null)
            {
                return false;
            }

            // Trim/ignore trailing directory separators for the comparison
            dir1 = dir1.TrimEnd(Path.DirectorySeparatorChar).TrimEnd(Path.AltDirectorySeparatorChar);
            dir2 = dir2.TrimEnd(Path.DirectorySeparatorChar).TrimEnd(Path.AltDirectorySeparatorChar);

            // Create DirectoryInfo to get a consistent directory style for comparison
            var dirInfo1 = new DirectoryInfo(dir1);
            var dirInfo2 = new DirectoryInfo(dir2);
            var result = string.Compare(dirInfo1.FullName, dirInfo2.FullName, StringComparison.InvariantCultureIgnoreCase);

            return result == 0;
        }
    }
}
