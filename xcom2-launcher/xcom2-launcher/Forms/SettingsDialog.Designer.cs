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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.modPathsListbox = new System.Windows.Forms.ListBox();
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
            this.searchForUpdatesCheckBox = new System.Windows.Forms.CheckBox();
            this.showHiddenEntriesCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.closeAfterLaunchCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.moveCategoryUpButton = new System.Windows.Forms.Button();
            this.moveCategoryDownButton = new System.Windows.Forms.Button();
            this.renameCategoryButton = new System.Windows.Forms.Button();
            this.removeCategoryButton = new System.Windows.Forms.Button();
            this.categoriesListBox = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Location = new System.Drawing.Point(12, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(655, 176);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Game";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.modPathsListbox, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.gamePathTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.browseGamePathButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.argumentsTextBox, 1, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(649, 157);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Arguments";
            // 
            // modPathsListbox
            // 
            this.modPathsListbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modPathsListbox.FormattingEnabled = true;
            this.modPathsListbox.Location = new System.Drawing.Point(103, 33);
            this.modPathsListbox.Name = "modPathsListbox";
            this.modPathsListbox.Size = new System.Drawing.Size(470, 90);
            this.modPathsListbox.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Base Path";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Mod Directories";
            // 
            // gamePathTextBox
            // 
            this.gamePathTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gamePathTextBox.Location = new System.Drawing.Point(103, 3);
            this.gamePathTextBox.Name = "gamePathTextBox";
            this.gamePathTextBox.Size = new System.Drawing.Size(470, 20);
            this.gamePathTextBox.TabIndex = 10;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.addModPathButton);
            this.flowLayoutPanel1.Controls.Add(this.removeModPathButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(576, 30);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(73, 96);
            this.flowLayoutPanel1.TabIndex = 12;
            // 
            // addModPathButton
            // 
            this.addModPathButton.Location = new System.Drawing.Point(3, 3);
            this.addModPathButton.Name = "addModPathButton";
            this.addModPathButton.Size = new System.Drawing.Size(67, 24);
            this.addModPathButton.TabIndex = 6;
            this.addModPathButton.Text = "Add";
            this.addModPathButton.UseVisualStyleBackColor = true;
            // 
            // removeModPathButton
            // 
            this.removeModPathButton.Location = new System.Drawing.Point(3, 33);
            this.removeModPathButton.Name = "removeModPathButton";
            this.removeModPathButton.Size = new System.Drawing.Size(67, 24);
            this.removeModPathButton.TabIndex = 8;
            this.removeModPathButton.Text = "Remove";
            this.removeModPathButton.UseVisualStyleBackColor = true;
            // 
            // browseGamePathButton
            // 
            this.browseGamePathButton.Location = new System.Drawing.Point(579, 3);
            this.browseGamePathButton.Name = "browseGamePathButton";
            this.browseGamePathButton.Size = new System.Drawing.Size(67, 24);
            this.browseGamePathButton.TabIndex = 14;
            this.browseGamePathButton.Text = "Browse";
            this.browseGamePathButton.UseVisualStyleBackColor = true;
            // 
            // argumentsTextBox
            // 
            this.argumentsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.argumentsTextBox.Location = new System.Drawing.Point(103, 129);
            this.argumentsTextBox.Name = "argumentsTextBox";
            this.argumentsTextBox.Size = new System.Drawing.Size(470, 20);
            this.argumentsTextBox.TabIndex = 15;
            this.argumentsTextBox.Values = null;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tableLayoutPanel3);
            this.groupBox2.Location = new System.Drawing.Point(15, 190);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(655, 268);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Launcher";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.searchForUpdatesCheckBox, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.showHiddenEntriesCheckBox, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.closeAfterLaunchCheckBox, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel2, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.categoriesListBox, 1, 3);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(649, 249);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // searchForUpdatesCheckBox
            // 
            this.searchForUpdatesCheckBox.AutoSize = true;
            this.searchForUpdatesCheckBox.Location = new System.Drawing.Point(103, 47);
            this.searchForUpdatesCheckBox.Name = "searchForUpdatesCheckBox";
            this.searchForUpdatesCheckBox.Size = new System.Drawing.Size(116, 16);
            this.searchForUpdatesCheckBox.TabIndex = 10;
            this.searchForUpdatesCheckBox.Text = "Search for updates";
            this.searchForUpdatesCheckBox.UseVisualStyleBackColor = true;
            // 
            // showHiddenEntriesCheckBox
            // 
            this.showHiddenEntriesCheckBox.AutoSize = true;
            this.showHiddenEntriesCheckBox.Location = new System.Drawing.Point(103, 3);
            this.showHiddenEntriesCheckBox.Name = "showHiddenEntriesCheckBox";
            this.showHiddenEntriesCheckBox.Size = new System.Drawing.Size(122, 16);
            this.showHiddenEntriesCheckBox.TabIndex = 9;
            this.showHiddenEntriesCheckBox.Text = "Show hidden entries";
            this.showHiddenEntriesCheckBox.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 13);
            this.label6.TabIndex = 4;
            // 
            // closeAfterLaunchCheckBox
            // 
            this.closeAfterLaunchCheckBox.AutoSize = true;
            this.closeAfterLaunchCheckBox.Location = new System.Drawing.Point(103, 25);
            this.closeAfterLaunchCheckBox.Name = "closeAfterLaunchCheckBox";
            this.closeAfterLaunchCheckBox.Size = new System.Drawing.Size(111, 16);
            this.closeAfterLaunchCheckBox.TabIndex = 7;
            this.closeAfterLaunchCheckBox.Text = "Close after launch";
            this.closeAfterLaunchCheckBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Categories";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.moveCategoryUpButton);
            this.flowLayoutPanel2.Controls.Add(this.moveCategoryDownButton);
            this.flowLayoutPanel2.Controls.Add(this.renameCategoryButton);
            this.flowLayoutPanel2.Controls.Add(this.removeCategoryButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(576, 66);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(73, 183);
            this.flowLayoutPanel2.TabIndex = 13;
            // 
            // moveCategoryUpButton
            // 
            this.moveCategoryUpButton.Location = new System.Drawing.Point(3, 3);
            this.moveCategoryUpButton.Name = "moveCategoryUpButton";
            this.moveCategoryUpButton.Size = new System.Drawing.Size(67, 24);
            this.moveCategoryUpButton.TabIndex = 10;
            this.moveCategoryUpButton.Text = "Up";
            this.moveCategoryUpButton.UseVisualStyleBackColor = true;
            // 
            // moveCategoryDownButton
            // 
            this.moveCategoryDownButton.Location = new System.Drawing.Point(3, 33);
            this.moveCategoryDownButton.Name = "moveCategoryDownButton";
            this.moveCategoryDownButton.Size = new System.Drawing.Size(67, 24);
            this.moveCategoryDownButton.TabIndex = 11;
            this.moveCategoryDownButton.Text = "Down";
            this.moveCategoryDownButton.UseVisualStyleBackColor = true;
            // 
            // renameCategoryButton
            // 
            this.renameCategoryButton.Location = new System.Drawing.Point(3, 63);
            this.renameCategoryButton.Name = "renameCategoryButton";
            this.renameCategoryButton.Size = new System.Drawing.Size(67, 24);
            this.renameCategoryButton.TabIndex = 12;
            this.renameCategoryButton.Text = "Rename";
            this.renameCategoryButton.UseVisualStyleBackColor = true;
            // 
            // removeCategoryButton
            // 
            this.removeCategoryButton.Location = new System.Drawing.Point(3, 93);
            this.removeCategoryButton.Name = "removeCategoryButton";
            this.removeCategoryButton.Size = new System.Drawing.Size(67, 24);
            this.removeCategoryButton.TabIndex = 13;
            this.removeCategoryButton.Text = "Remove";
            this.removeCategoryButton.UseVisualStyleBackColor = true;
            // 
            // categoriesListBox
            // 
            this.categoriesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.categoriesListBox.FormattingEnabled = true;
            this.categoriesListBox.Location = new System.Drawing.Point(103, 69);
            this.categoriesListBox.Name = "categoriesListBox";
            this.categoriesListBox.Size = new System.Drawing.Size(470, 173);
            this.categoriesListBox.TabIndex = 11;
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 470);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = global::XCOM2Launcher.Properties.Resources.xcom;
            this.Name = "SettingsDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void PropertyGrid1_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox closeAfterLaunchCheckBox;
        private System.Windows.Forms.CheckBox searchForUpdatesCheckBox;
        private System.Windows.Forms.CheckBox showHiddenEntriesCheckBox;
        private System.Windows.Forms.ListBox categoriesListBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button browseGamePathButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox modPathsListbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox gamePathTextBox;
        private UserElements.AutoCompleteTextBox argumentsTextBox;
        private System.Windows.Forms.Button moveCategoryUpButton;
        private System.Windows.Forms.Button moveCategoryDownButton;
        private System.Windows.Forms.Button renameCategoryButton;
        private System.Windows.Forms.Button removeCategoryButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button addModPathButton;
        private System.Windows.Forms.Button removeModPathButton;
    }
}