namespace XCOM2Launcher.Forms {
    partial class UnhandledExceptionDialog {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnhandledExceptionDialog));
			this.tException = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.bClose = new System.Windows.Forms.Button();
			this.linkGithub = new System.Windows.Forms.LinkLabel();
			this.linkDiscord = new System.Windows.Forms.LinkLabel();
			this.linkAppFolder = new System.Windows.Forms.LinkLabel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.bCopy = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tException
			// 
			this.tException.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tException.Location = new System.Drawing.Point(12, 70);
			this.tException.Multiline = true;
			this.tException.Name = "tException";
			this.tException.ReadOnly = true;
			this.tException.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tException.Size = new System.Drawing.Size(542, 221);
			this.tException.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(542, 34);
			this.label1.TabIndex = 1;
			this.label1.Text = "An unexpected problem caused AML to stop working correctly.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(566, 24);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// bClose
			// 
			this.bClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bClose.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bClose.Location = new System.Drawing.Point(440, 3);
			this.bClose.Name = "bClose";
			this.bClose.Size = new System.Drawing.Size(114, 23);
			this.bClose.TabIndex = 4;
			this.bClose.Text = "&Close";
			this.bClose.UseVisualStyleBackColor = true;
			// 
			// linkGithub
			// 
			this.linkGithub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.linkGithub.AutoSize = true;
			this.linkGithub.Location = new System.Drawing.Point(9, 294);
			this.linkGithub.Name = "linkGithub";
			this.linkGithub.Size = new System.Drawing.Size(117, 13);
			this.linkGithub.TabIndex = 5;
			this.linkGithub.TabStop = true;
			this.linkGithub.Text = "Report issue on GitHub";
			this.linkGithub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGithub_LinkClicked);
			// 
			// linkDiscord
			// 
			this.linkDiscord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.linkDiscord.AutoSize = true;
			this.linkDiscord.Location = new System.Drawing.Point(437, 294);
			this.linkDiscord.Name = "linkDiscord";
			this.linkDiscord.Size = new System.Drawing.Size(117, 13);
			this.linkDiscord.TabIndex = 6;
			this.linkDiscord.TabStop = true;
			this.linkDiscord.Text = "Ask for help on Discord";
			this.linkDiscord.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDiscord_LinkClicked);
			// 
			// linkAppFolder
			// 
			this.linkAppFolder.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.linkAppFolder.AutoSize = true;
			this.linkAppFolder.Location = new System.Drawing.Point(165, 294);
			this.linkAppFolder.Name = "linkAppFolder";
			this.linkAppFolder.Size = new System.Drawing.Size(237, 13);
			this.linkAppFolder.TabIndex = 7;
			this.linkAppFolder.TabStop = true;
			this.linkAppFolder.Text = "See \'AML.log\' and \'error.log\' for additional details.";
			this.linkAppFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAppFolder_LinkClicked);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.bCopy);
			this.panel1.Controls.Add(this.bClose);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 316);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(566, 31);
			this.panel1.TabIndex = 8;
			// 
			// bCopy
			// 
			this.bCopy.Location = new System.Drawing.Point(12, 3);
			this.bCopy.Name = "bCopy";
			this.bCopy.Size = new System.Drawing.Size(114, 23);
			this.bCopy.TabIndex = 5;
			this.bCopy.Text = "C&opy to clipboard";
			this.bCopy.UseVisualStyleBackColor = true;
			this.bCopy.Click += new System.EventHandler(this.bCopy_Click);
			// 
			// UnhandledExceptionDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(566, 347);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.linkAppFolder);
			this.Controls.Add(this.linkDiscord);
			this.Controls.Add(this.linkGithub);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tException);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UnhandledExceptionDialog";
			this.Text = "Critical error";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tException;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.LinkLabel linkGithub;
        private System.Windows.Forms.LinkLabel linkDiscord;
        private System.Windows.Forms.LinkLabel linkAppFolder;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bCopy;
    }
}