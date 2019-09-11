namespace XCOM2Launcher.Forms {
	partial class WelcomeDialog {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeDialog));
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.bContinue = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.rSentryEnabled = new System.Windows.Forms.RadioButton();
			this.rSentryDisabled = new System.Windows.Forms.RadioButton();
			this.label5 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(13, 181);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(510, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Please help us to improve AML, by enabling anonymous error reporting!";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(146, 68);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(245, 110);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// bContinue
			// 
			this.bContinue.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.bContinue.Location = new System.Drawing.Point(216, 278);
			this.bContinue.Name = "bContinue";
			this.bContinue.Size = new System.Drawing.Size(104, 25);
			this.bContinue.TabIndex = 2;
			this.bContinue.Text = "Continue";
			this.bContinue.UseVisualStyleBackColor = true;
			this.bContinue.Click += new System.EventHandler(this.bContinue_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(92, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(352, 26);
			this.label2.TabIndex = 3;
			this.label2.Text = "You are seeing this dialog, because you are starting AML for the first time \r\nor " +
    "upgraded to a new version.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(15, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(507, 20);
			this.label3.TabIndex = 4;
			this.label3.Text = "Welcome to the Alternative Mod Launcher!";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// rSentryEnabled
			// 
			this.rSentryEnabled.Appearance = System.Windows.Forms.Appearance.Button;
			this.rSentryEnabled.Checked = true;
			this.rSentryEnabled.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.rSentryEnabled.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rSentryEnabled.Location = new System.Drawing.Point(287, 207);
			this.rSentryEnabled.Name = "rSentryEnabled";
			this.rSentryEnabled.Size = new System.Drawing.Size(104, 24);
			this.rSentryEnabled.TabIndex = 6;
			this.rSentryEnabled.TabStop = true;
			this.rSentryEnabled.Text = "Enable";
			this.rSentryEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.rSentryEnabled.UseVisualStyleBackColor = true;
			// 
			// rSentryDisabled
			// 
			this.rSentryDisabled.Appearance = System.Windows.Forms.Appearance.Button;
			this.rSentryDisabled.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
			this.rSentryDisabled.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rSentryDisabled.Location = new System.Drawing.Point(146, 207);
			this.rSentryDisabled.Name = "rSentryDisabled";
			this.rSentryDisabled.Size = new System.Drawing.Size(104, 24);
			this.rSentryDisabled.TabIndex = 7;
			this.rSentryDisabled.Text = "Disable";
			this.rSentryDisabled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.rSentryDisabled.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.Location = new System.Drawing.Point(13, 233);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(510, 36);
			this.label5.TabIndex = 9;
			this.label5.Text = "Critical errors or other potential issues are then automatically reported to our " +
    "X2CommunityCore Sentry.io account. You can enable/disable this feature at any ti" +
    "me in the Settings dialog.";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// WelcomeDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(534, 313);
			this.ControlBox = false;
			this.Controls.Add(this.label5);
			this.Controls.Add(this.rSentryDisabled);
			this.Controls.Add(this.rSentryEnabled);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.bContinue);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WelcomeDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Welcome to AML";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button bContinue;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RadioButton rSentryEnabled;
		private System.Windows.Forms.RadioButton rSentryDisabled;
		private System.Windows.Forms.Label label5;
	}
}