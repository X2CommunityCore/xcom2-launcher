using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XCOM2Launcher.Classes.Mod
{
    public static class ModChangelogCache
    {
        private static Dictionary<long, string> changelogDict = new Dictionary<long, string>();

        public static async void GetChangeLog(long WorkshopID, Action<string> action)
        {
            if (changelogDict.ContainsKey(WorkshopID))
            {
                action(changelogDict[WorkshopID]);
            }
            else
            {
                WebClient changelogdownload = new WebClient();

                string changelograw = await changelogdownload.DownloadStringTaskAsync(new Uri("https://steamcommunity.com/sharedfiles/filedetails/changelog/" + WorkshopID));
                Regex rgx = new Regex("<div class=\"detailBox workshopAnnouncement noFooter\">[\\s]*<div class=\"headline\">[\\s]*(.*)[\\s]*</div>[\\s]*<p id=\"[0-9]+\">(.*)</p>");
                string changelogFormatted = "";
                foreach (Match m in rgx.Matches(changelograw))
                {
                    Regex htmlstrip = new Regex("<.*?>", RegexOptions.Compiled);
                    string desc = m.Groups[2].ToString();
                    changelogFormatted += m.Groups[1].ToString() + "\n" + htmlstrip.Replace(desc, "") + "\n\n";
                }
                changelogdownload.Dispose();
                if (changelogDict.ContainsKey(WorkshopID) == false)
                    changelogDict.Add(WorkshopID, changelogFormatted);
                action(changelogDict[WorkshopID]);

            }
        }
    }
}
