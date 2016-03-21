using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCOM2Launcher.Mod
{
    public class ModList
    {
        public Dictionary<string, ModCategory> Entries { get; private set; } = new Dictionary<string, ModCategory>();

        [JsonIgnore]
        public IEnumerable<ModEntry> All { get { return Entries.SelectMany(c => c.Value.Entries); } }

        [JsonIgnore]
        public IEnumerable<string> Categories { get { return Entries.Select(c => c.Key); } }

        [JsonIgnore]
        public IEnumerable<ModEntry> Active { get { return All.Where(m => m.isActive); } }



        public ModEntry FindByPath(string path)
        {
            return All.SingleOrDefault(m => m.Path == path);
        }

        public Dictionary<string, List<ModEntry>> getOverrides(IEnumerable<ModEntry> mod_subset)
        {
            Dictionary<string, List<ModEntry>> overrides = new Dictionary<string, List<ModEntry>>();

            foreach (ModEntry mod in mod_subset)
                foreach (ModClassOverride overwrite in mod.GetClassOverrides())
                {
                    if (overrides.ContainsKey(overwrite.OldClass))
                        overrides[overwrite.OldClass].Add(mod);

                    else
                        overrides[overwrite.OldClass] = new List<ModEntry> { mod };
                }

            return overrides;
        }

        public void ImportMods(string dir)
        {
            if (!Directory.Exists(dir))
                return;

            // (try to) load mods
            foreach (string mod_dir in Directory.GetDirectories(dir))
            {
                ModSource source = (mod_dir.IndexOf(@"\SteamApps\workshop\", StringComparison.OrdinalIgnoreCase) != -1)
                    ? ModSource.SteamWorkshop
                    : ModSource.Manual;

                Import(mod_dir, source);
            }
        }

        public ModEntry Import(string modDir, ModSource source = ModSource.Unknown)
        {
            if (FindByPath(modDir) != null)
                // Mod already loaded
                return null;

            // look for .XComMod file
            string infoFile;
            try
            {
                infoFile = Directory.GetFiles(modDir, "*.XComMod", SearchOption.TopDirectoryOnly).SingleOrDefault();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show($"A mod could not be loaded since it contains multiple .xcommod files\r\nPlease notify the mod creator.\r\n\r\nPath: {modDir}");
                return null;
            }

            if (infoFile == null)
                return null;

            string modID = Path.GetFileNameWithoutExtension(infoFile);
            ModInfo modinfo = null;
            bool isDupe = All.Any(m => m.ID == modID && m.Path != modDir);

            // Check if mod with package name alreay exists
                // todo: move rename ID somewhere?

                //// Parse .XComMod file
                //modinfo = new ModInfo(infoFile);

                //string msg =
                //    $"'{modinfo.Title}' could not be imported:\r\n" +
                //    $"ID '{modID}' is already used by '{existingMod.Name}'\r\n\r\n" +
                //    $"Do you want to change the package name for '{modinfo.Title}'?\r\nWarning: This might break the mod\r\n\r\n" +
                //    $"Additional informations:\r\n{modinfo.Title}: {modDir}\r\n{existingMod.Name}: {existingMod.Path}";

                //var result = MessageBox.Show(msg, "Error", MessageBoxButtons.YesNo);
                //if (result == DialogResult.No)
                //    return null;

                //string oldModID = modID;
                //do
                //{
                //    modID = Microsoft.VisualBasic.Interaction.InputBox($"Package name {modID} already exists!\r\nPlease enter the new package name for '{modinfo.Title}'.\r\nLeave empty to cancel.", "Rename package");

                //    if (modID == "")
                //        return null;
                //}
                //while (FindByID(modID) != null);

                //// Rename
                //var files = Directory.GetFiles(modDir, $"*{oldModID}*.*", SearchOption.AllDirectories);
                //foreach (string file in files)
                //{
                //    File.Move(file, file.Replace(oldModID, modID));
                //}

            // Parse .XComMod file
            if (modinfo == null)
                modinfo = new ModInfo(infoFile);

            ModEntry mod = new ModEntry
            {
                ID = modID,
                Name = modinfo.Title ?? "Unnamed Mod",
                Path = modDir,

                Source = source,
                isActive = false,

                DateAdded = DateTime.Now,
                State = ModState.New
            };

            AddMod(modinfo.Category, mod);

            // mark dupes
            if (isDupe)
                foreach (ModEntry m in All.Where(m => m.ID == modID))
                    m.State |= ModState.DuplicateID;

            return mod;
        }

        public virtual ModCategory this[string category]
        {
            get
            {
                ModCategory cat = null;
                Entries.TryGetValue(category, out cat);

                if (cat == null)
                {
                    cat = new ModCategory();
                    Entries.Add(category, cat);
                }

                return cat;
            }
        }

        public void AddMod(string category, ModEntry mod)
        {
            if (mod.Index == -1)
                mod.Index = All.Count();
            this[category].Entries.Add(mod);
        }

        public void RemoveMod(ModEntry mod)
        {
            string category = GetCategory(mod);
            this[category].Entries.Remove(mod);
        }

        internal void UpdateMod(ModEntry m)
        {
            // load ModInfo ?

            // Update Source
            if (m.Source == ModSource.Unknown)
            {
                if (m.Path.IndexOf(@"\SteamApps\workshop\", StringComparison.OrdinalIgnoreCase) != -1)
                    m.Source = ModSource.SteamWorkshop;

                else
                    // in workshop path but not loaded via steam
                    m.Source = ModSource.Manual;
            }

            // Ensure source ID exists
            if (m.WorkshopID == -1)
            {
                long source_id = m.WorkshopID;

                if (m.Source == ModSource.SteamWorkshop)
                {
                    if (!long.TryParse(Path.GetFileName(m.Path), out source_id))
                        m.Source = ModSource.Manual;
                }
                else
                {
                    var info = new ModInfo(m.getModInfoFile());

                    source_id = info.publishedFileID;
                }

                m.WorkshopID = source_id;

            }

            // Fill Date Added
            if (!m.DateAdded.HasValue)
                m.DateAdded = DateTime.Now;



            // Check Workshop for infos
            if (m.WorkshopID != -1)
            {
                ulong publishedID = (ulong)m.WorkshopID;

                Steamworks.SteamUGCDetails_t value = Steam.Workshop.GetDetails(publishedID);

                if (!m.ManualName)
                    m.Name = value.m_rgchTitle;

                m.DateCreated = DateTimeOffset.FromUnixTimeSeconds(value.m_rtimeCreated).DateTime;
                m.DateUpdated = DateTimeOffset.FromUnixTimeSeconds(value.m_rtimeUpdated).DateTime;

                if (value.m_rtimeAddedToUserList > 0)
                    m.DateAdded = DateTimeOffset.FromUnixTimeSeconds(value.m_rtimeAddedToUserList).DateTime;

                //m.Author = SteamWorkshop.GetUsername(value.m_ulSteamIDOwner);
                //MessageBox.Show(m.Author);

                // Update directory size
                m.Size = value.m_nFileSize;

                // Check Workshop for updates
                if (m.Source == ModSource.SteamWorkshop)
                    if (Steam.Workshop.GetDownloadStatus((ulong)m.WorkshopID).HasFlag(Steamworks.EItemState.k_EItemStateNeedsUpdate))
                        m.State |= ModState.UpdateAvailable;
            }
            else
            {
                // Update directory size
                // slow, but necessary ?
                long size = 0;

                foreach (var fileName in Directory.EnumerateFiles(m.Path, "*", SearchOption.AllDirectories))
                    size += new FileInfo(fileName).Length;

                m.Size = size;
            }

        }

        public string GetCategory(ModEntry mod)
        {
            return Entries.First(entry => entry.Value.Entries.Contains(mod)).Key;
        }

        public override string ToString()
        {
            return $"{All.Count()} mods in {Entries.Count} categories";
        }

        public IEnumerable<IGrouping<string, ModEntry>> GetDuplicates()
        {
            return All.GroupBy(m => m.ID).Where(g => g.Count() > 1);
        }
        public void MarkDuplicates()
        {
            foreach (IGrouping<string, ModEntry> group in GetDuplicates())
                foreach (ModEntry m in group)
                    m.State |= ModState.DuplicateID;
        }
    }

}
