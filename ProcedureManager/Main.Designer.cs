namespace ProcedureManager
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            tableLayoutPanel1 = new TableLayoutPanel();
            server2 = new Server();
            btn_OpenWinmerge = new Button();
            lv_ProcedureList = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            btn_WinMergeSetting = new Button();
            tb_ProcedureName = new TextBox();
            server1 = new Server();
            tmr_SearchProcedureName = new System.Windows.Forms.Timer(components);
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(server2, 3, 0);
            tableLayoutPanel1.Controls.Add(btn_OpenWinmerge, 0, 0);
            tableLayoutPanel1.Controls.Add(lv_ProcedureList, 0, 2);
            tableLayoutPanel1.Controls.Add(btn_WinMergeSetting, 1, 0);
            tableLayoutPanel1.Controls.Add(tb_ProcedureName, 0, 1);
            tableLayoutPanel1.Controls.Add(server1, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 106F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1584, 911);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // server2
            // 
            server2.Dock = DockStyle.Fill;
            server2.Location = new Point(920, 3);
            server2.Name = "server2";
            tableLayoutPanel1.SetRowSpan(server2, 3);
            server2.Size = new Size(661, 905);
            server2.TabIndex = 7;
            server2.TabStop = false;
            // 
            // btn_OpenWinmerge
            // 
            btn_OpenWinmerge.BackgroundImage = (Image)resources.GetObject("btn_OpenWinmerge.BackgroundImage");
            btn_OpenWinmerge.BackgroundImageLayout = ImageLayout.Zoom;
            btn_OpenWinmerge.Dock = DockStyle.Fill;
            btn_OpenWinmerge.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btn_OpenWinmerge.ImageAlign = ContentAlignment.MiddleLeft;
            btn_OpenWinmerge.Location = new Point(3, 3);
            btn_OpenWinmerge.Name = "btn_OpenWinmerge";
            btn_OpenWinmerge.Size = new Size(194, 100);
            btn_OpenWinmerge.TabIndex = 2;
            btn_OpenWinmerge.TabStop = false;
            btn_OpenWinmerge.Text = "Open WinMerge\r\n(F1)\r\n";
            btn_OpenWinmerge.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_OpenWinmerge.UseVisualStyleBackColor = true;
            btn_OpenWinmerge.Click += btn_OpenWinmerge_Click;
            // 
            // lv_ProcedureList
            // 
            lv_ProcedureList.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            tableLayoutPanel1.SetColumnSpan(lv_ProcedureList, 2);
            lv_ProcedureList.Dock = DockStyle.Fill;
            lv_ProcedureList.GridLines = true;
            lv_ProcedureList.Location = new Point(3, 139);
            lv_ProcedureList.Name = "lv_ProcedureList";
            lv_ProcedureList.Size = new Size(244, 769);
            lv_ProcedureList.TabIndex = 3;
            lv_ProcedureList.TabStop = false;
            lv_ProcedureList.UseCompatibleStateImageBehavior = false;
            lv_ProcedureList.View = View.Details;
            lv_ProcedureList.SelectedIndexChanged += lv_ProcedureList_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Procedure Name";
            columnHeader1.Width = 180;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Type";
            columnHeader2.TextAlign = HorizontalAlignment.Center;
            columnHeader2.Width = 50;
            // 
            // btn_WinMergeSetting
            // 
            btn_WinMergeSetting.Dock = DockStyle.Fill;
            btn_WinMergeSetting.Image = (Image)resources.GetObject("btn_WinMergeSetting.Image");
            btn_WinMergeSetting.Location = new Point(203, 3);
            btn_WinMergeSetting.Name = "btn_WinMergeSetting";
            btn_WinMergeSetting.Size = new Size(44, 100);
            btn_WinMergeSetting.TabIndex = 4;
            btn_WinMergeSetting.TabStop = false;
            btn_WinMergeSetting.UseVisualStyleBackColor = true;
            btn_WinMergeSetting.Click += btn_WinMergeSetting_Click;
            // 
            // tb_ProcedureName
            // 
            tableLayoutPanel1.SetColumnSpan(tb_ProcedureName, 2);
            tb_ProcedureName.Dock = DockStyle.Fill;
            tb_ProcedureName.Location = new Point(3, 109);
            tb_ProcedureName.Name = "tb_ProcedureName";
            tb_ProcedureName.PlaceholderText = "(F2) Input Procedure Name And Press Enter";
            tb_ProcedureName.Size = new Size(244, 23);
            tb_ProcedureName.TabIndex = 5;
            tb_ProcedureName.TabStop = false;
            tb_ProcedureName.TextChanged += tb_ProcedureName_TextChanged;
            tb_ProcedureName.KeyUp += tb_ProcedureName_KeyUp;
            // 
            // server1
            // 
            server1.Dock = DockStyle.Fill;
            server1.Location = new Point(253, 3);
            server1.Name = "server1";
            tableLayoutPanel1.SetRowSpan(server1, 3);
            server1.Size = new Size(661, 905);
            server1.TabIndex = 6;
            server1.TabStop = false;
            // 
            // tmr_SearchProcedureName
            // 
            tmr_SearchProcedureName.Interval = 200;
            tmr_SearchProcedureName.Tick += tmr_SearchProcedureName_Tick;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1584, 911);
            Controls.Add(tableLayoutPanel1);
            KeyPreview = true;
            Name = "FormMain";
            Text = "Procedure Manager";
            WindowState = FormWindowState.Maximized;
            FormClosing += FormMain_FormClosing;
            Load += FormMain_Load;
            KeyUp += FormMain_KeyUp;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button btn_OpenWinmerge;
        private Button btn_WinMergeSetting;
        private TextBox tb_ProcedureName;
        private Server server2;
        private Server server1;
        private ListView lv_ProcedureList;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private System.Windows.Forms.Timer tmr_SearchProcedureName;
    }
}