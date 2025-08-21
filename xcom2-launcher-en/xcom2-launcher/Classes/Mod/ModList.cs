using System;
using System.Collections.Concurrent;
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
        private readonly ConcurrentDictionary<long, ModEntry> _dependencyCache = new ConcurrentDictionary<long, ModEntry>();

        public virtual ModCategory this[string category]
        {
            get
            {
                Entries.TryGetValue(category, out var cat);

                if (cat == null)
                {
                    cat = new ModCategory(Entries.Any() ? Entries.Max(entry => entry.Value.Index) + 1 : 0);
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
                    // If all overrides are textually identical, no conflict
                    && overridesForThisClass.Select(m => m.TextLine).Distinct().Count() > 1
                select new ModConflict(className, overridesForThisClass);
        }

        public List<ModEntry> UpdateModsConflictState()
        {
            var activeConflicts = GetActiveConflicts();
            var changedMods = new List<ModEntry>();
            
            foreach (var mod in All)
            {
                if (mod.State.HasFlag(ModState.ModConflict))
                {
                    mod.RemoveState(ModState.ModConflict);
                    changedMods.Add(mod);
                }
            }

            foreach (var classOverride in activeConflicts.SelectMany(conflict => conflict.Overrides))
            {
                if (!classOverride.Mod.State.HasFlag(ModState.ModConflict))
                {
                    classOverride.Mod.AddState(ModState.ModConflict);
                    changedMods.Add(classOverride.Mod);
                }
            }

            return changedMods;
        }

        /// <summary>
        /// Checks if all required mods for the specified <paramref name="mod"/> are properly installed and activated.
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public void UpdatedModDependencyState(ModEntry mod)
        {
            var requiredMods = GetRequiredMods(mod, true, true);
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
                    Log.Warn($"Unable to parse WorkShop-Id ({s}) from Steam mod directory " + modDir);
                    MessageBox.Show("A mod could not be loaded, because the workshop mod folder does not correspond to a valid workshop ID." +
                                    $"\n\nPlease check the following directory:\n{modDir}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                Log.Warn("Multiple XComMod files in folder " + modDir);
                MessageBox.Show($"A mod could not be loaded since it contains multiple .XComMod files\r\n\r\nPath: {modDir}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                // the user probably added a system folder or a root directory as mod folder
                // where AML has no access rights to all or some of the sub-folders
                Log.Warn("Unauthorized access to directory " + modDir);
                return null;
            }
            catch (DirectoryNotFoundException)
            {
                // Handle if directory/mod was removed between enumerating all directories and later accessing it here.
                Log.Warn("Mod directory no longer available " + modDir);
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

        public async Task<List<ModEntry>> UpdateModAsync(ModEntry m, Settings settings)
        {
            return await UpdateModsAsync(new List<ModEntry> {m}, settings).ConfigureAwait(false);
        }

        public async Task<List<ModEntry>> UpdateModsAsync(List<ModEntry> mods, Settings settings, IProgress<ModUpdateProgress> progress = null, CancellationToken cancelToken = default(CancellationToken))
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
            var getDetailsTasks = new List<Task<List<SteamUGCDetails>>>();
            var totalModCount = steamMods.Count + localMods.Count;
            var steamProgress = 0;
            
            while(steamModsCopy.Any())
            {    
                var batchQueryModList = new List<ModEntry>(steamModsCopy.Take(Workshop.MAX_UGC_RESULTS).ToList());
                steamModsCopy = steamModsCopy.Skip(Workshop.MAX_UGC_RESULTS).ToList();

                Log.Debug($"Creating SteamUGCDetails_t batch request for {batchQueryModList.Count} mods.");

                getDetailsTasks.Add(GetDetailsTask());
                continue;

                async Task<List<SteamUGCDetails>> GetDetailsTask()
                {
                    var details = await Workshop.GetDetailsAsync(batchQueryModList.ConvertAll(mod => (ulong)mod.WorkshopID), true).ConfigureAwait(false);

                    if (details == null)
                    {
                        Log.Warn("GetDetails() request returned NULL");
                        return null;
                    }

                    foreach (var workshopDetails in details)
                    {
                        if (cancelToken.IsCancellationRequested)
                        {
                            Log.Debug("Task cancelled before processing workshopDetails");
                            cancelToken.ThrowIfCancellationRequested();
                            return null;
                        }

                        // A requested workshop detail may match more than one mod (having the same mod installed from Steam and locally for example).
                        var matchingMods = batchQueryModList.FindAll(mod => (ulong)mod.WorkshopID == workshopDetails.Details.m_nPublishedFileId.m_PublishedFileId);

                        foreach (var m in matchingMods)
                        {
                            if (cancelToken.IsCancellationRequested)
                            {
                                Log.Debug("Update mod task cancelled");
                                cancelToken.ThrowIfCancellationRequested();
                                return null;
                            }

                            var incremented = Interlocked.Increment(ref steamProgress);
                            progress?.Report(new ModUpdateProgress($"Updating mods {incremented}/{totalModCount}...", incremented, totalModCount));
                                
                            try
                            {
                                await UpdateSteamModAsync(m, workshopDetails).ConfigureAwait(false);
                            }
                            catch (Exception ex)
                            {
                                // Log Exception and throw it to indicate that the Task failed
                                Log.Warn($"Error while updating Steam mod '{m.Name}'", ex);
                                throw;
                            }
                        }
                    }
                   
                    Log.Debug("UpdateSteamMod tasks completed.");
                    return details;
                }
            }

            Log.Debug($"Waiting for {getDetailsTasks.Count} GetDetails tasks to complete.");
            await Task.WhenAny(Task.WhenAll(getDetailsTasks), Task.Delay(Timeout.Infinite, cancelToken)).ConfigureAwait(false);
            Log.Debug("GetDetails tasks completed.");

            var totalProgress = steamProgress;

            Log.Debug("Updating local mods.");

            foreach (var localMod in localMods)
            {
                if (cancelToken.IsCancellationRequested)
                {
                    Log.Debug("UpdateModsAsync cancelled while updating local mods.");
                    cancelToken.ThrowIfCancellationRequested();
                }

                progress?.Report(new ModUpdateProgress($"Updating mods {totalProgress}/{totalModCount}...", totalProgress, totalModCount));
                totalProgress++;
                
                await UpdateLocalModAsync(localMod);
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

        async Task UpdateLocalModAsync(ModEntry m)
        {
            Log.Debug("Processing local information for " + m.ID);

            m.DateCreated = Directory.GetCreationTime(m.Path);
            m.DateUpdated = Directory.GetLastWriteTime(m.Path);

            // Update directory size
            // slow, but necessary ?
            m.CalculateSize();

            await m.LoadOverridesAsync();
            
            // Update Name and Description
            // look for .XComMod file
            try
            {
                // Parse .XComMod file
                var modInfo = new ModInfo(m.GetModInfoFile());

                if (!m.ManualName || m.Name == "")
                {
                    m.Name = modInfo.Title;
                }

                m.Description = modInfo.Description;
                m.SetRequiresWOTC(modInfo.RequiresXPACK);
            }
            catch (InvalidOperationException ex)
            {
                Log.Error("Failed parsing XComMod file for " + m.ID, ex);
                Debug.Fail(ex.Message);
            }

        }

        async Task UpdateSteamModAsync(ModEntry m, SteamUGCDetails workshopDetailsWrapper)
        {
            if (m == null || m.WorkshopID <= 0)
            {
                return;
            }

            var workshopDetails = workshopDetailsWrapper.Details;
            if (workshopDetails.m_eResult != EResult.k_EResultOK)
            {
                return;
            }

            Log.Debug("Processing Workshop details for " + m.ID);
            
            if (!m.ManualName || m.Name == "")
            {
                m.Name = workshopDetails.m_rgchTitle;
            }

            m.DateCreated = DateTimeOffset.FromUnixTimeSeconds(workshopDetails.m_rtimeCreated).DateTime;
            m.DateUpdated = DateTimeOffset.FromUnixTimeSeconds(workshopDetails.m_rtimeUpdated).DateTime;

            if (workshopDetails.m_rtimeAddedToUserList > 0)
                m.DateAdded = DateTimeOffset.FromUnixTimeSeconds(workshopDetails.m_rtimeAddedToUserList).DateTime;

            // Update directory size
            m.Size = workshopDetails.m_nFileSize;
            if (m.Size < 0)
            {
                m.CalculateSize();
            }

            m.Description = workshopDetails.m_rgchDescription;

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
            
            // If the mod has dependencies, update them and make sure we have the workshop information of those
            if (workshopDetails.m_unNumChildren > 0)
            {
                var dependencies = workshopDetailsWrapper.Children;
                m.Dependencies.Clear();
                m.Dependencies.AddRange(dependencies.Select(x => (long)x));

                await LoadNotInstalledDependencies(m.Dependencies).ConfigureAwait(false);
            }

            // Check Workshop for updates
            if (Workshop.GetDownloadStatus((ulong) m.WorkshopID).HasFlag(EItemState.k_EItemStateNeedsUpdate))
            {
                Log.Info("Update available for " + m.ID);
                m.AddState(ModState.UpdateAvailable);
            }
            
            await m.LoadOverridesAsync();
            
            UpdatedModDependencyState(m);
            
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

        /// <summary>
        /// Load mods into the <see cref="_dependencyCache"/> if they are not part of the main mod list and missing from the cache.
        /// </summary>
        /// <param name="requiredModIds">List of mod ids from mods to be checked.</param>
        /// <returns>List of mods that were added to the cache.</returns>
        private async Task<List<ModEntry>> LoadNotInstalledDependencies(List<long> requiredModIds)
        {
            var missingDependencies = new List<ulong>();
            
            foreach (var requiredModId in requiredModIds)
            {
                var result = All.FirstOrDefault(m => m.WorkshopID == requiredModId);
                if (result != null)
                {
                    // dependency is already installed
                    continue;
                }

                if (_dependencyCache.TryGetValue(requiredModId, out result) && result != null)
                {
                    // dependency is already known in cache
                    continue;
                }
                
                missingDependencies.Add((ulong)requiredModId);
            }

            var details = new List<SteamUGCDetails>();
            
            // Query details from workshop in batches
            while (missingDependencies.Any())
            {
                var identifiers = new List<ulong>(missingDependencies.Take(Workshop.MAX_UGC_RESULTS));
                missingDependencies.RemoveRange(0, identifiers.Count);
                var result = await Workshop.GetDetailsAsync(identifiers);
                details.AddRange(result);
            }

            var loadedDependencies = new List<ModEntry>();

            // Process results and create Mod-Entries
            foreach (var detail in details)
            {
                if (detail.Details.m_eResult == EResult.k_EResultOK)
                {
                    var newMod = new ModEntry(detail);
                    _dependencyCache.TryAdd(newMod.WorkshopID, newMod);
                    loadedDependencies.Add(newMod);
                }
                else
                {
                    Log.Warn($"Workshop request for WorkshopId={detail.Details.m_nPublishedFileId} failed with result '{detail.Details.m_eResult}'");
                }
            }

            return loadedDependencies;
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
            var result = new List<ModEntry>();
            if (compareModId)
            {
                foreach (var modEntry in All)
                {
                    var requiredMods = GetRequiredMods(modEntry);
                    if (requiredMods.Any(x => x.ID == mod.ID))
                    {
                        result.Add(modEntry);
                    }
                }
                
                return result;
            }

            return All.Where(m => m.Dependencies.Contains(mod.WorkshopID)).ToList();
        }

        /// <summary>
        /// Returns all mods that are required for the the specified mod (based on REQUIRED ITEMS section in the workshop).
        /// </summary>
        /// <param name="mod">Mod to check required mods for</param>
        /// <param name="substituteDuplicates">If set to true, the primary duplicate will be returned if the real dependency is a disabled duplicate.</param>
        /// <param name="skipIgnoredDependencies">If set to true, dependencies that have been set to be ignored are not returned.</param>
        /// <returns></returns>
        public List<ModEntry> GetRequiredMods(ModEntry mod, bool substituteDuplicates = true, bool skipIgnoredDependencies = false)
        {
            List<ModEntry> requiredMods = new List<ModEntry>();
            var installedMods = All.ToList();

            var dependencies = mod.Dependencies;

            if (skipIgnoredDependencies)
            {
                dependencies = dependencies.Except(mod.IgnoredDependencies).ToList();
            }

            var missingDependencies = new List<long>();
            
            foreach (var id in dependencies)
            {
                // Check if required mod is already installed and use it if available.
                var result = installedMods.FirstOrDefault(m => m.WorkshopID == id);

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
                    // Dependencies that are not part of the mod list are cached
                    if (_dependencyCache.TryGetValue(id, out result))
                    {
                        requiredMods.Add(result);
                    }
                    else
                    {
                        missingDependencies.Add(id);
                    }
                }
            }

            if (missingDependencies.Any())
            {
                // Load and add dependencies that were missing from cache
                var loadedDependencies = Task.Run(async () => await LoadNotInstalledDependencies(missingDependencies)).GetAwaiter().GetResult();
                requiredMods.AddRange(loadedDependencies);
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