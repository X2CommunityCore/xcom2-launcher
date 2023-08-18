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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rGameXCom2 = new System.Windows.Forms.RadioButton();
            this.rGameChimera = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(481, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please help us to improve AML, by enabling anonymous error reporting!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(162, 68);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(245, 110);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // bContinue
            // 
            this.bContinue.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bContinue.AutoSize = true;
            this.bContinue.Enabled = false;
            this.bContinue.Image = ((System.Drawing.Image)(resources.GetObject("bContinue.Image")));
            this.bContinue.Location = new System.Drawing.Point(218, 453);
            this.bContinue.Name = "bContinue";
            this.bContinue.Size = new System.Drawing.Size(132, 33);
            this.bContinue.TabIndex = 2;
            this.bContinue.UseVisualStyleBackColor = true;
            this.bContinue.Click += new System.EventHandler(this.bContinue_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(16, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(541, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "You are seeing this dialog, because you are starting AML for the first time.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(541, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Welcome to the Alternative Mod Launcher!";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rSentryEnabled
            // 
            this.rSentryEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rSentryEnabled.Appearance = System.Windows.Forms.Appearance.Button;
            this.rSentryEnabled.Checked = true;
            this.rSentryEnabled.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(179)))), ((int)(((byte)(94)))));
            this.rSentryEnabled.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rSentryEnabled.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rSentryEnabled.Location = new System.Drawing.Point(297, 31);
            this.rSentryEnabled.Name = "rSentryEnabled";
            this.rSentryEnabled.Size = new System.Drawing.Size(104, 24);
            this.rSentryEnabled.TabIndex = 6;
            this.rSentryEnabled.TabStop = true;
            this.rSentryEnabled.Text = "ENABLED";
            this.rSentryEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rSentryEnabled.UseVisualStyleBackColor = true;
            // 
            // rSentryDisabled
            // 
            this.rSentryDisabled.Appearance = System.Windows.Forms.Appearance.Button;
            this.rSentryDisabled.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.rSentryDisabled.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rSentryDisabled.Font = new System.Drawing.Font("Helvetica", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rSentryDisabled.Location = new System.Drawing.Point(143, 31);
            this.rSentryDisabled.Name = "rSentryDisabled";
            this.rSentryDisabled.Size = new System.Drawing.Size(104, 24);
            this.rSentryDisabled.TabIndex = 7;
            this.rSentryDisabled.Text = "DISABLED";
            this.rSentryDisabled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rSentryDisabled.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(10, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(525, 21);
            this.label5.TabIndex = 9;
            this.label5.Text = "You can enable/disable this feature at any time from the Settings dialog.";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.rGameXCom2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rGameChimera, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(97, 28);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(351, 138);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // rGameXCom2
            // 
            this.rGameXCom2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rGameXCom2.Appearance = System.Windows.Forms.Appearance.Button;
            this.rGameXCom2.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(179)))), ((int)(((byte)(94)))));
            this.rGameXCom2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rGameXCom2.Image = ((System.Drawing.Image)(resources.GetObject("rGameXCom2.Image")));
            this.rGameXCom2.Location = new System.Drawing.Point(22, 4);
            this.rGameXCom2.Name = "rGameXCom2";
            this.rGameXCom2.Size = new System.Drawing.Size(130, 130);
            this.rGameXCom2.TabIndex = 9;
            this.rGameXCom2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rGameXCom2.UseVisualStyleBackColor = true;
            this.rGameXCom2.CheckedChanged += new System.EventHandler(this.GameSelectionChanged);
            // 
            // rGameChimera
            // 
            this.rGameChimera.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rGameChimera.Appearance = System.Windows.Forms.Appearance.Button;
            this.rGameChimera.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(179)))), ((int)(((byte)(94)))));
            this.rGameChimera.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rGameChimera.Image = ((System.Drawing.Image)(resources.GetObject("rGameChimera.Image")));
            this.rGameChimera.Location = new System.Drawing.Point(198, 4);
            this.rGameChimera.Name = "rGameChimera";
            this.rGameChimera.Size = new System.Drawing.Size(130, 130);
            this.rGameChimera.TabIndex = 8;
            this.rGameChimera.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rGameChimera.UseVisualStyleBackColor = true;
            this.rGameChimera.CheckedChanged += new System.EventHandler(this.GameSelectionChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.rSentryEnabled);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.rSentryDisabled);
            this.panel1.Location = new System.Drawing.Point(12, 361);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(545, 87);
            this.panel1.TabIndex = 14;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Location = new System.Drawing.Point(12, 184);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(545, 171);
            this.panel2.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(31, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(481, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Select the game you want to use this copy of AML for!";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WelcomeDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 490);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bContinue);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WelcomeDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome to AML";
            this.Load += new System.EventHandler(this.WelcomeDialog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WelcomeDialog_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
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
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.RadioButton rGameXCom2;
		private System.Windows.Forms.RadioButton rGameChimera;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label4;
	}
}