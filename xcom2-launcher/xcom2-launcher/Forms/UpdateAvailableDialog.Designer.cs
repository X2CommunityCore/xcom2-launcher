namespace XCOM2Launcher.Forms
{
    partial class UpdateAvailableDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateAvailableDialog));
            this.version_current_label = new System.Windows.Forms.Label();
            this.version_new_label = new System.Windows.Forms.Label();
            this.version_current_value_label = new System.Windows.Forms.Label();
            this.version_new_value_label = new System.Windows.Forms.Label();
            this.changelog_label = new System.Windows.Forms.Label();
            this.filesize_label = new System.Windows.Forms.Label();
            this.filesize_value_label = new System.Windows.Forms.Label();
            this.date_label = new System.Windows.Forms.Label();
            this.date_value_label = new System.Windows.Forms.Label();
            this.show_button = new System.Windows.Forms.Button();
			this.bClose = new System.Windows.Forms.Button();
            this.lBetaVersion = new System.Windows.Forms.Label();
            this.releaseNoteBrowser = new System.Windows.Forms.WebBrowser();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // version_current_label
            // 
            this.version_current_label.AutoSize = true;
            this.version_current_label.Location = new System.Drawing.Point(12, 14);
            this.version_current_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.version_current_label.Name = "version_current_label";
            this.version_current_label.Size = new System.Drawing.Size(75, 15);
            this.version_current_label.TabIndex = 2;
            this.version_current_label.Text = "当前版本:";
            // 
            // version_new_label
            // 
            this.version_new_label.AutoSize = true;
            this.version_new_label.Location = new System.Drawing.Point(12, 35);
            this.version_new_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.version_new_label.Name = "version_new_label";
            this.version_new_label.Size = new System.Drawing.Size(75, 15);
            this.version_new_label.TabIndex = 6;
            this.version_new_label.Text = "最新版本:";
            // 
            // version_current_value_label
            // 
            this.version_current_value_label.AutoSize = true;
            this.version_current_value_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.version_current_value_label.Location = new System.Drawing.Point(149, 14);
            this.version_current_value_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.version_current_value_label.Name = "version_current_value_label";
            this.version_current_value_label.Size = new System.Drawing.Size(105, 17);
            this.version_current_value_label.TabIndex = 7;
            this.version_current_value_label.Text = "1.2.3-alpha.2";
            // 
            // version_new_value_label
            // 
            this.version_new_value_label.AutoSize = true;
            this.version_new_value_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.version_new_value_label.Location = new System.Drawing.Point(149, 35);
            this.version_new_value_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.version_new_value_label.Name = "version_new_value_label";
            this.version_new_value_label.Size = new System.Drawing.Size(83, 17);
            this.version_new_value_label.TabIndex = 7;
            this.version_new_value_label.Text = "1.2.3-beta";
            // 
            // changelog_label
            // 
            this.changelog_label.AutoSize = true;
            this.changelog_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changelog_label.Location = new System.Drawing.Point(12, 63);
            this.changelog_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.changelog_label.Name = "changelog_label";
            this.changelog_label.Size = new System.Drawing.Size(197, 25);
            this.changelog_label.TabIndex = 8;
            this.changelog_label.Text = "Release 1.2.3 Final";
            // 
            // filesize_label
            // 
            this.filesize_label.AutoSize = true;
            this.filesize_label.Location = new System.Drawing.Point(343, 14);
            this.filesize_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.filesize_label.Name = "filesize_label";
            this.filesize_label.Size = new System.Drawing.Size(45, 15);
            this.filesize_label.TabIndex = 10;
            this.filesize_label.Text = "大小:";
            // 
            // filesize_value_label
            // 
            this.filesize_value_label.AutoSize = true;
            this.filesize_value_label.Location = new System.Drawing.Point(408, 14);
            this.filesize_value_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.filesize_value_label.Name = "filesize_value_label";
            this.filesize_value_label.Size = new System.Drawing.Size(97, 15);
            this.filesize_value_label.TabIndex = 11;
            this.filesize_value_label.Text = "暂无可用下载";
            // 
            // date_label
            // 
            this.date_label.AutoSize = true;
            this.date_label.Location = new System.Drawing.Point(343, 35);
            this.date_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.date_label.Name = "date_label";
            this.date_label.Size = new System.Drawing.Size(45, 15);
            this.date_label.TabIndex = 10;
            this.date_label.Text = "日期:";
            // 
            // date_value_label
            // 
            this.date_value_label.AutoSize = true;
            this.date_value_label.Location = new System.Drawing.Point(408, 35);
            this.date_value_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.date_value_label.Name = "date_value_label";
            this.date_value_label.Size = new System.Drawing.Size(87, 15);
            this.date_value_label.TabIndex = 11;
            this.date_value_label.Text = "00.00.0000";
            // 
            // show_button
            // 
            this.show_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.show_button.Location = new System.Drawing.Point(489, 284);
            this.show_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.show_button.Name = "show_button";
            this.show_button.Size = new System.Drawing.Size(135, 27);
            this.show_button.TabIndex = 10;
            this.show_button.Text = "GitHub上查看";
            this.show_button.UseVisualStyleBackColor = true;
            this.show_button.Click += new System.EventHandler(this.show_button_Click);
            // 
            // close_button
            // 
			this.bClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bClose.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bClose.Location = new System.Drawing.Point(474, 246);
			this.bClose.Name = "bClose";
			this.bClose.Size = new System.Drawing.Size(75, 23);
			this.bClose.TabIndex = 1;
			this.bClose.Text = "关闭";
			this.bClose.UseVisualStyleBackColor = true;
			// 
            // 
            // lBetaVersion
            // 
            this.lBetaVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lBetaVersion.AutoSize = true;
            this.lBetaVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lBetaVersion.ForeColor = System.Drawing.Color.Red;
            this.lBetaVersion.Location = new System.Drawing.Point(12, 290);
            this.lBetaVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lBetaVersion.Name = "lBetaVersion";
            this.lBetaVersion.Size = new System.Drawing.Size(238, 17);
            this.lBetaVersion.TabIndex = 11;
            this.lBetaVersion.Text = "这是一个抢先发行版,可能会不稳定";
            // 
            // releaseNoteBrowser
            // 
            this.releaseNoteBrowser.AllowWebBrowserDrop = false;
            this.releaseNoteBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.releaseNoteBrowser.Location = new System.Drawing.Point(0, 0);
            this.releaseNoteBrowser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.releaseNoteBrowser.MinimumSize = new System.Drawing.Size(27, 23);
            this.releaseNoteBrowser.Name = "releaseNoteBrowser";
            this.releaseNoteBrowser.Size = new System.Drawing.Size(712, 182);
            this.releaseNoteBrowser.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.releaseNoteBrowser);
            this.panel1.Location = new System.Drawing.Point(17, 92);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(714, 184);
            this.panel1.TabIndex = 13;
            // 
            // UpdateAvailableDialog
            // 
            this.AcceptButton = this.show_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 324);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.date_value_label);
            this.Controls.Add(this.filesize_value_label);
            this.Controls.Add(this.filesize_label);
            this.Controls.Add(this.date_label);
            this.Controls.Add(this.version_new_value_label);
            this.Controls.Add(this.version_current_label);
            this.Controls.Add(this.version_current_value_label);
            this.Controls.Add(this.version_new_label);
            this.Controls.Add(this.show_button);
            this.Controls.Add(this.changelog_label);
			this.Controls.Add(this.bClose);
            this.Controls.Add(this.lBetaVersion);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "UpdateAvailableDialog";
            this.Text = "更新可用!";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label version_current_label;
        private System.Windows.Forms.Label version_new_label;
        private System.Windows.Forms.Label version_current_value_label;
        private System.Windows.Forms.Label version_new_value_label;
        private System.Windows.Forms.Label changelog_label;
        private System.Windows.Forms.Label filesize_label;
        private System.Windows.Forms.Label filesize_value_label;
        private System.Windows.Forms.Button show_button;
        private System.Windows.Forms.Label date_label;
        private System.Windows.Forms.Label date_value_label;
        private System.Windows.Forms.Button bClose;
		private System.Windows.Forms.Label lBetaVersion;
		private System.Windows.Forms.WebBrowser releaseNoteBrowser;
        private System.Windows.Forms.Panel panel1;
    }
}