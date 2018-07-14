using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using Steamworks;
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
            return All.SingleOrDefault(m => string.Compare(m.Path, path, StringComparison.OrdinalIgnoreCase) == 0);
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
                    // If every mod uses a UIScreenListener, there is no conflict
                    && overridesForThisClass.Any(o => o.OverrideType == ModClassOverrideType.Class)
                    // If all overrides uses the same mod ID, assume there is no conflict
                    && overridesForThisClass.Any(o => o.Mod.ID != overridesForThisClass[0].Mod.ID)
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
            var isDupe = All.Any(m => m.ID == modID && string.Compare(m.Path, modDir, StringComparison.OrdinalIgnoreCase) == 0);

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
                BuiltForWOTC = modinfo.RequiresXPACK,
                State = ModState.New
            };
	        if (source == ModSource.SteamWorkshop)
	        {
		        var s = modDir.Split(Path.DirectorySeparatorChar).Last();
		        try
				{
					mod.WorkshopID = Convert.ToInt64(s);
				}
		        catch (Exception)
		        {
			        MessageBox.Show(
				        $"A mod could not be loaded because the workshop ID failed to parse.\r\nPlease check that the following directory conforms to valid workshop numbering.\r\n\r\nPath: {modDir}");
			        return null;
		        }
			}

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

        internal void UpdateMod(ModEntry m, Settings settings)
        {
            // Check if mod directory exists
            if (!Directory.Exists(m.Path))
            {
                m.State |= ModState.NotInstalled;
                m.State |= ModState.NotLoaded;
                m.isHidden = true;
                return;
            }

            // Check if in ModPaths
            if (!settings.ModPaths.Any(modPath => m.IsInModPath(modPath)))
                m.State |= ModState.NotLoaded;

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
            if (m.WorkshopID != 0)
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
                if (m.Size < 0)
                    m.Size = Directory.EnumerateFiles(m.Path, "*", SearchOption.AllDirectories).Sum(fileName => new FileInfo(fileName).Length);

                // Check Workshop for updates
                if (m.Source == ModSource.SteamWorkshop)
                    if (
                        Workshop.GetDownloadStatus((ulong) m.WorkshopID)
                            .HasFlag(EItemState.k_EItemStateNeedsUpdate))
                        m.State |= ModState.UpdateAvailable;

                // Check if it is built for WOTC
                try
                {
                    // Parse .XComMod file
                    var modinfo = new ModInfo(m.GetModInfoFile());
                    m.BuiltForWOTC = modinfo.RequiresXPACK;
                }
                catch (InvalidOperationException)
                {
                    return;
                }
            }
            else
            {
                m.DateCreated = Directory.GetCreationTime(m.Path);
                m.DateUpdated = Directory.GetLastWriteTime(m.Path);


                // Update directory size
                // slow, but necessary ?
                m.Size = Directory.EnumerateFiles(m.Path, "*", SearchOption.AllDirectories).Sum(fileName => new FileInfo(fileName).Length);

                // Update Name and Description
                // look for .XComMod file
                try
                {
                    // Parse .XComMod file
                    var modinfo = new ModInfo(m.GetModInfoFile());
                    if (!m.ManualName || m.Name == "")
                        m.Name = modinfo.Title;

                    m.Description = modinfo.Description;
                    m.BuiltForWOTC = modinfo.RequiresXPACK;
                }
                catch (InvalidOperationException)
                {
                    return;
                }
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