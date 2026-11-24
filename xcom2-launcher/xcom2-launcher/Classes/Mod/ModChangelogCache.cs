using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XCOM2Launcher.Classes.Helper;

namespace XCOM2Launcher.Classes.Mod
{
    public static class ModChangelogCache
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(nameof(ModChangelogCache));
        private static readonly ConcurrentDictionary<long, string> Cache = new();

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

                foreach (Match m in RegexHelper.ChangeLogRegex().Matches(changelogRaw))
                {
                    var desc = m.Groups[2].Value.Replace("<br>", "\r\n\t");
                    desc = WebUtility.HtmlDecode(desc);
                    desc = RegexHelper.HtmlStripRegex().Replace(desc, "").Trim();

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