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
            this.label1.Location = new System.Drawing.Point(41, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(641, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "请通过启用匿名错误报告来帮助我们改善AML！";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(216, 78);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(327, 127);
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
            this.bContinue.Location = new System.Drawing.Point(293, 531);
            this.bContinue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bContinue.Name = "bContinue";
            this.bContinue.Size = new System.Drawing.Size(176, 38);
            this.bContinue.TabIndex = 2;
            this.bContinue.UseVisualStyleBackColor = true;
            this.bContinue.Click += new System.EventHandler(this.bContinue_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(21, 44);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(724, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "因为是第一次启动AML,您看到此对话框";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 14);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(724, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "欢迎使用Alternative Mod Launcher!";
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
            this.rSentryEnabled.Location = new System.Drawing.Point(396, 36);
            this.rSentryEnabled.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rSentryEnabled.Name = "rSentryEnabled";
            this.rSentryEnabled.Size = new System.Drawing.Size(139, 28);
            this.rSentryEnabled.TabIndex = 6;
            this.rSentryEnabled.TabStop = true;
            this.rSentryEnabled.Text = "启用";
            this.rSentryEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rSentryEnabled.UseVisualStyleBackColor = true;
            // 
            // rSentryDisabled
            // 
            this.rSentryDisabled.Appearance = System.Windows.Forms.Appearance.Button;
            this.rSentryDisabled.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.rSentryDisabled.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rSentryDisabled.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rSentryDisabled.Location = new System.Drawing.Point(191, 36);
            this.rSentryDisabled.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rSentryDisabled.Name = "rSentryDisabled";
            this.rSentryDisabled.Size = new System.Drawing.Size(139, 28);
            this.rSentryDisabled.TabIndex = 7;
            this.rSentryDisabled.Text = "禁用";
            this.rSentryDisabled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rSentryDisabled.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(13, 66);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(700, 24);
            this.label5.TabIndex = 9;
            this.label5.Text = "你可以在设置中随时开启/关闭此功能";
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(129, 32);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(468, 159);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // rGameXCom2
            // 
            this.rGameXCom2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rGameXCom2.Appearance = System.Windows.Forms.Appearance.Button;
            this.rGameXCom2.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(179)))), ((int)(((byte)(94)))));
            this.rGameXCom2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rGameXCom2.Image = ((System.Drawing.Image)(resources.GetObject("rGameXCom2.Image")));
            this.rGameXCom2.Location = new System.Drawing.Point(30, 4);
            this.rGameXCom2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rGameXCom2.Name = "rGameXCom2";
            this.rGameXCom2.Size = new System.Drawing.Size(173, 150);
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
            this.rGameChimera.Location = new System.Drawing.Point(264, 4);
            this.rGameChimera.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rGameChimera.Name = "rGameChimera";
            this.rGameChimera.Size = new System.Drawing.Size(173, 150);
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
            this.panel1.Location = new System.Drawing.Point(16, 417);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(727, 100);
            this.panel1.TabIndex = 14;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Location = new System.Drawing.Point(16, 212);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(727, 197);
            this.panel2.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(41, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(641, 23);
            this.label4.TabIndex = 10;
            this.label4.Text = "选择您要使用此AML副本的游戏！";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WelcomeDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 592);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bContinue);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WelcomeDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "欢迎使用AML";
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