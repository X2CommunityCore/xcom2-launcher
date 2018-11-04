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

                if (value != null) UpdateFromSettings();
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
            bool argumentEnabledOld = IsArgumentEnabled(Text, currentArguments);

            // If the new value already matches the argument string, do nothing
            if (argumentEnabledOld == Checked) return;

            if (Checked)
            {
                currentArguments.Add(Text);
            }
            else
            {
                // Cannot just do "currentArguments.Remove(Text)" since that does case sensitive check

                GetExistingMatchingArguments(Text, currentArguments)
                    .ToList() // Break dependency on underlying list
                    .ForEach(s => currentArguments.Remove(s));
            }

            // Pack the arguments back together
            CurrentArguments = currentArguments;
        }

        public void UpdateFromSettings()
        {
            _doNotUpdateSettings = true;
            Checked = IsArgumentEnabled(Text, CurrentArguments);
            _doNotUpdateSettings = false;
        }

        private List<string> CurrentArguments
        {
            get => Settings.Arguments.Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            set => Settings.Arguments = string.Join(" ", value);
        }

        private static IEnumerable<string> GetExistingMatchingArguments(string argument, IEnumerable<string> arguments)
        {
            return arguments.Where(s => StringComparer.OrdinalIgnoreCase.Equals(s, argument));
        }

        private static bool IsArgumentEnabled(string argument, IEnumerable<string> arguments)
        {
            return GetExistingMatchingArguments(argument, arguments).Any();
        }
    }
}