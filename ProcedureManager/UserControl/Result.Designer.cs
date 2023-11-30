namespace ProcedureManager
{
    partial class Result
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
            tlp_Main = new TableLayoutPanel();
            btn_Close = new Button();
            tc_Result = new TabControl();
            tlp_Main.SuspendLayout();
            SuspendLayout();
            // 
            // tlp_Main
            // 
            tlp_Main.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tlp_Main.ColumnCount = 1;
            tlp_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlp_Main.Controls.Add(btn_Close, 0, 0);
            tlp_Main.Controls.Add(tc_Result, 0, 1);
            tlp_Main.Dock = DockStyle.Fill;
            tlp_Main.Location = new Point(0, 0);
            tlp_Main.Name = "tlp_Main";
            tlp_Main.RowCount = 2;
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp_Main.Size = new Size(752, 220);
            tlp_Main.TabIndex = 0;
            // 
            // btn_Close
            // 
            btn_Close.BackgroundImage = Properties.Resources.double_down_25;
            btn_Close.BackgroundImageLayout = ImageLayout.Center;
            btn_Close.Dock = DockStyle.Fill;
            btn_Close.Location = new Point(4, 4);
            btn_Close.Name = "btn_Close";
            btn_Close.Size = new Size(744, 34);
            btn_Close.TabIndex = 0;
            btn_Close.UseVisualStyleBackColor = true;
            // 
            // tc_Result
            // 
            tc_Result.Dock = DockStyle.Fill;
            tc_Result.Location = new Point(4, 45);
            tc_Result.Name = "tc_Result";
            tc_Result.SelectedIndex = 0;
            tc_Result.Size = new Size(744, 171);
            tc_Result.TabIndex = 1;
            // 
            // Result
            // 
            Controls.Add(tlp_Main);
            Name = "Result";
            Size = new Size(752, 220);
            tlp_Main.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlp_Main;
        private TabControl tc_Result;
        public Button btn_Close;
    }
}
