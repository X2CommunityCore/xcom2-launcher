using System.Collections.Concurrent;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XCOM2Launcher.Helper;

namespace XCOM2Launcher.Mod
{
    public static class ModChangelogCache
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(nameof(ModChangelogCache));
        private static readonly ConcurrentDictionary<long, string> Cache = new ConcurrentDictionary<long, string>();

        private static readonly Regex Regexp = new Regex(
            @"<div class=""changelog headline"">\s*(.*?)\s*</div>" +
            @"(?:(?!<div class=""detailBox).)*?" +
            @"<p id=""[0-9]+"">(.*?)</p>",
            RegexOptions.Singleline | RegexOptions.Compiled);

        public static async Task<string> GetChangeLogAsync(long workshopID)
        {
            if (Cache.TryGetValue(workshopID, out var changelog))
            {
                return changelog;
            }

            try
            {
                using (var client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8; // Ensure proper encoding for special characters
                    var changelogRaw = await client.DownloadStringTaskAsync("https://steamcommunity.com/sharedfiles/filedetails/changelog/" + workshopID);
                    var htmlstrip = new Regex("<.*?>", RegexOptions.Compiled);
                    var output = new StringBuilder();

                    output.AppendLine(@"{\rtf1\ansi\deff0"); // Adding RTF header

                    foreach (Match m in Regexp.Matches(changelogRaw))
                    {
                        var desc = m.Groups[2].Value.Replace("<br>", @"\n"); // Preserving line breaks
                        desc = WebUtility.HtmlDecode(desc);
                        desc = htmlstrip.Replace(desc, "").Trim();

                        if (desc.Length == 0)
                            desc = "No description.";
                        else
                        {
                            desc = Tools.GetRtfEscapedString(desc).Replace(@"\\n", @"\line "); // Adding line breaks after escaping RTF special characters
                        }

                        output.AppendLine($@"\pard\li0\ul {m.Groups[1].Value.Trim()}\ul0\par\par"); // Adding heading with no indentation
                        output.AppendLine($@"\pard\li600 {desc}\par\par");  // Adding body with indentation (600 twips)
                    }

                    output.AppendLine("}"); // Closing RTF
                    return Cache.GetOrAdd(workshopID, output.ToString());
                }
            }
            catch (WebException ex)
            {
                Log.Warn($"Error loading changelog for Workshop Id: {workshopID}", ex);
                return @"{\rtf1\ansi\deff0 An error occurred while loading the changelog.}";    // Return a simple RTF error message
            }
        }
    }
}