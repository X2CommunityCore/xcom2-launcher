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
			this.close_button = new System.Windows.Forms.Button();
			this.lBetaVersion = new System.Windows.Forms.Label();
			this.releaseNoteBrowser = new System.Windows.Forms.WebBrowser();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// version_current_label
			// 
			this.version_current_label.AutoSize = true;
			this.version_current_label.Location = new System.Drawing.Point(9, 12);
			this.version_current_label.Name = "version_current_label";
			this.version_current_label.Size = new System.Drawing.Size(81, 13);
			this.version_current_label.TabIndex = 2;
			this.version_current_label.Text = "Current version:";
			// 
			// version_new_label
			// 
			this.version_new_label.AutoSize = true;
			this.version_new_label.Location = new System.Drawing.Point(9, 30);
			this.version_new_label.Name = "version_new_label";
			this.version_new_label.Size = new System.Drawing.Size(69, 13);
			this.version_new_label.TabIndex = 6;
			this.version_new_label.Text = "New version:";
			// 
			// version_current_value_label
			// 
			this.version_current_value_label.AutoSize = true;
			this.version_current_value_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.version_current_value_label.Location = new System.Drawing.Point(112, 12);
			this.version_current_value_label.Name = "version_current_value_label";
			this.version_current_value_label.Size = new System.Drawing.Size(82, 13);
			this.version_current_value_label.TabIndex = 7;
			this.version_current_value_label.Text = "1.2.3-alpha.2";
			// 
			// version_new_value_label
			// 
			this.version_new_value_label.AutoSize = true;
			this.version_new_value_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.version_new_value_label.Location = new System.Drawing.Point(112, 30);
			this.version_new_value_label.Name = "version_new_value_label";
			this.version_new_value_label.Size = new System.Drawing.Size(65, 13);
			this.version_new_value_label.TabIndex = 7;
			this.version_new_value_label.Text = "1.2.3-beta";
			// 
			// changelog_label
			// 
			this.changelog_label.AutoSize = true;
			this.changelog_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.changelog_label.Location = new System.Drawing.Point(9, 55);
			this.changelog_label.Name = "changelog_label";
			this.changelog_label.Size = new System.Drawing.Size(164, 20);
			this.changelog_label.TabIndex = 8;
			this.changelog_label.Text = "Release 1.2.3 Final";
			// 
			// filesize_label
			// 
			this.filesize_label.AutoSize = true;
			this.filesize_label.Location = new System.Drawing.Point(257, 12);
			this.filesize_label.Name = "filesize_label";
			this.filesize_label.Size = new System.Drawing.Size(30, 13);
			this.filesize_label.TabIndex = 10;
			this.filesize_label.Text = "Size:";
			// 
			// filesize_value_label
			// 
			this.filesize_value_label.AutoSize = true;
			this.filesize_value_label.Location = new System.Drawing.Point(306, 12);
			this.filesize_value_label.Name = "filesize_value_label";
			this.filesize_value_label.Size = new System.Drawing.Size(135, 13);
			this.filesize_value_label.TabIndex = 11;
			this.filesize_value_label.Text = "No download available yet.";
			// 
			// date_label
			// 
			this.date_label.AutoSize = true;
			this.date_label.Location = new System.Drawing.Point(257, 30);
			this.date_label.Name = "date_label";
			this.date_label.Size = new System.Drawing.Size(33, 13);
			this.date_label.TabIndex = 10;
			this.date_label.Text = "Date:";
			// 
			// date_value_label
			// 
			this.date_value_label.AutoSize = true;
			this.date_value_label.Location = new System.Drawing.Point(306, 30);
			this.date_value_label.Name = "date_value_label";
			this.date_value_label.Size = new System.Drawing.Size(61, 13);
			this.date_value_label.TabIndex = 11;
			this.date_value_label.Text = "00.00.0000";
			// 
			// show_button
			// 
			this.show_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.show_button.Location = new System.Drawing.Point(367, 246);
			this.show_button.Name = "show_button";
			this.show_button.Size = new System.Drawing.Size(101, 23);
			this.show_button.TabIndex = 10;
			this.show_button.Text = "Show on GitHub";
			this.show_button.UseVisualStyleBackColor = true;
			this.show_button.Click += new System.EventHandler(this.show_button_Click);
			// 
			// close_button
			// 
			this.close_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.close_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.close_button.Location = new System.Drawing.Point(474, 246);
			this.close_button.Name = "close_button";
			this.close_button.Size = new System.Drawing.Size(75, 23);
			this.close_button.TabIndex = 1;
			this.close_button.Text = "Close";
			this.close_button.UseVisualStyleBackColor = true;
			this.close_button.Click += new System.EventHandler(this.close_button_Click);
			// 
			// lBetaVersion
			// 
			this.lBetaVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lBetaVersion.AutoSize = true;
			this.lBetaVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lBetaVersion.ForeColor = System.Drawing.Color.Red;
			this.lBetaVersion.Location = new System.Drawing.Point(9, 251);
			this.lBetaVersion.Name = "lBetaVersion";
			this.lBetaVersion.Size = new System.Drawing.Size(346, 13);
			this.lBetaVersion.TabIndex = 11;
			this.lBetaVersion.Text = "This is a Pre-Release version, and thus might not be stable.";
			// 
			// releaseNoteBrowser
			// 
			this.releaseNoteBrowser.AllowWebBrowserDrop = false;
			this.releaseNoteBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.releaseNoteBrowser.Location = new System.Drawing.Point(0, 0);
			this.releaseNoteBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.releaseNoteBrowser.Name = "releaseNoteBrowser";
			this.releaseNoteBrowser.Size = new System.Drawing.Size(534, 158);
			this.releaseNoteBrowser.TabIndex = 12;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.releaseNoteBrowser);
			this.panel1.Location = new System.Drawing.Point(13, 80);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(536, 160);
			this.panel1.TabIndex = 13;
			// 
			// UpdateAvailableDialog
			// 
			this.AcceptButton = this.show_button;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(561, 281);
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
			this.Controls.Add(this.close_button);
			this.Controls.Add(this.lBetaVersion);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "UpdateAvailableDialog";
			this.Text = "Update available!";
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
        private System.Windows.Forms.Button close_button;
		private System.Windows.Forms.Label lBetaVersion;
		private System.Windows.Forms.WebBrowser releaseNoteBrowser;
        private System.Windows.Forms.Panel panel1;
    }
}