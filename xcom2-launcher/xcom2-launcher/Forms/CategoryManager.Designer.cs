namespace XCOM2Launcher.Forms {
	partial class CategoryManager {
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
			this.moveCategoryUpButton = new System.Windows.Forms.Button();
			this.categoriesListBox = new System.Windows.Forms.ListBox();
			this.moveCategoryDownButton = new System.Windows.Forms.Button();
			this.addCategoryButton = new System.Windows.Forms.Button();
			this.removeCategoryButton = new System.Windows.Forms.Button();
			this.renameCategoryButton = new System.Windows.Forms.Button();
			this.pLeft = new System.Windows.Forms.Panel();
			this.bClose = new System.Windows.Forms.Button();
			this.pMain = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.pLeft.SuspendLayout();
			this.pMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// moveCategoryUpButton
			// 
			this.moveCategoryUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.moveCategoryUpButton.Location = new System.Drawing.Point(6, 28);
			this.moveCategoryUpButton.Name = "moveCategoryUpButton";
			this.moveCategoryUpButton.Size = new System.Drawing.Size(75, 23);
			this.moveCategoryUpButton.TabIndex = 15;
			this.moveCategoryUpButton.Text = "Up";
			this.moveCategoryUpButton.UseVisualStyleBackColor = true;
			this.moveCategoryUpButton.Click += new System.EventHandler(this.MoveCategoryUpButtonOnClick);
			// 
			// categoriesListBox
			// 
			this.categoriesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.categoriesListBox.FormattingEnabled = true;
			this.categoriesListBox.Location = new System.Drawing.Point(12, 28);
			this.categoriesListBox.Name = "categoriesListBox";
			this.categoriesListBox.Size = new System.Drawing.Size(284, 238);
			this.categoriesListBox.TabIndex = 16;
			this.categoriesListBox.DoubleClick += new System.EventHandler(this.CategoriesListBoxDoubleClick);
			// 
			// moveCategoryDownButton
			// 
			this.moveCategoryDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.moveCategoryDownButton.Location = new System.Drawing.Point(6, 58);
			this.moveCategoryDownButton.Name = "moveCategoryDownButton";
			this.moveCategoryDownButton.Size = new System.Drawing.Size(75, 23);
			this.moveCategoryDownButton.TabIndex = 17;
			this.moveCategoryDownButton.Text = "Down";
			this.moveCategoryDownButton.UseVisualStyleBackColor = true;
			this.moveCategoryDownButton.Click += new System.EventHandler(this.MoveCategoryDownButtonOnClick);
			// 
			// addCategoryButton
			// 
			this.addCategoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.addCategoryButton.Location = new System.Drawing.Point(6, 88);
			this.addCategoryButton.Name = "addCategoryButton";
			this.addCategoryButton.Size = new System.Drawing.Size(75, 23);
			this.addCategoryButton.TabIndex = 20;
			this.addCategoryButton.Text = "Add";
			this.addCategoryButton.UseVisualStyleBackColor = true;
			this.addCategoryButton.Click += new System.EventHandler(this.AddCategoryButtonClick);
			// 
			// removeCategoryButton
			// 
			this.removeCategoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.removeCategoryButton.Location = new System.Drawing.Point(6, 118);
			this.removeCategoryButton.Name = "removeCategoryButton";
			this.removeCategoryButton.Size = new System.Drawing.Size(75, 23);
			this.removeCategoryButton.TabIndex = 19;
			this.removeCategoryButton.Text = "Remove";
			this.removeCategoryButton.UseVisualStyleBackColor = true;
			this.removeCategoryButton.Click += new System.EventHandler(this.RemoveCategoryButtonOnClick);
			// 
			// renameCategoryButton
			// 
			this.renameCategoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.renameCategoryButton.Location = new System.Drawing.Point(6, 148);
			this.renameCategoryButton.Name = "renameCategoryButton";
			this.renameCategoryButton.Size = new System.Drawing.Size(75, 23);
			this.renameCategoryButton.TabIndex = 18;
			this.renameCategoryButton.Text = "Rename";
			this.renameCategoryButton.UseVisualStyleBackColor = true;
			this.renameCategoryButton.Click += new System.EventHandler(this.RenameCategoryButtonOnClick);
			// 
			// pLeft
			// 
			this.pLeft.Controls.Add(this.bClose);
			this.pLeft.Controls.Add(this.moveCategoryUpButton);
			this.pLeft.Controls.Add(this.renameCategoryButton);
			this.pLeft.Controls.Add(this.removeCategoryButton);
			this.pLeft.Controls.Add(this.moveCategoryDownButton);
			this.pLeft.Controls.Add(this.addCategoryButton);
			this.pLeft.Dock = System.Windows.Forms.DockStyle.Right;
			this.pLeft.Location = new System.Drawing.Point(302, 0);
			this.pLeft.Name = "pLeft";
			this.pLeft.Size = new System.Drawing.Size(93, 278);
			this.pLeft.TabIndex = 21;
			// 
			// bClose
			// 
			this.bClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bClose.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bClose.Location = new System.Drawing.Point(6, 243);
			this.bClose.Name = "bClose";
			this.bClose.Size = new System.Drawing.Size(75, 23);
			this.bClose.TabIndex = 0;
			this.bClose.Text = "Close";
			this.bClose.UseVisualStyleBackColor = true;
			// 
			// pMain
			// 
			this.pMain.Controls.Add(this.label1);
			this.pMain.Controls.Add(this.categoriesListBox);
			this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pMain.Location = new System.Drawing.Point(0, 0);
			this.pMain.Name = "pMain";
			this.pMain.Size = new System.Drawing.Size(302, 278);
			this.pMain.TabIndex = 22;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57, 13);
			this.label1.TabIndex = 17;
			this.label1.Text = "Categories";
			// 
			// CategoryManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(395, 278);
			this.Controls.Add(this.pMain);
			this.Controls.Add(this.pLeft);
			this.Name = "CategoryManager";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Category manager";
			this.pLeft.ResumeLayout(false);
			this.pMain.ResumeLayout(false);
			this.pMain.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button moveCategoryUpButton;
		private System.Windows.Forms.ListBox categoriesListBox;
		private System.Windows.Forms.Button moveCategoryDownButton;
		private System.Windows.Forms.Button addCategoryButton;
		private System.Windows.Forms.Button removeCategoryButton;
		private System.Windows.Forms.Button renameCategoryButton;
		private System.Windows.Forms.Panel pLeft;
		private System.Windows.Forms.Panel pMain;
		private System.Windows.Forms.Button bClose;
		private System.Windows.Forms.Label label1;
	}
}