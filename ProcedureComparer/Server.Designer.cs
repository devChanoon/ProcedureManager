namespace ProcedureComparer
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
            groupBox2 = new GroupBox();
            tb_ProcedureContent = new TextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            btn_OpenBackupFolder = new Button();
            btn_ExecOtherServer = new Button();
            label5 = new Label();
            btn_ExecThisServer = new Button();
            tlp_Server.SuspendLayout();
            gb_ServerName.SuspendLayout();
            tlp_ServerInfo.SuspendLayout();
            panel1.SuspendLayout();
            groupBox2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tlp_Server
            // 
            tlp_Server.ColumnCount = 1;
            tlp_Server.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlp_Server.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tlp_Server.Controls.Add(gb_ServerName, 0, 0);
            tlp_Server.Controls.Add(groupBox2, 0, 1);
            tlp_Server.Dock = DockStyle.Fill;
            tlp_Server.Location = new Point(0, 0);
            tlp_Server.Name = "tlp_Server";
            tlp_Server.RowCount = 2;
            tlp_Server.RowStyles.Add(new RowStyle(SizeType.Absolute, 136F));
            tlp_Server.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp_Server.Size = new Size(509, 677);
            tlp_Server.TabIndex = 0;
            // 
            // gb_ServerName
            // 
            gb_ServerName.Controls.Add(tlp_ServerInfo);
            gb_ServerName.Dock = DockStyle.Fill;
            gb_ServerName.Location = new Point(3, 3);
            gb_ServerName.Name = "gb_ServerName";
            gb_ServerName.Size = new Size(503, 130);
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
            tlp_ServerInfo.Size = new Size(497, 108);
            tlp_ServerInfo.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(btn_Connect);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(416, 1);
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
            btn_Connect.Text = "Connect";
            btn_Connect.UseVisualStyleBackColor = true;
            btn_Connect.Click += btn_Connect_Click;
            // 
            // tb_PW
            // 
            tb_PW.Dock = DockStyle.Fill;
            tb_PW.Location = new Point(105, 82);
            tb_PW.Name = "tb_PW";
            tb_PW.Size = new Size(307, 23);
            tb_PW.TabIndex = 8;
            // 
            // tb_ID
            // 
            tb_ID.Dock = DockStyle.Fill;
            tb_ID.Location = new Point(105, 56);
            tb_ID.Name = "tb_ID";
            tb_ID.Size = new Size(307, 23);
            tb_ID.TabIndex = 7;
            // 
            // tb_Name
            // 
            tb_Name.Dock = DockStyle.Fill;
            tb_Name.Location = new Point(105, 30);
            tb_Name.Name = "tb_Name";
            tb_Name.Size = new Size(307, 23);
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
            tb_Address.Size = new Size(307, 23);
            tb_Address.TabIndex = 5;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(tb_ProcedureContent);
            groupBox2.Controls.Add(tableLayoutPanel1);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(3, 139);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(503, 535);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Procedure Content";
            // 
            // tb_ProcedureContent
            // 
            tb_ProcedureContent.Dock = DockStyle.Fill;
            tb_ProcedureContent.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            tb_ProcedureContent.Location = new Point(3, 52);
            tb_ProcedureContent.Multiline = true;
            tb_ProcedureContent.Name = "tb_ProcedureContent";
            tb_ProcedureContent.ScrollBars = ScrollBars.Vertical;
            tb_ProcedureContent.Size = new Size(497, 480);
            tb_ProcedureContent.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(btn_OpenBackupFolder, 3, 0);
            tableLayoutPanel1.Controls.Add(btn_ExecOtherServer, 2, 0);
            tableLayoutPanel1.Controls.Add(label5, 0, 0);
            tableLayoutPanel1.Controls.Add(btn_ExecThisServer, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Enabled = false;
            tableLayoutPanel1.Location = new Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(497, 33);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // btn_OpenBackupFolder
            // 
            btn_OpenBackupFolder.Dock = DockStyle.Fill;
            btn_OpenBackupFolder.Location = new Point(358, 2);
            btn_OpenBackupFolder.Margin = new Padding(2);
            btn_OpenBackupFolder.Name = "btn_OpenBackupFolder";
            btn_OpenBackupFolder.Size = new Size(137, 29);
            btn_OpenBackupFolder.TabIndex = 5;
            btn_OpenBackupFolder.Text = "Open Backup Folder";
            btn_OpenBackupFolder.UseVisualStyleBackColor = true;
            btn_OpenBackupFolder.Click += btn_OpenBackupFolder_Click;
            // 
            // btn_ExecOtherServer
            // 
            btn_ExecOtherServer.Dock = DockStyle.Fill;
            btn_ExecOtherServer.Location = new Point(220, 2);
            btn_ExecOtherServer.Margin = new Padding(2);
            btn_ExecOtherServer.Name = "btn_ExecOtherServer";
            btn_ExecOtherServer.Size = new Size(134, 29);
            btn_ExecOtherServer.TabIndex = 4;
            btn_ExecOtherServer.Text = "Other Server";
            btn_ExecOtherServer.UseVisualStyleBackColor = true;
            btn_ExecOtherServer.Click += btn_ExecOtherServer_Click;
            // 
            // label5
            // 
            label5.Dock = DockStyle.Fill;
            label5.Location = new Point(3, 0);
            label5.Name = "label5";
            label5.Size = new Size(74, 33);
            label5.TabIndex = 0;
            label5.Text = "EXEC ▶";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btn_ExecThisServer
            // 
            btn_ExecThisServer.Dock = DockStyle.Fill;
            btn_ExecThisServer.Location = new Point(82, 2);
            btn_ExecThisServer.Margin = new Padding(2);
            btn_ExecThisServer.Name = "btn_ExecThisServer";
            btn_ExecThisServer.Size = new Size(134, 29);
            btn_ExecThisServer.TabIndex = 1;
            btn_ExecThisServer.Text = "This Server";
            btn_ExecThisServer.UseVisualStyleBackColor = true;
            btn_ExecThisServer.Click += btn_Exec_Click;
            // 
            // Server
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlp_Server);
            Name = "Server";
            Size = new Size(509, 677);
            tlp_Server.ResumeLayout(false);
            gb_ServerName.ResumeLayout(false);
            tlp_ServerInfo.ResumeLayout(false);
            tlp_ServerInfo.PerformLayout();
            panel1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlp_Server;
        private GroupBox gb_ServerName;
        private TableLayoutPanel tlp_ServerInfo;
        private GroupBox groupBox2;
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
        private Button btn_OpenBackupFolder;
    }
}
