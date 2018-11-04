using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace XCOM2Launcher.UserElements
{
    class LaunchArgumentCheckbox : CheckBox
    {
        private Settings _settings;
        private bool _doNotUpdateSettings;

        public Settings Settings
        {
            get => _settings;
            set
            {
                _settings = value;
                UpdateFromSettings();
            }
        }

        public LaunchArgumentCheckbox()
        {
            CheckedChanged += HandleCheckedChanged;
        }

        private void HandleCheckedChanged(object sender, EventArgs e)
        {
            if (_doNotUpdateSettings) return;
            if (Settings == null) return;

            // Unpack the arguments
            List<string> currentArguments = CurrentArguments;
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
            CurrentArguments = currentArguments;
        }

        private void UpdateFromSettings()
        {
            _doNotUpdateSettings = true;
            Checked = CurrentArguments.Contains(Text);
            _doNotUpdateSettings = false;
        }

        private List<string> CurrentArguments
        {
            get => Settings.Arguments.Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            set => Settings.Arguments = string.Join(" ", value);
        }
    }
}