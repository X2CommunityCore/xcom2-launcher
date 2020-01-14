namespace XCOM2Launcher.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.main_statusstrip = new System.Windows.Forms.StatusStrip();
            this.status_toolstrip_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.progress_toolstrip_progressbar = new System.Windows.Forms.ToolStripProgressBar();
            this.labelFillsFreeSpace = new System.Windows.Forms.ToolStripStatusLabel();
            this.appRestartPendingLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.main_menustrip = new System.Windows.Forms.MenuStrip();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.searchForModsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateEntriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHiddenModsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.editOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageCategoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importActiveModsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFromXCOM2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFromWotCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanModsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resubscribeToModsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runXCOM2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runWarOfTheChosenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runChallengeModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.openHomepageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.amlWikiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDiscordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.error_provider = new System.Windows.Forms.ErrorProvider(this.components);
            this.main_tabcontrol = new System.Windows.Forms.TabControl();
            this.modlist_tab = new System.Windows.Forms.TabPage();
            this.horizontal_splitcontainer = new System.Windows.Forms.SplitContainer();
            this.modlist_ListObjectListView = new BrightIdeasSoftware.ObjectListView();
            this.olvcActive = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvAuthor = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcCategory = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcState = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcOrder = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcSize = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcLastUpdated = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcDateAdded = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcDateCreated = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcPath = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcHasBackup = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcWorkshopID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcHidden = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcTags = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvSteamLink = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvBrowserLink = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvForWOTC = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.pModsLegend = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pModsTop = new System.Windows.Forms.Panel();
            this.LauchOptionsPanel = new System.Windows.Forms.Panel();
            this.modTabToolStrip = new System.Windows.Forms.ToolStrip();
            this.quickLaunchToolstripButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.noSeekFreeLoadingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoDebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cShowLegend = new System.Windows.Forms.CheckBox();
            this.cEnableGrouping = new System.Windows.Forms.CheckBox();
            this.modlist_toggleGroupsButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.modlist_filterClearButton = new System.Windows.Forms.Button();
            this.modinfo_groupbox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.modinfo_tabcontrol = new System.Windows.Forms.TabControl();
            this.modinfo_details_tab = new System.Windows.Forms.TabPage();
            this.btnDescUndo = new System.Windows.Forms.Button();
            this.btnDescSave = new System.Windows.Forms.Button();
            this.modinfo_info_CreatedLabel = new System.Windows.Forms.Label();
            this.modinfo_info_DescriptionLabel = new System.Windows.Forms.Label();
            this.modinfo_info_InstalledTextBox = new System.Windows.Forms.TextBox();
            this.modinfo_info_DateCreatedTextBox = new System.Windows.Forms.TextBox();
            this.modinfo_info_InstalledLabel = new System.Windows.Forms.Label();
            this.modinfo_info_TitleTextBox = new System.Windows.Forms.TextBox();
            this.modinfo_info_AuthorTextBox = new System.Windows.Forms.TextBox();
            this.modinfo_info_TitleLabel = new System.Windows.Forms.Label();
            this.modinfo_info_AuthorLabel = new System.Windows.Forms.Label();
            this.modinfo_info_DescriptionRichTextBox = new System.Windows.Forms.RichTextBox();
            this.modinfo_readme_tab = new System.Windows.Forms.TabPage();
            this.modinfo_readme_RichTextBox = new System.Windows.Forms.RichTextBox();
            this.modinfo_inspect_tab = new System.Windows.Forms.TabPage();
            this.modinfo_inspect_propertygrid = new System.Windows.Forms.PropertyGrid();
            this.modinfo_config_tab = new System.Windows.Forms.TabPage();
            this.modinfo_config_TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.modinfo_ConfigFCTB = new FastColoredTextBoxNS.FastColoredTextBox();
            this.modinfo_config_buttonsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.modinfo_config_ExpandButton = new System.Windows.Forms.Button();
            this.modinfo_config_CompareButton = new System.Windows.Forms.Button();
            this.modinfo_config_SaveButton = new System.Windows.Forms.Button();
            this.modinfo_config_LoadButton = new System.Windows.Forms.Button();
            this.modinfo_config_RemoveButton = new System.Windows.Forms.Button();
            this.modinfo_changelog_tab = new System.Windows.Forms.TabPage();
            this.modinfo_changelog_richtextbox = new System.Windows.Forms.RichTextBox();
            this.modinfo_dependencies_tab = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.olvDependentMods = new BrightIdeasSoftware.ObjectListView();
            this.olvColDepModsActive = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColDepModsName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColDepModsState = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColDepModsHidden = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColDepModsSteamUrl = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColDepModsUrl = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColDepModsWotc = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.label6 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.olvRequiredMods = new BrightIdeasSoftware.ObjectListView();
            this.olvColReqModsActive = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColReqModsName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColReqModsState = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColReqModsHidden = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColReqModsSteamUrl = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColReqModsWotc = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.label5 = new System.Windows.Forms.Label();
            this.modinfo_image_picturebox = new System.Windows.Forms.PictureBox();
            this.conflicts_tab = new System.Windows.Forms.TabPage();
            this.conflicts_tab_tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.conflicts_log_label = new System.Windows.Forms.Label();
            this.conflicts_datagrid = new System.Windows.Forms.DataGridView();
            this.ColumnModName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnInternalClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnModClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.conflicts_textbox = new System.Windows.Forms.TextBox();
            this.export_tab = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.export_richtextbox = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.export_all_mods_checkbox = new System.Windows.Forms.CheckBox();
            this.export_workshop_link_checkbox = new System.Windows.Forms.CheckBox();
            this.export_group_checkbox = new System.Windows.Forms.CheckBox();
            this.export_load_button = new System.Windows.Forms.Button();
            this.export_save_button = new System.Windows.Forms.Button();
            this.tabImageList = new System.Windows.Forms.ImageList(this.components);
            this.olvcSavedIni = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.fillPanel = new System.Windows.Forms.Panel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.modlist_FilterCueTextBox = new XCOM2Launcher.UserElements.CueTextBox();
            this.modinfo_config_FileSelectCueComboBox = new XCOM2Launcher.UserElements.CueComboBox();
            this.main_statusstrip.SuspendLayout();
            this.main_menustrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.error_provider)).BeginInit();
            this.main_tabcontrol.SuspendLayout();
            this.modlist_tab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.horizontal_splitcontainer)).BeginInit();
            this.horizontal_splitcontainer.Panel1.SuspendLayout();
            this.horizontal_splitcontainer.Panel2.SuspendLayout();
            this.horizontal_splitcontainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.modlist_ListObjectListView)).BeginInit();
            this.pModsLegend.SuspendLayout();
            this.pModsTop.SuspendLayout();
            this.LauchOptionsPanel.SuspendLayout();
            this.modTabToolStrip.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.modinfo_groupbox.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.modinfo_tabcontrol.SuspendLayout();
            this.modinfo_details_tab.SuspendLayout();
            this.modinfo_readme_tab.SuspendLayout();
            this.modinfo_inspect_tab.SuspendLayout();
            this.modinfo_config_tab.SuspendLayout();
            this.modinfo_config_TableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.modinfo_ConfigFCTB)).BeginInit();
            this.modinfo_config_buttonsTableLayoutPanel.SuspendLayout();
            this.modinfo_changelog_tab.SuspendLayout();
            this.modinfo_dependencies_tab.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvDependentMods)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvRequiredMods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.modinfo_image_picturebox)).BeginInit();
            this.conflicts_tab.SuspendLayout();
            this.conflicts_tab_tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.conflicts_datagrid)).BeginInit();
            this.export_tab.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // main_statusstrip
            // 
            this.main_statusstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status_toolstrip_label,
            this.progress_toolstrip_progressbar,
            this.labelFillsFreeSpace,
            this.appRestartPendingLabel});
            this.main_statusstrip.Location = new System.Drawing.Point(0, 719);
            this.main_statusstrip.Name = "main_statusstrip";
            this.main_statusstrip.ShowItemToolTips = true;
            this.main_statusstrip.Size = new System.Drawing.Size(984, 22);
            this.main_statusstrip.TabIndex = 5;
            this.main_statusstrip.Text = "statusStrip1";
            // 
            // status_toolstrip_label
            // 
            this.status_toolstrip_label.Name = "status_toolstrip_label";
            this.status_toolstrip_label.Size = new System.Drawing.Size(42, 17);
            this.status_toolstrip_label.Text = "Ready.";
            // 
            // progress_toolstrip_progressbar
            // 
            this.progress_toolstrip_progressbar.AutoSize = false;
            this.progress_toolstrip_progressbar.Name = "progress_toolstrip_progressbar";
            this.progress_toolstrip_progressbar.Size = new System.Drawing.Size(120, 16);
            this.progress_toolstrip_progressbar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // labelFillsFreeSpace
            // 
            this.labelFillsFreeSpace.Name = "labelFillsFreeSpace";
            this.labelFillsFreeSpace.Size = new System.Drawing.Size(647, 17);
            this.labelFillsFreeSpace.Spring = true;
            // 
            // appRestartPendingLabel
            // 
            this.appRestartPendingLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appRestartPendingLabel.ForeColor = System.Drawing.Color.Red;
            this.appRestartPendingLabel.Name = "appRestartPendingLabel";
            this.appRestartPendingLabel.Size = new System.Drawing.Size(158, 17);
            this.appRestartPendingLabel.Text = "Application restart pending";
            this.appRestartPendingLabel.ToolTipText = "Some changes to the settings won\'t take effect,\r\nuntil after the application has " +
    "been restarted.";
            // 
            // main_menustrip
            // 
            this.main_menustrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.runXCOM2ToolStripMenuItem,
            this.runWarOfTheChosenToolStripMenuItem,
            this.runChallengeModeToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.main_menustrip.Location = new System.Drawing.Point(0, 0);
            this.main_menustrip.Name = "main_menustrip";
            this.main_menustrip.ShowItemToolTips = true;
            this.main_menustrip.Size = new System.Drawing.Size(984, 24);
            this.main_menustrip.TabIndex = 6;
            this.main_menustrip.Text = "menuStrip1";
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.reloadToolStripMenuItem,
            this.toolStripSeparator1,
            this.searchForModsToolStripMenuItem,
            this.updateEntriesToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.runToolStripMenuItem.Text = "&File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.saveToolStripMenuItem.Text = "Save settings";
            this.saveToolStripMenuItem.ToolTipText = "Saves the current settings and updates XCOM configuration \r\nfiles with respect to" +
    " currently active mods and mod folders.";
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.reloadToolStripMenuItem.Text = "Reset settings";
            this.reloadToolStripMenuItem.ToolTipText = "Restores the settings from last manual save or\r\nsince last game launch.";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(203, 6);
            // 
            // searchForModsToolStripMenuItem
            // 
            this.searchForModsToolStripMenuItem.Name = "searchForModsToolStripMenuItem";
            this.searchForModsToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.searchForModsToolStripMenuItem.Text = "Search for new mods";
            this.searchForModsToolStripMenuItem.ToolTipText = "Scans all known mod directories for new mods and adds them to the mod list.\r\nThis" +
    " is useful for adding new mods without re-starting AML.";
            // 
            // updateEntriesToolStripMenuItem
            // 
            this.updateEntriesToolStripMenuItem.Name = "updateEntriesToolStripMenuItem";
            this.updateEntriesToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.updateEntriesToolStripMenuItem.Text = "Update mod information";
            this.updateEntriesToolStripMenuItem.ToolTipText = "Updates the current state of all mods, by performing some validations \r\nand by re" +
    "questing the latest mod information from the Steam workshop.\r\nThis task is also " +
    "performed when you start AML.";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(203, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHiddenModsToolStripMenuItem,
            this.toolStripSeparator3,
            this.editOptionsToolStripMenuItem,
            this.manageCategoriesToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Options";
            // 
            // showHiddenModsToolStripMenuItem
            // 
            this.showHiddenModsToolStripMenuItem.CheckOnClick = true;
            this.showHiddenModsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.showHiddenModsToolStripMenuItem.Name = "showHiddenModsToolStripMenuItem";
            this.showHiddenModsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.showHiddenModsToolStripMenuItem.Text = "Show hidden mods";
            this.showHiddenModsToolStripMenuItem.ToolTipText = "Show/hide all mods, that are currently set to \"hidden\".";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(173, 6);
            // 
            // editOptionsToolStripMenuItem
            // 
            this.editOptionsToolStripMenuItem.Name = "editOptionsToolStripMenuItem";
            this.editOptionsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.editOptionsToolStripMenuItem.Text = "Settings...";
            // 
            // manageCategoriesToolStripMenuItem
            // 
            this.manageCategoriesToolStripMenuItem.Name = "manageCategoriesToolStripMenuItem";
            this.manageCategoriesToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.manageCategoriesToolStripMenuItem.Text = "Categories...";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importActiveModsToolStripMenuItem,
            this.cleanModsToolStripMenuItem,
            this.resubscribeToModsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // importActiveModsToolStripMenuItem
            // 
            this.importActiveModsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importFromXCOM2ToolStripMenuItem,
            this.importFromWotCToolStripMenuItem});
            this.importActiveModsToolStripMenuItem.Name = "importActiveModsToolStripMenuItem";
            this.importActiveModsToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.importActiveModsToolStripMenuItem.Text = "Import active mods";
            // 
            // importFromXCOM2ToolStripMenuItem
            // 
            this.importFromXCOM2ToolStripMenuItem.Name = "importFromXCOM2ToolStripMenuItem";
            this.importFromXCOM2ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.importFromXCOM2ToolStripMenuItem.Text = "XCOM 2";
            this.importFromXCOM2ToolStripMenuItem.ToolTipText = "Enables all mods in the mods list, that are currently listed \r\nas active in the X" +
    "COM 2 basegame config file.";
            // 
            // importFromWotCToolStripMenuItem
            // 
            this.importFromWotCToolStripMenuItem.Name = "importFromWotCToolStripMenuItem";
            this.importFromWotCToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.importFromWotCToolStripMenuItem.Text = "WotC";
            this.importFromWotCToolStripMenuItem.ToolTipText = "Enables all mods in the mods list, that are currently listed \r\nas active in the W" +
    "otC config file.";
            // 
            // cleanModsToolStripMenuItem
            // 
            this.cleanModsToolStripMenuItem.Name = "cleanModsToolStripMenuItem";
            this.cleanModsToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.cleanModsToolStripMenuItem.Text = "Clean mods...";
            // 
            // resubscribeToModsToolStripMenuItem
            // 
            this.resubscribeToModsToolStripMenuItem.Name = "resubscribeToModsToolStripMenuItem";
            this.resubscribeToModsToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.resubscribeToModsToolStripMenuItem.Text = "Resubscribe to uninstalled mods";
            this.resubscribeToModsToolStripMenuItem.ToolTipText = "Subscribe to and download all workshop mods, \r\nthat are currently listed in AML, " +
    "but no longer installed.";
            // 
            // runXCOM2ToolStripMenuItem
            // 
            this.runXCOM2ToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.runXCOM2ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("runXCOM2ToolStripMenuItem.Image")));
            this.runXCOM2ToolStripMenuItem.Name = "runXCOM2ToolStripMenuItem";
            this.runXCOM2ToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
            this.runXCOM2ToolStripMenuItem.Text = "Run &XCOM 2";
            // 
            // runWarOfTheChosenToolStripMenuItem
            // 
            this.runWarOfTheChosenToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.runWarOfTheChosenToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("runWarOfTheChosenToolStripMenuItem.Image")));
            this.runWarOfTheChosenToolStripMenuItem.Name = "runWarOfTheChosenToolStripMenuItem";
            this.runWarOfTheChosenToolStripMenuItem.Size = new System.Drawing.Size(157, 20);
            this.runWarOfTheChosenToolStripMenuItem.Text = "Run War of the Chosen";
            // 
            // runChallengeModeToolStripMenuItem
            // 
            this.runChallengeModeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("runChallengeModeToolStripMenuItem.Image")));
            this.runChallengeModeToolStripMenuItem.Name = "runChallengeModeToolStripMenuItem";
            this.runChallengeModeToolStripMenuItem.Size = new System.Drawing.Size(146, 20);
            this.runChallengeModeToolStripMenuItem.Text = "Run Challenge Mode";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem,
            this.toolStripSeparator4,
            this.openHomepageToolStripMenuItem,
            this.amlWikiToolStripMenuItem,
            this.openDiscordToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.infoToolStripMenuItem.Text = "Info...";
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for updates...";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(186, 6);
            // 
            // openHomepageToolStripMenuItem
            // 
            this.openHomepageToolStripMenuItem.Name = "openHomepageToolStripMenuItem";
            this.openHomepageToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.openHomepageToolStripMenuItem.Text = "AML Project Page";
            // 
            // amlWikiToolStripMenuItem
            // 
            this.amlWikiToolStripMenuItem.Name = "amlWikiToolStripMenuItem";
            this.amlWikiToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.amlWikiToolStripMenuItem.Text = "AML Wiki";
            // 
            // openDiscordToolStripMenuItem
            // 
            this.openDiscordToolStripMenuItem.Name = "openDiscordToolStripMenuItem";
            this.openDiscordToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.openDiscordToolStripMenuItem.Text = "AML Discord Channel";
            this.openDiscordToolStripMenuItem.ToolTipText = "Opens a Discord invite link to our channel \r\non the XCOM 2 Modding server.";
            // 
            // error_provider
            // 
            this.error_provider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.error_provider.ContainerControl = this;
            // 
            // main_tabcontrol
            // 
            this.main_tabcontrol.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.main_tabcontrol.Controls.Add(this.modlist_tab);
            this.main_tabcontrol.Controls.Add(this.conflicts_tab);
            this.main_tabcontrol.Controls.Add(this.export_tab);
            this.main_tabcontrol.ImageList = this.tabImageList;
            this.main_tabcontrol.Location = new System.Drawing.Point(0, 27);
            this.main_tabcontrol.Name = "main_tabcontrol";
            this.main_tabcontrol.SelectedIndex = 0;
            this.main_tabcontrol.Size = new System.Drawing.Size(984, 689);
            this.main_tabcontrol.TabIndex = 6;
            this.main_tabcontrol.Selected += new System.Windows.Forms.TabControlEventHandler(this.MainTabSelected);
            // 
            // modlist_tab
            // 
            this.modlist_tab.Controls.Add(this.horizontal_splitcontainer);
            this.modlist_tab.Location = new System.Drawing.Point(4, 23);
            this.modlist_tab.Name = "modlist_tab";
            this.modlist_tab.Padding = new System.Windows.Forms.Padding(3);
            this.modlist_tab.Size = new System.Drawing.Size(976, 662);
            this.modlist_tab.TabIndex = 0;
            this.modlist_tab.Text = "Mods";
            this.modlist_tab.UseVisualStyleBackColor = true;
            // 
            // horizontal_splitcontainer
            // 
            this.horizontal_splitcontainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.horizontal_splitcontainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.horizontal_splitcontainer.Location = new System.Drawing.Point(3, 3);
            this.horizontal_splitcontainer.Name = "horizontal_splitcontainer";
            this.horizontal_splitcontainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // horizontal_splitcontainer.Panel1
            // 
            this.horizontal_splitcontainer.Panel1.Controls.Add(this.modlist_ListObjectListView);
            this.horizontal_splitcontainer.Panel1.Controls.Add(this.pModsLegend);
            this.horizontal_splitcontainer.Panel1.Controls.Add(this.pModsTop);
            // 
            // horizontal_splitcontainer.Panel2
            // 
            this.horizontal_splitcontainer.Panel2.Controls.Add(this.modinfo_groupbox);
            this.horizontal_splitcontainer.Size = new System.Drawing.Size(970, 656);
            this.horizontal_splitcontainer.SplitterDistance = 414;
            this.horizontal_splitcontainer.TabIndex = 5;
            // 
            // modlist_ListObjectListView
            // 
            this.modlist_ListObjectListView.AllColumns.Add(this.olvcActive);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvcName);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvAuthor);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvcID);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvcCategory);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvcState);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvcOrder);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvcSize);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvcLastUpdated);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvcDateAdded);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvcDateCreated);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvcPath);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvcHasBackup);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvcWorkshopID);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvcHidden);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvcTags);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvSteamLink);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvBrowserLink);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvForWOTC);
            this.modlist_ListObjectListView.AllowColumnReorder = true;
            this.modlist_ListObjectListView.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.modlist_ListObjectListView.CellEditUseWholeCell = false;
            this.modlist_ListObjectListView.CheckBoxes = true;
            this.modlist_ListObjectListView.CheckedAspectName = "isActive";
            this.modlist_ListObjectListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvcActive,
            this.olvcName,
            this.olvAuthor,
            this.olvcID,
            this.olvcCategory,
            this.olvcState,
            this.olvcOrder,
            this.olvcSize,
            this.olvcLastUpdated,
            this.olvcHasBackup,
            this.olvcHidden,
            this.olvcTags,
            this.olvForWOTC});
            this.modlist_ListObjectListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.modlist_ListObjectListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modlist_ListObjectListView.EmptyListMsg = "No mods found. Check mod folder configuration.";
            this.modlist_ListObjectListView.EmptyListMsgFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modlist_ListObjectListView.FullRowSelect = true;
            this.modlist_ListObjectListView.HideSelection = false;
            this.modlist_ListObjectListView.IsSearchOnSortColumn = false;
            this.modlist_ListObjectListView.Location = new System.Drawing.Point(0, 33);
            this.modlist_ListObjectListView.Name = "modlist_ListObjectListView";
            this.modlist_ListObjectListView.ShowItemCountOnGroups = true;
            this.modlist_ListObjectListView.Size = new System.Drawing.Size(970, 353);
            this.modlist_ListObjectListView.SortGroupItemsByPrimaryColumn = false;
            this.modlist_ListObjectListView.SpaceBetweenGroups = 10;
            this.modlist_ListObjectListView.TabIndex = 0;
            this.modlist_ListObjectListView.TintSortColumn = true;
            this.modlist_ListObjectListView.UseCompatibleStateImageBehavior = false;
            this.modlist_ListObjectListView.UseFilterIndicator = true;
            this.modlist_ListObjectListView.UseFiltering = true;
            this.modlist_ListObjectListView.UseHyperlinks = true;
            this.modlist_ListObjectListView.UseTranslucentSelection = true;
            this.modlist_ListObjectListView.View = System.Windows.Forms.View.Details;
            this.modlist_ListObjectListView.CellEditFinished += new BrightIdeasSoftware.CellEditEventHandler(this.ModListEditFinished);
            this.modlist_ListObjectListView.CellRightClick += new System.EventHandler<BrightIdeasSoftware.CellRightClickEventArgs>(this.ModListCellRightClick);
            this.modlist_ListObjectListView.CellToolTipShowing += new System.EventHandler<BrightIdeasSoftware.ToolTipShowingEventArgs>(this.ModListCellToolTipShowing);
            this.modlist_ListObjectListView.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.ModListFormatRow);
            this.modlist_ListObjectListView.GroupExpandingCollapsing += new System.EventHandler<BrightIdeasSoftware.GroupExpandingCollapsingEventArgs>(this.ModListGroupExpandingCollapsing);
            this.modlist_ListObjectListView.SelectionChanged += new System.EventHandler(this.ModListSelectionChanged);
            this.modlist_ListObjectListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ModListItemChecked);
            this.modlist_ListObjectListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ModListKeyDown);
            this.modlist_ListObjectListView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ModListKeyUp);
            // 
            // olvcActive
            // 
            this.olvcActive.AspectName = "isActive";
            this.olvcActive.Hideable = false;
            this.olvcActive.Text = "";
            this.olvcActive.Width = 20;
            // 
            // olvcName
            // 
            this.olvcName.AspectName = "Name";
            this.olvcName.Hideable = false;
            this.olvcName.Text = "Name";
            this.olvcName.Width = 350;
            // 
            // olvAuthor
            // 
            this.olvAuthor.AspectName = "Author";
            this.olvAuthor.Text = "Author";
            // 
            // olvcID
            // 
            this.olvcID.AspectName = "ID";
            this.olvcID.IsEditable = false;
            this.olvcID.Text = "ID";
            this.olvcID.Width = 200;
            // 
            // olvcCategory
            // 
            this.olvcCategory.AspectName = "Category";
            this.olvcCategory.Text = "Category";
            // 
            // olvcState
            // 
            this.olvcState.IsEditable = false;
            this.olvcState.Text = "State";
            this.olvcState.Width = 40;
            // 
            // olvcOrder
            // 
            this.olvcOrder.AspectName = "Index";
            this.olvcOrder.CellEditUseWholeCell = true;
            this.olvcOrder.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcOrder.MinimumWidth = 40;
            this.olvcOrder.Text = "Order";
            this.olvcOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // olvcSize
            // 
            this.olvcSize.AspectName = "Size";
            this.olvcSize.IsEditable = false;
            this.olvcSize.Searchable = false;
            this.olvcSize.Text = "Size";
            this.olvcSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcSize.Width = 100;
            // 
            // olvcLastUpdated
            // 
            this.olvcLastUpdated.AspectName = "DateUpdated";
            this.olvcLastUpdated.IsEditable = false;
            this.olvcLastUpdated.Searchable = false;
            this.olvcLastUpdated.Text = "Last Update";
            this.olvcLastUpdated.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcLastUpdated.Width = 120;
            // 
            // olvcDateAdded
            // 
            this.olvcDateAdded.AspectName = "DateAdded";
            this.olvcDateAdded.IsEditable = false;
            this.olvcDateAdded.IsVisible = false;
            this.olvcDateAdded.Searchable = false;
            this.olvcDateAdded.Text = "Date Added";
            this.olvcDateAdded.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcDateAdded.Width = 120;
            // 
            // olvcDateCreated
            // 
            this.olvcDateCreated.AspectName = "DateCreated";
            this.olvcDateCreated.IsEditable = false;
            this.olvcDateCreated.IsVisible = false;
            this.olvcDateCreated.Searchable = false;
            this.olvcDateCreated.Text = "Date Created";
            this.olvcDateCreated.Width = 120;
            // 
            // olvcPath
            // 
            this.olvcPath.AspectName = "Path";
            this.olvcPath.IsEditable = false;
            this.olvcPath.IsVisible = false;
            this.olvcPath.Searchable = false;
            this.olvcPath.Text = "Path";
            this.olvcPath.Width = 160;
            // 
            // olvcHasBackup
            // 
            this.olvcHasBackup.AspectName = "HasBackedUpSettings";
            this.olvcHasBackup.Text = "Has Backups";
            // 
            // olvcWorkshopID
            // 
            this.olvcWorkshopID.AspectName = "WorkshopID";
            this.olvcWorkshopID.IsEditable = false;
            this.olvcWorkshopID.IsVisible = false;
            this.olvcWorkshopID.Text = "Workshop ID";
            // 
            // olvcHidden
            // 
            this.olvcHidden.AspectName = "isHidden";
            this.olvcHidden.Text = "Hidden";
            // 
            // olvcTags
            // 
            this.olvcTags.AspectName = "";
            this.olvcTags.AutoCompleteEditor = false;
            this.olvcTags.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcTags.MinimumWidth = 250;
            this.olvcTags.Text = "Tags";
            this.olvcTags.Width = 250;
            // 
            // olvSteamLink
            // 
            this.olvSteamLink.AspectName = "GetSteamLink";
            this.olvSteamLink.Hyperlink = true;
            this.olvSteamLink.IsEditable = false;
            this.olvSteamLink.Searchable = false;
            this.olvSteamLink.Text = "Steam Link";
            this.olvSteamLink.Width = 225;
            // 
            // olvBrowserLink
            // 
            this.olvBrowserLink.AspectName = "BrowserLink";
            this.olvBrowserLink.DisplayIndex = 9;
            this.olvBrowserLink.Hyperlink = true;
            this.olvBrowserLink.IsEditable = false;
            this.olvBrowserLink.IsVisible = false;
            this.olvBrowserLink.Searchable = false;
            this.olvBrowserLink.Text = "Browser Link";
            // 
            // olvForWOTC
            // 
            this.olvForWOTC.AspectName = "BuiltForWOTC";
            this.olvForWOTC.IsEditable = false;
            this.olvForWOTC.Searchable = false;
            this.olvForWOTC.Text = "For WOTC";
            // 
            // pModsLegend
            // 
            this.pModsLegend.Controls.Add(this.label10);
            this.pModsLegend.Controls.Add(this.label9);
            this.pModsLegend.Controls.Add(this.label8);
            this.pModsLegend.Controls.Add(this.label7);
            this.pModsLegend.Controls.Add(this.label4);
            this.pModsLegend.Controls.Add(this.label3);
            this.pModsLegend.Controls.Add(this.label2);
            this.pModsLegend.Controls.Add(this.label1);
            this.pModsLegend.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pModsLegend.Location = new System.Drawing.Point(0, 386);
            this.pModsLegend.Name = "pModsLegend";
            this.pModsLegend.Size = new System.Drawing.Size(970, 28);
            this.pModsLegend.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.LightGreen;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(841, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 20);
            this.label10.TabIndex = 7;
            this.label10.Text = "New";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.label10, resources.GetString("label10.ToolTip"));
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.ForeColor = System.Drawing.Color.Gray;
            this.label9.Location = new System.Drawing.Point(715, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 20);
            this.label9.TabIndex = 6;
            this.label9.Text = "Hidden";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.label9, resources.GetString("label9.ToolTip"));
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Plum;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(589, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 20);
            this.label8.TabIndex = 5;
            this.label8.Text = "Duplicate ID";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.label8, "A pair of mods are flagged as \"Duplicate ID\", when the IDs of those\r\nmods are ide" +
        "ntical, because they are supposed to be unique.");
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.LightCoral;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(463, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 20);
            this.label7.TabIndex = 4;
            this.label7.Text = "Conflicted";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.label7, "A pair of mods are flagged as \"Conflicted\", when the class\r\noverrides are likely " +
        "to cause issues.");
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(211, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Not loaded";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.label4, "A mod is flagged as \"Not loaded\" when the mods installation folder is no longer \r" +
        "\nlocated in one of the configured mod directories (Option->Settings).");
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.LightSalmon;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(337, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Missing dependency";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.label3, "A mod will indicate a \"Missing dependency\", when not all required mods \r\nare enab" +
        "led or installed. Check \"Mod Info -> Dependency Tab\".");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Legend:";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightGray;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(85, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Not installed";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.label1, "A mod is flagged as \"Not installed\" when the mod folder\r\nno longer exists, or whe" +
        "n the XComMod file is missing.");
            // 
            // pModsTop
            // 
            this.pModsTop.Controls.Add(this.LauchOptionsPanel);
            this.pModsTop.Controls.Add(this.panel2);
            this.pModsTop.Controls.Add(this.panel3);
            this.pModsTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pModsTop.Location = new System.Drawing.Point(0, 0);
            this.pModsTop.Name = "pModsTop";
            this.pModsTop.Size = new System.Drawing.Size(970, 33);
            this.pModsTop.TabIndex = 4;
            // 
            // LauchOptionsPanel
            // 
            this.LauchOptionsPanel.Controls.Add(this.modTabToolStrip);
            this.LauchOptionsPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.LauchOptionsPanel.Location = new System.Drawing.Point(800, 0);
            this.LauchOptionsPanel.Name = "LauchOptionsPanel";
            this.LauchOptionsPanel.Size = new System.Drawing.Size(170, 33);
            this.LauchOptionsPanel.TabIndex = 5;
            // 
            // modTabToolStrip
            // 
            this.modTabToolStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.modTabToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.modTabToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.modTabToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quickLaunchToolstripButton});
            this.modTabToolStrip.Location = new System.Drawing.Point(11, 4);
            this.modTabToolStrip.Name = "modTabToolStrip";
            this.modTabToolStrip.Size = new System.Drawing.Size(153, 25);
            this.modTabToolStrip.TabIndex = 4;
            this.modTabToolStrip.Text = "toolStrip1";
            // 
            // quickLaunchToolstripButton
            // 
            this.quickLaunchToolstripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.quickLaunchToolstripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.noSeekFreeLoadingToolStripMenuItem,
            this.autoDebugToolStripMenuItem,
            this.reviewToolStripMenuItem});
            this.quickLaunchToolstripButton.Image = ((System.Drawing.Image)(resources.GetObject("quickLaunchToolstripButton.Image")));
            this.quickLaunchToolstripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.quickLaunchToolstripButton.Name = "quickLaunchToolstripButton";
            this.quickLaunchToolstripButton.Size = new System.Drawing.Size(150, 22);
            this.quickLaunchToolstripButton.Text = "Quick launch arguments";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.CheckOnClick = true;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(182, 22);
            this.toolStripMenuItem2.Text = "-log";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.QuickArgumentItemClick);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.CheckOnClick = true;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(182, 22);
            this.toolStripMenuItem3.Text = "-noRedScreens";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.QuickArgumentItemClick);
            // 
            // noSeekFreeLoadingToolStripMenuItem
            // 
            this.noSeekFreeLoadingToolStripMenuItem.CheckOnClick = true;
            this.noSeekFreeLoadingToolStripMenuItem.Name = "noSeekFreeLoadingToolStripMenuItem";
            this.noSeekFreeLoadingToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.noSeekFreeLoadingToolStripMenuItem.Text = "-noSeekFreeLoading";
            this.noSeekFreeLoadingToolStripMenuItem.Click += new System.EventHandler(this.QuickArgumentItemClick);
            // 
            // autoDebugToolStripMenuItem
            // 
            this.autoDebugToolStripMenuItem.CheckOnClick = true;
            this.autoDebugToolStripMenuItem.Name = "autoDebugToolStripMenuItem";
            this.autoDebugToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.autoDebugToolStripMenuItem.Text = "-autoDebug";
            this.autoDebugToolStripMenuItem.Click += new System.EventHandler(this.QuickArgumentItemClick);
            // 
            // reviewToolStripMenuItem
            // 
            this.reviewToolStripMenuItem.CheckOnClick = true;
            this.reviewToolStripMenuItem.Name = "reviewToolStripMenuItem";
            this.reviewToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.reviewToolStripMenuItem.Text = "-review";
            this.reviewToolStripMenuItem.Click += new System.EventHandler(this.QuickArgumentItemClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cShowLegend);
            this.panel2.Controls.Add(this.cEnableGrouping);
            this.panel2.Controls.Add(this.modlist_toggleGroupsButton);
            this.panel2.Location = new System.Drawing.Point(210, 1);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(360, 30);
            this.panel2.TabIndex = 2;
            // 
            // cShowLegend
            // 
            this.cShowLegend.AutoSize = true;
            this.cShowLegend.Checked = true;
            this.cShowLegend.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cShowLegend.Location = new System.Drawing.Point(253, 7);
            this.cShowLegend.Name = "cShowLegend";
            this.cShowLegend.Size = new System.Drawing.Size(88, 17);
            this.cShowLegend.TabIndex = 2;
            this.cShowLegend.Text = "Show legend";
            this.cShowLegend.UseVisualStyleBackColor = true;
            this.cShowLegend.CheckedChanged += new System.EventHandler(this.cShowLegend_CheckedChanged);
            // 
            // cEnableGrouping
            // 
            this.cEnableGrouping.AutoSize = true;
            this.cEnableGrouping.Checked = true;
            this.cEnableGrouping.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cEnableGrouping.Location = new System.Drawing.Point(144, 7);
            this.cEnableGrouping.Name = "cEnableGrouping";
            this.cEnableGrouping.Size = new System.Drawing.Size(103, 17);
            this.cEnableGrouping.TabIndex = 1;
            this.cEnableGrouping.Text = "Enable grouping";
            this.cEnableGrouping.UseVisualStyleBackColor = true;
            this.cEnableGrouping.CheckedChanged += new System.EventHandler(this.cEnableGrouping_CheckedChanged);
            // 
            // modlist_toggleGroupsButton
            // 
            this.modlist_toggleGroupsButton.Location = new System.Drawing.Point(3, 3);
            this.modlist_toggleGroupsButton.Name = "modlist_toggleGroupsButton";
            this.modlist_toggleGroupsButton.Size = new System.Drawing.Size(132, 23);
            this.modlist_toggleGroupsButton.TabIndex = 0;
            this.modlist_toggleGroupsButton.Text = "Expand/collapse groups";
            this.modlist_toggleGroupsButton.UseVisualStyleBackColor = true;
            this.modlist_toggleGroupsButton.Click += new System.EventHandler(this.modlist_toggleGroupsButton_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.modlist_filterClearButton);
            this.panel3.Controls.Add(this.modlist_FilterCueTextBox);
            this.panel3.Location = new System.Drawing.Point(-3, 1);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(210, 30);
            this.panel3.TabIndex = 3;
            // 
            // modlist_filterClearButton
            // 
            this.modlist_filterClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.modlist_filterClearButton.AutoSize = true;
            this.modlist_filterClearButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.modlist_filterClearButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.modlist_filterClearButton.FlatAppearance.BorderSize = 0;
            this.modlist_filterClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.modlist_filterClearButton.ForeColor = System.Drawing.Color.Black;
            this.modlist_filterClearButton.Image = ((System.Drawing.Image)(resources.GetObject("modlist_filterClearButton.Image")));
            this.modlist_filterClearButton.Location = new System.Drawing.Point(180, 3);
            this.modlist_filterClearButton.Name = "modlist_filterClearButton";
            this.modlist_filterClearButton.Size = new System.Drawing.Size(22, 22);
            this.modlist_filterClearButton.TabIndex = 1;
            this.toolTip.SetToolTip(this.modlist_filterClearButton, "Clear filter");
            this.modlist_filterClearButton.UseVisualStyleBackColor = true;
            this.modlist_filterClearButton.Click += new System.EventHandler(this.modlist_filterClearButton_Click);
            // 
            // modinfo_groupbox
            // 
            this.modinfo_groupbox.Controls.Add(this.tableLayoutPanel3);
            this.modinfo_groupbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modinfo_groupbox.Location = new System.Drawing.Point(0, 0);
            this.modinfo_groupbox.Margin = new System.Windows.Forms.Padding(0);
            this.modinfo_groupbox.Name = "modinfo_groupbox";
            this.modinfo_groupbox.Padding = new System.Windows.Forms.Padding(0);
            this.modinfo_groupbox.Size = new System.Drawing.Size(970, 238);
            this.modinfo_groupbox.TabIndex = 3;
            this.modinfo_groupbox.TabStop = false;
            this.modinfo_groupbox.Text = "Mod Info";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.modinfo_tabcontrol, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.modinfo_image_picturebox, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 13);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(970, 225);
            this.tableLayoutPanel3.TabIndex = 10;
            // 
            // modinfo_tabcontrol
            // 
            this.modinfo_tabcontrol.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modinfo_tabcontrol.Controls.Add(this.modinfo_details_tab);
            this.modinfo_tabcontrol.Controls.Add(this.modinfo_readme_tab);
            this.modinfo_tabcontrol.Controls.Add(this.modinfo_inspect_tab);
            this.modinfo_tabcontrol.Controls.Add(this.modinfo_config_tab);
            this.modinfo_tabcontrol.Controls.Add(this.modinfo_changelog_tab);
            this.modinfo_tabcontrol.Controls.Add(this.modinfo_dependencies_tab);
            this.modinfo_tabcontrol.Location = new System.Drawing.Point(200, 0);
            this.modinfo_tabcontrol.Margin = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.modinfo_tabcontrol.Name = "modinfo_tabcontrol";
            this.modinfo_tabcontrol.SelectedIndex = 0;
            this.modinfo_tabcontrol.Size = new System.Drawing.Size(768, 223);
            this.modinfo_tabcontrol.TabIndex = 9;
            this.modinfo_tabcontrol.Selected += new System.Windows.Forms.TabControlEventHandler(this.ModInfoTabSelected);
            // 
            // modinfo_details_tab
            // 
            this.modinfo_details_tab.Controls.Add(this.btnDescUndo);
            this.modinfo_details_tab.Controls.Add(this.btnDescSave);
            this.modinfo_details_tab.Controls.Add(this.modinfo_info_CreatedLabel);
            this.modinfo_details_tab.Controls.Add(this.modinfo_info_DescriptionLabel);
            this.modinfo_details_tab.Controls.Add(this.modinfo_info_InstalledTextBox);
            this.modinfo_details_tab.Controls.Add(this.modinfo_info_DateCreatedTextBox);
            this.modinfo_details_tab.Controls.Add(this.modinfo_info_InstalledLabel);
            this.modinfo_details_tab.Controls.Add(this.modinfo_info_TitleTextBox);
            this.modinfo_details_tab.Controls.Add(this.modinfo_info_AuthorTextBox);
            this.modinfo_details_tab.Controls.Add(this.modinfo_info_TitleLabel);
            this.modinfo_details_tab.Controls.Add(this.modinfo_info_AuthorLabel);
            this.modinfo_details_tab.Controls.Add(this.modinfo_info_DescriptionRichTextBox);
            this.modinfo_details_tab.Location = new System.Drawing.Point(4, 22);
            this.modinfo_details_tab.Name = "modinfo_details_tab";
            this.modinfo_details_tab.Padding = new System.Windows.Forms.Padding(3);
            this.modinfo_details_tab.Size = new System.Drawing.Size(760, 197);
            this.modinfo_details_tab.TabIndex = 0;
            this.modinfo_details_tab.Text = "Info";
            this.modinfo_details_tab.UseVisualStyleBackColor = true;
            // 
            // btnDescUndo
            // 
            this.btnDescUndo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDescUndo.Location = new System.Drawing.Point(6, 115);
            this.btnDescUndo.Name = "btnDescUndo";
            this.btnDescUndo.Size = new System.Drawing.Size(84, 23);
            this.btnDescUndo.TabIndex = 12;
            this.btnDescUndo.Text = "Undo";
            this.btnDescUndo.UseVisualStyleBackColor = true;
            this.btnDescUndo.Click += new System.EventHandler(this.btnDescUndo_Click);
            // 
            // btnDescSave
            // 
            this.btnDescSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDescSave.Location = new System.Drawing.Point(6, 86);
            this.btnDescSave.Name = "btnDescSave";
            this.btnDescSave.Size = new System.Drawing.Size(84, 23);
            this.btnDescSave.TabIndex = 11;
            this.btnDescSave.Text = "Save";
            this.btnDescSave.UseVisualStyleBackColor = true;
            this.btnDescSave.Click += new System.EventHandler(this.btnDescSave_Click);
            // 
            // modinfo_info_CreatedLabel
            // 
            this.modinfo_info_CreatedLabel.AutoSize = true;
            this.modinfo_info_CreatedLabel.Location = new System.Drawing.Point(6, 35);
            this.modinfo_info_CreatedLabel.Name = "modinfo_info_CreatedLabel";
            this.modinfo_info_CreatedLabel.Size = new System.Drawing.Size(44, 13);
            this.modinfo_info_CreatedLabel.TabIndex = 10;
            this.modinfo_info_CreatedLabel.Text = "Created";
            // 
            // modinfo_info_DescriptionLabel
            // 
            this.modinfo_info_DescriptionLabel.AutoSize = true;
            this.modinfo_info_DescriptionLabel.Location = new System.Drawing.Point(6, 59);
            this.modinfo_info_DescriptionLabel.Name = "modinfo_info_DescriptionLabel";
            this.modinfo_info_DescriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.modinfo_info_DescriptionLabel.TabIndex = 9;
            this.modinfo_info_DescriptionLabel.Text = "Description";
            // 
            // modinfo_info_InstalledTextBox
            // 
            this.modinfo_info_InstalledTextBox.Location = new System.Drawing.Point(431, 32);
            this.modinfo_info_InstalledTextBox.Name = "modinfo_info_InstalledTextBox";
            this.modinfo_info_InstalledTextBox.ReadOnly = true;
            this.modinfo_info_InstalledTextBox.Size = new System.Drawing.Size(224, 20);
            this.modinfo_info_InstalledTextBox.TabIndex = 7;
            // 
            // modinfo_info_DateCreatedTextBox
            // 
            this.modinfo_info_DateCreatedTextBox.Location = new System.Drawing.Point(96, 32);
            this.modinfo_info_DateCreatedTextBox.Name = "modinfo_info_DateCreatedTextBox";
            this.modinfo_info_DateCreatedTextBox.ReadOnly = true;
            this.modinfo_info_DateCreatedTextBox.Size = new System.Drawing.Size(224, 20);
            this.modinfo_info_DateCreatedTextBox.TabIndex = 5;
            // 
            // modinfo_info_InstalledLabel
            // 
            this.modinfo_info_InstalledLabel.AutoSize = true;
            this.modinfo_info_InstalledLabel.Location = new System.Drawing.Point(341, 35);
            this.modinfo_info_InstalledLabel.Name = "modinfo_info_InstalledLabel";
            this.modinfo_info_InstalledLabel.Size = new System.Drawing.Size(46, 13);
            this.modinfo_info_InstalledLabel.TabIndex = 4;
            this.modinfo_info_InstalledLabel.Text = "Installed";
            // 
            // modinfo_info_TitleTextBox
            // 
            this.modinfo_info_TitleTextBox.Location = new System.Drawing.Point(96, 6);
            this.modinfo_info_TitleTextBox.Name = "modinfo_info_TitleTextBox";
            this.modinfo_info_TitleTextBox.ReadOnly = true;
            this.modinfo_info_TitleTextBox.Size = new System.Drawing.Size(224, 20);
            this.modinfo_info_TitleTextBox.TabIndex = 3;
            // 
            // modinfo_info_AuthorTextBox
            // 
            this.modinfo_info_AuthorTextBox.Location = new System.Drawing.Point(431, 9);
            this.modinfo_info_AuthorTextBox.Name = "modinfo_info_AuthorTextBox";
            this.modinfo_info_AuthorTextBox.ReadOnly = true;
            this.modinfo_info_AuthorTextBox.Size = new System.Drawing.Size(224, 20);
            this.modinfo_info_AuthorTextBox.TabIndex = 2;
            // 
            // modinfo_info_TitleLabel
            // 
            this.modinfo_info_TitleLabel.AutoSize = true;
            this.modinfo_info_TitleLabel.Location = new System.Drawing.Point(6, 9);
            this.modinfo_info_TitleLabel.Name = "modinfo_info_TitleLabel";
            this.modinfo_info_TitleLabel.Size = new System.Drawing.Size(27, 13);
            this.modinfo_info_TitleLabel.TabIndex = 1;
            this.modinfo_info_TitleLabel.Text = "Title";
            // 
            // modinfo_info_AuthorLabel
            // 
            this.modinfo_info_AuthorLabel.AutoSize = true;
            this.modinfo_info_AuthorLabel.Location = new System.Drawing.Point(341, 12);
            this.modinfo_info_AuthorLabel.Name = "modinfo_info_AuthorLabel";
            this.modinfo_info_AuthorLabel.Size = new System.Drawing.Size(38, 13);
            this.modinfo_info_AuthorLabel.TabIndex = 0;
            this.modinfo_info_AuthorLabel.Text = "Author";
            // 
            // modinfo_info_DescriptionRichTextBox
            // 
            this.modinfo_info_DescriptionRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modinfo_info_DescriptionRichTextBox.Location = new System.Drawing.Point(96, 58);
            this.modinfo_info_DescriptionRichTextBox.Name = "modinfo_info_DescriptionRichTextBox";
            this.modinfo_info_DescriptionRichTextBox.Size = new System.Drawing.Size(661, 136);
            this.modinfo_info_DescriptionRichTextBox.TabIndex = 8;
            this.modinfo_info_DescriptionRichTextBox.Text = "";
            this.modinfo_info_DescriptionRichTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.ControlLinkClicked);
            this.modinfo_info_DescriptionRichTextBox.TextChanged += new System.EventHandler(this.modinfo_info_DescriptionRichTextBox_TextChanged);
            // 
            // modinfo_readme_tab
            // 
            this.modinfo_readme_tab.Controls.Add(this.modinfo_readme_RichTextBox);
            this.modinfo_readme_tab.Location = new System.Drawing.Point(4, 22);
            this.modinfo_readme_tab.Name = "modinfo_readme_tab";
            this.modinfo_readme_tab.Padding = new System.Windows.Forms.Padding(3);
            this.modinfo_readme_tab.Size = new System.Drawing.Size(760, 197);
            this.modinfo_readme_tab.TabIndex = 1;
            this.modinfo_readme_tab.Text = "ReadMe";
            this.modinfo_readme_tab.UseVisualStyleBackColor = true;
            // 
            // modinfo_readme_RichTextBox
            // 
            this.modinfo_readme_RichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modinfo_readme_RichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.modinfo_readme_RichTextBox.Location = new System.Drawing.Point(3, 3);
            this.modinfo_readme_RichTextBox.Name = "modinfo_readme_RichTextBox";
            this.modinfo_readme_RichTextBox.ReadOnly = true;
            this.modinfo_readme_RichTextBox.Size = new System.Drawing.Size(754, 191);
            this.modinfo_readme_RichTextBox.TabIndex = 0;
            this.modinfo_readme_RichTextBox.Text = "";
            this.modinfo_readme_RichTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.ControlLinkClicked);
            // 
            // modinfo_inspect_tab
            // 
            this.modinfo_inspect_tab.Controls.Add(this.modinfo_inspect_propertygrid);
            this.modinfo_inspect_tab.Location = new System.Drawing.Point(4, 22);
            this.modinfo_inspect_tab.Name = "modinfo_inspect_tab";
            this.modinfo_inspect_tab.Padding = new System.Windows.Forms.Padding(3);
            this.modinfo_inspect_tab.Size = new System.Drawing.Size(760, 197);
            this.modinfo_inspect_tab.TabIndex = 2;
            this.modinfo_inspect_tab.Text = "Inspect";
            this.modinfo_inspect_tab.UseVisualStyleBackColor = true;
            // 
            // modinfo_inspect_propertygrid
            // 
            this.modinfo_inspect_propertygrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modinfo_inspect_propertygrid.HelpVisible = false;
            this.modinfo_inspect_propertygrid.LineColor = System.Drawing.SystemColors.ControlDark;
            this.modinfo_inspect_propertygrid.Location = new System.Drawing.Point(3, 3);
            this.modinfo_inspect_propertygrid.Name = "modinfo_inspect_propertygrid";
            this.modinfo_inspect_propertygrid.Size = new System.Drawing.Size(754, 191);
            this.modinfo_inspect_propertygrid.TabIndex = 9;
            this.modinfo_inspect_propertygrid.Layout += new System.Windows.Forms.LayoutEventHandler(this.modinfo_inspect_propertygrid_Layout);
            // 
            // modinfo_config_tab
            // 
            this.modinfo_config_tab.Controls.Add(this.modinfo_config_TableLayoutPanel);
            this.modinfo_config_tab.Location = new System.Drawing.Point(4, 22);
            this.modinfo_config_tab.Name = "modinfo_config_tab";
            this.modinfo_config_tab.Padding = new System.Windows.Forms.Padding(3);
            this.modinfo_config_tab.Size = new System.Drawing.Size(760, 197);
            this.modinfo_config_tab.TabIndex = 3;
            this.modinfo_config_tab.Text = "Config";
            this.modinfo_config_tab.UseVisualStyleBackColor = true;
            // 
            // modinfo_config_TableLayoutPanel
            // 
            this.modinfo_config_TableLayoutPanel.ColumnCount = 2;
            this.modinfo_config_TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.modinfo_config_TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.modinfo_config_TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.modinfo_config_TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.modinfo_config_TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.modinfo_config_TableLayoutPanel.Controls.Add(this.modinfo_ConfigFCTB, 0, 1);
            this.modinfo_config_TableLayoutPanel.Controls.Add(this.modinfo_config_FileSelectCueComboBox, 0, 0);
            this.modinfo_config_TableLayoutPanel.Controls.Add(this.modinfo_config_buttonsTableLayoutPanel, 1, 0);
            this.modinfo_config_TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modinfo_config_TableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.modinfo_config_TableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.modinfo_config_TableLayoutPanel.Name = "modinfo_config_TableLayoutPanel";
            this.modinfo_config_TableLayoutPanel.RowCount = 2;
            this.modinfo_config_TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.modinfo_config_TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.modinfo_config_TableLayoutPanel.Size = new System.Drawing.Size(754, 191);
            this.modinfo_config_TableLayoutPanel.TabIndex = 13;
            // 
            // modinfo_ConfigFCTB
            // 
            this.modinfo_ConfigFCTB.AllowSeveralTextStyleDrawing = true;
            this.modinfo_ConfigFCTB.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.modinfo_ConfigFCTB.AutoScrollMinSize = new System.Drawing.Size(0, 14);
            this.modinfo_ConfigFCTB.BackBrush = null;
            this.modinfo_ConfigFCTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.modinfo_ConfigFCTB.ChangedLineColor = System.Drawing.SystemColors.Info;
            this.modinfo_ConfigFCTB.CharHeight = 14;
            this.modinfo_ConfigFCTB.CharWidth = 8;
            this.modinfo_config_TableLayoutPanel.SetColumnSpan(this.modinfo_ConfigFCTB, 2);
            this.modinfo_ConfigFCTB.CommentPrefix = ";";
            this.modinfo_ConfigFCTB.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.modinfo_ConfigFCTB.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.modinfo_ConfigFCTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modinfo_ConfigFCTB.IsReplaceMode = false;
            this.modinfo_ConfigFCTB.Location = new System.Drawing.Point(2, 30);
            this.modinfo_ConfigFCTB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.modinfo_ConfigFCTB.Name = "modinfo_ConfigFCTB";
            this.modinfo_ConfigFCTB.Paddings = new System.Windows.Forms.Padding(0);
            this.modinfo_ConfigFCTB.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.modinfo_ConfigFCTB.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("modinfo_ConfigFCTB.ServiceColors")));
            this.modinfo_ConfigFCTB.Size = new System.Drawing.Size(750, 159);
            this.modinfo_ConfigFCTB.TabIndex = 11;
            this.modinfo_ConfigFCTB.WordWrap = true;
            this.modinfo_ConfigFCTB.Zoom = 100;
            this.modinfo_ConfigFCTB.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.modinfo_ConfigFCTB_TextChanged);
            // 
            // modinfo_config_buttonsTableLayoutPanel
            // 
            this.modinfo_config_buttonsTableLayoutPanel.ColumnCount = 5;
            this.modinfo_config_buttonsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.modinfo_config_buttonsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.modinfo_config_buttonsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.modinfo_config_buttonsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.modinfo_config_buttonsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.modinfo_config_buttonsTableLayoutPanel.Controls.Add(this.modinfo_config_ExpandButton, 0, 0);
            this.modinfo_config_buttonsTableLayoutPanel.Controls.Add(this.modinfo_config_CompareButton, 4, 0);
            this.modinfo_config_buttonsTableLayoutPanel.Controls.Add(this.modinfo_config_SaveButton, 1, 0);
            this.modinfo_config_buttonsTableLayoutPanel.Controls.Add(this.modinfo_config_LoadButton, 2, 0);
            this.modinfo_config_buttonsTableLayoutPanel.Controls.Add(this.modinfo_config_RemoveButton, 3, 0);
            this.modinfo_config_buttonsTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.modinfo_config_buttonsTableLayoutPanel.Location = new System.Drawing.Point(353, 0);
            this.modinfo_config_buttonsTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.modinfo_config_buttonsTableLayoutPanel.Name = "modinfo_config_buttonsTableLayoutPanel";
            this.modinfo_config_buttonsTableLayoutPanel.RowCount = 1;
            this.modinfo_config_buttonsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.modinfo_config_buttonsTableLayoutPanel.Size = new System.Drawing.Size(401, 30);
            this.modinfo_config_buttonsTableLayoutPanel.TabIndex = 13;
            // 
            // modinfo_config_ExpandButton
            // 
            this.modinfo_config_ExpandButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.modinfo_config_ExpandButton.Location = new System.Drawing.Point(3, 3);
            this.modinfo_config_ExpandButton.Name = "modinfo_config_ExpandButton";
            this.modinfo_config_ExpandButton.Size = new System.Drawing.Size(74, 23);
            this.modinfo_config_ExpandButton.TabIndex = 13;
            this.modinfo_config_ExpandButton.Text = "Expand";
            this.toolTip.SetToolTip(this.modinfo_config_ExpandButton, "Expand the INI editor to fill the window");
            this.modinfo_config_ExpandButton.UseVisualStyleBackColor = true;
            this.modinfo_config_ExpandButton.Click += new System.EventHandler(this.modinfo_config_ExpandButton_Click);
            // 
            // modinfo_config_CompareButton
            // 
            this.modinfo_config_CompareButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.modinfo_config_CompareButton.Enabled = false;
            this.modinfo_config_CompareButton.Location = new System.Drawing.Point(323, 4);
            this.modinfo_config_CompareButton.Name = "modinfo_config_CompareButton";
            this.modinfo_config_CompareButton.Size = new System.Drawing.Size(74, 21);
            this.modinfo_config_CompareButton.TabIndex = 14;
            this.modinfo_config_CompareButton.Text = "Compare";
            this.toolTip.SetToolTip(this.modinfo_config_CompareButton, "Compare the current file on disk to the backup file");
            this.modinfo_config_CompareButton.UseVisualStyleBackColor = true;
            this.modinfo_config_CompareButton.Click += new System.EventHandler(this.modinfo_config_CompareButton_Click);
            // 
            // modinfo_config_SaveButton
            // 
            this.modinfo_config_SaveButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.modinfo_config_SaveButton.Location = new System.Drawing.Point(83, 4);
            this.modinfo_config_SaveButton.Name = "modinfo_config_SaveButton";
            this.modinfo_config_SaveButton.Size = new System.Drawing.Size(74, 21);
            this.modinfo_config_SaveButton.TabIndex = 3;
            this.modinfo_config_SaveButton.Text = "Save";
            this.toolTip.SetToolTip(this.modinfo_config_SaveButton, "Save current settings to the file on disk and the backup file");
            this.modinfo_config_SaveButton.UseVisualStyleBackColor = true;
            this.modinfo_config_SaveButton.Click += new System.EventHandler(this.modinfo_config_SaveButton_Click);
            // 
            // modinfo_config_LoadButton
            // 
            this.modinfo_config_LoadButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.modinfo_config_LoadButton.Enabled = false;
            this.modinfo_config_LoadButton.Location = new System.Drawing.Point(163, 4);
            this.modinfo_config_LoadButton.Name = "modinfo_config_LoadButton";
            this.modinfo_config_LoadButton.Size = new System.Drawing.Size(74, 21);
            this.modinfo_config_LoadButton.TabIndex = 10;
            this.modinfo_config_LoadButton.Text = "Load";
            this.toolTip.SetToolTip(this.modinfo_config_LoadButton, "Load settings from the backup file");
            this.modinfo_config_LoadButton.UseVisualStyleBackColor = true;
            this.modinfo_config_LoadButton.Click += new System.EventHandler(this.modinfo_config_LoadButton_Click);
            // 
            // modinfo_config_RemoveButton
            // 
            this.modinfo_config_RemoveButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.modinfo_config_RemoveButton.Enabled = false;
            this.modinfo_config_RemoveButton.Location = new System.Drawing.Point(243, 4);
            this.modinfo_config_RemoveButton.Name = "modinfo_config_RemoveButton";
            this.modinfo_config_RemoveButton.Size = new System.Drawing.Size(74, 21);
            this.modinfo_config_RemoveButton.TabIndex = 15;
            this.modinfo_config_RemoveButton.Text = "Remove";
            this.toolTip.SetToolTip(this.modinfo_config_RemoveButton, "Remove settings from the backup file");
            this.modinfo_config_RemoveButton.UseVisualStyleBackColor = true;
            this.modinfo_config_RemoveButton.Click += new System.EventHandler(this.modinfo_config_RemoveButton_Click);
            // 
            // modinfo_changelog_tab
            // 
            this.modinfo_changelog_tab.Controls.Add(this.modinfo_changelog_richtextbox);
            this.modinfo_changelog_tab.Location = new System.Drawing.Point(4, 22);
            this.modinfo_changelog_tab.Name = "modinfo_changelog_tab";
            this.modinfo_changelog_tab.Padding = new System.Windows.Forms.Padding(3);
            this.modinfo_changelog_tab.Size = new System.Drawing.Size(760, 197);
            this.modinfo_changelog_tab.TabIndex = 4;
            this.modinfo_changelog_tab.Text = "Changelog";
            this.modinfo_changelog_tab.UseVisualStyleBackColor = true;
            // 
            // modinfo_changelog_richtextbox
            // 
            this.modinfo_changelog_richtextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.modinfo_changelog_richtextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modinfo_changelog_richtextbox.Location = new System.Drawing.Point(3, 3);
            this.modinfo_changelog_richtextbox.Name = "modinfo_changelog_richtextbox";
            this.modinfo_changelog_richtextbox.ReadOnly = true;
            this.modinfo_changelog_richtextbox.Size = new System.Drawing.Size(754, 191);
            this.modinfo_changelog_richtextbox.TabIndex = 0;
            this.modinfo_changelog_richtextbox.Text = "";
            this.modinfo_changelog_richtextbox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.ControlLinkClicked);
            // 
            // modinfo_dependencies_tab
            // 
            this.modinfo_dependencies_tab.Controls.Add(this.panel5);
            this.modinfo_dependencies_tab.Controls.Add(this.panel4);
            this.modinfo_dependencies_tab.Location = new System.Drawing.Point(4, 22);
            this.modinfo_dependencies_tab.Name = "modinfo_dependencies_tab";
            this.modinfo_dependencies_tab.Padding = new System.Windows.Forms.Padding(3);
            this.modinfo_dependencies_tab.Size = new System.Drawing.Size(760, 197);
            this.modinfo_dependencies_tab.TabIndex = 5;
            this.modinfo_dependencies_tab.Text = "Dependencies";
            this.modinfo_dependencies_tab.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.olvDependentMods);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 98);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(754, 96);
            this.panel5.TabIndex = 3;
            // 
            // olvDependentMods
            // 
            this.olvDependentMods.AllColumns.Add(this.olvColDepModsActive);
            this.olvDependentMods.AllColumns.Add(this.olvColDepModsName);
            this.olvDependentMods.AllColumns.Add(this.olvColDepModsState);
            this.olvDependentMods.AllColumns.Add(this.olvColDepModsHidden);
            this.olvDependentMods.AllColumns.Add(this.olvColDepModsSteamUrl);
            this.olvDependentMods.AllColumns.Add(this.olvColDepModsUrl);
            this.olvDependentMods.AllColumns.Add(this.olvColDepModsWotc);
            this.olvDependentMods.CellEditUseWholeCell = false;
            this.olvDependentMods.CheckBoxes = true;
            this.olvDependentMods.CheckedAspectName = "isActive";
            this.olvDependentMods.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColDepModsActive,
            this.olvColDepModsName,
            this.olvColDepModsState,
            this.olvColDepModsHidden,
            this.olvColDepModsSteamUrl,
            this.olvColDepModsWotc});
            this.olvDependentMods.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvDependentMods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvDependentMods.FullRowSelect = true;
            this.olvDependentMods.HideSelection = false;
            this.olvDependentMods.IsSearchOnSortColumn = false;
            this.olvDependentMods.Location = new System.Drawing.Point(0, 15);
            this.olvDependentMods.Name = "olvDependentMods";
            this.olvDependentMods.ShowGroups = false;
            this.olvDependentMods.ShowItemCountOnGroups = true;
            this.olvDependentMods.Size = new System.Drawing.Size(754, 81);
            this.olvDependentMods.SortGroupItemsByPrimaryColumn = false;
            this.olvDependentMods.TabIndex = 2;
            this.olvDependentMods.TintSortColumn = true;
            this.olvDependentMods.UseCompatibleStateImageBehavior = false;
            this.olvDependentMods.UseFilterIndicator = true;
            this.olvDependentMods.UseFiltering = true;
            this.olvDependentMods.UseHyperlinks = true;
            this.olvDependentMods.UseTranslucentSelection = true;
            this.olvDependentMods.View = System.Windows.Forms.View.Details;
            this.olvDependentMods.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.olvRequiredMods_FormatRow);
            this.olvDependentMods.ItemActivate += new System.EventHandler(this.olvDependencyMods_ItemActivate);
            // 
            // olvColDepModsActive
            // 
            this.olvColDepModsActive.AspectName = "isActive";
            this.olvColDepModsActive.Hideable = false;
            this.olvColDepModsActive.Text = "";
            this.olvColDepModsActive.Width = 20;
            // 
            // olvColDepModsName
            // 
            this.olvColDepModsName.AspectName = "Name";
            this.olvColDepModsName.Hideable = false;
            this.olvColDepModsName.Text = "Name";
            this.olvColDepModsName.Width = 271;
            // 
            // olvColDepModsState
            // 
            this.olvColDepModsState.IsEditable = false;
            this.olvColDepModsState.Text = "State";
            this.olvColDepModsState.Width = 86;
            // 
            // olvColDepModsHidden
            // 
            this.olvColDepModsHidden.AspectName = "isHidden";
            this.olvColDepModsHidden.Text = "Hidden";
            // 
            // olvColDepModsSteamUrl
            // 
            this.olvColDepModsSteamUrl.AspectName = "GetSteamLink";
            this.olvColDepModsSteamUrl.Hyperlink = true;
            this.olvColDepModsSteamUrl.IsEditable = false;
            this.olvColDepModsSteamUrl.Searchable = false;
            this.olvColDepModsSteamUrl.Text = "Steam Link";
            this.olvColDepModsSteamUrl.Width = 227;
            // 
            // olvColDepModsUrl
            // 
            this.olvColDepModsUrl.AspectName = "BrowserLink";
            this.olvColDepModsUrl.DisplayIndex = 5;
            this.olvColDepModsUrl.Hyperlink = true;
            this.olvColDepModsUrl.IsEditable = false;
            this.olvColDepModsUrl.IsVisible = false;
            this.olvColDepModsUrl.Searchable = false;
            this.olvColDepModsUrl.Text = "Browser Link";
            // 
            // olvColDepModsWotc
            // 
            this.olvColDepModsWotc.AspectName = "BuiltForWOTC";
            this.olvColDepModsWotc.IsEditable = false;
            this.olvColDepModsWotc.Searchable = false;
            this.olvColDepModsWotc.Text = "For WOTC";
            this.olvColDepModsWotc.Width = 63;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(754, 15);
            this.label6.TabIndex = 3;
            this.label6.Text = "Dependent mods:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.olvRequiredMods);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(754, 95);
            this.panel4.TabIndex = 1;
            // 
            // olvRequiredMods
            // 
            this.olvRequiredMods.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.olvRequiredMods.AllColumns.Add(this.olvColReqModsActive);
            this.olvRequiredMods.AllColumns.Add(this.olvColReqModsName);
            this.olvRequiredMods.AllColumns.Add(this.olvColReqModsState);
            this.olvRequiredMods.AllColumns.Add(this.olvColReqModsHidden);
            this.olvRequiredMods.AllColumns.Add(this.olvColReqModsSteamUrl);
            this.olvRequiredMods.AllColumns.Add(this.olvColReqModsWotc);
            this.olvRequiredMods.CellEditUseWholeCell = false;
            this.olvRequiredMods.CheckBoxes = true;
            this.olvRequiredMods.CheckedAspectName = "isActive";
            this.olvRequiredMods.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColReqModsActive,
            this.olvColReqModsName,
            this.olvColReqModsState,
            this.olvColReqModsHidden,
            this.olvColReqModsSteamUrl,
            this.olvColReqModsWotc});
            this.olvRequiredMods.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvRequiredMods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvRequiredMods.FullRowSelect = true;
            this.olvRequiredMods.HideSelection = false;
            this.olvRequiredMods.IsSearchOnSortColumn = false;
            this.olvRequiredMods.Location = new System.Drawing.Point(0, 15);
            this.olvRequiredMods.Name = "olvRequiredMods";
            this.olvRequiredMods.ShowGroups = false;
            this.olvRequiredMods.ShowItemCountOnGroups = true;
            this.olvRequiredMods.Size = new System.Drawing.Size(754, 80);
            this.olvRequiredMods.SortGroupItemsByPrimaryColumn = false;
            this.olvRequiredMods.TabIndex = 1;
            this.olvRequiredMods.TintSortColumn = true;
            this.olvRequiredMods.UseCompatibleStateImageBehavior = false;
            this.olvRequiredMods.UseFilterIndicator = true;
            this.olvRequiredMods.UseFiltering = true;
            this.olvRequiredMods.UseHyperlinks = true;
            this.olvRequiredMods.UseTranslucentSelection = true;
            this.olvRequiredMods.View = System.Windows.Forms.View.Details;
            this.olvRequiredMods.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.olvRequiredMods_FormatRow);
            this.olvRequiredMods.ItemActivate += new System.EventHandler(this.olvDependencyMods_ItemActivate);
            // 
            // olvColReqModsActive
            // 
            this.olvColReqModsActive.AspectName = "";
            this.olvColReqModsActive.Hideable = false;
            this.olvColReqModsActive.Text = "";
            this.olvColReqModsActive.Width = 20;
            // 
            // olvColReqModsName
            // 
            this.olvColReqModsName.AspectName = "Name";
            this.olvColReqModsName.Hideable = false;
            this.olvColReqModsName.Text = "Name";
            this.olvColReqModsName.Width = 273;
            // 
            // olvColReqModsState
            // 
            this.olvColReqModsState.IsEditable = false;
            this.olvColReqModsState.Text = "State";
            this.olvColReqModsState.Width = 84;
            // 
            // olvColReqModsHidden
            // 
            this.olvColReqModsHidden.AspectName = "isHidden";
            this.olvColReqModsHidden.Text = "Hidden";
            // 
            // olvColReqModsSteamUrl
            // 
            this.olvColReqModsSteamUrl.AspectName = "GetSteamLink";
            this.olvColReqModsSteamUrl.Hyperlink = true;
            this.olvColReqModsSteamUrl.IsEditable = false;
            this.olvColReqModsSteamUrl.Searchable = false;
            this.olvColReqModsSteamUrl.Text = "Steam Link";
            this.olvColReqModsSteamUrl.Width = 225;
            // 
            // olvColReqModsWotc
            // 
            this.olvColReqModsWotc.AspectName = "BuiltForWOTC";
            this.olvColReqModsWotc.IsEditable = false;
            this.olvColReqModsWotc.Searchable = false;
            this.olvColReqModsWotc.Text = "For WOTC";
            this.olvColReqModsWotc.Width = 63;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(754, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "Required mods:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // modinfo_image_picturebox
            // 
            this.modinfo_image_picturebox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.modinfo_image_picturebox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.modinfo_image_picturebox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.modinfo_image_picturebox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.modinfo_image_picturebox.Location = new System.Drawing.Point(4, 16);
            this.modinfo_image_picturebox.Name = "modinfo_image_picturebox";
            this.modinfo_image_picturebox.Size = new System.Drawing.Size(192, 192);
            this.modinfo_image_picturebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.modinfo_image_picturebox.TabIndex = 8;
            this.modinfo_image_picturebox.TabStop = false;
            // 
            // conflicts_tab
            // 
            this.conflicts_tab.Controls.Add(this.conflicts_tab_tableLayoutPanel);
            this.conflicts_tab.Location = new System.Drawing.Point(4, 23);
            this.conflicts_tab.Name = "conflicts_tab";
            this.conflicts_tab.Padding = new System.Windows.Forms.Padding(3);
            this.conflicts_tab.Size = new System.Drawing.Size(976, 662);
            this.conflicts_tab.TabIndex = 1;
            this.conflicts_tab.Text = "Class Overrides";
            this.conflicts_tab.UseVisualStyleBackColor = true;
            // 
            // conflicts_tab_tableLayoutPanel
            // 
            this.conflicts_tab_tableLayoutPanel.ColumnCount = 2;
            this.conflicts_tab_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.conflicts_tab_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.conflicts_tab_tableLayoutPanel.Controls.Add(this.conflicts_log_label, 0, 0);
            this.conflicts_tab_tableLayoutPanel.Controls.Add(this.conflicts_datagrid, 1, 0);
            this.conflicts_tab_tableLayoutPanel.Controls.Add(this.conflicts_textbox, 0, 1);
            this.conflicts_tab_tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conflicts_tab_tableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.conflicts_tab_tableLayoutPanel.Name = "conflicts_tab_tableLayoutPanel";
            this.conflicts_tab_tableLayoutPanel.RowCount = 2;
            this.conflicts_tab_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.conflicts_tab_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.conflicts_tab_tableLayoutPanel.Size = new System.Drawing.Size(970, 656);
            this.conflicts_tab_tableLayoutPanel.TabIndex = 9;
            // 
            // conflicts_log_label
            // 
            this.conflicts_log_label.AutoSize = true;
            this.conflicts_log_label.Location = new System.Drawing.Point(3, 0);
            this.conflicts_log_label.Name = "conflicts_log_label";
            this.conflicts_log_label.Size = new System.Drawing.Size(28, 13);
            this.conflicts_log_label.TabIndex = 8;
            this.conflicts_log_label.Text = "Log:";
            // 
            // conflicts_datagrid
            // 
            this.conflicts_datagrid.AllowUserToAddRows = false;
            this.conflicts_datagrid.AllowUserToDeleteRows = false;
            this.conflicts_datagrid.AllowUserToOrderColumns = true;
            this.conflicts_datagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.conflicts_datagrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnModName,
            this.ColumnInternalClass,
            this.ColumnModClass});
            this.conflicts_datagrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conflicts_datagrid.Location = new System.Drawing.Point(303, 3);
            this.conflicts_datagrid.Name = "conflicts_datagrid";
            this.conflicts_datagrid.ReadOnly = true;
            this.conflicts_tab_tableLayoutPanel.SetRowSpan(this.conflicts_datagrid, 2);
            this.conflicts_datagrid.Size = new System.Drawing.Size(664, 650);
            this.conflicts_datagrid.TabIndex = 6;
            // 
            // ColumnModName
            // 
            this.ColumnModName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnModName.FillWeight = 30F;
            this.ColumnModName.HeaderText = "Mod";
            this.ColumnModName.Name = "ColumnModName";
            this.ColumnModName.ReadOnly = true;
            // 
            // ColumnInternalClass
            // 
            this.ColumnInternalClass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnInternalClass.FillWeight = 30F;
            this.ColumnInternalClass.HeaderText = "Internal Class";
            this.ColumnInternalClass.Name = "ColumnInternalClass";
            this.ColumnInternalClass.ReadOnly = true;
            // 
            // ColumnModClass
            // 
            this.ColumnModClass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnModClass.FillWeight = 40F;
            this.ColumnModClass.HeaderText = "Mod Class";
            this.ColumnModClass.Name = "ColumnModClass";
            this.ColumnModClass.ReadOnly = true;
            // 
            // conflicts_textbox
            // 
            this.conflicts_textbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conflicts_textbox.Location = new System.Drawing.Point(3, 23);
            this.conflicts_textbox.Multiline = true;
            this.conflicts_textbox.Name = "conflicts_textbox";
            this.conflicts_textbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.conflicts_textbox.Size = new System.Drawing.Size(294, 630);
            this.conflicts_textbox.TabIndex = 7;
            // 
            // export_tab
            // 
            this.export_tab.Controls.Add(this.tableLayoutPanel2);
            this.export_tab.Location = new System.Drawing.Point(4, 23);
            this.export_tab.Name = "export_tab";
            this.export_tab.Padding = new System.Windows.Forms.Padding(3);
            this.export_tab.Size = new System.Drawing.Size(976, 662);
            this.export_tab.TabIndex = 2;
            this.export_tab.Text = "Profiles";
            this.export_tab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.Controls.Add(this.export_richtextbox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.export_load_button, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.export_save_button, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(970, 656);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // export_richtextbox
            // 
            this.export_richtextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.export_richtextbox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.export_richtextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel2.SetColumnSpan(this.export_richtextbox, 3);
            this.export_richtextbox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.export_richtextbox.Location = new System.Drawing.Point(3, 38);
            this.export_richtextbox.Name = "export_richtextbox";
            this.export_richtextbox.ReadOnly = true;
            this.export_richtextbox.Size = new System.Drawing.Size(964, 615);
            this.export_richtextbox.TabIndex = 2;
            this.export_richtextbox.Text = "";
            this.export_richtextbox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.ControlLinkClicked);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.export_all_mods_checkbox);
            this.panel1.Controls.Add(this.export_workshop_link_checkbox);
            this.panel1.Controls.Add(this.export_group_checkbox);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(513, 29);
            this.panel1.TabIndex = 5;
            // 
            // export_all_mods_checkbox
            // 
            this.export_all_mods_checkbox.AutoSize = true;
            this.export_all_mods_checkbox.Location = new System.Drawing.Point(262, 6);
            this.export_all_mods_checkbox.Name = "export_all_mods_checkbox";
            this.export_all_mods_checkbox.Size = new System.Drawing.Size(104, 17);
            this.export_all_mods_checkbox.TabIndex = 3;
            this.export_all_mods_checkbox.Text = "Include All Mods";
            this.export_all_mods_checkbox.UseVisualStyleBackColor = true;
            this.export_all_mods_checkbox.Visible = false;
            this.export_all_mods_checkbox.CheckedChanged += new System.EventHandler(this.ExportCheckboxCheckedChanged);
            // 
            // export_workshop_link_checkbox
            // 
            this.export_workshop_link_checkbox.AutoSize = true;
            this.export_workshop_link_checkbox.Location = new System.Drawing.Point(7, 6);
            this.export_workshop_link_checkbox.Name = "export_workshop_link_checkbox";
            this.export_workshop_link_checkbox.Size = new System.Drawing.Size(136, 17);
            this.export_workshop_link_checkbox.TabIndex = 0;
            this.export_workshop_link_checkbox.Text = "Include Workshop Link";
            this.export_workshop_link_checkbox.UseVisualStyleBackColor = true;
            // 
            // export_group_checkbox
            // 
            this.export_group_checkbox.AutoSize = true;
            this.export_group_checkbox.Checked = true;
            this.export_group_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.export_group_checkbox.Location = new System.Drawing.Point(149, 6);
            this.export_group_checkbox.Name = "export_group_checkbox";
            this.export_group_checkbox.Size = new System.Drawing.Size(107, 17);
            this.export_group_checkbox.TabIndex = 2;
            this.export_group_checkbox.Text = "Include Grouping";
            this.export_group_checkbox.UseVisualStyleBackColor = true;
            // 
            // export_load_button
            // 
            this.export_load_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.export_load_button.Location = new System.Drawing.Point(813, 9);
            this.export_load_button.Name = "export_load_button";
            this.export_load_button.Size = new System.Drawing.Size(74, 23);
            this.export_load_button.TabIndex = 4;
            this.export_load_button.Text = "Load";
            this.export_load_button.UseVisualStyleBackColor = true;
            // 
            // export_save_button
            // 
            this.export_save_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.export_save_button.Location = new System.Drawing.Point(893, 9);
            this.export_save_button.Name = "export_save_button";
            this.export_save_button.Size = new System.Drawing.Size(74, 23);
            this.export_save_button.TabIndex = 3;
            this.export_save_button.Text = "Save";
            this.export_save_button.UseVisualStyleBackColor = true;
            // 
            // tabImageList
            // 
            this.tabImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.tabImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.tabImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // olvcSavedIni
            // 
            this.olvcSavedIni.DisplayIndex = 6;
            this.olvcSavedIni.Text = "Saved INI";
            // 
            // fillPanel
            // 
            this.fillPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fillPanel.Location = new System.Drawing.Point(0, 27);
            this.fillPanel.Name = "fillPanel";
            this.fillPanel.Size = new System.Drawing.Size(984, 689);
            this.fillPanel.TabIndex = 6;
            this.fillPanel.Visible = false;
            // 
            // modlist_FilterCueTextBox
            // 
            this.modlist_FilterCueTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.modlist_FilterCueTextBox.CueText = "Filter Mods";
            this.modlist_FilterCueTextBox.Location = new System.Drawing.Point(3, 5);
            this.modlist_FilterCueTextBox.Name = "modlist_FilterCueTextBox";
            this.modlist_FilterCueTextBox.Size = new System.Drawing.Size(175, 20);
            this.modlist_FilterCueTextBox.TabIndex = 1;
            this.modlist_FilterCueTextBox.TextChanged += new System.EventHandler(this.filterMods_TextChanged);
            // 
            // modinfo_config_FileSelectCueComboBox
            // 
            this.modinfo_config_FileSelectCueComboBox.CueText = "Select INI to edit";
            this.modinfo_config_FileSelectCueComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modinfo_config_FileSelectCueComboBox.FormattingEnabled = true;
            this.modinfo_config_FileSelectCueComboBox.Location = new System.Drawing.Point(3, 3);
            this.modinfo_config_FileSelectCueComboBox.Name = "modinfo_config_FileSelectCueComboBox";
            this.modinfo_config_FileSelectCueComboBox.Size = new System.Drawing.Size(194, 21);
            this.modinfo_config_FileSelectCueComboBox.TabIndex = 12;
            this.toolTip.SetToolTip(this.modinfo_config_FileSelectCueComboBox, "Select an INI file to view or edit");
            this.modinfo_config_FileSelectCueComboBox.DropDown += new System.EventHandler(this.AdjustWidthComboBox_DropDown);
            this.modinfo_config_FileSelectCueComboBox.SelectedIndexChanged += new System.EventHandler(this.modinfo_config_FileSelectCueComboBox_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 741);
            this.Controls.Add(this.main_statusstrip);
            this.Controls.Add(this.main_menustrip);
            this.Controls.Add(this.main_tabcontrol);
            this.Controls.Add(this.fillPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.main_menustrip;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "XCOM 2 Alternative Mod Launcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.main_statusstrip.ResumeLayout(false);
            this.main_statusstrip.PerformLayout();
            this.main_menustrip.ResumeLayout(false);
            this.main_menustrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.error_provider)).EndInit();
            this.main_tabcontrol.ResumeLayout(false);
            this.modlist_tab.ResumeLayout(false);
            this.horizontal_splitcontainer.Panel1.ResumeLayout(false);
            this.horizontal_splitcontainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.horizontal_splitcontainer)).EndInit();
            this.horizontal_splitcontainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.modlist_ListObjectListView)).EndInit();
            this.pModsLegend.ResumeLayout(false);
            this.pModsLegend.PerformLayout();
            this.pModsTop.ResumeLayout(false);
            this.LauchOptionsPanel.ResumeLayout(false);
            this.LauchOptionsPanel.PerformLayout();
            this.modTabToolStrip.ResumeLayout(false);
            this.modTabToolStrip.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.modinfo_groupbox.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.modinfo_tabcontrol.ResumeLayout(false);
            this.modinfo_details_tab.ResumeLayout(false);
            this.modinfo_details_tab.PerformLayout();
            this.modinfo_readme_tab.ResumeLayout(false);
            this.modinfo_inspect_tab.ResumeLayout(false);
            this.modinfo_config_tab.ResumeLayout(false);
            this.modinfo_config_TableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.modinfo_ConfigFCTB)).EndInit();
            this.modinfo_config_buttonsTableLayoutPanel.ResumeLayout(false);
            this.modinfo_changelog_tab.ResumeLayout(false);
            this.modinfo_dependencies_tab.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvDependentMods)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvRequiredMods)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.modinfo_image_picturebox)).EndInit();
            this.conflicts_tab.ResumeLayout(false);
            this.conflicts_tab_tableLayoutPanel.ResumeLayout(false);
            this.conflicts_tab_tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.conflicts_datagrid)).EndInit();
            this.export_tab.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip main_statusstrip;
        private System.Windows.Forms.MenuStrip main_menustrip;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ErrorProvider error_provider;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.TabControl main_tabcontrol;
        private System.Windows.Forms.TabPage modlist_tab;
        private System.Windows.Forms.TabPage conflicts_tab;
        private System.Windows.Forms.Label conflicts_log_label;
        private System.Windows.Forms.TextBox conflicts_textbox;
        private System.Windows.Forms.DataGridView conflicts_datagrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnModName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnInternalClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnModClass;
        private System.Windows.Forms.ToolStripMenuItem importActiveModsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanModsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer horizontal_splitcontainer;
        private System.Windows.Forms.GroupBox modinfo_groupbox;
        private System.Windows.Forms.TabControl modinfo_tabcontrol;
        private System.Windows.Forms.TabPage modinfo_details_tab;
        private System.Windows.Forms.Label modinfo_info_DescriptionLabel;
        private System.Windows.Forms.TextBox modinfo_info_InstalledTextBox;
        private System.Windows.Forms.TextBox modinfo_info_DateCreatedTextBox;
        private System.Windows.Forms.Label modinfo_info_InstalledLabel;
        private System.Windows.Forms.TextBox modinfo_info_TitleTextBox;
        private System.Windows.Forms.TextBox modinfo_info_AuthorTextBox;
        private System.Windows.Forms.Label modinfo_info_TitleLabel;
        private System.Windows.Forms.Label modinfo_info_AuthorLabel;
        private System.Windows.Forms.RichTextBox modinfo_info_DescriptionRichTextBox;
        private System.Windows.Forms.TabPage modinfo_readme_tab;
        private System.Windows.Forms.RichTextBox modinfo_readme_RichTextBox;
        private System.Windows.Forms.TabPage modinfo_inspect_tab;
        private System.Windows.Forms.PropertyGrid modinfo_inspect_propertygrid;
        private System.Windows.Forms.TabPage modinfo_config_tab;
        private System.Windows.Forms.Button modinfo_config_SaveButton;
        private System.Windows.Forms.PictureBox modinfo_image_picturebox;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem searchForModsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem showHiddenModsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel status_toolstrip_label;
        private System.Windows.Forms.ToolStripProgressBar progress_toolstrip_progressbar;
        private System.Windows.Forms.Label modinfo_info_CreatedLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TabPage export_tab;
        private System.Windows.Forms.RichTextBox export_richtextbox;
        private System.Windows.Forms.CheckBox export_workshop_link_checkbox;
        private System.Windows.Forms.CheckBox export_group_checkbox;
        private System.Windows.Forms.TabPage modinfo_changelog_tab;
        private System.Windows.Forms.RichTextBox modinfo_changelog_richtextbox;
        private System.Windows.Forms.ToolStripMenuItem updateEntriesToolStripMenuItem;
        private System.Windows.Forms.ImageList tabImageList;
        private System.Windows.Forms.Button export_save_button;
        private System.Windows.Forms.Button export_load_button;
        private System.Windows.Forms.ToolStripMenuItem runXCOM2ToolStripMenuItem;
        private BrightIdeasSoftware.ObjectListView modlist_ListObjectListView;
        private BrightIdeasSoftware.OLVColumn olvcActive;
        private BrightIdeasSoftware.OLVColumn olvcName;
        private BrightIdeasSoftware.OLVColumn olvcID;
        private BrightIdeasSoftware.OLVColumn olvcState;
        private BrightIdeasSoftware.OLVColumn olvcOrder;
        private BrightIdeasSoftware.OLVColumn olvcSize;
        private BrightIdeasSoftware.OLVColumn olvcLastUpdated;
        private BrightIdeasSoftware.OLVColumn olvcDateAdded;
        private BrightIdeasSoftware.OLVColumn olvcDateCreated;
        private BrightIdeasSoftware.OLVColumn olvcPath;
        private UserElements.CueTextBox modlist_FilterCueTextBox;
        private FastColoredTextBoxNS.FastColoredTextBox modinfo_ConfigFCTB;
        private UserElements.CueComboBox modinfo_config_FileSelectCueComboBox;
        private System.Windows.Forms.TableLayoutPanel modinfo_config_TableLayoutPanel;
        private System.Windows.Forms.Button modinfo_config_ExpandButton;
        private System.Windows.Forms.Panel fillPanel;
        private System.Windows.Forms.Button modinfo_config_CompareButton;
        private System.Windows.Forms.Button modinfo_config_LoadButton;
        private System.Windows.Forms.TableLayoutPanel conflicts_tab_tableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button modlist_toggleGroupsButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button modlist_filterClearButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.CheckBox cEnableGrouping;
        private BrightIdeasSoftware.OLVColumn olvcSavedIni;
        private System.Windows.Forms.TableLayoutPanel modinfo_config_buttonsTableLayoutPanel;
        private System.Windows.Forms.Button modinfo_config_RemoveButton;
        private BrightIdeasSoftware.OLVColumn olvcHasBackup;
        private System.Windows.Forms.CheckBox export_all_mods_checkbox;
        private BrightIdeasSoftware.OLVColumn olvcWorkshopID;
        private BrightIdeasSoftware.OLVColumn olvcHidden;
        private BrightIdeasSoftware.OLVColumn olvSteamLink;
        private BrightIdeasSoftware.OLVColumn olvBrowserLink;
        private System.Windows.Forms.ToolStripMenuItem resubscribeToModsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runWarOfTheChosenToolStripMenuItem;
        private BrightIdeasSoftware.OLVColumn olvcCategory;
        private BrightIdeasSoftware.OLVColumn olvcTags;
        private BrightIdeasSoftware.OLVColumn olvForWOTC;
        private System.Windows.Forms.Button btnDescSave;
        private BrightIdeasSoftware.OLVColumn olvAuthor;
        private System.Windows.Forms.ToolStripMenuItem runChallengeModeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem manageCategoriesToolStripMenuItem;
		private System.Windows.Forms.ToolStrip modTabToolStrip;
		private System.Windows.Forms.ToolStripDropDownButton quickLaunchToolstripButton;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem noSeekFreeLoadingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem autoDebugToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem reviewToolStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel labelFillsFreeSpace;
		private System.Windows.Forms.ToolStripStatusLabel appRestartPendingLabel;
		private System.Windows.Forms.Button btnDescUndo;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem openHomepageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openDiscordToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem amlWikiToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importFromXCOM2ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importFromWotCToolStripMenuItem;
        private System.Windows.Forms.TabPage modinfo_dependencies_tab;
		private System.Windows.Forms.Panel pModsLegend;
		private System.Windows.Forms.Panel pModsTop;
		private System.Windows.Forms.Panel LauchOptionsPanel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox cShowLegend;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel panel4;
		private BrightIdeasSoftware.ObjectListView olvRequiredMods;
		private BrightIdeasSoftware.OLVColumn olvColReqModsActive;
		private BrightIdeasSoftware.OLVColumn olvColReqModsName;
		private BrightIdeasSoftware.OLVColumn olvColReqModsState;
		private BrightIdeasSoftware.OLVColumn olvColReqModsHidden;
		private BrightIdeasSoftware.OLVColumn olvColReqModsWotc;
		private BrightIdeasSoftware.ObjectListView olvDependentMods;
		private BrightIdeasSoftware.OLVColumn olvColDepModsActive;
		private BrightIdeasSoftware.OLVColumn olvColDepModsName;
		private BrightIdeasSoftware.OLVColumn olvColDepModsState;
		private BrightIdeasSoftware.OLVColumn olvColDepModsHidden;
		private BrightIdeasSoftware.OLVColumn olvColDepModsSteamUrl;
		private BrightIdeasSoftware.OLVColumn olvColDepModsWotc;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private BrightIdeasSoftware.OLVColumn olvColDepModsUrl;
		private BrightIdeasSoftware.OLVColumn olvColReqModsSteamUrl;
	}
}