namespace ProcedureManager
{
    partial class ErrorMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorMessageBox));
            tableLayoutPanel1 = new TableLayoutPanel();
            tb_ErrorQuery = new TextBox();
            lb_ErrorMessage = new Label();
            pictureBox1 = new PictureBox();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(tb_ErrorQuery, 1, 3);
            tableLayoutPanel1.Controls.Add(lb_ErrorMessage, 3, 1);
            tableLayoutPanel1.Controls.Add(pictureBox1, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(800, 562);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tb_ErrorQuery
            // 
            tableLayoutPanel1.SetColumnSpan(tb_ErrorQuery, 3);
            tb_ErrorQuery.Dock = DockStyle.Fill;
            tb_ErrorQuery.Location = new Point(23, 193);
            tb_ErrorQuery.Multiline = true;
            tb_ErrorQuery.Name = "tb_ErrorQuery";
            tb_ErrorQuery.ReadOnly = true;
            tb_ErrorQuery.ScrollBars = ScrollBars.Vertical;
            tb_ErrorQuery.Size = new Size(754, 346);
            tb_ErrorQuery.TabIndex = 0;
            // 
            // lb_ErrorMessage
            // 
            lb_ErrorMessage.Dock = DockStyle.Fill;
            lb_ErrorMessage.Location = new Point(193, 20);
            lb_ErrorMessage.Name = "lb_ErrorMessage";
            lb_ErrorMessage.Padding = new Padding(20, 0, 20, 0);
            lb_ErrorMessage.Size = new Size(584, 150);
            lb_ErrorMessage.TabIndex = 1;
            lb_ErrorMessage.Text = "Error Message";
            lb_ErrorMessage.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(23, 23);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(144, 144);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // ErrorMessageBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 562);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ErrorMessageBox";
            Text = "Error";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TextBox tb_ErrorQuery;
        private Label lb_ErrorMessage;
        private PictureBox pictureBox1;
    }
}