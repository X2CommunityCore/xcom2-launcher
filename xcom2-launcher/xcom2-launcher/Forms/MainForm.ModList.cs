using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCOM2Launcher.Mod;
using XCOM2Launcher.Helper;
using System.Drawing;
using BrightIdeasSoftware;
using Microsoft.VisualBasic;

namespace XCOM2Launcher
{
    public partial class MainForm : Form
    {
        public ModList Mods { get { return Settings.Mods; } }

        public TypedObjectListView<ModEntry> ModList { get; private set; }

        public List<ModEntry> Downloads { get; } = new List<ModEntry>();

        protected BrightIdeasSoftware.ObjectListView modlist_objectlistview;

        public void initObjectListView()
        {
            if (modlist_objectlistview != null)
                modlist_objectlistview.Dispose();

            modlist_objectlistview = new ObjectListView
            {
                // General
                Name = "modlist_objectlistview",
                Size = new System.Drawing.Size(886, 222),
                Location = new System.Drawing.Point(0, 0),
                Dock = System.Windows.Forms.DockStyle.Fill,
                TabIndex = 0,

                // Behavior
                FullRowSelect = true,
                CellEditActivation = ObjectListView.CellEditActivateMode.DoubleClick,

                // Sorting
                ShowSortIndicators = true,
                TintSortColumn = true,
                IsSearchOnSortColumn = false,
                SortGroupItemsByPrimaryColumn = false,
                AlwaysGroupBySortOrder = SortOrder.None,

                // Checkbox
                CheckBoxes = true,
                CheckedAspectName = "isActive",
            };

            horizontal_splitcontainer.Panel1.Controls.Add(modlist_objectlistview);
            //modlist_objectlistview.Update();

            var CategoryGroupingDelegate = new GroupKeyGetterDelegate((object o) =>
            {
                return Mods.GetCategory((o as ModEntry));
            });

            var CategoryFormatterDelegate = new GroupFormatterDelegate((OLVGroup group, GroupingParameters parameters) =>
            {
                string GroupName = group.Key as string;
                group.Collapsed = Mods.Entries[GroupName].Collapsed;

                if (GroupName == "Unsorted")
                    parameters.GroupComparer = Comparer<OLVGroup>.Create((a, b) => {
                        if (a.Key as string == "Unsorted")
                            return -1;

                        if (b.Key as string == "Unsorted")
                            return 1;

                        return String.Compare(a.ToString(), b.ToString());
                    });
            });

            var columns = new OLVColumn[]
            {
                new OLVColumn{
                    Text = "Name",
                    AspectName = "Name",
                    Width = 500,
                    GroupKeyGetter = CategoryGroupingDelegate,
                    GroupFormatter = CategoryFormatterDelegate,
                    DisplayIndex = 2,
                    IsEditable = true
                },

                new OLVColumn{
                    Text = "ID",
                    AspectName = "ID",
                    Width = 200,
                    GroupKeyGetter = CategoryGroupingDelegate,
                    GroupFormatter = CategoryFormatterDelegate,
                    DisplayIndex = 3,
                    IsEditable = false,
                },

                new OLVColumn {
                    Text = "Size",
                    AspectName = "Size",
                    AspectToStringConverter = (object size) => { return XCOM2Launcher.Helper.FileSizeFormatExtension.FormatAsFileSize((long) size);  },
                    TextAlign = HorizontalAlignment.Right,
                    Width = 100,
                    DisplayIndex = 4,
                    IsEditable = false,
                },

                new OLVColumn{
                    Text = "Last Update",
                    AspectName = "DateUpdated",
                    Width = 120,
                    TextAlign = HorizontalAlignment.Right,
                    DisplayIndex = 5,
                    IsEditable = false,
                },
                
                // State
                new OLVColumn {
                    Text = "State",
                    //Groupable = false,
                    //Sortable = false,
                    IsEditable = false,
                    Width = 30,
                    DisplayIndex = 0,
                    AspectGetter = (object o) =>
                    {
                        var mod = o as ModEntry;


                        if (mod.State.HasFlag(ModState.ModConflict))
                            return "Conflict";

                        else if (mod.State.HasFlag(ModState.DuplicateID))
                            return "Duplicate ID";

                        else if (mod.State.HasFlag(ModState.New))
                            return "New";

                        else if (mod.State.HasFlag(ModState.UpdateAvailable))
                            return "Update Available";

                        return "OK";
                    }
                },

                new OLVColumn{
                    Text = "Order",
                    AspectName = "Index",
                    TextAlign = HorizontalAlignment.Right,
                    HeaderTextAlign = HorizontalAlignment.Center,
                    Width = 50,
                    GroupKeyGetter = CategoryGroupingDelegate,
                    GroupFormatter = CategoryFormatterDelegate,
                    IsEditable = false,
                    DisplayIndex = 1,
                },
            };

            // size groupies
            columns[2].MakeGroupies(
                new long[] { 1024, 1024 * 1024, (long)50 * 1024 * 1024, (long)100 * 1024 * 1024 },
                new string[] { "< 1 KB", "< 1MB", "< 50 MB", "< 100 MB", "> 100 MB" }
                );

            // date updated groupies
            columns[3].MakeGroupies(
                new DateTime[] { DateTime.Now.Subtract(TimeSpan.FromHours(24 * 30)), DateTime.Now.Subtract(TimeSpan.FromHours(24 * 7)), DateTime.Now.Date },
                new string[] { "Older than one month", "Last Month", "This Week", "Today" }
                );

            // Wrapper
            ModList = new TypedObjectListView<ModEntry>(this.modlist_objectlistview);

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
            modlist_objectlistview.Columns.AddRange(columns);

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
                return;
            }
        }

        private void ModListKeyUp(object sender, KeyEventArgs e)
        {
        }

        private void ModListGroupExpandingCollapsing(object sender, GroupExpandingCollapsingEventArgs e)
        {
            // only handle if grouped by name or id
            if (modlist_objectlistview.PrimarySortColumn.AspectName != "Name" && modlist_objectlistview.PrimarySortColumn.AspectName != "ID")
                return;

            var category = Mods.Entries[e.Group.Key as string];
            category.Collapsed = !e.IsExpanding;
        }

        private void ModListFormatRow(object sender, FormatRowEventArgs e)
        {
            var mod = e.Model as ModEntry;
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
            Steamworks.SteamAPI.InitSafe();

            // Confirmation dialog
            string text = (modlist_objectlistview.SelectedItems.Count == 1)
                ? $"Are you sure you want to delete '{(modlist_objectlistview.SelectedObject as ModEntry).Name}'?"
                : $"Are you sure you want to delete {modlist_objectlistview.SelectedItems.Count} mods?";

            text += "\r\nThis can not be undone.";

            DialogResult r = MessageBox.Show(text, "Confirm deletion", MessageBoxButtons.OKCancel);
            if (r != DialogResult.OK)
                return;

            // Delete
            List<ModEntry> mods = ModList.SelectedObjects.ToList();
            foreach (ModEntry mod in mods)
            {
                modlist_objectlistview.RemoveObject(mod);
                Mods.RemoveMod(mod);

                Directory.Delete(mod.Path, true);
                // unsubscribe
                if (mod.Source == ModSource.SteamWorkshop)
                    Steamworks.SteamUGC.UnsubscribeItem(new Steamworks.PublishedFileId_t((ulong)mod.WorkshopID));
            }
        }

        private void MoveMods(string category)
        {
            modlist_objectlistview.BeginUpdate();
            foreach (ModEntry mod in ModList.SelectedObjects)
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

            if (Settings.ShowHiddenElements)
                modlist_objectlistview.Objects = Mods.All;

            else
                modlist_objectlistview.Objects = Mods.All.Where(m => !m.isHidden);
            
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
            ContextMenu menu = new ContextMenu();
            if (m == null || m.ID == null)
                return menu;

            MenuItem item = new MenuItem("Rename");
            item.Click += (a, b) => { modlist_objectlistview.EditModel(m); };
            menu.MenuItems.Add(item);

            // Move to ...
            MenuItem moveToGroupMenu = new MenuItem("Move to Category...");
            // ... new group
            moveToGroupMenu.MenuItems.Add("New category", new EventHandler(
                    (object a, EventArgs b) =>
                    {
                        string group = Interaction.InputBox("Please enter the name of the new category", "Create category", "New category");
                        if (group == "")
                            return;

                        MoveMods(group);
                    })
                );
            moveToGroupMenu.MenuItems.Add("-");

            // ... existing group
            foreach (string group in Settings.Mods.Categories)
            {
                if (group == Mods.GetCategory(m))
                    continue;

                moveToGroupMenu.MenuItems.Add(group, new EventHandler((object a, EventArgs b) => { MoveMods(group); }));
            }
            menu.MenuItems.Add(moveToGroupMenu);


            var toggleVisibility = new MenuItem();
            toggleVisibility.Text = m.isHidden ? "Unhide" : "Hide";
            toggleVisibility.Click += (object sender, EventArgs e) =>
            {
                // save as new list so we can remove mods if they are being hidden
                foreach (ModEntry mod in ModList.SelectedObjects.ToList())
                {
                    mod.isHidden = !m.isHidden;

                    if (!Settings.ShowHiddenElements && mod.isHidden)
                        modlist_objectlistview.RemoveObject(mod);
                }

                RefreshModList();
            };

            menu.MenuItems.Add(toggleVisibility);

            menu.MenuItems.Add("-");

            menu.MenuItems.Add(new MenuItem("Delete / Unsubscribe", new EventHandler((object a, EventArgs b) => { DeleteMods(); })));


            menu.MenuItems.Add("-");

            menu.MenuItems.Add(new MenuItem("Show in Explorer", new EventHandler((object a, EventArgs b) => { m.ShowInExplorer(); })));

            if (m.WorkshopID != -1)
                menu.MenuItems.Add(new MenuItem("Show on Steam", new EventHandler((object a, EventArgs b) => { m.ShowOnSteam(); })));



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
            //if (modlist_objectlistview.SelectedObjects.Count > 1)
            //    return;

            ModEntry m = modlist_objectlistview.SelectedObject as ModEntry;
            UpdateModInfo(m);
            CheckAndUpdateChangeLog(tabControl1.SelectedTab.Name, m);

            if (m != null)
            {
                m.State &= ~ModState.New;
                modlist_objectlistview.EnsureModelVisible(m);
            }
        }

        private void ModListEditFinished(object sender, CellEditEventArgs e)
        {
            var mod = e.RowObject as ModEntry;

            switch (e.Column.AspectName)
            {
                case "Name":
                    mod.ManualName = (e.NewValue as string).Length > 0;

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
