using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace XCOM2Launcher.UserElements
{
    class LaunchArgumentCheckbox : CheckBox
    {
        private Settings _settings;

        public Settings Settings
        {
            get => _settings;
            set
            {
                _settings = null; // Prevent updating setting due to event listener
                if (value == null) return;

                Checked = GetCurrentArguments(value).Contains(Text);
                _settings = value;
            }
        }

        public LaunchArgumentCheckbox()
        {
            CheckedChanged += HandleCheckedChanged;
        }

        private void HandleCheckedChanged(object sender, EventArgs e)
        {
            if (Settings == null) return;

            // Unpack the arguments
            List<string> currentArguments = GetCurrentArguments(Settings);
            bool argumentEnabledOld = currentArguments.Contains(Text);

            // If the new value already matches the argument string, do nothing
            if (argumentEnabledOld == Checked) return;

            if (Checked)
            {
                currentArguments.Add(Text);
            }
            else
            {
                currentArguments.Remove(Text);
            }

            // Pack the arguments back together
            Settings.Arguments = string.Join(" ", currentArguments);
        }

        private static List<string> GetCurrentArguments(Settings settings)
        {
            return settings.Arguments
                .Split(' ')
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();
        }
    }
}
