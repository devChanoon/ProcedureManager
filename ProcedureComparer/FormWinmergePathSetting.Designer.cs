namespace ProcedureComparer
{
    partial class FormWinmergePathSetting
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
            label1 = new Label();
            tb_Path = new TextBox();
            btn_Path = new Button();
            btn_Save = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 0;
            label1.Text = "WinMerge Path";
            // 
            // tb_Path
            // 
            tb_Path.Enabled = false;
            tb_Path.Location = new Point(107, 6);
            tb_Path.Name = "tb_Path";
            tb_Path.ReadOnly = true;
            tb_Path.Size = new Size(499, 23);
            tb_Path.TabIndex = 1;
            // 
            // btn_Path
            // 
            btn_Path.Location = new Point(612, 6);
            btn_Path.Name = "btn_Path";
            btn_Path.Size = new Size(87, 23);
            btn_Path.TabIndex = 2;
            btn_Path.Text = "Path";
            btn_Path.UseVisualStyleBackColor = true;
            btn_Path.Click += btn_Path_Click;
            // 
            // btn_Save
            // 
            btn_Save.Location = new Point(705, 5);
            btn_Save.Name = "btn_Save";
            btn_Save.Size = new Size(87, 23);
            btn_Save.TabIndex = 3;
            btn_Save.Text = "Save";
            btn_Save.UseVisualStyleBackColor = true;
            btn_Save.Click += btn_Save_Click;
            // 
            // FormWinmergePathSetting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 36);
            Controls.Add(btn_Save);
            Controls.Add(btn_Path);
            Controls.Add(tb_Path);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormWinmergePathSetting";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Setting";
            Load += FormWinmergePathSetting_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tb_Path;
        private Button btn_Path;
        private Button btn_Save;
    }
}