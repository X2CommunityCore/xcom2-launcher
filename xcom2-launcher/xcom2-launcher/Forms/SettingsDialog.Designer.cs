namespace XCOM2Launcher.Forms
{
    partial class SettingsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.quickArgumentsTextBox = new XCOM2Launcher.UserElements.AutoCompleteTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.modPathsListbox = new System.Windows.Forms.ListBox();
            this.ShowQuickLaunchArgumentsToggle = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gamePathTextBox = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.addModPathButton = new System.Windows.Forms.Button();
            this.removeModPathButton = new System.Windows.Forms.Button();
            this.browseGamePathButton = new System.Windows.Forms.Button();
            this.argumentsTextBox = new XCOM2Launcher.UserElements.AutoCompleteTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.updateModsOnStartup = new System.Windows.Forms.CheckBox();
            this.useModSpecifiedCategoriesCheckBox = new System.Windows.Forms.CheckBox();
            this.autoNumberModIndexesCheckBox = new System.Windows.Forms.CheckBox();
            this.neverAdoptTagsAndCatFromprofile = new System.Windows.Forms.CheckBox();
            this.useDuplicateModWorkaround = new System.Windows.Forms.CheckBox();
            this.onlyUpdateEnabledAndNew = new System.Windows.Forms.CheckBox();
            this.showHiddenEntriesCheckBox = new System.Windows.Forms.CheckBox();
            this.useTranslucentModListSelection = new System.Windows.Forms.CheckBox();
            this.useSentry = new System.Windows.Forms.CheckBox();
            this.closeAfterLaunchCheckBox = new System.Windows.Forms.CheckBox();
            this.searchForUpdatesCheckBox = new System.Windows.Forms.CheckBox();
            this.checkForPreReleaseUpdates = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.allowMutipleInstances = new System.Windows.Forms.CheckBox();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.hideRunX2Button = new System.Windows.Forms.CheckBox();
            this.hideChallengeModeButton = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Location = new System.Drawing.Point(16, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(873, 231);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "游戏设置";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 173F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.quickArgumentsTextBox, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.modPathsListbox, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.ShowQuickLaunchArgumentsToggle, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.gamePathTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.browseGamePathButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.argumentsTextBox, 1, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 22);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(855, 202);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // quickArgumentsTextBox
            // 
            this.quickArgumentsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.quickArgumentsTextBox.Location = new System.Drawing.Point(177, 176);
            this.quickArgumentsTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.quickArgumentsTextBox.Name = "quickArgumentsTextBox";
            this.quickArgumentsTextBox.Size = new System.Drawing.Size(577, 25);
            this.quickArgumentsTextBox.TabIndex = 16;
            this.quickArgumentsTextBox.Values = new string[0];
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 180);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 7, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "快速切换命令参数";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 151);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 7, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "启动命令参数";
            // 
            // modPathsListbox
            // 
            this.modPathsListbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modPathsListbox.FormattingEnabled = true;
            this.modPathsListbox.ItemHeight = 15;
            this.modPathsListbox.Location = new System.Drawing.Point(177, 38);
            this.modPathsListbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modPathsListbox.Name = "modPathsListbox";
            this.modPathsListbox.Size = new System.Drawing.Size(577, 103);
            this.modPathsListbox.TabIndex = 4;
            // 
            // ShowQuickLaunchArgumentsToggle
            // 
            this.ShowQuickLaunchArgumentsToggle.AutoSize = true;
            this.ShowQuickLaunchArgumentsToggle.Location = new System.Drawing.Point(762, 176);
            this.ShowQuickLaunchArgumentsToggle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ShowQuickLaunchArgumentsToggle.Name = "ShowQuickLaunchArgumentsToggle";
            this.ShowQuickLaunchArgumentsToggle.Padding = new System.Windows.Forms.Padding(4, 3, 0, 0);
            this.ShowQuickLaunchArgumentsToggle.Size = new System.Drawing.Size(63, 22);
            this.ShowQuickLaunchArgumentsToggle.TabIndex = 17;
            this.ShowQuickLaunchArgumentsToggle.Text = "启用";
            this.toolTip.SetToolTip(this.ShowQuickLaunchArgumentsToggle, "如果启用,则会在主界面显示一个快速切换参数的下拉菜单\r\n可以快速切换当前启动参数.");
            this.ShowQuickLaunchArgumentsToggle.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 7);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 7, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "游戏目录";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 42);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 7, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Mod文件夹(可多个)";
            // 
            // gamePathTextBox
            // 
            this.gamePathTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gamePathTextBox.Location = new System.Drawing.Point(177, 3);
            this.gamePathTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gamePathTextBox.Name = "gamePathTextBox";
            this.gamePathTextBox.Size = new System.Drawing.Size(577, 25);
            this.gamePathTextBox.TabIndex = 10;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.addModPathButton);
            this.flowLayoutPanel1.Controls.Add(this.removeModPathButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(758, 35);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(97, 109);
            this.flowLayoutPanel1.TabIndex = 12;
            // 
            // addModPathButton
            // 
            this.addModPathButton.Location = new System.Drawing.Point(4, 3);
            this.addModPathButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.addModPathButton.Name = "addModPathButton";
            this.addModPathButton.Size = new System.Drawing.Size(89, 28);
            this.addModPathButton.TabIndex = 6;
            this.addModPathButton.Text = "添加";
            this.addModPathButton.UseVisualStyleBackColor = true;
            this.addModPathButton.Click += new System.EventHandler(this.AddModPathButtonOnClick);
            // 
            // removeModPathButton
            // 
            this.removeModPathButton.Location = new System.Drawing.Point(4, 37);
            this.removeModPathButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.removeModPathButton.Name = "removeModPathButton";
            this.removeModPathButton.Size = new System.Drawing.Size(89, 28);
            this.removeModPathButton.TabIndex = 8;
            this.removeModPathButton.Text = "删除";
            this.removeModPathButton.UseVisualStyleBackColor = true;
            this.removeModPathButton.Click += new System.EventHandler(this.RemoveModPathButtonOnClick);
            // 
            // browseGamePathButton
            // 
            this.browseGamePathButton.Location = new System.Drawing.Point(762, 3);
            this.browseGamePathButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.browseGamePathButton.Name = "browseGamePathButton";
            this.browseGamePathButton.Size = new System.Drawing.Size(89, 28);
            this.browseGamePathButton.TabIndex = 14;
            this.browseGamePathButton.Text = "浏览";
            this.browseGamePathButton.UseVisualStyleBackColor = true;
            this.browseGamePathButton.Click += new System.EventHandler(this.BrowseGamePathButtonOnClick);
            // 
            // argumentsTextBox
            // 
            this.argumentsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.argumentsTextBox.Location = new System.Drawing.Point(177, 147);
            this.argumentsTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.argumentsTextBox.Name = "argumentsTextBox";
            this.argumentsTextBox.Size = new System.Drawing.Size(577, 25);
            this.argumentsTextBox.TabIndex = 15;
            this.argumentsTextBox.Values = new string[0];
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tableLayoutPanel3);
            this.groupBox2.Location = new System.Drawing.Point(16, 343);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(873, 95);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "使用";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel3.Controls.Add(this.useModSpecifiedCategoriesCheckBox, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.autoNumberModIndexesCheckBox, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.neverAdoptTagsAndCatFromprofile, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.useDuplicateModWorkaround, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.onlyUpdateEnabledAndNew, 1, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 23);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(855, 65);
			this.tableLayoutPanel3.TabIndex = 6;
			// 
			// updateModsOnStartup
			// 
			this.updateModsOnStartup.AutoSize = true;
			this.updateModsOnStartup.Location = new System.Drawing.Point(216, 29);
			this.updateModsOnStartup.Name = "updateModsOnStartup";
			this.updateModsOnStartup.Padding = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.updateModsOnStartup.Size = new System.Drawing.Size(142, 20);
			this.updateModsOnStartup.TabIndex = 18;
			this.updateModsOnStartup.Text = "启动时更新Mod";
			this.toolTip.SetToolTip(this.updateModsOnStartup, resources.GetString("updateModsOnStartup.ToolTip"));
			this.updateModsOnStartup.UseVisualStyleBackColor = true;
			this.updateModsOnStartup.CheckedChanged += new System.EventHandler(this.updateModsOnStartup_CheckedChanged);
            // useModSpecifiedCategoriesCheckBox
            // 
            this.useModSpecifiedCategoriesCheckBox.AutoSize = true;
            this.useModSpecifiedCategoriesCheckBox.Location = new System.Drawing.Point(4, 31);
            this.useModSpecifiedCategoriesCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.useModSpecifiedCategoriesCheckBox.Name = "useModSpecifiedCategoriesCheckBox";
            this.useModSpecifiedCategoriesCheckBox.Padding = new System.Windows.Forms.Padding(4, 3, 0, 0);
            this.useModSpecifiedCategoriesCheckBox.Size = new System.Drawing.Size(168, 22);
            this.useModSpecifiedCategoriesCheckBox.TabIndex = 15;
            this.useModSpecifiedCategoriesCheckBox.Text = "使用模式指定的类别";
            this.toolTip.SetToolTip(this.useModSpecifiedCategoriesCheckBox, "新Mod是否显示在默认分类中\r\n (关闭-新mod默认显示在未分组中)");
            this.useModSpecifiedCategoriesCheckBox.UseVisualStyleBackColor = true;
            // 
            // autoNumberModIndexesCheckBox
            // 
            this.autoNumberModIndexesCheckBox.AutoSize = true;
            this.autoNumberModIndexesCheckBox.Location = new System.Drawing.Point(4, 3);
            this.autoNumberModIndexesCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.autoNumberModIndexesCheckBox.Name = "autoNumberModIndexesCheckBox";
            this.autoNumberModIndexesCheckBox.Padding = new System.Windows.Forms.Padding(4, 3, 0, 0);
            this.autoNumberModIndexesCheckBox.Size = new System.Drawing.Size(117, 22);
            this.autoNumberModIndexesCheckBox.TabIndex = 14;
            this.autoNumberModIndexesCheckBox.Text = "自动编号Mod";
            this.toolTip.SetToolTip(this.autoNumberModIndexesCheckBox, "当做出任何修改操作后,将自动编号MOD\r\n(关闭后可手动修改)");
            this.autoNumberModIndexesCheckBox.UseVisualStyleBackColor = true;
            // 
            // neverAdoptTagsAndCatFromprofile
            // 
            this.neverAdoptTagsAndCatFromprofile.AutoSize = true;
            this.neverAdoptTagsAndCatFromprofile.Location = new System.Drawing.Point(288, 3);
            this.neverAdoptTagsAndCatFromprofile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.neverAdoptTagsAndCatFromprofile.Name = "neverAdoptTagsAndCatFromprofile";
            this.neverAdoptTagsAndCatFromprofile.Padding = new System.Windows.Forms.Padding(4, 3, 0, 0);
            this.neverAdoptTagsAndCatFromprofile.Size = new System.Drawing.Size(168, 22);
            this.neverAdoptTagsAndCatFromprofile.TabIndex = 16;
            this.neverAdoptTagsAndCatFromprofile.Text = "从不导入标签和分类";
            this.toolTip.SetToolTip(this.neverAdoptTagsAndCatFromprofile, "如果启用,在导入各种个性配置时,\r\n标签和分类不会被覆盖");
            this.neverAdoptTagsAndCatFromprofile.UseVisualStyleBackColor = true;
            // 
            // useDuplicateModWorkaround
            // 
            this.useDuplicateModWorkaround.AutoSize = true;
            this.useDuplicateModWorkaround.Location = new System.Drawing.Point(573, 3);
            this.useDuplicateModWorkaround.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.useDuplicateModWorkaround.Name = "useDuplicateModWorkaround";
            this.useDuplicateModWorkaround.Padding = new System.Windows.Forms.Padding(4, 3, 0, 0);
            this.useDuplicateModWorkaround.Size = new System.Drawing.Size(177, 22);
            this.useDuplicateModWorkaround.TabIndex = 17;
            this.useDuplicateModWorkaround.Text = "启用重复MOD解决方案";
            this.toolTip.SetToolTip(this.useDuplicateModWorkaround, "这是一个实验性功能,它提供了一些方法来管理多个具有相同ID的MOD.\n    它允许您选择重复项之一作为主要mod并禁用其他的,(通过重命名XComMod文件来实" +
        "现)。\n    ");
            this.useDuplicateModWorkaround.UseVisualStyleBackColor = true;
            // 
            // onlyUpdateEnabledAndNew
            // 
            this.onlyUpdateEnabledAndNew.AutoSize = true;
            this.onlyUpdateEnabledAndNew.Location = new System.Drawing.Point(288, 31);
            this.onlyUpdateEnabledAndNew.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.onlyUpdateEnabledAndNew.Name = "onlyUpdateEnabledAndNew";
            this.onlyUpdateEnabledAndNew.Padding = new System.Windows.Forms.Padding(4, 3, 0, 0);
            this.onlyUpdateEnabledAndNew.Size = new System.Drawing.Size(252, 22);
            this.onlyUpdateEnabledAndNew.TabIndex = 18;
            this.onlyUpdateEnabledAndNew.Text = "启动时只更新已启用的或新的mod";
            this.toolTip.SetToolTip(this.onlyUpdateEnabledAndNew, "如果你启用这个选项，每次启动AML,只有那些启用的或新的mod才会自动更新.\n如果你有许多mod，但很多都没有启用(备用)，开启此选项,可以大大减少启动AML时间" +
        "。");
            this.onlyUpdateEnabledAndNew.UseVisualStyleBackColor = true;
            // 
            // showHiddenEntriesCheckBox
            // 
            this.showHiddenEntriesCheckBox.AutoSize = true;
            this.showHiddenEntriesCheckBox.Location = new System.Drawing.Point(288, 3);
            this.showHiddenEntriesCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.showHiddenEntriesCheckBox.Name = "showHiddenEntriesCheckBox";
            this.showHiddenEntriesCheckBox.Padding = new System.Windows.Forms.Padding(4, 3, 0, 0);
            this.showHiddenEntriesCheckBox.Size = new System.Drawing.Size(117, 22);
            this.showHiddenEntriesCheckBox.TabIndex = 9;
            this.showHiddenEntriesCheckBox.Text = "显示隐藏MOD";
            this.toolTip.SetToolTip(this.showHiddenEntriesCheckBox, "如果启用,Mod列表中会显示隐藏MOD");
            this.showHiddenEntriesCheckBox.UseVisualStyleBackColor = true;
            // 
            // useTranslucentModListSelection
            // 
            this.useTranslucentModListSelection.AutoSize = true;
            this.useTranslucentModListSelection.Location = new System.Drawing.Point(4, 3);
            this.useTranslucentModListSelection.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.useTranslucentModListSelection.Name = "useTranslucentModListSelection";
            this.useTranslucentModListSelection.Padding = new System.Windows.Forms.Padding(4, 3, 0, 0);
            this.useTranslucentModListSelection.Size = new System.Drawing.Size(168, 22);
            this.useTranslucentModListSelection.TabIndex = 14;
            this.useTranslucentModListSelection.Text = "使用半透明选择样式";
            this.toolTip.SetToolTip(this.useTranslucentModListSelection, "如果启用,选择的MOD会使用半透明高亮选择条,\r\n而不是默认的不透明蓝色选择条");
            this.useTranslucentModListSelection.UseVisualStyleBackColor = true;
            // 
            // useSentry
            // 
            this.useSentry.AutoSize = true;
            this.useSentry.Location = new System.Drawing.Point(573, 3);
            this.useSentry.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.useSentry.Name = "useSentry";
            this.useSentry.Padding = new System.Windows.Forms.Padding(4, 3, 0, 0);
            this.useSentry.Size = new System.Drawing.Size(153, 22);
            this.useSentry.TabIndex = 17;
            this.useSentry.Text = "上传匿名错误报告";
            this.toolTip.SetToolTip(this.useSentry, "如果启用,一些严重错误的报告会自动\r\n匿名上传到 X2CommunityCore Sentry.io");
            this.useSentry.UseVisualStyleBackColor = true;
            // 
            // closeAfterLaunchCheckBox
            // 
            this.closeAfterLaunchCheckBox.AutoSize = true;
            this.closeAfterLaunchCheckBox.Location = new System.Drawing.Point(288, 31);
            this.closeAfterLaunchCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.closeAfterLaunchCheckBox.Name = "closeAfterLaunchCheckBox";
            this.closeAfterLaunchCheckBox.Padding = new System.Windows.Forms.Padding(4, 3, 0, 0);
            this.closeAfterLaunchCheckBox.Size = new System.Drawing.Size(108, 22);
            this.closeAfterLaunchCheckBox.TabIndex = 7;
            this.closeAfterLaunchCheckBox.Text = "打开后关闭";
            this.toolTip.SetToolTip(this.closeAfterLaunchCheckBox, "打开游戏后关闭AML");
            this.closeAfterLaunchCheckBox.UseVisualStyleBackColor = true;
            // 
            // searchForUpdatesCheckBox
            // 
            this.searchForUpdatesCheckBox.AutoSize = true;
            this.searchForUpdatesCheckBox.Location = new System.Drawing.Point(4, 3);
            this.searchForUpdatesCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.searchForUpdatesCheckBox.Name = "searchForUpdatesCheckBox";
            this.searchForUpdatesCheckBox.Padding = new System.Windows.Forms.Padding(4, 3, 0, 0);
            this.searchForUpdatesCheckBox.Size = new System.Drawing.Size(93, 22);
            this.searchForUpdatesCheckBox.TabIndex = 10;
            this.searchForUpdatesCheckBox.Text = "检查更新";
            this.toolTip.SetToolTip(this.searchForUpdatesCheckBox, "在启动时检测新版本\r\n汉化版已禁用");
            this.searchForUpdatesCheckBox.UseVisualStyleBackColor = true;
            this.searchForUpdatesCheckBox.CheckedChanged += new System.EventHandler(this.searchForUpdatesCheckBox_CheckedChanged);
            // 
            // checkForPreReleaseUpdates
            // 
            this.checkForPreReleaseUpdates.AutoSize = true;
            this.checkForPreReleaseUpdates.Location = new System.Drawing.Point(288, 3);
            this.checkForPreReleaseUpdates.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkForPreReleaseUpdates.Name = "checkForPreReleaseUpdates";
            this.checkForPreReleaseUpdates.Padding = new System.Windows.Forms.Padding(4, 3, 0, 0);
            this.checkForPreReleaseUpdates.Size = new System.Drawing.Size(153, 22);
            this.checkForPreReleaseUpdates.TabIndex = 18;
            this.checkForPreReleaseUpdates.Text = "是否包含测试版本";
            this.toolTip.SetToolTip(this.checkForPreReleaseUpdates, "如果你想得到预发布版本的更新通知，请启用该选项。");
            this.checkForPreReleaseUpdates.UseVisualStyleBackColor = true;
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 10000;
            this.toolTip.InitialDelay = 300;
            this.toolTip.IsBalloon = true;
            this.toolTip.ReshowDelay = 100;
            // 
            // allowMutipleInstances
            // 
            this.allowMutipleInstances.AutoSize = true;
            this.allowMutipleInstances.Location = new System.Drawing.Point(4, 31);
            this.allowMutipleInstances.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.allowMutipleInstances.Name = "allowMutipleInstances";
            this.allowMutipleInstances.Padding = new System.Windows.Forms.Padding(4, 3, 0, 0);
            this.allowMutipleInstances.Size = new System.Drawing.Size(87, 22);
            this.allowMutipleInstances.TabIndex = 19;
            this.allowMutipleInstances.Text = "AML多开";
            this.toolTip.SetToolTip(this.allowMutipleInstances, "如果启用,同一时间可运行多个AML管理器");
            this.allowMutipleInstances.UseVisualStyleBackColor = true;
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOK.Location = new System.Drawing.Point(604, 579);
            this.bOK.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(139, 28);
            this.bOK.TabIndex = 12;
            this.bOK.Text = "确定";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(751, 579);
            this.bCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(139, 28);
            this.bCancel.TabIndex = 11;
            this.bCancel.Text = "取消";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.tableLayoutPanel1);
            this.groupBox3.Location = new System.Drawing.Point(16, 247);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Size = new System.Drawing.Size(873, 89);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "特性";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.searchForUpdatesCheckBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkForPreReleaseUpdates, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.useSentry, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.allowMutipleInstances, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.closeAfterLaunchCheckBox, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 22);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(855, 60);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.tableLayoutPanel4);
            this.groupBox4.Location = new System.Drawing.Point(16, 444);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox4.Size = new System.Drawing.Size(873, 128);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "UI界面";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel4.Controls.Add(this.useTranslucentModListSelection, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.showHiddenEntriesCheckBox, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.hideRunX2Button, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.hideChallengeModeButton, 0, 2);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(4, 23);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(855, 98);
            this.tableLayoutPanel4.TabIndex = 6;
            // 
            // hideRunX2Button
            // 
            this.hideRunX2Button.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this.hideRunX2Button, 2);
            this.hideRunX2Button.Location = new System.Drawing.Point(4, 31);
            this.hideRunX2Button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.hideRunX2Button.Name = "hideRunX2Button";
            this.hideRunX2Button.Padding = new System.Windows.Forms.Padding(4, 3, 0, 0);
            this.hideRunX2Button.Size = new System.Drawing.Size(287, 22);
            this.hideRunX2Button.TabIndex = 15;
            this.hideRunX2Button.Text = "隐藏\"启动XCOM2\"按钮(如果WOTC可用)";
            this.hideRunX2Button.UseVisualStyleBackColor = true;
            // 
            // hideChallengeModeButton
            // 
            this.hideChallengeModeButton.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this.hideChallengeModeButton, 2);
            this.hideChallengeModeButton.Location = new System.Drawing.Point(4, 59);
            this.hideChallengeModeButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.hideChallengeModeButton.Name = "hideChallengeModeButton";
            this.hideChallengeModeButton.Padding = new System.Windows.Forms.Padding(4, 3, 0, 0);
            this.hideChallengeModeButton.Size = new System.Drawing.Size(199, 22);
            this.hideChallengeModeButton.TabIndex = 16;
            this.hideChallengeModeButton.Text = "隐藏\"启动挑战模式\"按钮";
            this.hideChallengeModeButton.UseVisualStyleBackColor = true;
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 621);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = global::XCOM2Launcher.Properties.Resources.xcom;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "SettingsDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置";
            this.Shown += new System.EventHandler(this.SettingsDialog_Shown);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.CheckBox closeAfterLaunchCheckBox;
        private System.Windows.Forms.CheckBox searchForUpdatesCheckBox;
        private System.Windows.Forms.CheckBox showHiddenEntriesCheckBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button browseGamePathButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox modPathsListbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox gamePathTextBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button addModPathButton;
        private System.Windows.Forms.Button removeModPathButton;
        private XCOM2Launcher.UserElements.AutoCompleteTextBox argumentsTextBox;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox autoNumberModIndexesCheckBox;
        private System.Windows.Forms.CheckBox neverAdoptTagsAndCatFromprofile;
        private System.Windows.Forms.CheckBox ShowQuickLaunchArgumentsToggle;
		private System.Windows.Forms.Button bCancel;
		private System.Windows.Forms.Button bOK;
		private System.Windows.Forms.CheckBox checkForPreReleaseUpdates;
		private System.Windows.Forms.CheckBox useSentry;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.CheckBox allowMutipleInstances;
		private XCOM2Launcher.UserElements.AutoCompleteTextBox quickArgumentsTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox useDuplicateModWorkaround;
		private System.Windows.Forms.CheckBox useModSpecifiedCategoriesCheckBox;
		private System.Windows.Forms.CheckBox useTranslucentModListSelection;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.CheckBox hideRunX2Button;
        private System.Windows.Forms.CheckBox hideChallengeModeButton;
        private System.Windows.Forms.CheckBox onlyUpdateEnabledAndNew;
		private System.Windows.Forms.CheckBox updateModsOnStartup;
	}
}