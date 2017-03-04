using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Microsoft.VisualBasic;
using XCOM2Launcher.Helper;
using XCOM2Launcher.Mod;
using Timer = System.Timers.Timer;

namespace XCOM2Launcher.Forms
{
	public partial class MainForm : Form
    {
		public ModList Mods => Settings.Mods;

		public TypedObjectListView<ModEntry> ModList { get; private set; }

		public List<ModEntry> Downloads { get; } = new List<ModEntry>();

		public ModEntry CurrentMod;


		public void InitObjectListView()
        {
            //modlist_objectlistview?.Dispose();

			var categoryGroupingDelegate = new GroupKeyGetterDelegate(o => Mods.GetCategory(o as ModEntry));

            var categoryFormatterDelegate = new GroupFormatterDelegate((@group, parameters) =>
            {
                var groupName = group.Key as string;
                if (groupName == null)
                    return;

                // Restore collapsed state
                group.Collapsed = Mods.Entries[groupName].Collapsed;

                // Sort Categories
                parameters.GroupComparer = Comparer<OLVGroup>.Create((a, b) => Mods.Entries[(string) a.Key].Index.CompareTo(Mods.Entries[(string) b.Key].Index));
            });

			olvcName.GroupKeyGetter = categoryGroupingDelegate;
			olvcName.GroupFormatter = categoryFormatterDelegate;

			olvcID.GroupKeyGetter = categoryGroupingDelegate;
			olvcID.GroupFormatter = categoryFormatterDelegate;

			olvcOrder.GroupKeyGetter = categoryGroupingDelegate;
			olvcOrder.GroupFormatter = categoryFormatterDelegate;

			olvcState.AspectGetter = o =>
			{
				var mod = (ModEntry)o;

				if (mod.State.HasFlag(ModState.NotLoaded))
					return "Not Loaded";

				if (mod.State.HasFlag(ModState.NotInstalled))
					return "Not Installed";

				if (mod.State.HasFlag(ModState.ModConflict))
					return "Conflict";

				if (mod.State.HasFlag(ModState.DuplicateID))
					return "Duplicate ID";

				if (mod.State.HasFlag(ModState.New))
					return "New";

				if (mod.State.HasFlag(ModState.UpdateAvailable))
					return "Update Available";

				return "OK";
			};

			olvcSize.AspectToStringConverter = size => ((long)size).FormatAsFileSize();

			olvcLastUpdated.DataType = typeof(DateTime?);
			olvcDateAdded.DataType = typeof(DateTime?);
			olvcDateCreated.DataType = typeof(DateTime?);

			olvcPath.GroupKeyGetter = o => Path.GetDirectoryName((o as ModEntry)?.Path);


			// size groupies
			var columns = modlist_ListObjectListView.AllColumns.ToArray();
            columns.Single(c => c.AspectName == "Size").MakeGroupies(
                new[] {1024, 1024*1024, (long) 50*1024*1024, (long) 100*1024*1024},
                new[] {"< 1 KB", "< 1MB", "< 50 MB", "< 100 MB", "> 100 MB"}
                );
			columns.Single(c => c.AspectName == "isHidden").MakeGroupies(
				new [] {false, true},
				new [] { "wut?", "Not Hidden", "Hidden"});

			// Sort by Order or WorkshopID column removes groups
			modlist_ListObjectListView.BeforeSorting += (sender, args) =>
			{
				modlist_ListObjectListView.ShowGroups = 
				!(args.ColumnToSort.Equals(olvcOrder) || args.ColumnToSort.Equals(olvcWorkshopID));
			};

            // Init DateTime columns
            foreach (var column in columns.Where(c => c.DataType == typeof (DateTime?)))
            {
                column.AspectToStringConverter = d => (d as DateTime?)?.ToLocalTime().ToString(CultureInfo.CurrentCulture);
                column.MakeGroupies(
                    new[] {DateTime.Now.Subtract(TimeSpan.FromHours(24*30)), DateTime.Now.Subtract(TimeSpan.FromHours(24*7)), DateTime.Now.Date},
                    new[] {"Older than one month", "Last Month", "This Week", "Today"}
                    );

                // Sord Desc
                column.GroupFormatter =
                    (g, param) => { param.GroupComparer = Comparer<OLVGroup>.Create((a, b) => (param.GroupByOrder == SortOrder.Descending ? 1 : -1)*a.Id.CompareTo(b.Id)); };
            }


            // Wrapper
            ModList = new TypedObjectListView<ModEntry>(modlist_ListObjectListView);

            // Events
            //modlist_objectlistview.SelectionChanged += ModListSelectionChanged;
            //modlist_objectlistview.ItemChecked += ModListItemChecked;
            //modlist_objectlistview.CellRightClick += ModListCellRightClick;
            //modlist_objectlistview.CellEditFinished += ModListEditFinished;
            //modlist_objectlistview.CellToolTipShowing += ModListCellToolTipShowing;
            //modlist_objectlistview.FormatRow += ModListFormatRow;
            //modlist_objectlistview.GroupExpandingCollapsing += ModListGroupExpandingCollapsing;
            //modlist_objectlistview.KeyUp += ModListKeyUp;
            //modlist_objectlistview.KeyDown += ModListKeyDown;

            // move state to the beginning
            columns.Single(c => c.Text == "State").DisplayIndex = 0;

            // Restore State
            if (Settings.Windows.ContainsKey("main") && Settings.Windows["main"].Data != null)
                modlist_ListObjectListView.RestoreState(Settings.Windows["main"].Data);

            RefreshModList();
        }


        private void ModListKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                //horizontal_splitcontainer.Panel2Collapsed = true;
            }
        }

        private void ModListKeyUp(object sender, KeyEventArgs e)
        {
        }

        private void ModListGroupExpandingCollapsing(object sender, GroupExpandingCollapsingEventArgs e)
        {
            // only handle if grouped by name or id
            if (modlist_ListObjectListView.PrimarySortColumn.AspectName != "Name" &&
                modlist_ListObjectListView.PrimarySortColumn.AspectName != "ID")
                return;

            if (e.Group.Key == null)
                return;

            var key = e.Group.Key as string;
            Contract.Assume(key != null);

            if (Mods.Entries.ContainsKey(key))
                Mods.Entries[key].Collapsed = !e.IsExpanding;
        }

        private void ModListFormatRow(object sender, FormatRowEventArgs e)
        {
            var mod = e.Model as ModEntry;
            Contract.Assume(mod != null);

            var item = e.Item;


            if (mod.State.HasFlag(ModState.NotLoaded))
            {
                item.BackColor = SystemColors.GradientInactiveCaption;
                item.ForeColor = SystemColors.InactiveCaptionText;
            }

            else if (mod.State.HasFlag(ModState.ModConflict))
            {
                item.BackColor = Color.PaleVioletRed;
                item.ForeColor = Color.Black;
            }


            else if (mod.State.HasFlag(ModState.DuplicateID))
            {
                item.BackColor = Color.PaleVioletRed;
                item.ForeColor = Color.Black;
            }


            else if (mod.isHidden)
                item.BackColor = SystemColors.InactiveBorder;

            else if (mod.State.HasFlag(ModState.New))
                item.BackColor = SystemColors.Info;
        }

        private void ModListCellToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            var mod = (ModEntry) e.Model;
            if (e.Column.Text != "State")
                return;

            string tooltip = null;

            if (mod.State.HasFlag(ModState.NotLoaded))
                tooltip = "This mod is not being loaded. Check your Mod Path settings.";


            else if (mod.State.HasFlag(ModState.ModConflict))
                tooltip = "This mod makes changes that conflict with another mod.";


            else if (mod.State.HasFlag(ModState.DuplicateID))
                tooltip = "This mods id is not unique. Mods with same id can only be (de-)activated together.";

            e.Text = tooltip;
        }

        private void UpdateMod(ModEntry m)
        {
            if (m.isHidden && !Settings.ShowHiddenElements)
                // cant update what's not there
                return;

            modlist_ListObjectListView.UpdateObject(m);
        }


        private void DeleteMods()
        {
            // Confirmation dialog
            var text = modlist_ListObjectListView.SelectedObjects.Count == 1
                ? $"Are you sure you want to delete '{ModList.SelectedObjects[0]?.Name}'?"
                : $"Are you sure you want to delete {modlist_ListObjectListView.SelectedObjects.Count} mods?";

            text += "\r\nThis can not be undone.";

            var r = MessageBox.Show(text, "Confirm deletion", MessageBoxButtons.OKCancel);
            if (r != DialogResult.OK)
                return;

            // Delete
            var mods = ModList.SelectedObjects.ToList();
            foreach (var mod in mods)
            {
                // unsubscribe
                if (mod.Source == ModSource.SteamWorkshop)
                    Steam.Workshop.Unsubscribe((ulong) mod.WorkshopID);

                // delete model
                modlist_ListObjectListView.RemoveObject(mod);
                Mods.RemoveMod(mod);

                // delete files
                try
                {
                    Directory.Delete(mod.Path, true);
                }
                catch (System.IO.DirectoryNotFoundException)
                {
                    // the directory was already removed
                }

                RefreshModList();
            }
        }

        private void MoveMods(string category)
        {
            modlist_ListObjectListView.BeginUpdate();
            foreach (var mod in ModList.SelectedObjects)
            {
                Mods.RemoveMod(mod);
                Mods.AddMod(category, mod);
                modlist_ListObjectListView.UpdateObject(mod);
            }
            modlist_ListObjectListView.EndUpdate();
        }


        private void RefreshModList()
        {
            // Un-register events
            modlist_ListObjectListView.SelectionChanged -= ModListSelectionChanged;
            modlist_ListObjectListView.ItemChecked -= ModListItemChecked;

            modlist_ListObjectListView.BeginUpdate();

            // add elements
            modlist_ListObjectListView.ClearObjects();

            modlist_ListObjectListView.Objects = Settings.ShowHiddenElements ? Mods.All : Mods.All.Where(m => !m.isHidden);

            modlist_ListObjectListView.EndUpdate();

            // Re-register events
            modlist_ListObjectListView.SelectionChanged += ModListSelectionChanged;
            modlist_ListObjectListView.ItemChecked += ModListItemChecked;
        }
        
        private ContextMenu CreateModListContextMenu(ModEntry m)
        {
            var menu = new ContextMenu();
            if (m?.ID == null)
                return menu;

            var item = new MenuItem("Rename");
            item.Click += (a, b) => { modlist_ListObjectListView.EditModel(m); };
            menu.MenuItems.Add(item);

            // Move to ...
            var moveToCategory = new MenuItem("Move to Category...");
            // ... new category
            moveToCategory.MenuItems.Add("New category", delegate
            {
                var category = Interaction.InputBox("Please enter the name of the new category", "Create category", "New category");
                if (category == "")
                    return;

                MoveMods(category);
            });

            moveToCategory.MenuItems.Add("-");

            // ... existing category
            foreach (var category in Settings.Mods.Categories.OrderBy(c => c))
            {
                if (category == Mods.GetCategory(m))
                    continue;

                moveToCategory.MenuItems.Add(category, delegate { MoveMods(category); });
            }
            menu.MenuItems.Add(moveToCategory);


            var toggleVisibility = new MenuItem {Text = m.isHidden ? "Unhide" : "Hide"};
            toggleVisibility.Click += delegate
            {
                // save as new list so we can remove mods if they are being hidden
                foreach (var mod in ModList.SelectedObjects.ToList())
                {
                    mod.isHidden = !m.isHidden;

                    if (!Settings.ShowHiddenElements && mod.isHidden)
                        modlist_ListObjectListView.RemoveObject(mod);
                }

                RefreshModList();
            };

            menu.MenuItems.Add(toggleVisibility);

            menu.MenuItems.Add("-");

            menu.MenuItems.Add(new MenuItem("Delete / Unsubscribe", delegate { DeleteMods(); }));


            menu.MenuItems.Add("-");

            menu.MenuItems.Add(new MenuItem("Show in Explorer", delegate { m.ShowInExplorer(); }));

            if (m.WorkshopID != -1)
                menu.MenuItems.Add(new MenuItem("Show on Steam", delegate { m.ShowOnSteam(); }));


            return menu;
        }

        #region Events

        private void ModListCellRightClick(object sender, CellRightClickEventArgs e)
        {
            CreateModListContextMenu(e.Model as ModEntry).Show(e.ListView, e.Location);
        }

        private Timer _updateTimer;

        private void ModListItemChecked(object sender, ItemCheckedEventArgs e)
        {
            // If there is a duplicate id conflict 
            // check all at the same time
            var modChecked = ModList.GetModelObject(e.Item.Index);
            if (modChecked.State.HasFlag(ModState.DuplicateID))
            {
                foreach (var mod in Mods.All.Where(@mod => mod.ID == modChecked.ID))
                {
                    mod.isActive = modChecked.isActive;
                    UpdateMod(mod);
                }
            }
            // put on a slight delay
            // so it will only update once
            if (_updateTimer != null && _updateTimer.Enabled)
                return;

            _updateTimer = new Timer(10)
            {
                SynchronizingObject = this,
                AutoReset = false
            };

            _updateTimer.Elapsed += delegate { UpdateConflicts(); };

            _updateTimer.Start();
        }

        private void ModListSelectionChanged(object sender, EventArgs e)
        {
	        if (modlist_ListObjectListView.SelectedObjects.Count > 1)
	        {
		        CurrentMod = null;
				return;
	        }

			var selected = modlist_ListObjectListView.SelectedObject as ModEntry;

			if (CurrentMod == selected) return;

		    CurrentMod = selected;


			UpdateModInfo(CurrentMod);
            CheckAndUpdateChangeLog(modinfo_tabcontrol.SelectedTab, CurrentMod);

            if (CurrentMod != null)
            {
                CurrentMod.State &= ~ModState.New;
                modlist_ListObjectListView.EnsureModelVisible(CurrentMod);
            }
        }

        private void ModListEditFinished(object sender, CellEditEventArgs e)
        {
            var mod = e.RowObject as ModEntry;
            if (mod == null) return;

            switch (e.Column.AspectName)
            {
                case "Name":
                    mod.ManualName = !string.IsNullOrEmpty(e.NewValue as string);

                    if (!mod.ManualName)
                        // Restore name
                        Mods.UpdateMod(mod, Settings);

                    break;

                case "Index":
		            if (Settings.AutoNumberIndexes == false) break;
					if ((int)e.NewValue == (int)e.Value) break;
					ReorderIndexes(mod, (int)e.Value);
					modlist_ListObjectListView.Sort();
					modlist_ListObjectListView.Refresh();
                    break;
            }
        }

        #endregion

		/// <summary>
		/// Reorders mod indexes if a mod has its index changed.
		/// </summary>
		/// <param name="mod">The mod object that was updated</param>
		/// <param name="oldIndex">The old index the mod had</param>
		private void ReorderIndexes(ModEntry mod, int oldIndex)
		{
			var currentIndex = mod.Index;

			var modList = Mods.All.ToList();
			int startPos = (currentIndex > oldIndex) ? oldIndex : currentIndex;
			var range = Math.Abs(currentIndex - oldIndex) + 1;
				
			for (int i = startPos; i < range; i++)
			{
				var modEntry = modList[i];
				if (modEntry != mod)
					modEntry.Index += ((currentIndex - oldIndex) > 0) ? -1 : 1;
			}

		}

	}
}