using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Steamworks;
using XCOM2Launcher.Steam;

namespace XCOM2Launcher.Mod
{
    public class ModList
    {
        private readonly object _ModUpdateLock = new object();

        [JsonIgnore]
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(nameof(ModList));

        public Dictionary<string, ModCategory> Entries { get; } = new Dictionary<string, ModCategory>();

        [JsonIgnore]
        public IEnumerable<ModEntry> All => Entries.SelectMany(c => c.Value.Entries);

        [JsonIgnore]
        public IEnumerable<string> CategoryNames => Entries.OrderBy(c => c.Value.Index).ThenBy(c => c.Key).Select(c => c.Key);

        [JsonIgnore]
        public IEnumerable<ModEntry> Active => All.Where(m => m.isActive);

        [JsonIgnore] 
        private readonly List<ModEntry> _DependencyCache = new List<ModEntry>();

        public virtual ModCategory this[string category]
        {
            get
            {
                Entries.TryGetValue(category, out var cat);

                if (cat == null)
                {
                    cat = new ModCategory(Entries.Max(entry => entry.Value.Index) + 1);
                    Entries.Add(category, cat);
                }

                return cat;
            }
        }

        /// <summary>
        /// Make sure category indices are numbered from 0..n without gaps.
        /// </summary>
        public void InitCategoryIndices()
        {
            var newIndex = 0;
            foreach (var entry in Entries.OrderBy(entry => entry.Value.Index))
            {
                entry.Value.Index = newIndex++;
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

        /// <summary>
        /// Checks if all required mods for the specified <paramref name="mod"/> are properly installed and activated.
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public void UpdatedModDependencyState(ModEntry mod)
        {
            var requiredMods = GetRequiredMods(mod);
            var allRequiredModsAvailable = requiredMods.All(m => m.WorkshopID != 0 && m.isActive && !m.State.HasFlag(ModState.NotInstalled) && !m.State.HasFlag(ModState.NotLoaded));
            
            if (allRequiredModsAvailable)
                mod.RemoveState(ModState.MissingDependencies);
            else 
                mod.AddState(ModState.MissingDependencies);
        }

        public List<ModEntry> ImportMods(List<string> modPaths)
        {
            Log.Info("Checking mod directories for new mods");

            if (modPaths == null)
                throw new ArgumentNullException(nameof(modPaths));

            var importedMods = new List<ModEntry>();

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

                    var importedMod = Import(modDir, source);

                    if (importedMod != null)
                        importedMods.Add(importedMod);
                }
            }

            MarkDuplicates();

            return importedMods;
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

        /// <summary>
        /// Checks if a single mod information file (.XComMod) exists in the given folder.
        /// </summary>
        /// <param name="modDir">The mod directory were the mod information file is expected to be located.</param>
        /// <returns>Full path to the file if found. NULL if no match or multiple matches were found or the folder could not be accessed.</returns>
        public string FindModInfo(string modDir)
        {
            string infoFile;
            try
            {
                infoFile = Directory.GetFiles(modDir, "*.XComMod", SearchOption.TopDirectoryOnly).SingleOrDefault();

                if (infoFile == null)
                {
                    infoFile = Directory.GetFiles(modDir, "*.XComMod" + ModEntry.MODFILE_DISABLE_POSTFIX, SearchOption.TopDirectoryOnly).SingleOrDefault();
                }
            }
            catch (InvalidOperationException)
            {
                Log.Error("Multiple XComMod files in folder " + modDir);
                MessageBox.Show(
                                $"A mod could not be loaded since it contains multiple .XComMod files\r\n\r\nPath: {modDir}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public async void UpdateModAsync(ModEntry m, Settings settings)
        {
            await UpdateModsAsync(new List<ModEntry> { m }, settings);
        }

        public async Task<List<ModEntry>> UpdateModsAsync(List<ModEntry> mods, Settings settings, IProgress<ModUpdateProgress> progress = null)
        {
            Log.Info($"Updating {mods.Count} mods...");

            var steamMods = new List<ModEntry>();
            var localMods = new List<ModEntry>();
                
            foreach (var mod in mods)
            {
                if (!VerifyModState(mod, settings))
                    continue;

                if (mod.WorkshopID != 0)
                {
                    steamMods.Add(mod);
                }
                else
                {
                    localMods.Add(mod);
                }
            }

            var steamModsCopy = new List<ModEntry>(steamMods);
            var getDetailsTasks = new List<Task<List<SteamUGCDetails_t>>>();
            var totalModCount = steamMods.Count + localMods.Count;
            var steamProgress = 1;
            
            while(steamModsCopy.Any())
            {    
                var batchQueryModList = new List<ModEntry>(steamModsCopy.Take(Workshop.MAX_UGC_RESULTS).ToList());
                steamModsCopy = steamModsCopy.Skip(Workshop.MAX_UGC_RESULTS).ToList();

                Log.Debug($"Creating SteamUGCDetails_t batch request for {batchQueryModList.Count} mods.");
                
                getDetailsTasks.Add(Task.Run(() =>
                {
                    var details = Workshop.GetDetails(batchQueryModList.ConvertAll(mod => (ulong) mod.WorkshopID), true);

                    if (details == null)
                    {
                        Log.Warn("GetDetails() request returned NULL");
                        return new List<SteamUGCDetails_t>();
                    }

                    var updateTasks = new List<Task>();

                    foreach (var workshopDetails in details)
                    {
                        updateTasks.Add(Task.Run(() =>
                        {
                            var m = steamMods.Find(mod => (ulong) mod.WorkshopID == workshopDetails.m_nPublishedFileId.m_PublishedFileId);

                            lock (_ModUpdateLock)
                            {
                                progress?.Report(new ModUpdateProgress($"Updating mods {steamProgress}/{totalModCount}...", steamProgress, totalModCount));
                                Interlocked.Increment(ref steamProgress);
                            }

                            //Log.Debug($"Start update {m.ID}");
                            UpdateSteamMod(m, workshopDetails);
                            //Log.Debug($"Finished {m.ID}");

                        }));
                    }

                    Log.Debug($"Waiting for {updateTasks.Count} UpdateSteamMod tasks to complete.");
                    Task.WhenAll(updateTasks).Wait();
                    Log.Debug("UpdateSteamMod tasks completed.");
                    return details;
                }));
            }

            Log.Debug($"Waiting for {getDetailsTasks.Count} GetDetails tasks to complete.");
            await Task.WhenAll(getDetailsTasks);
            Log.Debug("GetDetails tasks completed.");

            var totalProgress = steamProgress;

            Log.Debug("Updating local mods.");

            foreach (var localMod in localMods)
            {
                progress?.Report(new ModUpdateProgress($"Updating mods {totalProgress}/{totalModCount}...", totalProgress, totalModCount));
                totalProgress++;
                
                UpdateLocalMod(localMod);
            }

            List<ModEntry> updatedEntries = new List<ModEntry>();
            updatedEntries.AddRange(steamMods);
            updatedEntries.AddRange(localMods);
            
            Log.Debug($"{nameof(UpdateModsAsync)}() completed.");
            return updatedEntries;
        }

        bool VerifyModState(ModEntry m, Settings settings)
        {
            // Check if mod directory exists
            if (!Directory.Exists(m.Path))
            {
                Log.Warn($"Hiding mod {m.ID} because the directory {m.Path} no longer exists.");
                m.AddState(ModState.NotInstalled);
                m.AddState(ModState.NotLoaded);
                m.isHidden = true;
                return false;
            }

            // Check if in ModPaths
            if (!settings.ModPaths.Any(m.IsInModPath))
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

            return true;
        }

        void UpdateLocalMod(ModEntry m)
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

        void UpdateSteamMod(ModEntry m, SteamUGCDetails_t workshopDetails)
        {
            if (m == null || m.WorkshopID <= 0)
            {
                return;
            }

            if (workshopDetails.m_eResult != EResult.k_EResultOK)
            {
                return;
            }

            Log.Debug("Processing Workshop details for " + m.ID);

            if (!m.ManualName)
                m.Name = workshopDetails.m_rgchTitle;

            m.DateCreated = DateTimeOffset.FromUnixTimeSeconds(workshopDetails.m_rtimeCreated).DateTime;
            m.DateUpdated = DateTimeOffset.FromUnixTimeSeconds(workshopDetails.m_rtimeUpdated).DateTime;

            if (workshopDetails.m_rtimeAddedToUserList > 0)
                m.DateAdded = DateTimeOffset.FromUnixTimeSeconds(workshopDetails.m_rtimeAddedToUserList).DateTime;

            // Update directory size
            m.RealizeSize(workshopDetails.m_nFileSize);
            if (m.Size < 0)
                m.RealizeSize(Directory.EnumerateFiles(m.Path, "*", SearchOption.AllDirectories).Sum(fileName => new FileInfo(fileName).Length));

            if (string.IsNullOrEmpty(m.Description))
            {
                m.Description = workshopDetails.m_rgchDescription;
            }

            // Request mod author name if necessary.
            if (string.IsNullOrEmpty(m.Author) || m.Author == ModEntry.DEFAULT_AUTHOR_NAME)
            {
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
            }

            // We buffer the Steam tags so we do nor require another full UGC workshop request when the user chooses to use them.
            m.SteamTags = workshopDetails.m_rgchTags.Split(',').Select(s => s.TrimStart(' ').TrimEnd(' ')).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            
            // If the mod has dependencies, request the workshop id's of those mods.
            if (workshopDetails.m_unNumChildren > 0)
            {
                var dependencies = Workshop.GetDependencies(workshopDetails);

                if (dependencies != null)
                {
                    m.Dependencies.Clear();
                    m.Dependencies.AddRange(dependencies.ConvertAll(val => (long) val));
                }
                else
                {
                    Log.Warn($"Dependency request for {m.WorkshopID} failed.");
                }
            }

            // Check Workshop for updates
            if (Workshop.GetDownloadStatus((ulong) m.WorkshopID).HasFlag(EItemState.k_EItemStateNeedsUpdate))
            {
                Log.Info("Update available for " + m.ID);
                m.AddState(ModState.UpdateAvailable);
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

        public string GetCategory(ModEntry mod)
        {
            return Entries.First(entry => entry.Value.Entries.Contains(mod)).Key;
        }

        /// <summary>
        /// Returns all mods that depend on the specified mod (based on REQUIRED ITEMS section in the workshop).
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="compareModId">If set to false, dependencies are checked against the workshop ID. Otherwise, the mod ID is used which also matches for duplicates.</param>
        /// <returns></returns>
        public List<ModEntry> GetDependentMods(ModEntry mod, bool compareModId = true)
        {
            if (compareModId)
                return All.Where(m => GetRequiredMods(m).Select(requiredMod => requiredMod.ID).Contains(mod.ID)).ToList();

            return All.Where(m => m.Dependencies.Contains(mod.WorkshopID)).ToList();
        }

        /// <summary>
        /// Returns all mods that are required for the the specified mod (based on REQUIRED ITEMS section in the workshop).
        /// </summary>
        /// <param name="mod">Mod to check required mods for</param>
        /// <param name="substituteDuplicates">If set to true, the primary duplicate will be returned if the real dependency is a disabled duplicate.</param>
        /// <returns></returns>
        public List<ModEntry> GetRequiredMods(ModEntry mod, bool substituteDuplicates = true)
        {
            List<ModEntry> requiredMods = new List<ModEntry>();
            var installedSteamMods = All.Where(m => m.WorkshopID != 0).ToList();
            
            foreach (var id in mod.Dependencies)
            {
                // Check if required mod is already installed and use it if available.
                var result = installedSteamMods.FirstOrDefault(m => m.WorkshopID == id);

                if (result != null)
                {
                    // If the required mod is installed but disabled a duplicate, use the primary duplicate
                    if (substituteDuplicates && result.State.HasFlag(ModState.DuplicateDisabled))
                    {
                        var primaryDuplicate = All.FirstOrDefault(m => m.ID == result.ID && m.State.HasFlag(ModState.DuplicatePrimary));

                        if (primaryDuplicate != null)
                        {
                            result = primaryDuplicate;
                        }
                    }

                    requiredMods.Add(result);
                }
                else
                {
                    // If the required mod is not installed, we query the workshop details to be able to display some information.
                    // To prevent unnecessary queries, results are cached.
                    result = _DependencyCache.FirstOrDefault(m => m.WorkshopID == id);

                    if (result != null)
                    {
                        requiredMods.Add(result);
                    }
                    else
                    {
                        var details = Workshop.GetDetails((ulong)id);

                        if (details.m_nPublishedFileId.m_PublishedFileId != 0)
                        {
                            var newMod = new ModEntry(details);
                            requiredMods.Add(newMod);
                            _DependencyCache.Add(newMod);
                        }
                    }
                }
            }

            return requiredMods;
        }

        public override string ToString()
        {
            return $"{All.Count()} mods in {Entries.Count} categories";
        }

        public IEnumerable<IGrouping<string, ModEntry>> GetDuplicates()
        {
            return All.GroupBy(m => m.ID, StringComparer.InvariantCultureIgnoreCase).Where(g => g.Count() > 1);
        }

        /// <summary>
        /// Sets the appropriate ModState flags for all duplicate mods.
        /// </summary>
        public void MarkDuplicates()
        {
            foreach (var duplicateGroup in GetDuplicates())
            {
                // If any mod from a group of duplicates is disabled, we indicate this by adding special mod states to all mods of this group.
                if (Settings.Instance.EnableDuplicateModIdWorkaround && duplicateGroup.Any(m => m.CheckModFileDisabled()))
                {
                    Log.Debug($"Duplicate mod workaround active for mod ID '{duplicateGroup.First().ID}'");
                    bool primaryAlreadyAssigned = false;
                    
                    foreach (var mod in duplicateGroup.OrderBy(m => m.DateAdded))
                    {
                        if (mod.CheckModFileDisabled())
                        {
                            Log.Debug($"Duplicate {mod.Name} ({mod.WorkshopID}) is disabled");
                            mod.AddState(ModState.DuplicateDisabled);
                        }
                        else
                        {
                            // Make sure, there is only one primary mod.
                            if (primaryAlreadyAssigned)
                            {
                                Log.Debug($"Duplicate {mod.Name} ({mod.WorkshopID}) will now be disabled");
                                mod.DisableModFile();
                                mod.AddState(ModState.DuplicateDisabled);
                            }
                            else
                            {
                                Log.Debug($"Duplicate {mod.Name} ({mod.WorkshopID}) is the primary mod");
                                mod.AddState(ModState.DuplicatePrimary);
                                primaryAlreadyAssigned = true;
                            }
                        }
                    }
                }
                else
                {
                    foreach (var mod in duplicateGroup)
                    {
                        mod.AddState(ModState.DuplicateID);
                    }
                }
            }
        }
    }

    public class ModUpdateProgress
    {
        public string Message { get; }
        public int Current { get; }
        public int Max { get; }

        public ModUpdateProgress(string message, int current, int max = 100)
        {
            Message = message;
            Current = current;
            Max = max;
        }
    }
}