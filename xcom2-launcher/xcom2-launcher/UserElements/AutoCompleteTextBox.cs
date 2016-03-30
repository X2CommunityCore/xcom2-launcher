using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace XCOM2Launcher.UserElements
{
    public class AutoCompleteTextBox : TextBox
    {
        private string _formerValue = string.Empty;
        private bool _isAdded;
        private ListBox _suggestionsListBox;

        public AutoCompleteTextBox()
        {
            InitializeComponent();
            ResetListBox();
        }

        public string[] Values { get; set; }

        public List<string> SelectedValues => Text.Split(' ').Where(str => str.Length > 0).ToList();

        private void InitializeComponent()
        {
            _suggestionsListBox = new ListBox();
            KeyDown += this_KeyDown;
            KeyUp += this_KeyUp;
        }

        private void ShowListBox()
        {
            var form = FindForm();
            if (form == null)
                return;


            if (!_isAdded)
            {
                _isAdded = true;
                form.Controls.Add(_suggestionsListBox);

                // Move to the top
                form.Controls.SetChildIndex(_suggestionsListBox, 0);
            }

            // update location
            _suggestionsListBox.Location = form.PointToClient(Parent.PointToScreen(Location));
            
            var lastCharIndex = SelectionStart - GetActiveWord().Length;
            var caretPosition = GetPositionFromCharIndex(lastCharIndex);

            _suggestionsListBox.Left += caretPosition.X - 2;
            _suggestionsListBox.Top += Height - 1;

            // show
            _suggestionsListBox.Visible = true;
        }

        private void ResetListBox()
        {
            _suggestionsListBox.Visible = false;
        }

        private void this_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateListBox();
        }

        private void this_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Tab:
                    if (!_suggestionsListBox.Visible)
                        break;

                    InsertWord((string)_suggestionsListBox.SelectedItem);
                    ResetListBox();
                    _formerValue = Text;

                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;

                case Keys.Down:
                    if (_suggestionsListBox.Visible)
                        _suggestionsListBox.SelectedIndex = (_suggestionsListBox.SelectedIndex + 1) % _suggestionsListBox.Items.Count;

                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;

                case Keys.Up:
                    var m = _suggestionsListBox.Items.Count;
                    if (_suggestionsListBox.Visible)
                        _suggestionsListBox.SelectedIndex = ((_suggestionsListBox.SelectedIndex - 1) % m + m) % m;

                    e.SuppressKeyPress = true;
                    break;
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            return keyData == Keys.Tab || base.IsInputKey(keyData);
        }

        private void UpdateListBox()
        {
            if (Text == _formerValue)
                return;


            _formerValue = Text;
            var word = GetActiveWord();

            if (word.Length == 0)
            {
                ResetListBox();
                return;
            }

            var ignore = SelectedValues;
            var matches = Values.Where(x => x.StartsWith(word, StringComparison.OrdinalIgnoreCase) && !ignore.Contains(x, StringComparer.OrdinalIgnoreCase)).OrderBy(str => str).ToList();
            if (matches.Count == 0)
            {
                ResetListBox();
                return;
            }

            _suggestionsListBox.Items.Clear();

            foreach (var match in matches)
                _suggestionsListBox.Items.Add(match);

            _suggestionsListBox.SelectedIndex = 0;

            using (var graphics = _suggestionsListBox.CreateGraphics())
            {
                _suggestionsListBox.Width = 5 + matches.Max(s => (int) graphics.MeasureString(s, _suggestionsListBox.Font).Width);
                _suggestionsListBox.Height = _suggestionsListBox.ItemHeight* (1+ matches.Count);
            }

            ShowListBox();
            Focus();
        }

        private string GetActiveWord()
        {
            var text = Text;
            var pos = SelectionStart;

            var posStart = text.LastIndexOf(' ', pos < 1 ? 0 : pos - 1);
            posStart = posStart == -1 ? 0 : posStart + 1;
            var posEnd = text.IndexOf(' ', pos);
            posEnd = posEnd == -1 ? text.Length : posEnd;

            var length = posEnd - posStart < 0 ? 0 : posEnd - posStart;

            return text.Substring(posStart, length);
        }

        private void InsertWord(string newTag)
        {
            var text = Text;
            var pos = SelectionStart;

            var posStart = text.LastIndexOf(' ', pos < 1 ? 0 : pos - 1);
            posStart = posStart == -1 ? 0 : posStart + 1;
            var posEnd = text.IndexOf(' ', pos);

            var firstPart = text.Substring(0, posStart) + newTag;
            var updatedText = firstPart + (posEnd == -1 ? "" : text.Substring(posEnd, text.Length - posEnd));


            Text = updatedText;
            SelectionStart = firstPart.Length;
        }
    }
}