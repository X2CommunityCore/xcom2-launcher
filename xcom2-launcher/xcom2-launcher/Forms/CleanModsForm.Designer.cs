namespace XCOM2Launcher.Forms
{
    partial class CleanModsForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.shader_groupbox = new System.Windows.Forms.GroupBox();
            this.shadercache_none_radiobutton = new System.Windows.Forms.RadioButton();
            this.shadercache_empty_radiobutton = new System.Windows.Forms.RadioButton();
            this.shadercache_all_radiobutton = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.source_groupbox = new System.Windows.Forms.GroupBox();
            this.src_all_radiobutton = new System.Windows.Forms.RadioButton();
            this.src_xcomgame_radiobutton = new System.Windows.Forms.RadioButton();
            this.src_none_radiobutton = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.shader_groupbox.SuspendLayout();
            this.source_groupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(296, 135);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;

            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.shader_groupbox);
            this.groupBox1.Controls.Add(this.source_groupbox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 116);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Deletion settings";
            // 
            // shader_groupbox
            // 
            this.shader_groupbox.Controls.Add(this.shadercache_none_radiobutton);
            this.shader_groupbox.Controls.Add(this.shadercache_empty_radiobutton);
            this.shader_groupbox.Controls.Add(this.shadercache_all_radiobutton);
            this.shader_groupbox.Controls.Add(this.button3);
            this.shader_groupbox.Location = new System.Drawing.Point(182, 19);
            this.shader_groupbox.Name = "shader_groupbox";
            this.shader_groupbox.Size = new System.Drawing.Size(170, 91);
            this.shader_groupbox.TabIndex = 6;
            this.shader_groupbox.TabStop = false;
            this.shader_groupbox.Text = "ModShaderCache files";
            // 
            // shadercache_none_radiobutton
            // 
            this.shadercache_none_radiobutton.AutoSize = true;
            this.shadercache_none_radiobutton.Location = new System.Drawing.Point(6, 19);
            this.shadercache_none_radiobutton.Name = "shadercache_none_radiobutton";
            this.shadercache_none_radiobutton.Size = new System.Drawing.Size(51, 17);
            this.shadercache_none_radiobutton.TabIndex = 4;
            this.shadercache_none_radiobutton.Text = "None";
            this.toolTip1.SetToolTip(this.shadercache_none_radiobutton, "Do not delete anything.");
            this.shadercache_none_radiobutton.UseVisualStyleBackColor = true;
            // 
            // shadercache_empty_radiobutton
            // 
            this.shadercache_empty_radiobutton.AutoSize = true;
            this.shadercache_empty_radiobutton.Checked = true;
            this.shadercache_empty_radiobutton.Location = new System.Drawing.Point(6, 42);
            this.shadercache_empty_radiobutton.Name = "shadercache_empty_radiobutton";
            this.shadercache_empty_radiobutton.Size = new System.Drawing.Size(54, 17);
            this.shadercache_empty_radiobutton.TabIndex = 5;
            this.shadercache_empty_radiobutton.TabStop = true;
            this.shadercache_empty_radiobutton.Text = "Empty";
            this.toolTip1.SetToolTip(this.shadercache_empty_radiobutton, "Safe.");
            this.shadercache_empty_radiobutton.UseVisualStyleBackColor = true;
            // 
            // shadercache_all_radiobutton
            // 
            this.shadercache_all_radiobutton.AutoSize = true;
            this.shadercache_all_radiobutton.Location = new System.Drawing.Point(6, 65);
            this.shadercache_all_radiobutton.Name = "shadercache_all_radiobutton";
            this.shadercache_all_radiobutton.Size = new System.Drawing.Size(36, 17);
            this.shadercache_all_radiobutton.TabIndex = 6;
            this.shadercache_all_radiobutton.Text = "All";
            this.toolTip1.SetToolTip(this.shadercache_all_radiobutton, "NOT SAFE. This can/will cause graphical glitches.");
            this.shadercache_all_radiobutton.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(303, 75);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "Start";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // source_groupbox
            // 
            this.source_groupbox.Controls.Add(this.src_all_radiobutton);
            this.source_groupbox.Controls.Add(this.src_xcomgame_radiobutton);
            this.source_groupbox.Controls.Add(this.src_none_radiobutton);
            this.source_groupbox.Controls.Add(this.button2);
            this.source_groupbox.Location = new System.Drawing.Point(6, 19);
            this.source_groupbox.Name = "source_groupbox";
            this.source_groupbox.Size = new System.Drawing.Size(170, 91);
            this.source_groupbox.TabIndex = 4;
            this.source_groupbox.TabStop = false;
            this.source_groupbox.Text = "Source files";
            this.toolTip1.SetToolTip(this.source_groupbox, "Source files are only relevant for mod creators.");
            // 
            // src_all_radiobutton
            // 
            this.src_all_radiobutton.AutoSize = true;
            this.src_all_radiobutton.Location = new System.Drawing.Point(6, 19);
            this.src_all_radiobutton.Name = "src_all_radiobutton";
            this.src_all_radiobutton.Size = new System.Drawing.Size(51, 17);
            this.src_all_radiobutton.TabIndex = 1;
            this.src_all_radiobutton.Text = "None";
            this.toolTip1.SetToolTip(this.src_all_radiobutton, "Do not delete anything.");
            this.src_all_radiobutton.UseVisualStyleBackColor = true;
            // 
            // src_xcomgame_radiobutton
            // 
            this.src_xcomgame_radiobutton.AutoSize = true;
            this.src_xcomgame_radiobutton.Checked = true;
            this.src_xcomgame_radiobutton.Location = new System.Drawing.Point(6, 42);
            this.src_xcomgame_radiobutton.Name = "src_xcomgame_radiobutton";
            this.src_xcomgame_radiobutton.Size = new System.Drawing.Size(81, 17);
            this.src_xcomgame_radiobutton.TabIndex = 2;
            this.src_xcomgame_radiobutton.TabStop = true;
            this.src_xcomgame_radiobutton.Text = "XComGame";
            this.toolTip1.SetToolTip(this.src_xcomgame_radiobutton, "Only delete src/XComGame files.");
            this.src_xcomgame_radiobutton.UseVisualStyleBackColor = true;
            // 
            // src_none_radiobutton
            // 
            this.src_none_radiobutton.AutoSize = true;
            this.src_none_radiobutton.Location = new System.Drawing.Point(6, 65);
            this.src_none_radiobutton.Name = "src_none_radiobutton";
            this.src_none_radiobutton.Size = new System.Drawing.Size(36, 17);
            this.src_none_radiobutton.TabIndex = 3;
            this.src_none_radiobutton.Text = "All";
            this.toolTip1.SetToolTip(this.src_none_radiobutton, "Delete all source files.");
            this.src_none_radiobutton.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(303, 75);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Start";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // CleanModsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 165);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::XCOM2Launcher.Properties.Resources.xcom;
            this.Name = "CleanModsForm";
            this.Text = "Clean mods";
            this.groupBox1.ResumeLayout(false);
            this.shader_groupbox.ResumeLayout(false);
            this.shader_groupbox.PerformLayout();
            this.source_groupbox.ResumeLayout(false);
            this.source_groupbox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox source_groupbox;
        private System.Windows.Forms.RadioButton src_xcomgame_radiobutton;
        private System.Windows.Forms.RadioButton src_none_radiobutton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RadioButton src_all_radiobutton;
        private System.Windows.Forms.GroupBox shader_groupbox;
        private System.Windows.Forms.RadioButton shadercache_none_radiobutton;
        private System.Windows.Forms.RadioButton shadercache_empty_radiobutton;
        private System.Windows.Forms.RadioButton shadercache_all_radiobutton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}