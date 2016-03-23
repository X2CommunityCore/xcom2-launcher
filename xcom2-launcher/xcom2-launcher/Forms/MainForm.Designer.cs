namespace XCOM2Launcher
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
            this.main_statusstrip = new System.Windows.Forms.StatusStrip();
            this.status_toolstrip_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.progress_toolstrip_progressbar = new System.Windows.Forms.ToolStripProgressBar();
            this.main_menustrip = new System.Windows.Forms.MenuStrip();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.searchForModsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHiddenModsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.editSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importActiveModsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanModsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.error_provider = new System.Windows.Forms.ErrorProvider(this.components);
            this.main_tabcontrol = new System.Windows.Forms.TabControl();
            this.modlist_tab = new System.Windows.Forms.TabPage();
            this.horizontal_splitcontainer = new System.Windows.Forms.SplitContainer();
            this.modinfo_groupbox = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.modinfo_date_added_textbox = new System.Windows.Forms.TextBox();
            this.modinfo_date_created_textbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.modinfo_title_textbox = new System.Windows.Forms.TextBox();
            this.modinfo_author_textbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.modinfo_description_richtextbox = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.modinfo_readme_richtextbox = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.modinfo_details_propertygrid = new System.Windows.Forms.PropertyGrid();
            this.modinfo_config_tab = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.modinfo_config_propertygrid = new System.Windows.Forms.PropertyGrid();
            this.modinfo_image_picturebox = new System.Windows.Forms.PictureBox();
            this.conflicts_tab = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.conflicts_textbox = new System.Windows.Forms.TextBox();
            this.conflicts_datagrid = new System.Windows.Forms.DataGridView();
            this.ColumnModName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnInternalClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnModClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.export_group_checkbox = new System.Windows.Forms.CheckBox();
            this.export_background_panel = new System.Windows.Forms.Panel();
            this.export_richtextbox = new System.Windows.Forms.RichTextBox();
            this.export_workshop_link_checkbox = new System.Windows.Forms.CheckBox();
            this.run_game_button = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.modinfo_changelog_richtextbox = new System.Windows.Forms.RichTextBox();
            this.main_statusstrip.SuspendLayout();
            this.main_menustrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.error_provider)).BeginInit();
            this.main_tabcontrol.SuspendLayout();
            this.modlist_tab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.horizontal_splitcontainer)).BeginInit();
            this.horizontal_splitcontainer.Panel2.SuspendLayout();
            this.horizontal_splitcontainer.SuspendLayout();
            this.modinfo_groupbox.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.modinfo_config_tab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.modinfo_image_picturebox)).BeginInit();
            this.conflicts_tab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.conflicts_datagrid)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.export_background_panel.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // main_statusstrip
            // 
            this.main_statusstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status_toolstrip_label,
            this.progress_toolstrip_progressbar});
            this.main_statusstrip.Location = new System.Drawing.Point(0, 559);
            this.main_statusstrip.Name = "main_statusstrip";
            this.main_statusstrip.Size = new System.Drawing.Size(900, 22);
            this.main_statusstrip.TabIndex = 5;
            this.main_statusstrip.Text = "statusStrip1";
            // 
            // status_toolstrip_label
            // 
            this.status_toolstrip_label.Name = "status_toolstrip_label";
            this.status_toolstrip_label.Size = new System.Drawing.Size(0, 17);
            // 
            // progress_toolstrip_progressbar
            // 
            this.progress_toolstrip_progressbar.Name = "progress_toolstrip_progressbar";
            this.progress_toolstrip_progressbar.Size = new System.Drawing.Size(100, 16);
            // 
            // main_menustrip
            // 
            this.main_menustrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.main_menustrip.Location = new System.Drawing.Point(0, 0);
            this.main_menustrip.Name = "main_menustrip";
            this.main_menustrip.Size = new System.Drawing.Size(900, 24);
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
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.runToolStripMenuItem.Text = "&File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.reloadToolStripMenuItem.Text = "Reset";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // searchForModsToolStripMenuItem
            // 
            this.searchForModsToolStripMenuItem.Name = "searchForModsToolStripMenuItem";
            this.searchForModsToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.searchForModsToolStripMenuItem.Text = "Search for mods";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHiddenModsToolStripMenuItem,
            this.toolStripSeparator3,
            this.editSettingsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // showHiddenModsToolStripMenuItem
            // 
            this.showHiddenModsToolStripMenuItem.CheckOnClick = true;
            this.showHiddenModsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.showHiddenModsToolStripMenuItem.Name = "showHiddenModsToolStripMenuItem";
            this.showHiddenModsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.showHiddenModsToolStripMenuItem.Text = "Show hidden mods";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(173, 6);
            // 
            // editSettingsToolStripMenuItem
            // 
            this.editSettingsToolStripMenuItem.Name = "editSettingsToolStripMenuItem";
            this.editSettingsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.editSettingsToolStripMenuItem.Text = "Edit...";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importActiveModsToolStripMenuItem,
            this.cleanModsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // importActiveModsToolStripMenuItem
            // 
            this.importActiveModsToolStripMenuItem.Name = "importActiveModsToolStripMenuItem";
            this.importActiveModsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.importActiveModsToolStripMenuItem.Text = "Import active mods";
            // 
            // cleanModsToolStripMenuItem
            // 
            this.cleanModsToolStripMenuItem.Name = "cleanModsToolStripMenuItem";
            this.cleanModsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.cleanModsToolStripMenuItem.Text = "Clean mods";
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
            this.main_tabcontrol.Controls.Add(this.tabPage4);
            this.main_tabcontrol.Location = new System.Drawing.Point(0, 27);
            this.main_tabcontrol.Name = "main_tabcontrol";
            this.main_tabcontrol.SelectedIndex = 0;
            this.main_tabcontrol.Size = new System.Drawing.Size(900, 500);
            this.main_tabcontrol.TabIndex = 6;
            // 
            // modlist_tab
            // 
            this.modlist_tab.Controls.Add(this.horizontal_splitcontainer);
            this.modlist_tab.Location = new System.Drawing.Point(4, 22);
            this.modlist_tab.Name = "modlist_tab";
            this.modlist_tab.Padding = new System.Windows.Forms.Padding(3);
            this.modlist_tab.Size = new System.Drawing.Size(892, 474);
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
            // horizontal_splitcontainer.Panel2
            // 
            this.horizontal_splitcontainer.Panel2.Controls.Add(this.modinfo_groupbox);
            this.horizontal_splitcontainer.Size = new System.Drawing.Size(886, 468);
            this.horizontal_splitcontainer.SplitterDistance = 222;
            this.horizontal_splitcontainer.TabIndex = 5;
            // 
            // modinfo_groupbox
            // 
            this.modinfo_groupbox.Controls.Add(this.tabControl1);
            this.modinfo_groupbox.Controls.Add(this.modinfo_image_picturebox);
            this.modinfo_groupbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modinfo_groupbox.Location = new System.Drawing.Point(0, 0);
            this.modinfo_groupbox.Name = "modinfo_groupbox";
            this.modinfo_groupbox.Size = new System.Drawing.Size(886, 242);
            this.modinfo_groupbox.TabIndex = 3;
            this.modinfo_groupbox.TabStop = false;
            this.modinfo_groupbox.Text = "Mod Info";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.modinfo_config_tab);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(204, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(676, 228);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.modinfo_date_added_textbox);
            this.tabPage1.Controls.Add(this.modinfo_date_created_textbox);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.modinfo_title_textbox);
            this.tabPage1.Controls.Add(this.modinfo_author_textbox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.modinfo_description_richtextbox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(668, 202);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Info";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Created";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Description";
            // 
            // modinfo_date_added_textbox
            // 
            this.modinfo_date_added_textbox.Location = new System.Drawing.Point(431, 32);
            this.modinfo_date_added_textbox.Name = "modinfo_date_added_textbox";
            this.modinfo_date_added_textbox.ReadOnly = true;
            this.modinfo_date_added_textbox.Size = new System.Drawing.Size(224, 20);
            this.modinfo_date_added_textbox.TabIndex = 7;
            // 
            // modinfo_date_created_textbox
            // 
            this.modinfo_date_created_textbox.Location = new System.Drawing.Point(96, 32);
            this.modinfo_date_created_textbox.Name = "modinfo_date_created_textbox";
            this.modinfo_date_created_textbox.ReadOnly = true;
            this.modinfo_date_created_textbox.Size = new System.Drawing.Size(224, 20);
            this.modinfo_date_created_textbox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(341, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Installed";
            // 
            // modinfo_title_textbox
            // 
            this.modinfo_title_textbox.Location = new System.Drawing.Point(96, 6);
            this.modinfo_title_textbox.Name = "modinfo_title_textbox";
            this.modinfo_title_textbox.ReadOnly = true;
            this.modinfo_title_textbox.Size = new System.Drawing.Size(224, 20);
            this.modinfo_title_textbox.TabIndex = 3;
            // 
            // modinfo_author_textbox
            // 
            this.modinfo_author_textbox.Location = new System.Drawing.Point(431, 9);
            this.modinfo_author_textbox.Name = "modinfo_author_textbox";
            this.modinfo_author_textbox.ReadOnly = true;
            this.modinfo_author_textbox.Size = new System.Drawing.Size(224, 20);
            this.modinfo_author_textbox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Title";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(341, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Author";
            // 
            // modinfo_description_richtextbox
            // 
            this.modinfo_description_richtextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modinfo_description_richtextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.modinfo_description_richtextbox.Location = new System.Drawing.Point(96, 58);
            this.modinfo_description_richtextbox.Name = "modinfo_description_richtextbox";
            this.modinfo_description_richtextbox.ReadOnly = true;
            this.modinfo_description_richtextbox.Size = new System.Drawing.Size(569, 141);
            this.modinfo_description_richtextbox.TabIndex = 8;
            this.modinfo_description_richtextbox.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.modinfo_readme_richtextbox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(668, 202);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ReadMe";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // modinfo_readme_richtextbox
            // 
            this.modinfo_readme_richtextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.modinfo_readme_richtextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modinfo_readme_richtextbox.Location = new System.Drawing.Point(3, 3);
            this.modinfo_readme_richtextbox.Name = "modinfo_readme_richtextbox";
            this.modinfo_readme_richtextbox.ReadOnly = true;
            this.modinfo_readme_richtextbox.Size = new System.Drawing.Size(662, 196);
            this.modinfo_readme_richtextbox.TabIndex = 0;
            this.modinfo_readme_richtextbox.Text = "";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.modinfo_details_propertygrid);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(668, 202);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Inspect";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // modinfo_details_propertygrid
            // 
            this.modinfo_details_propertygrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modinfo_details_propertygrid.HelpVisible = false;
            this.modinfo_details_propertygrid.Location = new System.Drawing.Point(3, 3);
            this.modinfo_details_propertygrid.Name = "modinfo_details_propertygrid";
            this.modinfo_details_propertygrid.Size = new System.Drawing.Size(662, 196);
            this.modinfo_details_propertygrid.TabIndex = 9;
            // 
            // modinfo_config_tab
            // 
            this.modinfo_config_tab.Controls.Add(this.button1);
            this.modinfo_config_tab.Controls.Add(this.modinfo_config_propertygrid);
            this.modinfo_config_tab.Location = new System.Drawing.Point(4, 22);
            this.modinfo_config_tab.Name = "modinfo_config_tab";
            this.modinfo_config_tab.Padding = new System.Windows.Forms.Padding(3);
            this.modinfo_config_tab.Size = new System.Drawing.Size(668, 202);
            this.modinfo_config_tab.TabIndex = 3;
            this.modinfo_config_tab.Text = "Config";
            this.modinfo_config_tab.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(584, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // modinfo_config_propertygrid
            // 
            this.modinfo_config_propertygrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modinfo_config_propertygrid.HelpVisible = false;
            this.modinfo_config_propertygrid.Location = new System.Drawing.Point(3, 3);
            this.modinfo_config_propertygrid.Name = "modinfo_config_propertygrid";
            this.modinfo_config_propertygrid.Size = new System.Drawing.Size(662, 196);
            this.modinfo_config_propertygrid.TabIndex = 10;
            // 
            // modinfo_image_picturebox
            // 
            this.modinfo_image_picturebox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.modinfo_image_picturebox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.modinfo_image_picturebox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.modinfo_image_picturebox.Location = new System.Drawing.Point(6, 19);
            this.modinfo_image_picturebox.Name = "modinfo_image_picturebox";
            this.modinfo_image_picturebox.Size = new System.Drawing.Size(192, 192);
            this.modinfo_image_picturebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.modinfo_image_picturebox.TabIndex = 8;
            this.modinfo_image_picturebox.TabStop = false;
            // 
            // conflicts_tab
            // 
            this.conflicts_tab.Controls.Add(this.label3);
            this.conflicts_tab.Controls.Add(this.conflicts_textbox);
            this.conflicts_tab.Controls.Add(this.conflicts_datagrid);
            this.conflicts_tab.Location = new System.Drawing.Point(4, 22);
            this.conflicts_tab.Name = "conflicts_tab";
            this.conflicts_tab.Padding = new System.Windows.Forms.Padding(3);
            this.conflicts_tab.Size = new System.Drawing.Size(892, 474);
            this.conflicts_tab.TabIndex = 1;
            this.conflicts_tab.Text = "Class Overrides";
            this.conflicts_tab.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(594, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Log:";
            // 
            // conflicts_textbox
            // 
            this.conflicts_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.conflicts_textbox.Location = new System.Drawing.Point(594, 23);
            this.conflicts_textbox.Multiline = true;
            this.conflicts_textbox.Name = "conflicts_textbox";
            this.conflicts_textbox.Size = new System.Drawing.Size(285, 513);
            this.conflicts_textbox.TabIndex = 7;
            // 
            // conflicts_datagrid
            // 
            this.conflicts_datagrid.AllowUserToAddRows = false;
            this.conflicts_datagrid.AllowUserToDeleteRows = false;
            this.conflicts_datagrid.AllowUserToOrderColumns = true;
            this.conflicts_datagrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.conflicts_datagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.conflicts_datagrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnModName,
            this.ColumnInternalClass,
            this.ColumnModClass});
            this.conflicts_datagrid.Location = new System.Drawing.Point(3, 6);
            this.conflicts_datagrid.Name = "conflicts_datagrid";
            this.conflicts_datagrid.ReadOnly = true;
            this.conflicts_datagrid.Size = new System.Drawing.Size(585, 530);
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
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.export_group_checkbox);
            this.tabPage4.Controls.Add(this.export_background_panel);
            this.tabPage4.Controls.Add(this.export_workshop_link_checkbox);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(892, 474);
            this.tabPage4.TabIndex = 2;
            this.tabPage4.Text = "Export";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // export_group_checkbox
            // 
            this.export_group_checkbox.AutoSize = true;
            this.export_group_checkbox.Checked = true;
            this.export_group_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.export_group_checkbox.Location = new System.Drawing.Point(9, 7);
            this.export_group_checkbox.Name = "export_group_checkbox";
            this.export_group_checkbox.Size = new System.Drawing.Size(107, 17);
            this.export_group_checkbox.TabIndex = 2;
            this.export_group_checkbox.Text = "Include Grouping";
            this.export_group_checkbox.UseVisualStyleBackColor = true;
            this.export_group_checkbox.CheckedChanged += new System.EventHandler(this.export_group_checkbox_CheckedChanged);
            // 
            // export_background_panel
            // 
            this.export_background_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.export_background_panel.BackColor = System.Drawing.Color.DimGray;
            this.export_background_panel.Controls.Add(this.export_richtextbox);
            this.export_background_panel.Location = new System.Drawing.Point(8, 29);
            this.export_background_panel.Name = "export_background_panel";
            this.export_background_panel.Size = new System.Drawing.Size(876, 439);
            this.export_background_panel.TabIndex = 1;
            // 
            // export_richtextbox
            // 
            this.export_richtextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.export_richtextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.export_richtextbox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.export_richtextbox.Location = new System.Drawing.Point(1, 1);
            this.export_richtextbox.Name = "export_richtextbox";
            this.export_richtextbox.Size = new System.Drawing.Size(874, 437);
            this.export_richtextbox.TabIndex = 2;
            this.export_richtextbox.Text = "";
            // 
            // export_workshop_link_checkbox
            // 
            this.export_workshop_link_checkbox.AutoSize = true;
            this.export_workshop_link_checkbox.Location = new System.Drawing.Point(122, 7);
            this.export_workshop_link_checkbox.Name = "export_workshop_link_checkbox";
            this.export_workshop_link_checkbox.Size = new System.Drawing.Size(136, 17);
            this.export_workshop_link_checkbox.TabIndex = 0;
            this.export_workshop_link_checkbox.Text = "Include Workshop Link";
            this.export_workshop_link_checkbox.UseVisualStyleBackColor = true;
            this.export_workshop_link_checkbox.CheckedChanged += new System.EventHandler(this.export_workshop_link_checkbox_CheckedChanged);
            // 
            // run_game_button
            // 
            this.run_game_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.run_game_button.Location = new System.Drawing.Point(814, 533);
            this.run_game_button.Name = "run_game_button";
            this.run_game_button.Size = new System.Drawing.Size(82, 23);
            this.run_game_button.TabIndex = 0;
            this.run_game_button.Text = "Run XCOM 2";
            this.run_game_button.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.modinfo_changelog_richtextbox);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(668, 202);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Changelog";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.modinfo_changelog_richtextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.modinfo_changelog_richtextbox.Location = new System.Drawing.Point(3, 3);
            this.modinfo_changelog_richtextbox.Name = "modinfo_changelog_richtextbox";
            this.modinfo_changelog_richtextbox.ReadOnly = true;
            this.modinfo_changelog_richtextbox.Size = new System.Drawing.Size(662, 196);
            this.modinfo_changelog_richtextbox.TabIndex = 0;
            this.modinfo_changelog_richtextbox.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 581);
            this.Controls.Add(this.run_game_button);
            this.Controls.Add(this.main_statusstrip);
            this.Controls.Add(this.main_menustrip);
            this.Controls.Add(this.main_tabcontrol);
            this.Icon = global::XCOM2Launcher.Properties.Resources.xcom;
            this.MainMenuStrip = this.main_menustrip;
            this.Name = "MainForm";
            this.Text = "XCOM 2 Mod Launcher";
            this.main_statusstrip.ResumeLayout(false);
            this.main_statusstrip.PerformLayout();
            this.main_menustrip.ResumeLayout(false);
            this.main_menustrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.error_provider)).EndInit();
            this.main_tabcontrol.ResumeLayout(false);
            this.modlist_tab.ResumeLayout(false);
            this.horizontal_splitcontainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.horizontal_splitcontainer)).EndInit();
            this.horizontal_splitcontainer.ResumeLayout(false);
            this.modinfo_groupbox.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.modinfo_config_tab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.modinfo_image_picturebox)).EndInit();
            this.conflicts_tab.ResumeLayout(false);
            this.conflicts_tab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.conflicts_datagrid)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.export_background_panel.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox conflicts_textbox;
        private System.Windows.Forms.DataGridView conflicts_datagrid;
        private System.Windows.Forms.Button run_game_button;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnModName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnInternalClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnModClass;
        private System.Windows.Forms.ToolStripMenuItem importActiveModsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanModsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer horizontal_splitcontainer;
        private System.Windows.Forms.GroupBox modinfo_groupbox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox modinfo_date_added_textbox;
        private System.Windows.Forms.TextBox modinfo_date_created_textbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox modinfo_title_textbox;
        private System.Windows.Forms.TextBox modinfo_author_textbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox modinfo_description_richtextbox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox modinfo_readme_richtextbox;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.PropertyGrid modinfo_details_propertygrid;
        private System.Windows.Forms.TabPage modinfo_config_tab;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PropertyGrid modinfo_config_propertygrid;
        private System.Windows.Forms.PictureBox modinfo_image_picturebox;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem searchForModsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem showHiddenModsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel status_toolstrip_label;
        private System.Windows.Forms.ToolStripProgressBar progress_toolstrip_progressbar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Panel export_background_panel;
        private System.Windows.Forms.RichTextBox export_richtextbox;
        private System.Windows.Forms.CheckBox export_workshop_link_checkbox;
        private System.Windows.Forms.CheckBox export_group_checkbox;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.RichTextBox modinfo_changelog_richtextbox;
    }
}

