/////////////////////////////////////////////////////////////////////
//  Copyright (c) 2007 Aaron Lerch
//  http://www.aaronlerch.com/
//
//  Permission is hereby granted, free of charge, to any person
//  obtaining a copy of this software and associated documentation
//  files (the "Software"), to deal in the Software without
//  restriction, including without limitation the rights to use,
//  copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the
//  Software is furnished to do so, subject to the following
//  conditions:
//
//  The above copyright notice and this permission notice shall be
//  included in all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//  OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//  HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//  WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//  OTHER DEALINGS IN THE SOFTWARE.
/////////////////////////////////////////////////////////////////////

using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace XCOM2Launcher.UserElements
{
	public partial class CueTextBox : TextBox
	{
		#region PInvoke Helpers

		private static uint ECM_FIRST = 0x1500;
		private static uint EM_SETCUEBANNER = ECM_FIRST + 1;

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
		private static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, String lParam);

		#endregion PInvoke Helpers

		#region CueText

		private string _cueText = String.Empty;

		/// <summary>
		/// Gets or sets the text the <see cref="TextBox"/> will display as a cue to the user.
		/// </summary>
		[Description("The text value to be displayed as a cue to the user.")]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public string CueText
		{
			get { return _cueText; }
			set
			{
				if (value == null)
				{
					value = String.Empty;
				}

				if (!_cueText.Equals(value, StringComparison.CurrentCulture))
				{
					_cueText = value;
					UpdateCue();
					OnCueTextChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Occurs when the <see cref="CueText"/> property value changes.
		/// </summary>
		public event EventHandler CueTextChanged;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnCueTextChanged(EventArgs e)
		{
			EventHandler handler = CueTextChanged;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		#endregion CueText

		#region ShowCueTextOnFocus

		private bool _showCueTextWithFocus = false;

		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="TextBox"/> will display the <see cref="CueText"/>
		/// even when the control has focus.
		/// </summary>
		[Description("Indicates whether the CueText will be displayed even when the control has focus.")]
		[Category("Appearance")]
		[DefaultValue(false)]
		[Localizable(true)]
		public bool ShowCueTextWithFocus
		{
			get { return _showCueTextWithFocus; }
			set
			{
				if (_showCueTextWithFocus != value)
				{
					_showCueTextWithFocus = value;
					UpdateCue();
					OnShowCueTextWithFocusChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Occurs when the <see cref="ShowCueTextWithFocus"/> property value changes.
		/// </summary>
		public event EventHandler ShowCueTextWithFocusChanged;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnShowCueTextWithFocusChanged(EventArgs e)
		{
			EventHandler handler = ShowCueTextWithFocusChanged;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		#endregion ShowCueTextOnFocus

		#region Overrides

		protected override void OnHandleCreated(EventArgs e)
		{
			UpdateCue();

			base.OnHandleCreated(e);
		}


		#endregion Overrides

		private void UpdateCue()
		{
			// If the handle isn't yet created, 
			// this will be called when it is created
			if (this.IsHandleCreated)
			{
				SendMessage(new HandleRef(this, this.Handle), EM_SETCUEBANNER, (_showCueTextWithFocus) ? new IntPtr(1) : IntPtr.Zero, _cueText);
			}
		}
	}
}