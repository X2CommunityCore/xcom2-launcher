using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Microsoft.VisualBasic;
using Steamworks;
using XCOM2Launcher.Classes.Steam;
using XCOM2Launcher.Helper;
using XCOM2Launcher.Mod;

namespace XCOM2Launcher.Forms
{
    public partial class MainForm : Form
    {
        protected ObjectListView modlist_objectlistview;

        public ModList Mods => Settings.Mods;

        public TypedObjectListView<ModEntry> ModList { get; private set; }

        public List<ModEntry> Downloads { get; } = new List<ModEntry>();

        public void InitObjectListView()
        {
            modlist_objectlistview?.Dispose();

            modlist_objectlistview = new ObjectListView
            {
                // General
                Name = "modlist_objectlistview",
                Size = new Size(886, 222),
                Location = new Point(0, 0),
                Dock = DockStyle.Fill,
                TabIndex = 0,

                // Behavior
                FullRowSelect = true,
                CellEditActivation = ObjectListView.CellEditActivateMode.DoubleClick,
                AllowColumnReorder = true,

                // Sorting
                ShowSortIndicators = true,
                TintSortColumn = true,
                IsSearchOnSortColumn = false,
                SortGroupItemsByPrimaryColumn = false,
                AlwaysGroupBySortOrder = SortOrder.None,

                // Checkbox
                CheckBoxes = true,
                CheckedAspectName = "isActive"
            };

            horizontal_splitcontainer.Panel1.Controls.Add(modlist_objectlistview);
            //modlist_objectlistview.Update();

            var categoryGroupingDelegate = new GroupKeyGetterDelegate(o => Mods.GetCategory(o as ModEntry));

            var categoryFormatterDelegate = new GroupFormatterDelegate((@group, parameters) =>
            {
                var groupName = group.Key as string;
                if (groupName == null)
                    return;

                // Restore collapsed state
                group.Collapsed = Mods.Entries[groupName].Collapsed;

                // Move Unsorted to the top
                if (groupName == "Unsorted")
                    parameters.GroupComparer = Comparer<OLVGroup>.Create((a, b) =>
                    {
                        if (a.Key as string == "Unsorted")
                            return -1;

                        if (b.Key as string == "Unsorted")
                            return 1;

                        return string.CompareOrdinal(a.ToString(), b.ToString());
                    });
            });

            var columns = new[]
            {
                // first column is marked as primary column
                new OLVColumn 
                {
                    Text = "Name",
                    AspectName = "Name",
                    Width = 500,
                    GroupKeyGetter = categoryGroupingDelegate,
                    GroupFormatter = categoryFormatterDelegate,
                    IsEditable = true
                },
                new OLVColumn
                {
                    Text = "ID",
                    AspectName = "ID",
                    Width = 200,
                    GroupKeyGetter = categoryGroupingDelegate,
                    GroupFormatter = categoryFormatterDelegate,
                    IsEditable = false
                },

                // State
                new OLVColumn
                {
                    Text = "State",
                    //Groupable = false,
                    //Sortable = false,
                    IsEditable = false,
                    Width = 40,
                    AspectGetter = o =>
                    {
                        var mod = o as ModEntry;

                        if (mod.State.HasFlag(ModState.ModConflict))
                            return "Conflict";

                        if (mod.State.HasFlag(ModState.DuplicateID))
                            return "Duplicate ID";

                        if (mod.State.HasFlag(ModState.New))
                            return "New";

                        if (mod.State.HasFlag(ModState.UpdateAvailable))
                            return "Update Available";

                        return "OK";
                    }
                },
                new OLVColumn
                {
                    Text = "Order",
                    AspectName = "Index",
                    TextAlign = HorizontalAlignment.Right,
                    HeaderTextAlign = HorizontalAlignment.Center,
                    Width = 50,
                    GroupKeyGetter = categoryGroupingDelegate,
                    GroupFormatter = categoryFormatterDelegate,
                    IsEditable = false,
                },

                new OLVColumn
                {
                    Text = "Size",
                    AspectName = "Size",
                    AspectToStringConverter = size => ((long) size).FormatAsFileSize(),
                    TextAlign = HorizontalAlignment.Right,
                    Width = 100,
                    IsEditable = false
                },
                new OLVColumn
                {
                    Text = "Last Update",
                    AspectName = "DateUpdated",
                    Width = 120,
                    TextAlign = HorizontalAlignment.Right,
                    IsEditable = false
                },
                new OLVColumn
                {
                    Text = "Date Added",
                    AspectName = "DateAdded",
                    Width = 120,
                    TextAlign = HorizontalAlignment.Right,
                    IsEditable = false,
                    IsVisible = false,
                },
                new OLVColumn
                {
                    Text = "Date Created",
                    AspectName = "DateCreated",
                    Width = 120,
                    TextAlign = HorizontalAlignment.Right,
                    IsEditable = false,
                    IsVisible = false,
                },
                new OLVColumn
                {
                    Text = "Path",
                    AspectName = "Path",
                    Width = 160,
                    IsEditable = false,
                    IsVisible = false,
                    GroupKeyGetter = o => Path.GetDirectoryName((o as ModEntry)?.Path)
                },
            };

            // size groupies
            columns.Single(c => c.AspectName == "Size").MakeGroupies(
                new[] { 1024, 1024 * 1024, (long)50 * 1024 * 1024, (long)100 * 1024 * 1024 },
                new[] { "< 1 KB", "< 1MB", "< 50 MB", "< 100 MB", "> 100 MB" }
                );

            // date updated groupies
            columns.Single(c => c.AspectName == "DateUpdated").MakeGroupies(
                new[]
                {
                    DateTime.Now.Subtract(TimeSpan.FromHours(24*30)), DateTime.Now.Subtract(TimeSpan.FromHours(24*7)),
                    DateTime.Now.Date
                },
                new[] { "Older than one month", "Last Month", "This Week", "Today" }
                );

            // Wrapper
            ModList = new TypedObjectListView<ModEntry>(modlist_objectlistview);

            // Events
            modlist_objectlistview.SelectionChanged += ModListSelectionChanged;
            modlist_objectlistview.ItemChecked += ModListItemChecked;
            modlist_objectlistview.CellRightClick += ModListCellRightClick;
            modlist_objectlistview.CellEditFinished += ModListEditFinished;
            modlist_objectlistview.FormatRow += ModListFormatRow;
            modlist_objectlistview.GroupExpandingCollapsing += ModListGroupExpandingCollapsing;
            modlist_objectlistview.KeyUp += ModListKeyUp;
            modlist_objectlistview.KeyDown += ModListKeyDown;

            // Content
            modlist_objectlistview.AllColumns.AddRange(columns);
            modlist_objectlistview.RebuildColumns();

            // move state to the beginning
            columns.Single(c => c.Text == "State").DisplayIndex = 0;

            // Restore State
            if (Settings.Windows.ContainsKey("main") && Settings.Windows["main"].Data != null)
                modlist_objectlistview.RestoreState(Settings.Windows["main"].Data);

            RefreshModList();
        }

        private void ModListKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                horizontal_splitcontainer.Panel2Collapsed = true;
            }
        }

        private void ModListKeyUp(object sender, KeyEventArgs e)
        {
        }

        private void ModListGroupExpandingCollapsing(object sender, GroupExpandingCollapsingEventArgs e)
        {
            // only handle if grouped by name or id
            if (modlist_objectlistview.PrimarySortColumn.AspectName != "Name" &&
                modlist_objectlistview.PrimarySortColumn.AspectName != "ID")
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

            if (mod.State.HasFlag(ModState.ModConflict) || mod.State.HasFlag(ModState.DuplicateID))
            {
                item.BackColor = Color.PaleVioletRed;
                item.ForeColor = Color.Black;
            }

            else if (mod.isHidden)
                item.BackColor = SystemColors.InactiveBorder;

            else if (mod.State.HasFlag(ModState.New))
                item.BackColor = SystemColors.Info;
        }

        private void UpdateMod(ModEntry m)
        {
            if (m.isHidden && !Settings.ShowHiddenElements)
                // cant update what's not there
                return;

            modlist_objectlistview.UpdateObject(m);
        }


        private void DeleteMods()
        {
            SteamAPIWrapper.InitSafe();

            // Confirmation dialog
            var text = modlist_objectlistview.SelectedObjects.Count == 1
                ? $"Are you sure you want to delete '{(modlist_objectlistview.SelectedObjects[0] as ModEntry)?.Name}'?"
                : $"Are you sure you want to delete {modlist_objectlistview.SelectedObjects.Count} mods?";

            text += "\r\nThis can not be undone.";

            var r = MessageBox.Show(text, "Confirm deletion", MessageBoxButtons.OKCancel);
            if (r != DialogResult.OK)
                return;

            // Delete
            var mods = ModList.SelectedObjects.ToList();
            foreach (var mod in mods)
            {
                modlist_objectlistview.RemoveObject(mod);
                Mods.RemoveMod(mod);

                Directory.Delete(mod.Path, true);
                // unsubscribe
                if (mod.Source == ModSource.SteamWorkshop)
                    SteamUGC.UnsubscribeItem(new PublishedFileId_t((ulong)mod.WorkshopID));
            }
        }

        private void MoveMods(string category)
        {
            modlist_objectlistview.BeginUpdate();
            foreach (var mod in ModList.SelectedObjects)
            {
                Mods.RemoveMod(mod);
                Mods.AddMod(category, mod);
                modlist_objectlistview.UpdateObject(mod);
            }
            modlist_objectlistview.EndUpdate();
        }


        private void RefreshModList()
        {
            // Un-register events
            modlist_objectlistview.SelectionChanged -= ModListSelectionChanged;
            modlist_objectlistview.ItemChecked -= ModListItemChecked;

            modlist_objectlistview.BeginUpdate();

            // add elements
            modlist_objectlistview.ClearObjects();

            modlist_objectlistview.Objects = Settings.ShowHiddenElements ? Mods.All : Mods.All.Where(m => !m.isHidden);

            modlist_objectlistview.EndUpdate();

            // Re-register events
            modlist_objectlistview.SelectionChanged += ModListSelectionChanged;
            modlist_objectlistview.ItemChecked += ModListItemChecked;
        }

        private void UpdateModListItem(ListViewItem item)
        {
            UpdateMod(ModList.GetModelObject(item.Index));
        }

        private ContextMenu CreateModListContextMenu(ModEntry m)
        {
            var menu = new ContextMenu();
            if (m?.ID == null)
                return menu;

            var item = new MenuItem("Rename");
            item.Click += (a, b) => { modlist_objectlistview.EditModel(m); };
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


            var toggleVisibility = new MenuItem { Text = m.isHidden ? "Unhide" : "Hide" };
            toggleVisibility.Click += delegate
            {
                // save as new list so we can remove mods if they are being hidden
                foreach (var mod in ModList.SelectedObjects.ToList())
                {
                    mod.isHidden = !m.isHidden;

                    if (!Settings.ShowHiddenElements && mod.isHidden)
                        modlist_objectlistview.RemoveObject(mod);
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

        private void ModListItemChecked(object sender, ItemCheckedEventArgs e)
        {
            UpdateLabels();
            UpdateConflicts();
        }

        private void ModListSelectionChanged(object sender, EventArgs e)
        {
            if (modlist_objectlistview.SelectedObjects.Count > 1)
                return;

            var m = modlist_objectlistview.SelectedObject as ModEntry;

            UpdateModInfo(m);
            CheckAndUpdateChangeLog(modinfo_tabcontrol.SelectedTab, m);

            if (m != null)
            {
                m.State &= ~ModState.New;
                modlist_objectlistview.EnsureModelVisible(m);
            }
        }

        private void ModListEditFinished(object sender, CellEditEventArgs e)
        {
            var mod = e.RowObject as ModEntry;
            Contract.Assume(mod != null);

            switch (e.Column.AspectName)
            {
                case "Name":
                    mod.ManualName = !string.IsNullOrEmpty(e.NewValue as string);

                    if (!mod.ManualName)
                        // Restore name
                        Mods.UpdateMod(mod);

                    break;

                case "Index":
                    // todo reorder
                    break;
            }
        }

        #endregion
    }
}