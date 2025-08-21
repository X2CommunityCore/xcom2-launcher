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
            this.openSpecialFoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderToAmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderToX2InstallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderToX2DataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderToWotcDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderToChimeraInstallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderToChimeraDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLogFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.amlLogFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.x2LogFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wotcLogFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chimeraLogFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
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
            this.importFromChimeraSquadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.runChimeraSquadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.error_provider = new System.Windows.Forms.ErrorProvider(this.components);
            this.main_tabcontrol = new System.Windows.Forms.TabControl();
            this.modlist_tab = new System.Windows.Forms.TabPage();
            this.horizontal_splitcontainer = new System.Windows.Forms.SplitContainer();
            this.modlist_ListObjectListView = new BrightIdeasSoftware.ObjectListView();
            this.olvcActive = new BrightIdeasSoftware.OLVColumn();
            this.olvcName = new BrightIdeasSoftware.OLVColumn();
            this.olvColNotes = new BrightIdeasSoftware.OLVColumn();
            this.olvAuthor = new BrightIdeasSoftware.OLVColumn();
            this.olvcCategory = new BrightIdeasSoftware.OLVColumn();
            this.olvcID = new BrightIdeasSoftware.OLVColumn();
            this.olvcState = new BrightIdeasSoftware.OLVColumn();
            this.olvcSource = new BrightIdeasSoftware.OLVColumn();
            this.olvcOrder = new BrightIdeasSoftware.OLVColumn();
            this.olvcSize = new BrightIdeasSoftware.OLVColumn();
            this.olvcLastUpdated = new BrightIdeasSoftware.OLVColumn();
            this.olvcDateAdded = new BrightIdeasSoftware.OLVColumn();
            this.olvcDateCreated = new BrightIdeasSoftware.OLVColumn();
            this.olvcPath = new BrightIdeasSoftware.OLVColumn();
            this.olvcHasBackup = new BrightIdeasSoftware.OLVColumn();
            this.olvcWorkshopID = new BrightIdeasSoftware.OLVColumn();
            this.olvcHidden = new BrightIdeasSoftware.OLVColumn();
            this.olvcTags = new BrightIdeasSoftware.OLVColumn();
            this.olvSteamLink = new BrightIdeasSoftware.OLVColumn();
            this.olvBrowserLink = new BrightIdeasSoftware.OLVColumn();
            this.olvForWOTC = new BrightIdeasSoftware.OLVColumn();
            this.pModsLegend = new System.Windows.Forms.Panel();
            this.bClearStateFilter = new System.Windows.Forms.Button();
            this.cFilterMissingDependency = new System.Windows.Forms.CheckBox();
            this.bRefreshStateFilter = new System.Windows.Forms.Button();
            this.cFilterHidden = new System.Windows.Forms.CheckBox();
            this.cFilterNew = new System.Windows.Forms.CheckBox();
            this.cFilterConflicted = new System.Windows.Forms.CheckBox();
            this.cFilterNotInstalled = new System.Windows.Forms.CheckBox();
            this.cFilterNotLoaded = new System.Windows.Forms.CheckBox();
            this.cFilterDuplicate = new System.Windows.Forms.CheckBox();
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
            this.cShowStateFilter = new System.Windows.Forms.CheckBox();
            this.cEnableGrouping = new System.Windows.Forms.CheckBox();
            this.modlist_toggleGroupsButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.modlist_filterClearButton = new System.Windows.Forms.Button();
            this.modlist_FilterCueTextBox = new XCOM2Launcher.UserElements.CueTextBox();
            this.modinfo_groupbox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.modinfo_tabcontrol = new System.Windows.Forms.TabControl();
            this.modinfo_details_tab = new System.Windows.Forms.TabPage();
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
            this.modinfo_notes_tab = new System.Windows.Forms.TabPage();
            this.modInfoNotesText = new System.Windows.Forms.RichTextBox();
            this.modinfo_readme_tab = new System.Windows.Forms.TabPage();
            this.modinfo_readme_RichTextBox = new System.Windows.Forms.RichTextBox();
            this.modinfo_inspect_tab = new System.Windows.Forms.TabPage();
            this.modinfo_inspect_propertygrid = new System.Windows.Forms.PropertyGrid();
            this.modinfo_config_tab = new System.Windows.Forms.TabPage();
            this.modinfo_config_TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.modinfo_ConfigFCTB = new FastColoredTextBoxNS.FastColoredTextBox();
            this.modinfo_config_FileSelectCueComboBox = new XCOM2Launcher.UserElements.CueComboBox();
            this.modinfo_config_buttonsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.modinfo_config_ExpandButton = new System.Windows.Forms.Button();
            this.modinfo_config_CompareButton = new System.Windows.Forms.Button();
            this.modinfo_config_SaveButton = new System.Windows.Forms.Button();
            this.modinfo_config_LoadButton = new System.Windows.Forms.Button();
            this.modinfo_config_RemoveButton = new System.Windows.Forms.Button();
            this.modinfo_changelog_tab = new System.Windows.Forms.TabPage();
            this.modinfo_changelog_richtextbox = new System.Windows.Forms.RichTextBox();
            this.modinfo_dependencies_tab = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.olvRequiredMods = new BrightIdeasSoftware.ObjectListView();
            this.olvColReqModsActive = new BrightIdeasSoftware.OLVColumn();
            this.olvColReqModsName = new BrightIdeasSoftware.OLVColumn();
            this.olvColReqModsState = new BrightIdeasSoftware.OLVColumn();
            this.olvColReqModsHidden = new BrightIdeasSoftware.OLVColumn();
            this.olvColReqModsSteamUrl = new BrightIdeasSoftware.OLVColumn();
            this.olvColReqModsWotc = new BrightIdeasSoftware.OLVColumn();
            this.olvColReqModsIgnore = new BrightIdeasSoftware.OLVColumn();
            this.panel6 = new System.Windows.Forms.Panel();
            this.cShowPrimaryDuplicates = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.olvDependentMods = new BrightIdeasSoftware.ObjectListView();
            this.olvColDepModsActive = new BrightIdeasSoftware.OLVColumn();
            this.olvColDepModsName = new BrightIdeasSoftware.OLVColumn();
            this.olvColDepModsState = new BrightIdeasSoftware.OLVColumn();
            this.olvColDepModsHidden = new BrightIdeasSoftware.OLVColumn();
            this.olvColDepModsSteamUrl = new BrightIdeasSoftware.OLVColumn();
            this.olvColDepModsUrl = new BrightIdeasSoftware.OLVColumn();
            this.olvColDepModsWotc = new BrightIdeasSoftware.OLVColumn();
            this.label6 = new System.Windows.Forms.Label();
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
            this.olvcSavedIni = new BrightIdeasSoftware.OLVColumn();
            this.fillPanel = new System.Windows.Forms.Panel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
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
            this.modinfo_notes_tab.SuspendLayout();
            this.modinfo_readme_tab.SuspendLayout();
            this.modinfo_inspect_tab.SuspendLayout();
            this.modinfo_config_tab.SuspendLayout();
            this.modinfo_config_TableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.modinfo_ConfigFCTB)).BeginInit();
            this.modinfo_config_buttonsTableLayoutPanel.SuspendLayout();
            this.modinfo_changelog_tab.SuspendLayout();
            this.modinfo_dependencies_tab.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvRequiredMods)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvDependentMods)).BeginInit();
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
            this.main_statusstrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.main_statusstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status_toolstrip_label,
            this.progress_toolstrip_progressbar,
            this.labelFillsFreeSpace,
            this.appRestartPendingLabel});
            this.main_statusstrip.Location = new System.Drawing.Point(3, 629);
            this.main_statusstrip.Name = "main_statusstrip";
            this.main_statusstrip.ShowItemToolTips = true;
            this.main_statusstrip.Size = new System.Drawing.Size(1064, 27);
            this.main_statusstrip.TabIndex = 5;
            this.main_statusstrip.Text = "状态条1";
            // 
            // status_toolstrip_label
            // 
            this.status_toolstrip_label.Name = "status_toolstrip_label";
            this.status_toolstrip_label.Size = new System.Drawing.Size(39, 22);
            this.status_toolstrip_label.Text = "就绪";
            // 
            // progress_toolstrip_progressbar
            // 
            this.progress_toolstrip_progressbar.AutoSize = false;
            this.progress_toolstrip_progressbar.Name = "progress_toolstrip_progressbar";
            this.progress_toolstrip_progressbar.Size = new System.Drawing.Size(120, 21);
            this.progress_toolstrip_progressbar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // labelFillsFreeSpace
            // 
            this.labelFillsFreeSpace.Name = "labelFillsFreeSpace";
            this.labelFillsFreeSpace.Size = new System.Drawing.Size(774, 22);
            this.labelFillsFreeSpace.Spring = true;
            // 
            // appRestartPendingLabel
            // 
            this.appRestartPendingLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appRestartPendingLabel.ForeColor = System.Drawing.Color.Red;
            this.appRestartPendingLabel.Name = "appRestartPendingLabel";
            this.appRestartPendingLabel.Size = new System.Drawing.Size(114, 22);
            this.appRestartPendingLabel.Text = "应用重启等待中";
            this.appRestartPendingLabel.ToolTipText = "设置的一些改变不会立即生效,\r\n 需要应用重启";
            // 
            // main_menustrip
            // 
            this.main_menustrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.main_menustrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.runXCOM2ToolStripMenuItem,
            this.runWarOfTheChosenToolStripMenuItem,
            this.runChallengeModeToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.runChimeraSquadToolStripMenuItem});
            this.main_menustrip.Location = new System.Drawing.Point(3, 3);
            this.main_menustrip.Name = "main_menustrip";
            this.main_menustrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.main_menustrip.ShowItemToolTips = true;
            this.main_menustrip.Size = new System.Drawing.Size(1064, 28);
            this.main_menustrip.TabIndex = 6;
            this.main_menustrip.Text = "菜单条1";
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
            this.openSpecialFoldersToolStripMenuItem,
            this.openLogFileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(69, 24);
            this.runToolStripMenuItem.Text = "文件(F)";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.saveToolStripMenuItem.Text = "保存设置";
            this.saveToolStripMenuItem.ToolTipText = "保存当前设置并更新XCOM配置文件 \r\n--与当前激活MODD和MOD文件夹相关";
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.reloadToolStripMenuItem.Text = "重置设置";
            this.reloadToolStripMenuItem.ToolTipText = "恢复到上次手动保存设置或\r\n最近一次游戏启动成功的设置.";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(180, 6);
            // 
            // searchForModsToolStripMenuItem
            // 
            this.searchForModsToolStripMenuItem.Name = "searchForModsToolStripMenuItem";
            this.searchForModsToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.searchForModsToolStripMenuItem.Text = "搜索新MOD";
            this.searchForModsToolStripMenuItem.ToolTipText = "扫描所有已知的MOD目录,寻找新的MOD,并将其添加到MOD列表。\r\n在添加新MOD后又不想重新启动AML时很有用。";
            // 
            // updateEntriesToolStripMenuItem
            // 
            this.updateEntriesToolStripMenuItem.Name = "updateEntriesToolStripMenuItem";
            this.updateEntriesToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.updateEntriesToolStripMenuItem.Text = "更新MOD信息";
            this.updateEntriesToolStripMenuItem.ToolTipText = "通过执行一些验证,更新所有mods的当前状态并\r\n从Steam创意工坊获取最新的MOD信息。\r\n在你每次启动AML时也会执行此动作.";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(180, 6);
            // 
            // openSpecialFoldersToolStripMenuItem
            // 
            this.openSpecialFoldersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.folderToAmlToolStripMenuItem,
            this.folderToX2InstallToolStripMenuItem,
            this.folderToX2DataToolStripMenuItem,
            this.folderToWotcDataToolStripMenuItem,
            this.folderToChimeraInstallToolStripMenuItem,
            this.folderToChimeraDataToolStripMenuItem});
            this.openSpecialFoldersToolStripMenuItem.Name = "openSpecialFoldersToolStripMenuItem";
            this.openSpecialFoldersToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.openSpecialFoldersToolStripMenuItem.Text = "打开特定文件夹";
            // 
            // folderToAmlToolStripMenuItem
            // 
            this.folderToAmlToolStripMenuItem.Name = "folderToAmlToolStripMenuItem";
            this.folderToAmlToolStripMenuItem.Size = new System.Drawing.Size(361, 24);
            this.folderToAmlToolStripMenuItem.Text = "AML";
            // 
            // folderToX2InstallToolStripMenuItem
            // 
            this.folderToX2InstallToolStripMenuItem.Name = "folderToX2InstallToolStripMenuItem";
            this.folderToX2InstallToolStripMenuItem.Size = new System.Drawing.Size(361, 24);
            this.folderToX2InstallToolStripMenuItem.Text = "XCOM 2 - 安装目录";
            // 
            // folderToX2DataToolStripMenuItem
            // 
            this.folderToX2DataToolStripMenuItem.Name = "folderToX2DataToolStripMenuItem";
            this.folderToX2DataToolStripMenuItem.Size = new System.Drawing.Size(361, 24);
            this.folderToX2DataToolStripMenuItem.Text = "XCOM 2 - 数据目录(我的文档)";
            // 
            // folderToWotcDataToolStripMenuItem
            // 
            this.folderToWotcDataToolStripMenuItem.Name = "folderToWotcDataToolStripMenuItem";
            this.folderToWotcDataToolStripMenuItem.Size = new System.Drawing.Size(361, 24);
            this.folderToWotcDataToolStripMenuItem.Text = "XCOM 2 天选者之战 - 数据目录(我的文档)";
            // 
            // folderToChimeraInstallToolStripMenuItem
            // 
            this.folderToChimeraInstallToolStripMenuItem.Name = "folderToChimeraInstallToolStripMenuItem";
            this.folderToChimeraInstallToolStripMenuItem.Size = new System.Drawing.Size(361, 24);
            this.folderToChimeraInstallToolStripMenuItem.Text = "XCOM奇美拉小队 - 安装目录";
            // 
            // folderToChimeraDataToolStripMenuItem
            // 
            this.folderToChimeraDataToolStripMenuItem.Name = "folderToChimeraDataToolStripMenuItem";
            this.folderToChimeraDataToolStripMenuItem.Size = new System.Drawing.Size(361, 24);
            this.folderToChimeraDataToolStripMenuItem.Text = "XCOM奇美拉小队 - 数据目录(我的文档)";
            // 
            // openLogFileToolStripMenuItem
            // 
            this.openLogFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.amlLogFileToolStripMenuItem1,
            this.x2LogFileToolStripMenuItem,
            this.wotcLogFileToolStripMenuItem,
            this.chimeraLogFileToolStripMenuItem});
            this.openLogFileToolStripMenuItem.Name = "openLogFileToolStripMenuItem";
            this.openLogFileToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.openLogFileToolStripMenuItem.Text = "打开日志";
            // 
            // amlLogFileToolStripMenuItem1
            // 
            this.amlLogFileToolStripMenuItem1.Name = "amlLogFileToolStripMenuItem1";
            this.amlLogFileToolStripMenuItem1.Size = new System.Drawing.Size(217, 24);
            this.amlLogFileToolStripMenuItem1.Text = "AML";
            // 
            // x2LogFileToolStripMenuItem
            // 
            this.x2LogFileToolStripMenuItem.Name = "x2LogFileToolStripMenuItem";
            this.x2LogFileToolStripMenuItem.Size = new System.Drawing.Size(217, 24);
            this.x2LogFileToolStripMenuItem.Text = "XCOM 2";
            // 
            // wotcLogFileToolStripMenuItem
            // 
            this.wotcLogFileToolStripMenuItem.Name = "wotcLogFileToolStripMenuItem";
            this.wotcLogFileToolStripMenuItem.Size = new System.Drawing.Size(217, 24);
            this.wotcLogFileToolStripMenuItem.Text = "XCOM 2 天选者之战";
            // 
            // chimeraLogFileToolStripMenuItem
            // 
            this.chimeraLogFileToolStripMenuItem.Name = "chimeraLogFileToolStripMenuItem";
            this.chimeraLogFileToolStripMenuItem.Size = new System.Drawing.Size(217, 24);
            this.chimeraLogFileToolStripMenuItem.Text = "XCOM 奇美拉小队";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.exitToolStripMenuItem.Text = "退出";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHiddenModsToolStripMenuItem,
            this.toolStripSeparator3,
            this.editOptionsToolStripMenuItem,
            this.manageCategoriesToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.settingsToolStripMenuItem.Text = "选项";
            // 
            // showHiddenModsToolStripMenuItem
            // 
            this.showHiddenModsToolStripMenuItem.CheckOnClick = true;
            this.showHiddenModsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.showHiddenModsToolStripMenuItem.Name = "showHiddenModsToolStripMenuItem";
            this.showHiddenModsToolStripMenuItem.Size = new System.Drawing.Size(176, 24);
            this.showHiddenModsToolStripMenuItem.Text = "显示隐藏MOD";
            this.showHiddenModsToolStripMenuItem.ToolTipText = "显示/隐藏所有mods,这些mods目前被设置为\'隐藏\'。";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(173, 6);
            // 
            // editOptionsToolStripMenuItem
            // 
            this.editOptionsToolStripMenuItem.Name = "editOptionsToolStripMenuItem";
            this.editOptionsToolStripMenuItem.Size = new System.Drawing.Size(176, 24);
            this.editOptionsToolStripMenuItem.Text = "设置";
            // 
            // manageCategoriesToolStripMenuItem
            // 
            this.manageCategoriesToolStripMenuItem.Name = "manageCategoriesToolStripMenuItem";
            this.manageCategoriesToolStripMenuItem.Size = new System.Drawing.Size(176, 24);
            this.manageCategoriesToolStripMenuItem.Text = "分类编辑";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importActiveModsToolStripMenuItem,
            this.cleanModsToolStripMenuItem,
            this.resubscribeToModsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.toolsToolStripMenuItem.Text = "工具";
            // 
            // importActiveModsToolStripMenuItem
            // 
            this.importActiveModsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importFromXCOM2ToolStripMenuItem,
            this.importFromWotCToolStripMenuItem,
            this.importFromChimeraSquadToolStripMenuItem});
            this.importActiveModsToolStripMenuItem.Name = "importActiveModsToolStripMenuItem";
            this.importActiveModsToolStripMenuItem.Size = new System.Drawing.Size(206, 24);
            this.importActiveModsToolStripMenuItem.Text = "导入已激活MOD";
            // 
            // importFromXCOM2ToolStripMenuItem
            // 
            this.importFromXCOM2ToolStripMenuItem.Name = "importFromXCOM2ToolStripMenuItem";
            this.importFromXCOM2ToolStripMenuItem.Size = new System.Drawing.Size(209, 24);
            this.importFromXCOM2ToolStripMenuItem.Text = "从XCOM2原版配置";
            this.importFromXCOM2ToolStripMenuItem.ToolTipText = "启用所有MOD列表中的MOD,\r\n这些都已在XCOM2原版游戏配置文件中激活";
            // 
            // importFromWotCToolStripMenuItem
            // 
            this.importFromWotCToolStripMenuItem.Name = "importFromWotCToolStripMenuItem";
            this.importFromWotCToolStripMenuItem.Size = new System.Drawing.Size(209, 24);
            this.importFromWotCToolStripMenuItem.Text = "从天选者配置";
            this.importFromWotCToolStripMenuItem.ToolTipText = "启用所有MOD列表中的MOD,\r\n这些都已在XCOM2天选者-游戏配置文件中激活";
            // 
            // importFromChimeraSquadToolStripMenuItem
            // 
            this.importFromChimeraSquadToolStripMenuItem.Name = "importFromChimeraSquadToolStripMenuItem";
            this.importFromChimeraSquadToolStripMenuItem.Size = new System.Drawing.Size(209, 24);
            this.importFromChimeraSquadToolStripMenuItem.Text = "从奇美拉小队配置";
            this.importFromChimeraSquadToolStripMenuItem.ToolTipText = "启用所有MOD列表中的MOD,\r\n这些都已在XCOM-奇美拉游戏配置文件中激活";
            // 
            // cleanModsToolStripMenuItem
            // 
            this.cleanModsToolStripMenuItem.Name = "cleanModsToolStripMenuItem";
            this.cleanModsToolStripMenuItem.Size = new System.Drawing.Size(206, 24);
            this.cleanModsToolStripMenuItem.Text = "清除MOD...";
            this.cleanModsToolStripMenuItem.ToolTipText = "内有菜单";
            // 
            // resubscribeToModsToolStripMenuItem
            // 
            this.resubscribeToModsToolStripMenuItem.Name = "resubscribeToModsToolStripMenuItem";
            this.resubscribeToModsToolStripMenuItem.Size = new System.Drawing.Size(206, 24);
            this.resubscribeToModsToolStripMenuItem.Text = "重订阅未安装MOD";
            this.resubscribeToModsToolStripMenuItem.ToolTipText = "部分MOD在AML显示,但是实际未安装\r\nAML会重新订阅,以便正确下载";
            // 
            // runXCOM2ToolStripMenuItem
            // 
            this.runXCOM2ToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.runXCOM2ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("runXCOM2ToolStripMenuItem.Image")));
            this.runXCOM2ToolStripMenuItem.Name = "runXCOM2ToolStripMenuItem";
            this.runXCOM2ToolStripMenuItem.Size = new System.Drawing.Size(131, 24);
            this.runXCOM2ToolStripMenuItem.Text = "启动&XCOM 2";
            this.runXCOM2ToolStripMenuItem.ToolTipText = "加载已激活MOD并启动XCOM2原版游戏\r\n";
            // 
            // runWarOfTheChosenToolStripMenuItem
            // 
            this.runWarOfTheChosenToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.runWarOfTheChosenToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("runWarOfTheChosenToolStripMenuItem.Image")));
            this.runWarOfTheChosenToolStripMenuItem.Name = "runWarOfTheChosenToolStripMenuItem";
            this.runWarOfTheChosenToolStripMenuItem.Size = new System.Drawing.Size(202, 24);
            this.runWarOfTheChosenToolStripMenuItem.Text = "启动WOTC(天选者之战)";
            this.runWarOfTheChosenToolStripMenuItem.ToolTipText = "加载已激活MOD并启动XCOM2天选者游戏";
            // 
            // runChallengeModeToolStripMenuItem
            // 
            this.runChallengeModeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("runChallengeModeToolStripMenuItem.Image")));
            this.runChallengeModeToolStripMenuItem.Name = "runChallengeModeToolStripMenuItem";
            this.runChallengeModeToolStripMenuItem.Size = new System.Drawing.Size(177, 24);
            this.runChallengeModeToolStripMenuItem.Text = "启动WOTC挑战模式";
            this.runChallengeModeToolStripMenuItem.ToolTipText = "不加载MOD,启动WOTC(天选者之战)";
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
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.aboutToolStripMenuItem.Text = "关于";
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(202, 24);
            this.infoToolStripMenuItem.Text = "信息";
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(202, 24);
            this.checkForUpdatesToolStripMenuItem.Text = "检查更新";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(199, 6);
            // 
            // openHomepageToolStripMenuItem
            // 
            this.openHomepageToolStripMenuItem.Name = "openHomepageToolStripMenuItem";
            this.openHomepageToolStripMenuItem.Size = new System.Drawing.Size(202, 24);
            this.openHomepageToolStripMenuItem.Text = "AML 项目页面";
            // 
            // amlWikiToolStripMenuItem
            // 
            this.amlWikiToolStripMenuItem.Name = "amlWikiToolStripMenuItem";
            this.amlWikiToolStripMenuItem.Size = new System.Drawing.Size(202, 24);
            this.amlWikiToolStripMenuItem.Text = "AML Wiki(百科)";
            // 
            // openDiscordToolStripMenuItem
            // 
            this.openDiscordToolStripMenuItem.Name = "openDiscordToolStripMenuItem";
            this.openDiscordToolStripMenuItem.Size = new System.Drawing.Size(202, 24);
            this.openDiscordToolStripMenuItem.Text = "AML Discord频道";
            this.openDiscordToolStripMenuItem.ToolTipText = "打开一个通往我们频道的Discord邀请链接 \r\nXCOM2 Modding 服务器.";
            // 
            // runChimeraSquadToolStripMenuItem
            // 
            this.runChimeraSquadToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("runChimeraSquadToolStripMenuItem.Image")));
            this.runChimeraSquadToolStripMenuItem.Name = "runChimeraSquadToolStripMenuItem";
            this.runChimeraSquadToolStripMenuItem.Size = new System.Drawing.Size(193, 24);
            this.runChimeraSquadToolStripMenuItem.Text = "启动XCOM奇美拉小队";
            this.runChimeraSquadToolStripMenuItem.ToolTipText = "加载已激活MOD并启动XCOM奇美拉小队游戏\r\n";
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
            this.main_tabcontrol.Location = new System.Drawing.Point(6, 38);
            this.main_tabcontrol.Name = "main_tabcontrol";
            this.main_tabcontrol.SelectedIndex = 0;
            this.main_tabcontrol.Size = new System.Drawing.Size(1058, 667);
            this.main_tabcontrol.TabIndex = 6;
            this.main_tabcontrol.Selected += new System.Windows.Forms.TabControlEventHandler(this.MainTabSelected);
            // 
            // modlist_tab
            // 
            this.modlist_tab.Controls.Add(this.horizontal_splitcontainer);
            this.modlist_tab.Location = new System.Drawing.Point(4, 25);
            this.modlist_tab.Name = "modlist_tab";
            this.modlist_tab.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.modlist_tab.Size = new System.Drawing.Size(1050, 638);
            this.modlist_tab.TabIndex = 0;
            this.modlist_tab.Text = "模组(0)";
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
            this.horizontal_splitcontainer.Size = new System.Drawing.Size(1044, 632);
            this.horizontal_splitcontainer.SplitterDistance = 348;
            this.horizontal_splitcontainer.SplitterWidth = 5;
            this.horizontal_splitcontainer.TabIndex = 5;
            // 
            // modlist_ListObjectListView
            // 
            this.modlist_ListObjectListView.AllColumns.Add(this.olvcActive);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvcName);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvColNotes);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvAuthor);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvcCategory);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvcID);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvcState);
            this.modlist_ListObjectListView.AllColumns.Add(this.olvcSource);
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
            this.olvcCategory,
            this.olvcID,
            this.olvcState,
            this.olvcSize,
            this.olvcLastUpdated,
            this.olvcDateAdded,
            this.olvcTags,
            this.olvForWOTC});
            this.modlist_ListObjectListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modlist_ListObjectListView.EmptyListMsg = "没有可用的MOD,或者没有MOD与当前过滤器匹配。\n如果AML根本没有检测到任何MOD,则需要检查mod路径配置项。";
            this.modlist_ListObjectListView.EmptyListMsgFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modlist_ListObjectListView.FullRowSelect = true;
            this.modlist_ListObjectListView.HideSelection = false;
            this.modlist_ListObjectListView.IsSearchOnSortColumn = false;
            this.modlist_ListObjectListView.Location = new System.Drawing.Point(0, 33);
            this.modlist_ListObjectListView.Name = "modlist_ListObjectListView";
            this.modlist_ListObjectListView.ShowItemCountOnGroups = true;
            this.modlist_ListObjectListView.Size = new System.Drawing.Size(1044, 275);
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
            this.modlist_ListObjectListView.SelectionChanged += new System.EventHandler(this.ModListSelectionChanged);
            this.modlist_ListObjectListView.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ModListItemCheck);
            this.modlist_ListObjectListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ModListItemChecked);
            this.modlist_ListObjectListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ModListKeyDown);
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
            this.olvcName.Text = "标题";
            this.olvcName.Width = 300;
            // 
            // olvColNotes
            // 
            this.olvColNotes.AspectName = "Note";
            this.olvColNotes.DisplayIndex = 2;
            this.olvColNotes.IsVisible = false;
            this.olvColNotes.Text = "备注";
            this.olvColNotes.Width = 100;
            // 
            // olvAuthor
            // 
            this.olvAuthor.AspectName = "Author";
            this.olvAuthor.CellEditUseWholeCell = true;
            this.olvAuthor.Text = "作者";
            this.olvAuthor.Width = 120;
            this.olvAuthor.WordWrap = true;
            // 
            // olvcCategory
            // 
            this.olvcCategory.AspectName = "Category";
            this.olvcCategory.Text = "分类";
            this.olvcCategory.Width = 80;
            // 
            // olvcID
            // 
            this.olvcID.AspectName = "ID";
            this.olvcID.IsEditable = false;
            this.olvcID.Text = "编码";
            this.olvcID.Width = 120;
            // 
            // olvcState
            // 
            this.olvcState.IsEditable = false;
            this.olvcState.Text = "状态";
            this.olvcState.Width = 80;
            // 
            // olvcSource
            // 
            this.olvcSource.DisplayIndex = 6;
            this.olvcSource.IsVisible = false;
            this.olvcSource.Text = "来源";
            // 
            // olvcOrder
            // 
            this.olvcOrder.AspectName = "Index";
            this.olvcOrder.CellEditUseWholeCell = true;
            this.olvcOrder.DisplayIndex = 6;
            this.olvcOrder.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcOrder.IsVisible = false;
            this.olvcOrder.MinimumWidth = 40;
            this.olvcOrder.Text = "顺序";
            this.olvcOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // olvcSize
            // 
            this.olvcSize.AspectName = "Size";
            this.olvcSize.IsEditable = false;
            this.olvcSize.Searchable = false;
            this.olvcSize.Text = "大小";
            this.olvcSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // olvcLastUpdated
            // 
            this.olvcLastUpdated.AspectName = "DateUpdated";
            this.olvcLastUpdated.IsEditable = false;
            this.olvcLastUpdated.Searchable = false;
            this.olvcLastUpdated.Text = "最后更新时间";
            this.olvcLastUpdated.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcLastUpdated.Width = 120;
            // 
            // olvcDateAdded
            // 
            this.olvcDateAdded.AspectName = "DateAdded";
            this.olvcDateAdded.IsEditable = false;
            this.olvcDateAdded.Searchable = false;
            this.olvcDateAdded.Text = "安装时间";
            this.olvcDateAdded.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcDateAdded.Width = 120;
            // 
            // olvcDateCreated
            // 
            this.olvcDateCreated.AspectName = "DateCreated";
            this.olvcDateCreated.IsEditable = false;
            this.olvcDateCreated.IsVisible = false;
            this.olvcDateCreated.Searchable = false;
            this.olvcDateCreated.Text = "创建时间";
            this.olvcDateCreated.Width = 120;
            // 
            // olvcPath
            // 
            this.olvcPath.AspectName = "Path";
            this.olvcPath.IsEditable = false;
            this.olvcPath.IsVisible = false;
            this.olvcPath.Searchable = false;
            this.olvcPath.Text = "路径";
            this.olvcPath.Width = 160;
            // 
            // olvcHasBackup
            // 
            this.olvcHasBackup.AspectName = "HasBackedUpSettings";
            this.olvcHasBackup.DisplayIndex = 9;
            this.olvcHasBackup.IsVisible = false;
            this.olvcHasBackup.Text = "是否备份";
            // 
            // olvcWorkshopID
            // 
            this.olvcWorkshopID.AspectName = "WorkshopID";
            this.olvcWorkshopID.IsEditable = false;
            this.olvcWorkshopID.IsVisible = false;
            this.olvcWorkshopID.Text = "工坊编码";
            // 
            // olvcHidden
            // 
            this.olvcHidden.AspectName = "isHidden";
            this.olvcHidden.DisplayIndex = 10;
            this.olvcHidden.IsVisible = false;
            this.olvcHidden.Text = "隐藏?";
            // 
            // olvcTags
            // 
            this.olvcTags.AspectName = "";
            this.olvcTags.AutoCompleteEditor = false;
            this.olvcTags.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcTags.MinimumWidth = 250;
            this.olvcTags.Text = "标签";
            this.olvcTags.Width = 250;
            // 
            // olvSteamLink
            // 
            this.olvSteamLink.AspectName = "GetSteamLink";
            this.olvSteamLink.DisplayIndex = 12;
            this.olvSteamLink.Hyperlink = true;
            this.olvSteamLink.IsEditable = false;
            this.olvSteamLink.IsVisible = false;
            this.olvSteamLink.Searchable = false;
            this.olvSteamLink.Text = "工坊链接";
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
            this.olvBrowserLink.Text = "浏览地址";
            // 
            // olvForWOTC
            // 
            this.olvForWOTC.AspectName = "BuiltForWOTC";
            this.olvForWOTC.IsEditable = false;
            this.olvForWOTC.Searchable = false;
            this.olvForWOTC.Text = "是否兼容WOTC";
            // 
            // pModsLegend
            // 
            this.pModsLegend.Controls.Add(this.bClearStateFilter);
            this.pModsLegend.Controls.Add(this.cFilterMissingDependency);
            this.pModsLegend.Controls.Add(this.bRefreshStateFilter);
            this.pModsLegend.Controls.Add(this.cFilterHidden);
            this.pModsLegend.Controls.Add(this.cFilterNew);
            this.pModsLegend.Controls.Add(this.cFilterConflicted);
            this.pModsLegend.Controls.Add(this.cFilterNotInstalled);
            this.pModsLegend.Controls.Add(this.cFilterNotLoaded);
            this.pModsLegend.Controls.Add(this.cFilterDuplicate);
            this.pModsLegend.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pModsLegend.Location = new System.Drawing.Point(0, 308);
            this.pModsLegend.Name = "pModsLegend";
            this.pModsLegend.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.pModsLegend.Size = new System.Drawing.Size(1044, 40);
            this.pModsLegend.TabIndex = 5;
            // 
            // bClearStateFilter
            // 
            this.bClearStateFilter.Location = new System.Drawing.Point(926, 3);
            this.bClearStateFilter.Name = "bClearStateFilter";
            this.bClearStateFilter.Size = new System.Drawing.Size(80, 28);
            this.bClearStateFilter.TabIndex = 16;
            this.bClearStateFilter.Text = "清除";
            this.toolTip.SetToolTip(this.bClearStateFilter, "清除所有已启用筛选条.");
            this.bClearStateFilter.UseVisualStyleBackColor = true;
            this.bClearStateFilter.Click += new System.EventHandler(this.bClearStateFilter_Click);
            // 
            // cFilterMissingDependency
            // 
            this.cFilterMissingDependency.Appearance = System.Windows.Forms.Appearance.Button;
            this.cFilterMissingDependency.BackColor = System.Drawing.Color.LightSalmon;
            this.cFilterMissingDependency.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.cFilterMissingDependency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cFilterMissingDependency.Font = new System.Drawing.Font("微软雅黑", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cFilterMissingDependency.Location = new System.Drawing.Point(239, 3);
            this.cFilterMissingDependency.MinimumSize = new System.Drawing.Size(0, 30);
            this.cFilterMissingDependency.Name = "cFilterMissingDependency";
            this.cFilterMissingDependency.Size = new System.Drawing.Size(173, 30);
            this.cFilterMissingDependency.TabIndex = 15;
            this.cFilterMissingDependency.Text = "缺少依赖";
            this.cFilterMissingDependency.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.cFilterMissingDependency, "当有1个mod的前置mod没有加载或者安装,将显示在\"缺少依赖\"筛选条下. \r\n检查下侧 \"Mod信息 -> 依赖关系 选项卡\"以获取更多信息.");
            this.cFilterMissingDependency.UseVisualStyleBackColor = false;
            this.cFilterMissingDependency.CheckedChanged += new System.EventHandler(this.cStateFilter_CheckedChanged);
            // 
            // bRefreshStateFilter
            // 
            this.bRefreshStateFilter.BackColor = System.Drawing.Color.Transparent;
            this.bRefreshStateFilter.Location = new System.Drawing.Point(840, 3);
            this.bRefreshStateFilter.Name = "bRefreshStateFilter";
            this.bRefreshStateFilter.Size = new System.Drawing.Size(80, 28);
            this.bRefreshStateFilter.TabIndex = 14;
            this.bRefreshStateFilter.Text = "刷新";
            this.toolTip.SetToolTip(this.bRefreshStateFilter, "使用选定的过滤器手动刷新Mod视图.");
            this.bRefreshStateFilter.UseVisualStyleBackColor = false;
            this.bRefreshStateFilter.Click += new System.EventHandler(this.bRefreshStateFilter_Click);
            // 
            // cFilterHidden
            // 
            this.cFilterHidden.Appearance = System.Windows.Forms.Appearance.Button;
            this.cFilterHidden.BackColor = System.Drawing.Color.White;
            this.cFilterHidden.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.cFilterHidden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cFilterHidden.Font = new System.Drawing.Font("微软雅黑", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cFilterHidden.ForeColor = System.Drawing.Color.Gray;
            this.cFilterHidden.Location = new System.Drawing.Point(641, 3);
            this.cFilterHidden.MinimumSize = new System.Drawing.Size(0, 30);
            this.cFilterHidden.Name = "cFilterHidden";
            this.cFilterHidden.Size = new System.Drawing.Size(94, 30);
            this.cFilterHidden.TabIndex = 13;
            this.cFilterHidden.Text = "隐藏 (00)";
            this.cFilterHidden.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.cFilterHidden, "如果某个Mod不再可用（“未安装”或“未加载”），它将被自动隐藏。\r\n可以使用“ mod列表”上下文菜单手动（取消隐藏）mod。\r\n可以通过激活“选项->显示隐藏" +
        "MOD”来显示隐藏的模组。");
            this.cFilterHidden.UseVisualStyleBackColor = false;
            this.cFilterHidden.CheckedChanged += new System.EventHandler(this.cStateFilter_CheckedChanged);
            // 
            // cFilterNew
            // 
            this.cFilterNew.Appearance = System.Windows.Forms.Appearance.Button;
            this.cFilterNew.BackColor = System.Drawing.Color.LightGreen;
            this.cFilterNew.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.cFilterNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cFilterNew.Font = new System.Drawing.Font("微软雅黑", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cFilterNew.Location = new System.Drawing.Point(741, 3);
            this.cFilterNew.MinimumSize = new System.Drawing.Size(0, 30);
            this.cFilterNew.Name = "cFilterNew";
            this.cFilterNew.Size = new System.Drawing.Size(86, 30);
            this.cFilterNew.TabIndex = 12;
            this.cFilterNew.Text = "新 (000)";
            this.cFilterNew.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.cFilterNew, resources.GetString("cFilterNew.ToolTip"));
            this.cFilterNew.UseVisualStyleBackColor = false;
            this.cFilterNew.CheckedChanged += new System.EventHandler(this.cStateFilter_CheckedChanged);
            // 
            // cFilterConflicted
            // 
            this.cFilterConflicted.Appearance = System.Windows.Forms.Appearance.Button;
            this.cFilterConflicted.BackColor = System.Drawing.Color.LightCoral;
            this.cFilterConflicted.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.cFilterConflicted.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cFilterConflicted.Font = new System.Drawing.Font("微软雅黑", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cFilterConflicted.Location = new System.Drawing.Point(418, 3);
            this.cFilterConflicted.MinimumSize = new System.Drawing.Size(0, 30);
            this.cFilterConflicted.Name = "cFilterConflicted";
            this.cFilterConflicted.Size = new System.Drawing.Size(106, 30);
            this.cFilterConflicted.TabIndex = 11;
            this.cFilterConflicted.Text = "冲突 (00)";
            this.cFilterConflicted.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.cFilterConflicted, "一些mod被标记为 \"冲突\", 说明它们的class 互相覆盖,可能会导致运行错误");
            this.cFilterConflicted.UseVisualStyleBackColor = false;
            this.cFilterConflicted.CheckedChanged += new System.EventHandler(this.cStateFilter_CheckedChanged);
            // 
            // cFilterNotInstalled
            // 
            this.cFilterNotInstalled.Appearance = System.Windows.Forms.Appearance.Button;
            this.cFilterNotInstalled.AutoEllipsis = true;
            this.cFilterNotInstalled.BackColor = System.Drawing.Color.LightGray;
            this.cFilterNotInstalled.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.cFilterNotInstalled.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cFilterNotInstalled.Font = new System.Drawing.Font("微软雅黑", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cFilterNotInstalled.Location = new System.Drawing.Point(5, 3);
            this.cFilterNotInstalled.MinimumSize = new System.Drawing.Size(110, 30);
            this.cFilterNotInstalled.Name = "cFilterNotInstalled";
            this.cFilterNotInstalled.Size = new System.Drawing.Size(115, 30);
            this.cFilterNotInstalled.TabIndex = 10;
            this.cFilterNotInstalled.Text = "未安装 (00)";
            this.cFilterNotInstalled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.cFilterNotInstalled, "一个mod如果被标记为\"未安装\" 说明mod文件夹不存在 \r\n或者文件内相应的XComMod文件不存在");
            this.cFilterNotInstalled.UseVisualStyleBackColor = false;
            this.cFilterNotInstalled.CheckedChanged += new System.EventHandler(this.cStateFilter_CheckedChanged);
            // 
            // cFilterNotLoaded
            // 
            this.cFilterNotLoaded.Appearance = System.Windows.Forms.Appearance.Button;
            this.cFilterNotLoaded.BackColor = System.Drawing.Color.LightSteelBlue;
            this.cFilterNotLoaded.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.cFilterNotLoaded.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cFilterNotLoaded.Font = new System.Drawing.Font("微软雅黑", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cFilterNotLoaded.Location = new System.Drawing.Point(126, 3);
            this.cFilterNotLoaded.MinimumSize = new System.Drawing.Size(0, 30);
            this.cFilterNotLoaded.Name = "cFilterNotLoaded";
            this.cFilterNotLoaded.Size = new System.Drawing.Size(107, 30);
            this.cFilterNotLoaded.TabIndex = 9;
            this.cFilterNotLoaded.Text = "未加载 (00)";
            this.cFilterNotLoaded.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.cFilterNotLoaded, "当mods安装文件夹不再存在于已配置的mod\r\n目录(选项-> 设置)中,mod将标记为“未加载”");
            this.cFilterNotLoaded.UseVisualStyleBackColor = false;
            this.cFilterNotLoaded.CheckedChanged += new System.EventHandler(this.cStateFilter_CheckedChanged);
            // 
            // cFilterDuplicate
            // 
            this.cFilterDuplicate.Appearance = System.Windows.Forms.Appearance.Button;
            this.cFilterDuplicate.BackColor = System.Drawing.Color.Plum;
            this.cFilterDuplicate.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.cFilterDuplicate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cFilterDuplicate.Font = new System.Drawing.Font("微软雅黑", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cFilterDuplicate.Location = new System.Drawing.Point(530, 3);
            this.cFilterDuplicate.MinimumSize = new System.Drawing.Size(0, 30);
            this.cFilterDuplicate.Name = "cFilterDuplicate";
            this.cFilterDuplicate.Size = new System.Drawing.Size(105, 30);
            this.cFilterDuplicate.TabIndex = 8;
            this.cFilterDuplicate.Text = "重复 (00)";
            this.cFilterDuplicate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.cFilterDuplicate, "当mod的ID是相同的,mod将被标记为“重复”，因为它们应该是唯一的 ");
            this.cFilterDuplicate.UseVisualStyleBackColor = false;
            this.cFilterDuplicate.CheckedChanged += new System.EventHandler(this.cStateFilter_CheckedChanged);
            // 
            // pModsTop
            // 
            this.pModsTop.Controls.Add(this.LauchOptionsPanel);
            this.pModsTop.Controls.Add(this.panel2);
            this.pModsTop.Controls.Add(this.panel3);
            this.pModsTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pModsTop.Location = new System.Drawing.Point(0, 0);
            this.pModsTop.Name = "pModsTop";
            this.pModsTop.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.pModsTop.Size = new System.Drawing.Size(1044, 33);
            this.pModsTop.TabIndex = 4;
            // 
            // LauchOptionsPanel
            // 
            this.LauchOptionsPanel.Controls.Add(this.modTabToolStrip);
            this.LauchOptionsPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.LauchOptionsPanel.Location = new System.Drawing.Point(871, 3);
            this.LauchOptionsPanel.Name = "LauchOptionsPanel";
            this.LauchOptionsPanel.Size = new System.Drawing.Size(170, 27);
            this.LauchOptionsPanel.TabIndex = 5;
            // 
            // modTabToolStrip
            // 
            this.modTabToolStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.modTabToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.modTabToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.modTabToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.modTabToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quickLaunchToolstripButton});
            this.modTabToolStrip.Location = new System.Drawing.Point(48, 3);
            this.modTabToolStrip.Name = "modTabToolStrip";
            this.modTabToolStrip.Size = new System.Drawing.Size(115, 27);
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
            this.quickLaunchToolstripButton.Size = new System.Drawing.Size(112, 24);
            this.quickLaunchToolstripButton.Text = "快速启动参数";
            this.quickLaunchToolstripButton.ToolTipText = "你可以在设置页面自定义启动参数\r\n";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.CheckOnClick = true;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(229, 24);
            this.toolStripMenuItem2.Text = "-log";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.QuickArgumentItemClick);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.CheckOnClick = true;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(229, 24);
            this.toolStripMenuItem3.Text = "-noRedScreens";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.QuickArgumentItemClick);
            // 
            // noSeekFreeLoadingToolStripMenuItem
            // 
            this.noSeekFreeLoadingToolStripMenuItem.CheckOnClick = true;
            this.noSeekFreeLoadingToolStripMenuItem.Name = "noSeekFreeLoadingToolStripMenuItem";
            this.noSeekFreeLoadingToolStripMenuItem.Size = new System.Drawing.Size(229, 24);
            this.noSeekFreeLoadingToolStripMenuItem.Text = "-noSeekFreeLoading";
            this.noSeekFreeLoadingToolStripMenuItem.Click += new System.EventHandler(this.QuickArgumentItemClick);
            // 
            // autoDebugToolStripMenuItem
            // 
            this.autoDebugToolStripMenuItem.CheckOnClick = true;
            this.autoDebugToolStripMenuItem.Name = "autoDebugToolStripMenuItem";
            this.autoDebugToolStripMenuItem.Size = new System.Drawing.Size(229, 24);
            this.autoDebugToolStripMenuItem.Text = "-autoDebug";
            this.autoDebugToolStripMenuItem.Click += new System.EventHandler(this.QuickArgumentItemClick);
            // 
            // reviewToolStripMenuItem
            // 
            this.reviewToolStripMenuItem.CheckOnClick = true;
            this.reviewToolStripMenuItem.Name = "reviewToolStripMenuItem";
            this.reviewToolStripMenuItem.Size = new System.Drawing.Size(229, 24);
            this.reviewToolStripMenuItem.Text = "-review";
            this.reviewToolStripMenuItem.Click += new System.EventHandler(this.QuickArgumentItemClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cShowStateFilter);
            this.panel2.Controls.Add(this.cEnableGrouping);
            this.panel2.Controls.Add(this.modlist_toggleGroupsButton);
            this.panel2.Location = new System.Drawing.Point(210, 1);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(420, 30);
            this.panel2.TabIndex = 2;
            // 
            // cShowStateFilter
            // 
            this.cShowStateFilter.AutoSize = true;
            this.cShowStateFilter.Checked = true;
            this.cShowStateFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cShowStateFilter.Location = new System.Drawing.Point(253, 7);
            this.cShowStateFilter.Name = "cShowStateFilter";
            this.cShowStateFilter.Size = new System.Drawing.Size(134, 19);
            this.cShowStateFilter.TabIndex = 2;
            this.cShowStateFilter.Text = "显示状态筛选条";
            this.toolTip.SetToolTip(this.cShowStateFilter, "显示/隐藏 MOD列表下侧的快速状态筛选条");
            this.cShowStateFilter.UseVisualStyleBackColor = true;
            this.cShowStateFilter.CheckedChanged += new System.EventHandler(this.cShowLegend_CheckedChanged);
            // 
            // cEnableGrouping
            // 
            this.cEnableGrouping.AutoSize = true;
            this.cEnableGrouping.Checked = true;
            this.cEnableGrouping.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cEnableGrouping.Location = new System.Drawing.Point(144, 7);
            this.cEnableGrouping.Name = "cEnableGrouping";
            this.cEnableGrouping.Size = new System.Drawing.Size(89, 19);
            this.cEnableGrouping.TabIndex = 1;
            this.cEnableGrouping.Text = "启用分类";
            this.cEnableGrouping.UseVisualStyleBackColor = true;
            this.cEnableGrouping.CheckedChanged += new System.EventHandler(this.cEnableGrouping_CheckedChanged);
            // 
            // modlist_toggleGroupsButton
            // 
            this.modlist_toggleGroupsButton.Location = new System.Drawing.Point(3, 3);
            this.modlist_toggleGroupsButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.modlist_toggleGroupsButton.Name = "modlist_toggleGroupsButton";
            this.modlist_toggleGroupsButton.Size = new System.Drawing.Size(132, 25);
            this.modlist_toggleGroupsButton.TabIndex = 0;
            this.modlist_toggleGroupsButton.Text = "展开/收缩 分类";
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
            this.modlist_filterClearButton.Location = new System.Drawing.Point(248, 3);
            this.modlist_filterClearButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modlist_filterClearButton.Name = "modlist_filterClearButton";
            this.modlist_filterClearButton.Size = new System.Drawing.Size(22, 22);
            this.modlist_filterClearButton.TabIndex = 1;
            this.toolTip.SetToolTip(this.modlist_filterClearButton, "清除筛选");
            this.modlist_filterClearButton.UseVisualStyleBackColor = true;
            this.modlist_filterClearButton.Click += new System.EventHandler(this.modlist_filterClearButton_Click);
            // 
            // modlist_FilterCueTextBox
            // 
            this.modlist_FilterCueTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.modlist_FilterCueTextBox.CueText = "筛选Mod";
            this.modlist_FilterCueTextBox.Location = new System.Drawing.Point(3, 5);
            this.modlist_FilterCueTextBox.Name = "modlist_FilterCueTextBox";
            this.modlist_FilterCueTextBox.Size = new System.Drawing.Size(175, 25);
            this.modlist_FilterCueTextBox.TabIndex = 1;
            this.modlist_FilterCueTextBox.TextChanged += new System.EventHandler(this.filterMods_TextChanged);
            // 
            // modinfo_groupbox
            // 
            this.modinfo_groupbox.Controls.Add(this.tableLayoutPanel3);
            this.modinfo_groupbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modinfo_groupbox.Location = new System.Drawing.Point(0, 0);
            this.modinfo_groupbox.Name = "modinfo_groupbox";
            this.modinfo_groupbox.Padding = new System.Windows.Forms.Padding(0);
            this.modinfo_groupbox.Size = new System.Drawing.Size(1044, 279);
            this.modinfo_groupbox.TabIndex = 3;
            this.modinfo_groupbox.TabStop = false;
            this.modinfo_groupbox.Text = "Mod信息";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.modinfo_tabcontrol, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.modinfo_image_picturebox, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 18);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1044, 261);
            this.tableLayoutPanel3.TabIndex = 10;
            // 
            // modinfo_tabcontrol
            // 
            this.modinfo_tabcontrol.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modinfo_tabcontrol.Controls.Add(this.modinfo_details_tab);
            this.modinfo_tabcontrol.Controls.Add(this.modinfo_notes_tab);
            this.modinfo_tabcontrol.Controls.Add(this.modinfo_readme_tab);
            this.modinfo_tabcontrol.Controls.Add(this.modinfo_inspect_tab);
            this.modinfo_tabcontrol.Controls.Add(this.modinfo_config_tab);
            this.modinfo_tabcontrol.Controls.Add(this.modinfo_changelog_tab);
            this.modinfo_tabcontrol.Controls.Add(this.modinfo_dependencies_tab);
            this.modinfo_tabcontrol.Location = new System.Drawing.Point(267, 0);
            this.modinfo_tabcontrol.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.modinfo_tabcontrol.Name = "modinfo_tabcontrol";
            this.modinfo_tabcontrol.SelectedIndex = 0;
            this.modinfo_tabcontrol.Size = new System.Drawing.Size(774, 259);
            this.modinfo_tabcontrol.TabIndex = 9;
            this.modinfo_tabcontrol.Selected += new System.Windows.Forms.TabControlEventHandler(this.ModInfoTabSelected);
            // 
            // modinfo_details_tab
            // 
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
            this.modinfo_details_tab.Location = new System.Drawing.Point(4, 25);
            this.modinfo_details_tab.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_details_tab.Name = "modinfo_details_tab";
            this.modinfo_details_tab.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_details_tab.Size = new System.Drawing.Size(766, 230);
            this.modinfo_details_tab.TabIndex = 0;
            this.modinfo_details_tab.Text = "信息";
            this.modinfo_details_tab.UseVisualStyleBackColor = true;
            // 
            // modinfo_info_CreatedLabel
            // 
            this.modinfo_info_CreatedLabel.AutoSize = true;
            this.modinfo_info_CreatedLabel.Location = new System.Drawing.Point(8, 40);
            this.modinfo_info_CreatedLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.modinfo_info_CreatedLabel.Name = "modinfo_info_CreatedLabel";
            this.modinfo_info_CreatedLabel.Size = new System.Drawing.Size(67, 15);
            this.modinfo_info_CreatedLabel.TabIndex = 10;
            this.modinfo_info_CreatedLabel.Text = "创建时间";
            // 
            // modinfo_info_DescriptionLabel
            // 
            this.modinfo_info_DescriptionLabel.AutoSize = true;
            this.modinfo_info_DescriptionLabel.Location = new System.Drawing.Point(8, 68);
            this.modinfo_info_DescriptionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.modinfo_info_DescriptionLabel.Name = "modinfo_info_DescriptionLabel";
            this.modinfo_info_DescriptionLabel.Size = new System.Drawing.Size(37, 15);
            this.modinfo_info_DescriptionLabel.TabIndex = 9;
            this.modinfo_info_DescriptionLabel.Text = "描述";
            // 
            // modinfo_info_InstalledTextBox
            // 
            this.modinfo_info_InstalledTextBox.Location = new System.Drawing.Point(575, 37);
            this.modinfo_info_InstalledTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_info_InstalledTextBox.Name = "modinfo_info_InstalledTextBox";
            this.modinfo_info_InstalledTextBox.ReadOnly = true;
            this.modinfo_info_InstalledTextBox.Size = new System.Drawing.Size(185, 25);
            this.modinfo_info_InstalledTextBox.TabIndex = 7;
            // 
            // modinfo_info_DateCreatedTextBox
            // 
            this.modinfo_info_DateCreatedTextBox.Location = new System.Drawing.Point(128, 37);
            this.modinfo_info_DateCreatedTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_info_DateCreatedTextBox.Name = "modinfo_info_DateCreatedTextBox";
            this.modinfo_info_DateCreatedTextBox.ReadOnly = true;
            this.modinfo_info_DateCreatedTextBox.Size = new System.Drawing.Size(297, 25);
            this.modinfo_info_DateCreatedTextBox.TabIndex = 5;
            // 
            // modinfo_info_InstalledLabel
            // 
            this.modinfo_info_InstalledLabel.AutoSize = true;
            this.modinfo_info_InstalledLabel.Location = new System.Drawing.Point(455, 40);
            this.modinfo_info_InstalledLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.modinfo_info_InstalledLabel.Name = "modinfo_info_InstalledLabel";
            this.modinfo_info_InstalledLabel.Size = new System.Drawing.Size(67, 15);
            this.modinfo_info_InstalledLabel.TabIndex = 4;
            this.modinfo_info_InstalledLabel.Text = "安装时间";
            // 
            // modinfo_info_TitleTextBox
            // 
            this.modinfo_info_TitleTextBox.Location = new System.Drawing.Point(128, 7);
            this.modinfo_info_TitleTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_info_TitleTextBox.Name = "modinfo_info_TitleTextBox";
            this.modinfo_info_TitleTextBox.ReadOnly = true;
            this.modinfo_info_TitleTextBox.Size = new System.Drawing.Size(297, 25);
            this.modinfo_info_TitleTextBox.TabIndex = 3;
            // 
            // modinfo_info_AuthorTextBox
            // 
            this.modinfo_info_AuthorTextBox.Location = new System.Drawing.Point(575, 10);
            this.modinfo_info_AuthorTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_info_AuthorTextBox.Name = "modinfo_info_AuthorTextBox";
            this.modinfo_info_AuthorTextBox.ReadOnly = true;
            this.modinfo_info_AuthorTextBox.Size = new System.Drawing.Size(181, 25);
            this.modinfo_info_AuthorTextBox.TabIndex = 2;
            // 
            // modinfo_info_TitleLabel
            // 
            this.modinfo_info_TitleLabel.AutoSize = true;
            this.modinfo_info_TitleLabel.Location = new System.Drawing.Point(8, 10);
            this.modinfo_info_TitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.modinfo_info_TitleLabel.Name = "modinfo_info_TitleLabel";
            this.modinfo_info_TitleLabel.Size = new System.Drawing.Size(37, 15);
            this.modinfo_info_TitleLabel.TabIndex = 1;
            this.modinfo_info_TitleLabel.Text = "标题";
            // 
            // modinfo_info_AuthorLabel
            // 
            this.modinfo_info_AuthorLabel.AutoSize = true;
            this.modinfo_info_AuthorLabel.Location = new System.Drawing.Point(455, 14);
            this.modinfo_info_AuthorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.modinfo_info_AuthorLabel.Name = "modinfo_info_AuthorLabel";
            this.modinfo_info_AuthorLabel.Size = new System.Drawing.Size(37, 15);
            this.modinfo_info_AuthorLabel.TabIndex = 0;
            this.modinfo_info_AuthorLabel.Text = "作者";
            // 
            // modinfo_info_DescriptionRichTextBox
            // 
            this.modinfo_info_DescriptionRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modinfo_info_DescriptionRichTextBox.Location = new System.Drawing.Point(128, 67);
            this.modinfo_info_DescriptionRichTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_info_DescriptionRichTextBox.Name = "modinfo_info_DescriptionRichTextBox";
            this.modinfo_info_DescriptionRichTextBox.Size = new System.Drawing.Size(630, 161);
            this.modinfo_info_DescriptionRichTextBox.TabIndex = 8;
            this.modinfo_info_DescriptionRichTextBox.Text = "";
            this.modinfo_info_DescriptionRichTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.ControlLinkClicked);
            // 
            // modinfo_notes_tab
            // 
            this.modinfo_notes_tab.Controls.Add(this.modInfoNotesText);
            this.modinfo_notes_tab.Location = new System.Drawing.Point(4, 25);
            this.modinfo_notes_tab.Name = "modinfo_notes_tab";
            this.modinfo_notes_tab.Size = new System.Drawing.Size(766, 230);
            this.modinfo_notes_tab.TabIndex = 6;
            this.modinfo_notes_tab.Text = "笔记";
            this.modinfo_notes_tab.UseVisualStyleBackColor = true;
            // 
            // modInfoNotesText
            // 
            this.modInfoNotesText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modInfoNotesText.EnableAutoDragDrop = true;
            this.modInfoNotesText.Location = new System.Drawing.Point(0, 0);
            this.modInfoNotesText.Name = "modInfoNotesText";
            this.modInfoNotesText.Size = new System.Drawing.Size(766, 230);
            this.modInfoNotesText.TabIndex = 2;
            this.modInfoNotesText.Text = "";
            this.modInfoNotesText.TextChanged += new System.EventHandler(this.modInfoNotesText_TextChanged);
            // 
            // modinfo_readme_tab
            // 
            this.modinfo_readme_tab.Controls.Add(this.modinfo_readme_RichTextBox);
            this.modinfo_readme_tab.Location = new System.Drawing.Point(4, 25);
            this.modinfo_readme_tab.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_readme_tab.Name = "modinfo_readme_tab";
            this.modinfo_readme_tab.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_readme_tab.Size = new System.Drawing.Size(766, 230);
            this.modinfo_readme_tab.TabIndex = 1;
            this.modinfo_readme_tab.Text = "须知";
            this.modinfo_readme_tab.UseVisualStyleBackColor = true;
            // 
            // modinfo_readme_RichTextBox
            // 
            this.modinfo_readme_RichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modinfo_readme_RichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.modinfo_readme_RichTextBox.Location = new System.Drawing.Point(4, 3);
            this.modinfo_readme_RichTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_readme_RichTextBox.Name = "modinfo_readme_RichTextBox";
            this.modinfo_readme_RichTextBox.ReadOnly = true;
            this.modinfo_readme_RichTextBox.Size = new System.Drawing.Size(763, 127);
            this.modinfo_readme_RichTextBox.TabIndex = 0;
            this.modinfo_readme_RichTextBox.Text = "";
            this.modinfo_readme_RichTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.ControlLinkClicked);
            // 
            // modinfo_inspect_tab
            // 
            this.modinfo_inspect_tab.Controls.Add(this.modinfo_inspect_propertygrid);
            this.modinfo_inspect_tab.Location = new System.Drawing.Point(4, 25);
            this.modinfo_inspect_tab.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_inspect_tab.Name = "modinfo_inspect_tab";
            this.modinfo_inspect_tab.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_inspect_tab.Size = new System.Drawing.Size(766, 230);
            this.modinfo_inspect_tab.TabIndex = 2;
            this.modinfo_inspect_tab.Text = "检查";
            this.modinfo_inspect_tab.UseVisualStyleBackColor = true;
            // 
            // modinfo_inspect_propertygrid
            // 
            this.modinfo_inspect_propertygrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modinfo_inspect_propertygrid.HelpVisible = false;
            this.modinfo_inspect_propertygrid.LineColor = System.Drawing.SystemColors.ControlDark;
            this.modinfo_inspect_propertygrid.Location = new System.Drawing.Point(4, 3);
            this.modinfo_inspect_propertygrid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_inspect_propertygrid.Name = "modinfo_inspect_propertygrid";
            this.modinfo_inspect_propertygrid.Size = new System.Drawing.Size(758, 224);
            this.modinfo_inspect_propertygrid.TabIndex = 9;
            this.modinfo_inspect_propertygrid.Layout += new System.Windows.Forms.LayoutEventHandler(this.modinfo_inspect_propertygrid_Layout);
            // 
            // modinfo_config_tab
            // 
            this.modinfo_config_tab.Controls.Add(this.modinfo_config_TableLayoutPanel);
            this.modinfo_config_tab.Location = new System.Drawing.Point(4, 25);
            this.modinfo_config_tab.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_config_tab.Name = "modinfo_config_tab";
            this.modinfo_config_tab.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_config_tab.Size = new System.Drawing.Size(766, 230);
            this.modinfo_config_tab.TabIndex = 3;
            this.modinfo_config_tab.Text = "配置(Config)";
            this.modinfo_config_tab.UseVisualStyleBackColor = true;
            // 
            // modinfo_config_TableLayoutPanel
            // 
            this.modinfo_config_TableLayoutPanel.ColumnCount = 2;
            this.modinfo_config_TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.modinfo_config_TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.modinfo_config_TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.modinfo_config_TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.modinfo_config_TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.modinfo_config_TableLayoutPanel.Controls.Add(this.modinfo_ConfigFCTB, 0, 1);
            this.modinfo_config_TableLayoutPanel.Controls.Add(this.modinfo_config_FileSelectCueComboBox, 0, 0);
            this.modinfo_config_TableLayoutPanel.Controls.Add(this.modinfo_config_buttonsTableLayoutPanel, 1, 0);
            this.modinfo_config_TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modinfo_config_TableLayoutPanel.Location = new System.Drawing.Point(4, 3);
            this.modinfo_config_TableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.modinfo_config_TableLayoutPanel.Name = "modinfo_config_TableLayoutPanel";
            this.modinfo_config_TableLayoutPanel.RowCount = 2;
            this.modinfo_config_TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.modinfo_config_TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.modinfo_config_TableLayoutPanel.Size = new System.Drawing.Size(758, 224);
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
            this.modinfo_ConfigFCTB.AutoScrollMinSize = new System.Drawing.Size(0, 18);
            this.modinfo_ConfigFCTB.BackBrush = null;
            this.modinfo_ConfigFCTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.modinfo_ConfigFCTB.ChangedLineColor = System.Drawing.SystemColors.Info;
            this.modinfo_ConfigFCTB.CharHeight = 18;
            this.modinfo_ConfigFCTB.CharWidth = 10;
            this.modinfo_config_TableLayoutPanel.SetColumnSpan(this.modinfo_ConfigFCTB, 2);
            this.modinfo_ConfigFCTB.CommentPrefix = ";";
            this.modinfo_ConfigFCTB.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.modinfo_ConfigFCTB.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.modinfo_ConfigFCTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modinfo_ConfigFCTB.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.modinfo_ConfigFCTB.IsReplaceMode = false;
            this.modinfo_ConfigFCTB.Location = new System.Drawing.Point(3, 35);
            this.modinfo_ConfigFCTB.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.modinfo_ConfigFCTB.Name = "modinfo_ConfigFCTB";
            this.modinfo_ConfigFCTB.Paddings = new System.Windows.Forms.Padding(0);
            this.modinfo_ConfigFCTB.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.modinfo_ConfigFCTB.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("modinfo_ConfigFCTB.ServiceColors")));
            this.modinfo_ConfigFCTB.Size = new System.Drawing.Size(752, 187);
            this.modinfo_ConfigFCTB.TabIndex = 11;
            this.modinfo_ConfigFCTB.WordWrap = true;
            this.modinfo_ConfigFCTB.Zoom = 100;
            this.modinfo_ConfigFCTB.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.modinfo_ConfigFCTB_TextChanged);
            // 
            // modinfo_config_FileSelectCueComboBox
            // 
            this.modinfo_config_FileSelectCueComboBox.CueText = "选择INI以编辑";
            this.modinfo_config_FileSelectCueComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modinfo_config_FileSelectCueComboBox.FormattingEnabled = true;
            this.modinfo_config_FileSelectCueComboBox.Location = new System.Drawing.Point(3, 3);
            this.modinfo_config_FileSelectCueComboBox.Name = "modinfo_config_FileSelectCueComboBox";
            this.modinfo_config_FileSelectCueComboBox.Size = new System.Drawing.Size(194, 23);
            this.modinfo_config_FileSelectCueComboBox.TabIndex = 12;
            this.toolTip.SetToolTip(this.modinfo_config_FileSelectCueComboBox, "选择一个INI文件以查看或编辑");
            this.modinfo_config_FileSelectCueComboBox.DropDown += new System.EventHandler(this.AdjustWidthComboBox_DropDown);
            this.modinfo_config_FileSelectCueComboBox.SelectedIndexChanged += new System.EventHandler(this.modinfo_config_FileSelectCueComboBox_SelectedIndexChanged);
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
            this.modinfo_config_buttonsTableLayoutPanel.Location = new System.Drawing.Point(343, 0);
            this.modinfo_config_buttonsTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.modinfo_config_buttonsTableLayoutPanel.Name = "modinfo_config_buttonsTableLayoutPanel";
            this.modinfo_config_buttonsTableLayoutPanel.RowCount = 1;
            this.modinfo_config_buttonsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.modinfo_config_buttonsTableLayoutPanel.Size = new System.Drawing.Size(415, 35);
            this.modinfo_config_buttonsTableLayoutPanel.TabIndex = 13;
            // 
            // modinfo_config_ExpandButton
            // 
            this.modinfo_config_ExpandButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.modinfo_config_ExpandButton.Location = new System.Drawing.Point(4, 4);
            this.modinfo_config_ExpandButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_config_ExpandButton.Name = "modinfo_config_ExpandButton";
            this.modinfo_config_ExpandButton.Size = new System.Drawing.Size(75, 27);
            this.modinfo_config_ExpandButton.TabIndex = 13;
            this.modinfo_config_ExpandButton.Text = "展开";
            this.toolTip.SetToolTip(this.modinfo_config_ExpandButton, "展开INI编辑器以填充窗口");
            this.modinfo_config_ExpandButton.UseVisualStyleBackColor = true;
            this.modinfo_config_ExpandButton.Click += new System.EventHandler(this.modinfo_config_ExpandButton_Click);
            // 
            // modinfo_config_CompareButton
            // 
            this.modinfo_config_CompareButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.modinfo_config_CompareButton.Enabled = false;
            this.modinfo_config_CompareButton.Location = new System.Drawing.Point(336, 6);
            this.modinfo_config_CompareButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_config_CompareButton.Name = "modinfo_config_CompareButton";
            this.modinfo_config_CompareButton.Size = new System.Drawing.Size(75, 23);
            this.modinfo_config_CompareButton.TabIndex = 14;
            this.modinfo_config_CompareButton.Text = "比较";
            this.toolTip.SetToolTip(this.modinfo_config_CompareButton, "将当前文件与备份文件进行比较");
            this.modinfo_config_CompareButton.UseVisualStyleBackColor = true;
            this.modinfo_config_CompareButton.Click += new System.EventHandler(this.modinfo_config_CompareButton_Click);
            // 
            // modinfo_config_SaveButton
            // 
            this.modinfo_config_SaveButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.modinfo_config_SaveButton.Location = new System.Drawing.Point(87, 6);
            this.modinfo_config_SaveButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_config_SaveButton.Name = "modinfo_config_SaveButton";
            this.modinfo_config_SaveButton.Size = new System.Drawing.Size(75, 23);
            this.modinfo_config_SaveButton.TabIndex = 3;
            this.modinfo_config_SaveButton.Text = "保存";
            this.toolTip.SetToolTip(this.modinfo_config_SaveButton, "将当前设置保存到文件和备份文件中");
            this.modinfo_config_SaveButton.UseVisualStyleBackColor = true;
            this.modinfo_config_SaveButton.Click += new System.EventHandler(this.modinfo_config_SaveButton_Click);
            // 
            // modinfo_config_LoadButton
            // 
            this.modinfo_config_LoadButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.modinfo_config_LoadButton.Enabled = false;
            this.modinfo_config_LoadButton.Location = new System.Drawing.Point(170, 6);
            this.modinfo_config_LoadButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_config_LoadButton.Name = "modinfo_config_LoadButton";
            this.modinfo_config_LoadButton.Size = new System.Drawing.Size(75, 23);
            this.modinfo_config_LoadButton.TabIndex = 10;
            this.modinfo_config_LoadButton.Text = "加载";
            this.toolTip.SetToolTip(this.modinfo_config_LoadButton, "从备份文件加载设置");
            this.modinfo_config_LoadButton.UseVisualStyleBackColor = true;
            this.modinfo_config_LoadButton.Click += new System.EventHandler(this.modinfo_config_LoadButton_Click);
            // 
            // modinfo_config_RemoveButton
            // 
            this.modinfo_config_RemoveButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.modinfo_config_RemoveButton.Enabled = false;
            this.modinfo_config_RemoveButton.Location = new System.Drawing.Point(253, 6);
            this.modinfo_config_RemoveButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_config_RemoveButton.Name = "modinfo_config_RemoveButton";
            this.modinfo_config_RemoveButton.Size = new System.Drawing.Size(75, 23);
            this.modinfo_config_RemoveButton.TabIndex = 15;
            this.modinfo_config_RemoveButton.Text = "去掉";
            this.toolTip.SetToolTip(this.modinfo_config_RemoveButton, "从备份文件中删除设置");
            this.modinfo_config_RemoveButton.UseVisualStyleBackColor = true;
            this.modinfo_config_RemoveButton.Click += new System.EventHandler(this.modinfo_config_RemoveButton_Click);
            // 
            // modinfo_changelog_tab
            // 
            this.modinfo_changelog_tab.Controls.Add(this.modinfo_changelog_richtextbox);
            this.modinfo_changelog_tab.Location = new System.Drawing.Point(4, 25);
            this.modinfo_changelog_tab.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_changelog_tab.Name = "modinfo_changelog_tab";
            this.modinfo_changelog_tab.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_changelog_tab.Size = new System.Drawing.Size(766, 230);
            this.modinfo_changelog_tab.TabIndex = 4;
            this.modinfo_changelog_tab.Text = "更改日志";
            this.modinfo_changelog_tab.UseVisualStyleBackColor = true;
            // 
            // modinfo_changelog_richtextbox
            // 
            this.modinfo_changelog_richtextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.modinfo_changelog_richtextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modinfo_changelog_richtextbox.Location = new System.Drawing.Point(4, 3);
            this.modinfo_changelog_richtextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_changelog_richtextbox.Name = "modinfo_changelog_richtextbox";
            this.modinfo_changelog_richtextbox.ReadOnly = true;
            this.modinfo_changelog_richtextbox.Size = new System.Drawing.Size(758, 224);
            this.modinfo_changelog_richtextbox.TabIndex = 0;
            this.modinfo_changelog_richtextbox.Text = "";
            this.modinfo_changelog_richtextbox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.ControlLinkClicked);
            // 
            // modinfo_dependencies_tab
            // 
            this.modinfo_dependencies_tab.Controls.Add(this.tableLayoutPanel1);
            this.modinfo_dependencies_tab.Location = new System.Drawing.Point(4, 25);
            this.modinfo_dependencies_tab.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_dependencies_tab.Name = "modinfo_dependencies_tab";
            this.modinfo_dependencies_tab.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_dependencies_tab.Size = new System.Drawing.Size(766, 230);
            this.modinfo_dependencies_tab.TabIndex = 5;
            this.modinfo_dependencies_tab.Text = "依赖关系";
            this.modinfo_dependencies_tab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 3);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(758, 224);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.olvRequiredMods);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(4, 3);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(750, 106);
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
            this.olvRequiredMods.AllColumns.Add(this.olvColReqModsIgnore);
            this.olvRequiredMods.CellEditUseWholeCell = false;
            this.olvRequiredMods.CheckBoxes = true;
            this.olvRequiredMods.CheckedAspectName = "isActive";
            this.olvRequiredMods.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColReqModsActive,
            this.olvColReqModsName,
            this.olvColReqModsState,
            this.olvColReqModsHidden,
            this.olvColReqModsSteamUrl,
            this.olvColReqModsWotc,
            this.olvColReqModsIgnore});
            this.olvRequiredMods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvRequiredMods.FullRowSelect = true;
            this.olvRequiredMods.HideSelection = false;
            this.olvRequiredMods.IsSearchOnSortColumn = false;
            this.olvRequiredMods.Location = new System.Drawing.Point(0, 25);
            this.olvRequiredMods.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.olvRequiredMods.Name = "olvRequiredMods";
            this.olvRequiredMods.ShowGroups = false;
            this.olvRequiredMods.ShowItemCountOnGroups = true;
            this.olvRequiredMods.Size = new System.Drawing.Size(750, 81);
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
            this.olvColReqModsName.Text = "标题";
            this.olvColReqModsName.Width = 229;
            // 
            // olvColReqModsState
            // 
            this.olvColReqModsState.IsEditable = false;
            this.olvColReqModsState.Text = "状态";
            this.olvColReqModsState.Width = 84;
            // 
            // olvColReqModsHidden
            // 
            this.olvColReqModsHidden.AspectName = "isHidden";
            this.olvColReqModsHidden.Text = "隐藏";
            // 
            // olvColReqModsSteamUrl
            // 
            this.olvColReqModsSteamUrl.AspectName = "GetSteamLink";
            this.olvColReqModsSteamUrl.Hyperlink = true;
            this.olvColReqModsSteamUrl.IsEditable = false;
            this.olvColReqModsSteamUrl.Searchable = false;
            this.olvColReqModsSteamUrl.Text = "Steam链接";
            this.olvColReqModsSteamUrl.Width = 225;
            // 
            // olvColReqModsWotc
            // 
            this.olvColReqModsWotc.AspectName = "BuiltForWOTC";
            this.olvColReqModsWotc.IsEditable = false;
            this.olvColReqModsWotc.Searchable = false;
            this.olvColReqModsWotc.Text = "兼容WOTC";
            this.olvColReqModsWotc.Width = 63;
            // 
            // olvColReqModsIgnore
            // 
            this.olvColReqModsIgnore.CheckBoxes = true;
            this.olvColReqModsIgnore.Text = "忽略";
            this.olvColReqModsIgnore.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.cShowPrimaryDuplicates);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(750, 25);
            this.panel6.TabIndex = 3;
            // 
            // cShowPrimaryDuplicates
            // 
            this.cShowPrimaryDuplicates.AutoSize = true;
            this.cShowPrimaryDuplicates.Location = new System.Drawing.Point(135, 3);
            this.cShowPrimaryDuplicates.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cShowPrimaryDuplicates.Name = "cShowPrimaryDuplicates";
            this.cShowPrimaryDuplicates.Size = new System.Drawing.Size(119, 19);
            this.cShowPrimaryDuplicates.TabIndex = 3;
            this.cShowPrimaryDuplicates.Text = "显示主要副本";
            this.toolTip.SetToolTip(this.cShowPrimaryDuplicates, "显示主要副本，而不显示实际依赖项（如果有）。");
            this.cShowPrimaryDuplicates.UseVisualStyleBackColor = true;
            this.cShowPrimaryDuplicates.CheckedChanged += new System.EventHandler(this.cShowPrimaryDuplicates_CheckedChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(4, 1);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 22);
            this.label5.TabIndex = 2;
            this.label5.Text = "必须MOD:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.olvDependentMods);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(4, 115);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(750, 106);
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
            this.olvDependentMods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvDependentMods.FullRowSelect = true;
            this.olvDependentMods.HideSelection = false;
            this.olvDependentMods.IsSearchOnSortColumn = false;
            this.olvDependentMods.Location = new System.Drawing.Point(0, 22);
            this.olvDependentMods.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.olvDependentMods.Name = "olvDependentMods";
            this.olvDependentMods.ShowGroups = false;
            this.olvDependentMods.ShowItemCountOnGroups = true;
            this.olvDependentMods.Size = new System.Drawing.Size(750, 84);
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
            this.olvColDepModsName.Text = "标题";
            this.olvColDepModsName.Width = 229;
            // 
            // olvColDepModsState
            // 
            this.olvColDepModsState.IsEditable = false;
            this.olvColDepModsState.Text = "状态";
            this.olvColDepModsState.Width = 86;
            // 
            // olvColDepModsHidden
            // 
            this.olvColDepModsHidden.AspectName = "isHidden";
            this.olvColDepModsHidden.Text = "隐藏";
            // 
            // olvColDepModsSteamUrl
            // 
            this.olvColDepModsSteamUrl.AspectName = "GetSteamLink";
            this.olvColDepModsSteamUrl.Hyperlink = true;
            this.olvColDepModsSteamUrl.IsEditable = false;
            this.olvColDepModsSteamUrl.Searchable = false;
            this.olvColDepModsSteamUrl.Text = "Steam链接";
            this.olvColDepModsSteamUrl.Width = 224;
            // 
            // olvColDepModsUrl
            // 
            this.olvColDepModsUrl.AspectName = "BrowserLink";
            this.olvColDepModsUrl.DisplayIndex = 5;
            this.olvColDepModsUrl.Hyperlink = true;
            this.olvColDepModsUrl.IsEditable = false;
            this.olvColDepModsUrl.IsVisible = false;
            this.olvColDepModsUrl.Searchable = false;
            this.olvColDepModsUrl.Text = "网址";
            // 
            // olvColDepModsWotc
            // 
            this.olvColDepModsWotc.AspectName = "BuiltForWOTC";
            this.olvColDepModsWotc.IsEditable = false;
            this.olvColDepModsWotc.Searchable = false;
            this.olvColDepModsWotc.Text = "兼容WOTC";
            this.olvColDepModsWotc.Width = 63;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(750, 22);
            this.label6.TabIndex = 3;
            this.label6.Text = "相关MOD:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // modinfo_image_picturebox
            // 
            this.modinfo_image_picturebox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.modinfo_image_picturebox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.modinfo_image_picturebox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.modinfo_image_picturebox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.modinfo_image_picturebox.Location = new System.Drawing.Point(6, 53);
            this.modinfo_image_picturebox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modinfo_image_picturebox.Name = "modinfo_image_picturebox";
            this.modinfo_image_picturebox.Size = new System.Drawing.Size(255, 154);
            this.modinfo_image_picturebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.modinfo_image_picturebox.TabIndex = 8;
            this.modinfo_image_picturebox.TabStop = false;
            // 
            // conflicts_tab
            // 
            this.conflicts_tab.Controls.Add(this.conflicts_tab_tableLayoutPanel);
            this.conflicts_tab.Location = new System.Drawing.Point(4, 25);
            this.conflicts_tab.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.conflicts_tab.Name = "conflicts_tab";
            this.conflicts_tab.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.conflicts_tab.Size = new System.Drawing.Size(1050, 638);
            this.conflicts_tab.TabIndex = 1;
            this.conflicts_tab.Text = "类别冲突";
            this.conflicts_tab.UseVisualStyleBackColor = true;
            // 
            // conflicts_tab_tableLayoutPanel
            // 
            this.conflicts_tab_tableLayoutPanel.ColumnCount = 2;
            this.conflicts_tab_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.conflicts_tab_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.conflicts_tab_tableLayoutPanel.Controls.Add(this.conflicts_log_label, 0, 0);
            this.conflicts_tab_tableLayoutPanel.Controls.Add(this.conflicts_datagrid, 1, 0);
            this.conflicts_tab_tableLayoutPanel.Controls.Add(this.conflicts_textbox, 0, 1);
            this.conflicts_tab_tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conflicts_tab_tableLayoutPanel.Location = new System.Drawing.Point(4, 3);
            this.conflicts_tab_tableLayoutPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.conflicts_tab_tableLayoutPanel.Name = "conflicts_tab_tableLayoutPanel";
            this.conflicts_tab_tableLayoutPanel.RowCount = 2;
            this.conflicts_tab_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.conflicts_tab_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.conflicts_tab_tableLayoutPanel.Size = new System.Drawing.Size(1042, 632);
            this.conflicts_tab_tableLayoutPanel.TabIndex = 9;
            // 
            // conflicts_log_label
            // 
            this.conflicts_log_label.AutoSize = true;
            this.conflicts_log_label.Location = new System.Drawing.Point(4, 0);
            this.conflicts_log_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.conflicts_log_label.Name = "conflicts_log_label";
            this.conflicts_log_label.Size = new System.Drawing.Size(45, 15);
            this.conflicts_log_label.TabIndex = 8;
            this.conflicts_log_label.Text = "日志:";
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
            this.conflicts_datagrid.Location = new System.Drawing.Point(404, 3);
            this.conflicts_datagrid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.conflicts_datagrid.Name = "conflicts_datagrid";
            this.conflicts_datagrid.ReadOnly = true;
            this.conflicts_datagrid.RowHeadersWidth = 51;
            this.conflicts_tab_tableLayoutPanel.SetRowSpan(this.conflicts_datagrid, 2);
            this.conflicts_datagrid.Size = new System.Drawing.Size(634, 626);
            this.conflicts_datagrid.TabIndex = 6;
            // 
            // ColumnModName
            // 
            this.ColumnModName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnModName.FillWeight = 30F;
            this.ColumnModName.HeaderText = "Mod";
            this.ColumnModName.MinimumWidth = 6;
            this.ColumnModName.Name = "ColumnModName";
            this.ColumnModName.ReadOnly = true;
            // 
            // ColumnInternalClass
            // 
            this.ColumnInternalClass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnInternalClass.FillWeight = 30F;
            this.ColumnInternalClass.HeaderText = "内部类别";
            this.ColumnInternalClass.MinimumWidth = 6;
            this.ColumnInternalClass.Name = "ColumnInternalClass";
            this.ColumnInternalClass.ReadOnly = true;
            // 
            // ColumnModClass
            // 
            this.ColumnModClass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnModClass.FillWeight = 40F;
            this.ColumnModClass.HeaderText = "Mod类别";
            this.ColumnModClass.MinimumWidth = 6;
            this.ColumnModClass.Name = "ColumnModClass";
            this.ColumnModClass.ReadOnly = true;
            // 
            // conflicts_textbox
            // 
            this.conflicts_textbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conflicts_textbox.Location = new System.Drawing.Point(4, 26);
            this.conflicts_textbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.conflicts_textbox.Multiline = true;
            this.conflicts_textbox.Name = "conflicts_textbox";
            this.conflicts_textbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.conflicts_textbox.Size = new System.Drawing.Size(392, 603);
            this.conflicts_textbox.TabIndex = 7;
            // 
            // export_tab
            // 
            this.export_tab.Controls.Add(this.tableLayoutPanel2);
            this.export_tab.Location = new System.Drawing.Point(4, 25);
            this.export_tab.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.export_tab.Name = "export_tab";
            this.export_tab.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.export_tab.Size = new System.Drawing.Size(1050, 638);
            this.export_tab.TabIndex = 2;
            this.export_tab.Text = "个性配置";
            this.export_tab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tableLayoutPanel2.Controls.Add(this.export_richtextbox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.export_load_button, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.export_save_button, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 3);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1042, 632);
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
            this.export_richtextbox.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.export_richtextbox.Location = new System.Drawing.Point(4, 43);
            this.export_richtextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.export_richtextbox.Name = "export_richtextbox";
            this.export_richtextbox.ReadOnly = true;
            this.export_richtextbox.Size = new System.Drawing.Size(1034, 586);
            this.export_richtextbox.TabIndex = 2;
            this.export_richtextbox.Text = "";
            this.export_richtextbox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.ControlLinkClicked);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.export_all_mods_checkbox);
            this.panel1.Controls.Add(this.export_workshop_link_checkbox);
            this.panel1.Controls.Add(this.export_group_checkbox);
            this.panel1.Location = new System.Drawing.Point(4, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 33);
            this.panel1.TabIndex = 5;
            // 
            // export_all_mods_checkbox
            // 
            this.export_all_mods_checkbox.AutoSize = true;
            this.export_all_mods_checkbox.Location = new System.Drawing.Point(349, 7);
            this.export_all_mods_checkbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.export_all_mods_checkbox.Name = "export_all_mods_checkbox";
            this.export_all_mods_checkbox.Size = new System.Drawing.Size(113, 19);
            this.export_all_mods_checkbox.TabIndex = 3;
            this.export_all_mods_checkbox.Text = "包括所有Mod";
            this.export_all_mods_checkbox.UseVisualStyleBackColor = true;
            this.export_all_mods_checkbox.Visible = false;
            this.export_all_mods_checkbox.CheckedChanged += new System.EventHandler(this.ExportCheckboxCheckedChanged);
            // 
            // export_workshop_link_checkbox
            // 
            this.export_workshop_link_checkbox.AutoSize = true;
            this.export_workshop_link_checkbox.Location = new System.Drawing.Point(9, 7);
            this.export_workshop_link_checkbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.export_workshop_link_checkbox.Name = "export_workshop_link_checkbox";
            this.export_workshop_link_checkbox.Size = new System.Drawing.Size(119, 19);
            this.export_workshop_link_checkbox.TabIndex = 0;
            this.export_workshop_link_checkbox.Text = "包含工坊链接";
            this.export_workshop_link_checkbox.UseVisualStyleBackColor = true;
            // 
            // export_group_checkbox
            // 
            this.export_group_checkbox.AutoSize = true;
            this.export_group_checkbox.Checked = true;
            this.export_group_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.export_group_checkbox.Location = new System.Drawing.Point(199, 7);
            this.export_group_checkbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.export_group_checkbox.Name = "export_group_checkbox";
            this.export_group_checkbox.Size = new System.Drawing.Size(89, 19);
            this.export_group_checkbox.TabIndex = 2;
            this.export_group_checkbox.Text = "包括分类";
            this.export_group_checkbox.UseVisualStyleBackColor = true;
            // 
            // export_load_button
            // 
            this.export_load_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.export_load_button.Location = new System.Drawing.Point(832, 10);
            this.export_load_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.export_load_button.Name = "export_load_button";
            this.export_load_button.Size = new System.Drawing.Size(99, 27);
            this.export_load_button.TabIndex = 4;
            this.export_load_button.Text = "载入";
            this.export_load_button.UseVisualStyleBackColor = true;
            // 
            // export_save_button
            // 
            this.export_save_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.export_save_button.Location = new System.Drawing.Point(939, 10);
            this.export_save_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.export_save_button.Name = "export_save_button";
            this.export_save_button.Size = new System.Drawing.Size(99, 27);
            this.export_save_button.TabIndex = 3;
            this.export_save_button.Text = "保存";
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
            this.olvcSavedIni.Text = "保存INI";
            // 
            // fillPanel
            // 
            this.fillPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fillPanel.Location = new System.Drawing.Point(42, 30);
            this.fillPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.fillPanel.Name = "fillPanel";
            this.fillPanel.Size = new System.Drawing.Size(593, 674);
            this.fillPanel.TabIndex = 6;
            this.fillPanel.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 659);
            this.Controls.Add(this.main_statusstrip);
            this.Controls.Add(this.main_menustrip);
            this.Controls.Add(this.main_tabcontrol);
            this.Controls.Add(this.fillPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.main_menustrip;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(1061, 668);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.Text = "XCOM Alternative Mod Launcher(AML启动器 汉化:重楼一叶Coralfox)";
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
            this.modinfo_notes_tab.ResumeLayout(false);
            this.modinfo_readme_tab.ResumeLayout(false);
            this.modinfo_inspect_tab.ResumeLayout(false);
            this.modinfo_config_tab.ResumeLayout(false);
            this.modinfo_config_TableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.modinfo_ConfigFCTB)).EndInit();
            this.modinfo_config_buttonsTableLayoutPanel.ResumeLayout(false);
            this.modinfo_changelog_tab.ResumeLayout(false);
            this.modinfo_dependencies_tab.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvRequiredMods)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvDependentMods)).EndInit();
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
        private System.Windows.Forms.CheckBox cShowStateFilter;
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
        private BrightIdeasSoftware.OLVColumn olvColDepModsUrl;
        private BrightIdeasSoftware.OLVColumn olvColReqModsSteamUrl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.CheckBox cFilterDuplicate;
		private System.Windows.Forms.CheckBox cFilterNew;
		private System.Windows.Forms.CheckBox cFilterConflicted;
		private System.Windows.Forms.CheckBox cFilterNotInstalled;
		private System.Windows.Forms.CheckBox cFilterNotLoaded;
		private System.Windows.Forms.Button bRefreshStateFilter;
		private System.Windows.Forms.CheckBox cFilterHidden;
		private System.Windows.Forms.CheckBox cFilterMissingDependency;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.CheckBox cShowPrimaryDuplicates;
        private BrightIdeasSoftware.OLVColumn olvColReqModsIgnore;
        private BrightIdeasSoftware.OLVColumn olvcSource;
        private System.Windows.Forms.Button bClearStateFilter;
        private System.Windows.Forms.ToolStripMenuItem runChimeraSquadToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importFromChimeraSquadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSpecialFoldersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem folderToAmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem folderToX2InstallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem folderToX2DataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem folderToWotcDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLogFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem amlLogFileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem x2LogFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wotcLogFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem folderToChimeraDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chimeraLogFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem folderToChimeraInstallToolStripMenuItem;
        private System.Windows.Forms.TabPage modinfo_notes_tab;
        private System.Windows.Forms.RichTextBox modInfoNotesText;
        private BrightIdeasSoftware.OLVColumn olvColNotes;
    }
}