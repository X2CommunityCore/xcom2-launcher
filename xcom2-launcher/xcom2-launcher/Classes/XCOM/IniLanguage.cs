using System.Drawing;
using System.Text.RegularExpressions;
using FastColoredTextBoxNS;

namespace XCOM2Launcher.XCOM
{
	class IniLanguage
	{
		private static readonly Style CommentStyle = new TextStyle(Brushes.Green, null, FontStyle.Regular);
		private static readonly Style SectionStyle = new TextStyle(Brushes.DeepSkyBlue, null, FontStyle.Bold);
		private static readonly Style SeparatorStyle = new TextStyle(Brushes.Chocolate, null, FontStyle.Bold);

		public static void Process(TextChangedEventArgs args)
		{
			args.ChangedRange.ClearStyle(CommentStyle);
			args.ChangedRange.ClearStyle(SectionStyle);
			args.ChangedRange.ClearStyle(SeparatorStyle);

			args.ChangedRange.SetStyle(CommentStyle, @";.*$", RegexOptions.Multiline);
			args.ChangedRange.SetStyle(SectionStyle, @"^\[.*\]", RegexOptions.Multiline);
			args.ChangedRange.SetStyle(SeparatorStyle, @"^.*?(?<range>=)", RegexOptions.Multiline);
		}
	}
}
