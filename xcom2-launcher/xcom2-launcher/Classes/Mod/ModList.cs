using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        [JsonIgnore]
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(nameof(ModList));

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
                Entries.TryGetValue(category, out var cat);

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
            return All.SingleOrDefault(m => string.Equals(m.Path, path, StringComparison.OrdinalIgnoreCase));
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
                mod.RemoveState(ModState.ModConflict);
            }

            foreach (var classOverride in activeConflicts.SelectMany(conflict => conflict.Overrides))
            {
                classOverride.Mod.AddState(ModState.ModConflict);
            }
        }

        public void ImportMods(List<string> modPaths)
        {
            Log.Info("Checking mod directories for new mods");

            if (modPaths == null)
                throw new ArgumentNullException(nameof(modPaths));

            foreach (var dir in modPaths)
            {
                if (!Directory.Exists(dir))
                {
                    Log.Info("Directory is missing: " + dir);
                    continue;
                }

                Log.Info("Checking " + dir);

                // (try to) load mods
                foreach (var modDir in Directory.GetDirectories(dir))
                {
                    var source = modDir.IndexOf(@"\SteamApps\workshop\", StringComparison.OrdinalIgnoreCase) != -1
                        ? ModSource.SteamWorkshop
                        : ModSource.Manual;

                    Import(modDir, source);
                }
            }

            MarkDuplicates();
        }

        public ModEntry Import(string modDir, ModSource source = ModSource.Unknown)
        {
            // Check if Mod already loaded
            if (FindByPath(modDir) != null)
                return null;

            // look for .XComMod file
            string infoFile = FindModInfo(modDir);

            if (infoFile == null)
                return null;

            var modID = Path.GetFileNameWithoutExtension(infoFile);

            // Parse .XComMod file
            var modinfo = new ModInfo(infoFile);

            var mod = new ModEntry
            {
                Name = modinfo.Title ?? "Unnamed Mod",
                ID = modID,
                Path = modDir,
                isActive = false,
                DateAdded = DateTime.Now
            };

            mod.AddState(ModState.New);
            mod.SetRequiresWOTC(modinfo.RequiresXPACK);
            mod.SetSource(source);

            if (source == ModSource.SteamWorkshop)
            {
                // for Workshop mods, the folder name represents the Workshop-Id
                var s = modDir.Split(Path.DirectorySeparatorChar).Last();

                if (long.TryParse(s, out var workShopId))
                {
                    mod.WorkshopID = workShopId;
                }
                else
                {
                    Log.Error($"Unable to parse WorkShop-Id ({s}) from Steam mod directory " + modDir);
                    MessageBox.Show("A mod could not be loaded because the workshop ID failed to parse." +
                                    $"\nPlease check that the following directory conforms to valid workshop numbering.\n\nPath: {modDir}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            else if (source == ModSource.Manual)
            {
                // for mods from a custom mod path with custom folder names, we check if the XComMod file contains a Workshop-Id (i.e. publishedfileid)
                mod.WorkshopID = modinfo.PublishedFileID;
            }

            AddMod(modinfo.Category, mod);

            return mod;
        }

        public string FindModInfo(string modDir)
        {
            string infoFile;
            try
            {
                infoFile = Directory.GetFiles(modDir, "*.XComMod", SearchOption.TopDirectoryOnly).SingleOrDefault();
            }
            catch (InvalidOperationException)
            {
                Log.Error("Multiple XComMod files in folder " + modDir);
                MessageBox.Show(
                    $"A mod could not be loaded since it contains multiple .xcommod files\r\nPlease notify the mod creator.\r\n\r\nPath: {modDir}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                // the user probably added a system folder or a root directory as mod folder
                // where AML has no access rights to all or some of the sub-folders
                Log.Error("Unauthorized access to directory " + modDir);
                return null;
            }

            return infoFile;
        }

        public void AddMod(string category, ModEntry mod)
        {
            if (mod.Index == -1)
                mod.Index = All.Count();

            this[category].Entries.Add(mod);

            Log.Info($"Mod '{mod.ID}' added to category '{category}'");
        }

        public void RemoveMod(ModEntry mod)
        {
            var category = GetCategory(mod);
            this[category].Entries.Remove(mod);

            Log.Info($"Mod '{mod.ID}' removed from category '{category}'");
        }

        internal void UpdateMod(ModEntry m, Settings settings)
        {
            // Check if mod directory exists
            if (!Directory.Exists(m.Path))
            {
                Log.Warn($"Hiding mod {m.ID} because the directory {m.Path} no longer exists.");
                m.AddState(ModState.NotInstalled);
                m.AddState(ModState.NotLoaded);
                m.isHidden = true;
                return;
            }

            // Check if in ModPaths
            if (!settings.ModPaths.Any(modPath => m.IsInModPath(modPath)))
            {
                Log.Warn($"The mod {m.ID} is not located in any of the configured mod directories -> ModState.NotLoaded");
                m.AddState(ModState.NotLoaded);
            }

            // Update Source
            if (m.Source == ModSource.Unknown)
            {
                if (m.Path.IndexOf(@"\SteamApps\workshop\", StringComparison.OrdinalIgnoreCase) != -1)
                    m.SetSource(ModSource.SteamWorkshop);
                else
                    // in workshop path but not loaded via steam
                    m.SetSource(ModSource.Manual);

                Log.Info("Updated unknown mod source to " + m.Source);
            }

            // Ensure source ID exists
            if (m.WorkshopID <= 0)
            {
                if (m.Source == ModSource.SteamWorkshop && long.TryParse(Path.GetFileName(m.Path), out var sourceID))
                {
                    m.WorkshopID = sourceID;
                }
                else
                {
                    m.WorkshopID = new ModInfo(m.GetModInfoFile()).PublishedFileID;
                }

                if (m.WorkshopID > 0)
                    Log.Info("Updated uninitialized WorkShop Id to " + m.WorkshopID);
            }

            // Fill Date Added
            if (!m.DateAdded.HasValue)
                m.DateAdded = DateTime.Now;

            SteamUGCDetails_t workshopDetails = new SteamUGCDetails_t();

            // Check Workshop for infos
            if (m.WorkshopID != 0)
            {
                workshopDetails = Workshop.GetDetails((ulong) m.WorkshopID, string.IsNullOrEmpty(m.Description));
            }

            if (workshopDetails.m_eResult == EResult.k_EResultOK)
            {
                Log.Debug("Processing Workshop details for " + m.ID);

                if (!m.ManualName)
                    m.Name = workshopDetails.m_rgchTitle;

                m.DateCreated = DateTimeOffset.FromUnixTimeSeconds(workshopDetails.m_rtimeCreated).DateTime;
                m.DateUpdated = DateTimeOffset.FromUnixTimeSeconds(workshopDetails.m_rtimeUpdated).DateTime;

                if (workshopDetails.m_rtimeAddedToUserList > 0)
                    m.DateAdded = DateTimeOffset.FromUnixTimeSeconds(workshopDetails.m_rtimeAddedToUserList).DateTime;

                //m.Author = SteamWorkshop.GetUsername(value.m_ulSteamIDOwner);
                //MessageBox.Show(m.Author);

                // Update directory size
                m.RealizeSize(workshopDetails.m_nFileSize);
                if (m.Size < 0)
                    m.RealizeSize(Directory.EnumerateFiles(m.Path, "*", SearchOption.AllDirectories).Sum(fileName => new FileInfo(fileName).Length));

                if (string.IsNullOrEmpty(m.Description))
                {
                    m.Description = workshopDetails.m_rgchDescription;
                }

                if (workshopDetails.m_ulSteamIDOwner > 0)
                {
                    string newAuthorName = Workshop.GetUsername(workshopDetails.m_ulSteamIDOwner);

                    // Getusername() sometimes returns null, an empty string or "[unknown]".
                    // We do not want to overwrite a potentially already correct author name in that case.
                    if (!string.IsNullOrEmpty(newAuthorName) && newAuthorName != "[unknown]")
                    {
                        m.Author = newAuthorName;
                    }
                }

                // Check Workshop for updates
                if (m.Source == ModSource.SteamWorkshop)
                {
                    if (Workshop.GetDownloadStatus((ulong) m.WorkshopID).HasFlag(EItemState.k_EItemStateNeedsUpdate))
                    {
                        Log.Info("Update available for " + m.ID);
                        m.AddState(ModState.UpdateAvailable);
                    }
                }

                // Check if it is built for WOTC
                try
                {
                    // Parse .XComMod file
                    var modInfo = new ModInfo(m.GetModInfoFile());
                    m.SetRequiresWOTC(modInfo.RequiresXPACK);
                }
                catch (InvalidOperationException ex)
                {
                    Log.Error("Failed parsing XComMod file for " + m.ID, ex);
                    Debug.Fail(ex.Message);
                }
            }
            else
            {
                Log.Debug("Processing local information for " + m.ID);

                m.DateCreated = Directory.GetCreationTime(m.Path);
                m.DateUpdated = Directory.GetLastWriteTime(m.Path);

                // Update directory size
                // slow, but necessary ?
                m.RealizeSize(Directory.EnumerateFiles(m.Path, "*", SearchOption.AllDirectories).Sum(fileName => new FileInfo(fileName).Length));

                // Update Name and Description
                // look for .XComMod file
                try
                {
                    // Parse .XComMod file
                    var modInfo = new ModInfo(m.GetModInfoFile());

                    if (!m.ManualName || m.Name == "")
                        m.Name = modInfo.Title;

                    m.Description = modInfo.Description;
                    m.SetRequiresWOTC(modInfo.RequiresXPACK);
                }
                catch (InvalidOperationException ex)
                {
                    Log.Error("Failed parsing XComMod file for " + m.ID, ex);
                    Debug.Fail(ex.Message);
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
            return All.GroupBy(m => m.ID, StringComparer.InvariantCultureIgnoreCase).Where(g => g.Count() > 1);
        }

        public void MarkDuplicates()
        {
            foreach (var m in GetDuplicates().SelectMany(group => group))
                m.AddState(ModState.DuplicateID);
        }
    }
}