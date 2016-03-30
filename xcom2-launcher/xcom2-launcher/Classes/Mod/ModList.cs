using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using Steamworks;
using XCOM2Launcher.Classes.Mod;
using XCOM2Launcher.Steam;

namespace XCOM2Launcher.Mod
{
    public class ModList
    {
        public Dictionary<string, ModCategory> Entries { get; } = new Dictionary<string, ModCategory>();

        [JsonIgnore]
        public IEnumerable<ModEntry> All => Entries.SelectMany(c => c.Value.Entries);

        [JsonIgnore]
        public IEnumerable<string> Categories => Entries.OrderBy(c => c.Value.Index).ThenBy(c => c.Key).Select(c => c.Key);

        [JsonIgnore]
        public IEnumerable<ModEntry> Active => All.Where(m => m.isActive);

        public virtual ModCategory this[string category]
        {
            get
            {
                ModCategory cat;
                Entries.TryGetValue(category, out cat);

                if (cat == null)
                {
                    cat = new ModCategory();
                    Entries.Add(category, cat);
                }

                return cat;
            }
        }

        public ModEntry FindByPath(string path)
        {
            return All.SingleOrDefault(m => m.Path == path);
        }

        public IEnumerable<ModConflict> GetActiveConflicts()
        {
            var activeConflicts = GetActiveConflictsImplementation().ToList();
            UpdateModsConflictState(activeConflicts);
            return activeConflicts;
        }

        private IEnumerable<ModConflict> GetActiveConflictsImplementation()
        {
            IEnumerable<ModClassOverride> allOverrides = Active.SelectMany(o => o.GetOverrides()).ToList();
            var classesOverriden = allOverrides
                .Select(o => o.OldClass)
                .Distinct(StringComparer.InvariantCultureIgnoreCase);

            return from className in classesOverriden
                let overridesForThisClass = allOverrides.Where(o =>
                    o.OldClass.Equals(className, StringComparison.InvariantCultureIgnoreCase)).ToList()
                where overridesForThisClass.Count > 1
                      && overridesForThisClass.Any(o => o.OverrideType == ModClassOverrideType.Class)
                //If every mod uses a UIScreenListener, there is no conflict
                select new ModConflict(className, overridesForThisClass);
        }

        private void UpdateModsConflictState(IEnumerable<ModConflict> activeConflicts)
        {
            foreach (var mod in All)
            {
                mod.State &= ~ModState.ModConflict;
            }

            foreach (var classOverride in activeConflicts.SelectMany(conflict => conflict.Overrides))
            {
                classOverride.Mod.State |= ModState.ModConflict;
            }
        }

        public void ImportMods(string dir)
        {
            if (!Directory.Exists(dir))
                return;

            // (try to) load mods
            foreach (var modDir in Directory.GetDirectories(dir))
            {
                var source = modDir.IndexOf(@"\SteamApps\workshop\", StringComparison.OrdinalIgnoreCase) != -1
                    ? ModSource.SteamWorkshop
                    : ModSource.Manual;

                Import(modDir, source);
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
                MessageBox.Show(
                    $"A mod could not be loaded since it contains multiple .xcommod files\r\nPlease notify the mod creator.\r\n\r\nPath: {modDir}");
                return null;
            }

            if (infoFile == null)
                return null;

            var modID = Path.GetFileNameWithoutExtension(infoFile);
            var isDupe = All.Any(m => m.ID == modID && m.Path != modDir);

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
            var modinfo = new ModInfo(infoFile);

            var mod = new ModEntry
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
                foreach (var m in All.Where(m => m.ID == modID))
                    m.State |= ModState.DuplicateID;

            return mod;
        }

        public void AddMod(string category, ModEntry mod)
        {
            if (mod.Index == -1)
                mod.Index = All.Count();
            this[category].Entries.Add(mod);
        }

        public void RemoveMod(ModEntry mod)
        {
            var category = GetCategory(mod);
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
            if (m.WorkshopID <= 0)
            {
                long sourceID;

                if (m.Source == ModSource.SteamWorkshop && long.TryParse(Path.GetFileName(m.Path), out sourceID))
                    m.Source = ModSource.Manual;

                else
                    sourceID = new ModInfo(m.GetModInfoFile()).PublishedFileID;

                m.WorkshopID = sourceID;
            }

            // Fill Date Added
            if (!m.DateAdded.HasValue)
                m.DateAdded = DateTime.Now;


            // Check Workshop for infos
            if (m.WorkshopID != -1)
            {
                var publishedID = (ulong) m.WorkshopID;

                var value = Workshop.GetDetails(publishedID);

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
                    if (
                        Workshop.GetDownloadStatus((ulong) m.WorkshopID)
                            .HasFlag(EItemState.k_EItemStateNeedsUpdate))
                        m.State |= ModState.UpdateAvailable;
            }
            else
            {
                // Update directory size
                // slow, but necessary ?
                m.Size = Directory.EnumerateFiles(m.Path, "*", SearchOption.AllDirectories).Sum(fileName => new FileInfo(fileName).Length);
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
            foreach (var m in GetDuplicates().SelectMany(@group => @group))
                m.State |= ModState.DuplicateID;
        }
    }
}