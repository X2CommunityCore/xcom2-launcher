using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XCOM2Launcher.Classes.Mod
{
    public static partial class ModChangelogCache
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(nameof(ModChangelogCache));
        private static readonly ConcurrentDictionary<long, string> Cache = new();

        // GeneratedRegex for better performance and maintainability
        // Generated at compile time, so no need to compile regex at runtime
        [GeneratedRegex(
            @"<div class=""detailBox workshopAnnouncement noFooter changeLogCtn"">\s*" +
            @"<div class=""changelog headline"">\s*(.*?)\s*</div>" +
            @"(?:(?!<div class=""detailBox).)*?" +
            @"<p id=""[0-9]+"">(.*?)</p>",
            RegexOptions.Singleline)]
        private static partial Regex ChangeLogRegex();

        [GeneratedRegex("<.*?>")]
        private static partial Regex HtmlStripRegex();

        public static async Task<string> GetChangeLogAsync(long workshopID)
        {
            if (Cache.TryGetValue(workshopID, out var changelog))
            {
                return changelog;
            }

            try
            {
                using var client = new HttpClient();
                var changelogRaw = await client.GetStringAsync("https://steamcommunity.com/sharedfiles/filedetails/changelog/" + workshopID);

                var output = new StringBuilder();

                foreach (Match m in ChangeLogRegex().Matches(changelogRaw))
                {
                    var desc = m.Groups[2].Value.Replace("<br>", "\r\n\t");
                    desc = WebUtility.HtmlDecode(desc);
                    desc = HtmlStripRegex().Replace(desc, "").Trim();

                    if (desc.Length == 0)
                        desc = "No description.";

                    output.AppendLine(m.Groups[1].Value.Trim());
                    output.AppendLine("\t" + desc);
                    output.AppendLine();
                }

                Cache.TryAdd(workshopID, output.ToString());

                return output.ToString();
            }
            catch (HttpRequestException ex)
            {
                Log.Warn($"Error loading changelog for Workshop Id: {workshopID}", ex);
                return "An error occurred while loading the changelog.";
            }
        }
    }
}