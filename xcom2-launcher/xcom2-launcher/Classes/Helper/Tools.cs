using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    }
}
