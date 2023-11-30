namespace ProcedureManager
{
    partial class Server
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Server));
            tlp_Server = new TableLayoutPanel();
            gb_ServerName = new GroupBox();
            tlp_ServerInfo = new TableLayoutPanel();
            panel1 = new Panel();
            btn_Connect = new Button();
            tb_PW = new TextBox();
            tb_ID = new TextBox();
            tb_Name = new TextBox();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            label2 = new Label();
            tb_Address = new TextBox();
            gb_ProcedureContent = new GroupBox();
            splitContainer1 = new SplitContainer();
            tb_ProcedureContent = new TextBox();
            result1 = new Result();
            tableLayoutPanel1 = new TableLayoutPanel();
            label6 = new Label();
            btn_ExecOtherServer = new Button();
            label5 = new Label();
            btn_ExecThisServer = new Button();
            cb_BackupList = new ComboBox();
            btn_RefreshBackupList = new Button();
            tlp_Server.SuspendLayout();
            gb_ServerName.SuspendLayout();
            tlp_ServerInfo.SuspendLayout();
            panel1.SuspendLayout();
            gb_ProcedureContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tlp_Server
            // 
            tlp_Server.ColumnCount = 1;
            tlp_Server.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlp_Server.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tlp_Server.Controls.Add(gb_ServerName, 0, 0);
            tlp_Server.Controls.Add(gb_ProcedureContent, 0, 1);
            tlp_Server.Dock = DockStyle.Fill;
            tlp_Server.Location = new Point(0, 0);
            tlp_Server.Name = "tlp_Server";
            tlp_Server.RowCount = 2;
            tlp_Server.RowStyles.Add(new RowStyle(SizeType.Absolute, 136F));
            tlp_Server.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp_Server.Size = new Size(625, 950);
            tlp_Server.TabIndex = 0;
            // 
            // gb_ServerName
            // 
            gb_ServerName.Controls.Add(tlp_ServerInfo);
            gb_ServerName.Dock = DockStyle.Fill;
            gb_ServerName.Location = new Point(3, 3);
            gb_ServerName.Name = "gb_ServerName";
            gb_ServerName.Size = new Size(619, 130);
            gb_ServerName.TabIndex = 0;
            gb_ServerName.TabStop = false;
            gb_ServerName.Text = "Server Info";
            // 
            // tlp_ServerInfo
            // 
            tlp_ServerInfo.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tlp_ServerInfo.ColumnCount = 3;
            tlp_ServerInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tlp_ServerInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlp_ServerInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tlp_ServerInfo.Controls.Add(panel1, 2, 0);
            tlp_ServerInfo.Controls.Add(tb_PW, 1, 3);
            tlp_ServerInfo.Controls.Add(tb_ID, 1, 2);
            tlp_ServerInfo.Controls.Add(tb_Name, 1, 1);
            tlp_ServerInfo.Controls.Add(label1, 0, 0);
            tlp_ServerInfo.Controls.Add(label3, 0, 2);
            tlp_ServerInfo.Controls.Add(label4, 0, 3);
            tlp_ServerInfo.Controls.Add(label2, 0, 1);
            tlp_ServerInfo.Controls.Add(tb_Address, 1, 0);
            tlp_ServerInfo.Dock = DockStyle.Fill;
            tlp_ServerInfo.Location = new Point(3, 19);
            tlp_ServerInfo.Name = "tlp_ServerInfo";
            tlp_ServerInfo.RowCount = 4;
            tlp_ServerInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlp_ServerInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlp_ServerInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlp_ServerInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlp_ServerInfo.Size = new Size(613, 108);
            tlp_ServerInfo.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(btn_Connect);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(532, 1);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            tlp_ServerInfo.SetRowSpan(panel1, 4);
            panel1.Size = new Size(80, 106);
            panel1.TabIndex = 0;
            // 
            // btn_Connect
            // 
            btn_Connect.Dock = DockStyle.Fill;
            btn_Connect.Location = new Point(0, 0);
            btn_Connect.Name = "btn_Connect";
            btn_Connect.Size = new Size(80, 106);
            btn_Connect.TabIndex = 0;
            btn_Connect.TabStop = false;
            btn_Connect.Text = "Connect";
            btn_Connect.UseVisualStyleBackColor = true;
            btn_Connect.Click += btn_Connect_Click;
            // 
            // tb_PW
            // 
            tb_PW.Dock = DockStyle.Fill;
            tb_PW.Location = new Point(105, 82);
            tb_PW.Name = "tb_PW";
            tb_PW.Size = new Size(423, 23);
            tb_PW.TabIndex = 8;
            // 
            // tb_ID
            // 
            tb_ID.Dock = DockStyle.Fill;
            tb_ID.Location = new Point(105, 56);
            tb_ID.Name = "tb_ID";
            tb_ID.Size = new Size(423, 23);
            tb_ID.TabIndex = 7;
            // 
            // tb_Name
            // 
            tb_Name.Dock = DockStyle.Fill;
            tb_Name.Location = new Point(105, 30);
            tb_Name.Name = "tb_Name";
            tb_Name.Size = new Size(423, 23);
            tb_Name.TabIndex = 6;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(4, 1);
            label1.Name = "label1";
            label1.Size = new Size(94, 25);
            label1.TabIndex = 1;
            label1.Text = "DB Address";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Dock = DockStyle.Fill;
            label3.Location = new Point(4, 53);
            label3.Name = "label3";
            label3.Size = new Size(94, 25);
            label3.TabIndex = 3;
            label3.Text = "ID";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.Dock = DockStyle.Fill;
            label4.Location = new Point(4, 79);
            label4.Name = "label4";
            label4.Size = new Size(94, 28);
            label4.TabIndex = 4;
            label4.Text = "PW";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(4, 27);
            label2.Name = "label2";
            label2.Size = new Size(94, 25);
            label2.TabIndex = 2;
            label2.Text = "DB Name";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tb_Address
            // 
            tb_Address.Dock = DockStyle.Fill;
            tb_Address.Location = new Point(105, 4);
            tb_Address.Name = "tb_Address";
            tb_Address.Size = new Size(423, 23);
            tb_Address.TabIndex = 5;
            // 
            // gb_ProcedureContent
            // 
            gb_ProcedureContent.BackColor = Color.Transparent;
            gb_ProcedureContent.Controls.Add(splitContainer1);
            gb_ProcedureContent.Controls.Add(tableLayoutPanel1);
            gb_ProcedureContent.Dock = DockStyle.Fill;
            gb_ProcedureContent.Location = new Point(3, 139);
            gb_ProcedureContent.Name = "gb_ProcedureContent";
            gb_ProcedureContent.Size = new Size(619, 808);
            gb_ProcedureContent.TabIndex = 1;
            gb_ProcedureContent.TabStop = false;
            gb_ProcedureContent.Text = "Procedure Content";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 52);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tb_ProcedureContent);
            splitContainer1.Panel1MinSize = 40;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(result1);
            splitContainer1.Panel2MinSize = 40;
            splitContainer1.Size = new Size(613, 753);
            splitContainer1.SplitterDistance = 533;
            splitContainer1.TabIndex = 5;
            splitContainer1.SplitterMoved += splitContainer1_SplitterMoved;
            // 
            // tb_ProcedureContent
            // 
            tb_ProcedureContent.Dock = DockStyle.Fill;
            tb_ProcedureContent.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            tb_ProcedureContent.Location = new Point(0, 0);
            tb_ProcedureContent.Multiline = true;
            tb_ProcedureContent.Name = "tb_ProcedureContent";
            tb_ProcedureContent.ScrollBars = ScrollBars.Vertical;
            tb_ProcedureContent.Size = new Size(613, 533);
            tb_ProcedureContent.TabIndex = 0;
            tb_ProcedureContent.TabStop = false;
            // 
            // result1
            // 
            result1.Dock = DockStyle.Fill;
            result1.Location = new Point(0, 0);
            result1.Name = "result1";
            result1.Size = new Size(613, 216);
            result1.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.Controls.Add(label6, 3, 0);
            tableLayoutPanel1.Controls.Add(btn_ExecOtherServer, 2, 0);
            tableLayoutPanel1.Controls.Add(label5, 0, 0);
            tableLayoutPanel1.Controls.Add(btn_ExecThisServer, 1, 0);
            tableLayoutPanel1.Controls.Add(cb_BackupList, 5, 0);
            tableLayoutPanel1.Controls.Add(btn_RefreshBackupList, 4, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Enabled = false;
            tableLayoutPanel1.Location = new Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(613, 33);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // label6
            // 
            label6.Dock = DockStyle.Fill;
            label6.Location = new Point(337, 0);
            label6.Name = "label6";
            label6.Size = new Size(64, 33);
            label6.TabIndex = 6;
            label6.Text = "Backup ▶";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btn_ExecOtherServer
            // 
            btn_ExecOtherServer.Dock = DockStyle.Fill;
            btn_ExecOtherServer.Location = new Point(204, 2);
            btn_ExecOtherServer.Margin = new Padding(2);
            btn_ExecOtherServer.Name = "btn_ExecOtherServer";
            btn_ExecOtherServer.Size = new Size(128, 29);
            btn_ExecOtherServer.TabIndex = 4;
            btn_ExecOtherServer.TabStop = false;
            btn_ExecOtherServer.Text = "Other Server";
            btn_ExecOtherServer.UseVisualStyleBackColor = true;
            btn_ExecOtherServer.Click += btn_ExecOtherServer_Click;
            // 
            // label5
            // 
            label5.Dock = DockStyle.Fill;
            label5.Location = new Point(3, 0);
            label5.Name = "label5";
            label5.Size = new Size(64, 33);
            label5.TabIndex = 0;
            label5.Text = "EXEC ▶";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btn_ExecThisServer
            // 
            btn_ExecThisServer.Dock = DockStyle.Fill;
            btn_ExecThisServer.Location = new Point(72, 2);
            btn_ExecThisServer.Margin = new Padding(2);
            btn_ExecThisServer.Name = "btn_ExecThisServer";
            btn_ExecThisServer.Size = new Size(128, 29);
            btn_ExecThisServer.TabIndex = 1;
            btn_ExecThisServer.TabStop = false;
            btn_ExecThisServer.Text = "This Server";
            btn_ExecThisServer.UseVisualStyleBackColor = true;
            btn_ExecThisServer.Click += btn_ExecThisServer_Click;
            // 
            // cb_BackupList
            // 
            cb_BackupList.Dock = DockStyle.Fill;
            cb_BackupList.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_BackupList.FormattingEnabled = true;
            cb_BackupList.Location = new Point(434, 5);
            cb_BackupList.Margin = new Padding(0, 5, 3, 3);
            cb_BackupList.MaxDropDownItems = 10;
            cb_BackupList.Name = "cb_BackupList";
            cb_BackupList.Size = new Size(176, 23);
            cb_BackupList.TabIndex = 5;
            cb_BackupList.TabStop = false;
            cb_BackupList.SelectedIndexChanged += cb_BackupList_SelectedIndexChanged;
            // 
            // btn_RefreshBackupList
            // 
            btn_RefreshBackupList.BackgroundImage = (Image)resources.GetObject("btn_RefreshBackupList.BackgroundImage");
            btn_RefreshBackupList.BackgroundImageLayout = ImageLayout.Zoom;
            btn_RefreshBackupList.Dock = DockStyle.Fill;
            btn_RefreshBackupList.Location = new Point(407, 3);
            btn_RefreshBackupList.Name = "btn_RefreshBackupList";
            btn_RefreshBackupList.Size = new Size(24, 27);
            btn_RefreshBackupList.TabIndex = 7;
            btn_RefreshBackupList.TabStop = false;
            btn_RefreshBackupList.UseVisualStyleBackColor = true;
            btn_RefreshBackupList.Click += btn_RefreshBackupList_Click;
            // 
            // Server
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlp_Server);
            Name = "Server";
            Size = new Size(625, 950);
            tlp_Server.ResumeLayout(false);
            gb_ServerName.ResumeLayout(false);
            tlp_ServerInfo.ResumeLayout(false);
            tlp_ServerInfo.PerformLayout();
            panel1.ResumeLayout(false);
            gb_ProcedureContent.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlp_Server;
        private GroupBox gb_ServerName;
        private TableLayoutPanel tlp_ServerInfo;
        private GroupBox gb_ProcedureContent;
        private TextBox tb_PW;
        private TextBox tb_ID;
        private TextBox tb_Name;
        private Button btn_Connect;
        private Label label1;
        private Label label3;
        private Label label4;
        private Label label2;
        private TextBox tb_Address;
        private Panel panel1;
        private TextBox tb_ProcedureContent;
        private Button btn_ExecThisServer;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btn_ExecOtherServer;
        private Label label5;
        private ComboBox cb_BackupList;
        private Label label6;
        private Button btn_RefreshBackupList;
        private Result result1;
        private SplitContainer splitContainer1;
    }
}
