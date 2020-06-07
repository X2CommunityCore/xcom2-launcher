using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
