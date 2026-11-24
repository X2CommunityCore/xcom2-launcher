using System.Text.RegularExpressions;

namespace XCOM2Launcher.Classes.Helper
{
    static partial class RegexHelper
    {
        // GeneratedRegex improves performance and reduces memory usage.
        // They are generated at compile time instead of at runtime.

        [GeneratedRegex(
            @"<div class=""detailBox workshopAnnouncement noFooter changeLogCtn"">\s*" +
            @"<div class=""changelog headline"">\s*(.*?)\s*</div>" +
            @"(?:(?!<div class=""detailBox).)*?" +
            @"<p id=""[0-9]+"">(.*?)</p>",
            RegexOptions.Singleline)]
        public static partial Regex ChangeLogRegex();


        [GeneratedRegex("<.*?>")]
        public static partial Regex HtmlStripRegex();


        [GeneratedRegex("^[+]?ModClassOverrides=\\(BaseGameClass=\"([^\"]+)\",ModClass=\"([^\"]+)\"\\)")]
        public static partial Regex ClassOverridesRegex();


        [GeneratedRegex("\\s+")]
        public static partial Regex WhitespaceRegex();


        [GeneratedRegex(@"(?i)^\s*ScreenClass\s*=\s*(?:class')?([a-z_]+)")]
        public static partial Regex ScreenClassRegex();


        [GeneratedRegex(@"(?<!\\\\)\[(/?)(.*?)(?<!\\\\)\]")]
        public static partial Regex BbCodeTagRegex();


        [GeneratedRegex(@"[\r\n]{1,2}")]
        public static partial Regex LineBreakRegex();


        [GeneratedRegex(@"^(?<category>.*?)\s\(\d*\):$")]
        public static partial Regex CategoryHeaderRegex();


        [GeneratedRegex(@"^\s*(?<name>.*?)[ ]*\t(?<id>.*?)[ ]*\t(?:.*=)?(?<sourceID>\d+)([ ]*\t(?<tags>.*?))?$", RegexOptions.Compiled | RegexOptions.Multiline)]
        public static partial Regex ModEntryRegex();


        [GeneratedRegex("[^\r\n]*")]
        public static partial Regex FirstLineRegex();
    }
}
