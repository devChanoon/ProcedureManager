using System.Diagnostics;

namespace ProcedureComparer
{
    public partial class FormMain : Form
    {
        private const string SERVER1_NAME = "Server1";
        private const string SERVER2_NAME = "Server2";
        private const string INI_FOLDER_NAME = "ini_files";
        private readonly string PROCEDURE_LIST_INI_PATH = Path.Combine(Application.StartupPath, INI_FOLDER_NAME, "ProcedureList.ini");

        public delegate void ExecOtherServerDelegate(string serverName, string procedureContent);

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            InitializeServerInfo();
            InitializeProcedureList();
        }

        private void InitializeServerInfo()
        {
            server1.Initailize(SERVER1_NAME);
            server1._ExecOtherServer = new ExecOtherServerDelegate(ExecOtherServer);
            server2.Initailize(SERVER2_NAME);
            server2._ExecOtherServer = new ExecOtherServerDelegate(ExecOtherServer);
        }

        private void InitializeProcedureList()
        {
            lv_ProcedureList.Items.Clear();
            foreach (string procedureName in File.ReadAllLines(PROCEDURE_LIST_INI_PATH))
            {
                string _procedureName = procedureName.Trim();
                if (_procedureName.Trim() != string.Empty)
                {
                    lv_ProcedureList.Items.Add(new ListViewItem(_procedureName));
                }
            }
        }

        private void lv_ProcedureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_ProcedureList.SelectedItems.Count > 0)
            {
                string? procedureName = lv_ProcedureList.SelectedItems[0].Text;
                server1.Search(procedureName);
                server2.Search(procedureName);
            }
        }

        private void btn_OpenWinmerge_Click(object sender, EventArgs e)
        {
            string path1 = server1.SaveProcedure();
            string path2 = server2.SaveProcedure();
            if (path1 != string.Empty && path2 != string.Empty)
            {
                RunWinMerge(path1, path2);
            }
        }
        private void RunWinMerge(string path1, string path2)
        {
            try
            {
                // WinMerge 실행 경로 설정 (WinMerge가 설치된 디렉터리에 따라 수정)
                string winMergePath = @"C:\Program Files\WinMerge\WinMergeU.exe";

                // 명령줄 인수 설정
                string arguments = $@"""{path1}"" ""{path2}""";

                // 프로세스 실행
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = winMergePath,
                    Arguments = arguments,
                    UseShellExecute = true
                };

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (server1.isConnect)
                server1.Disconnect();

            if (server2.isConnect)
                server2.Disconnect();
        }

        private void ExecOtherServer(string serverName, string procedureContent)
        {
            Server otherServer = serverName == SERVER1_NAME ? server2 : server1;
            string otherServerName = serverName == SERVER1_NAME ? SERVER2_NAME : SERVER1_NAME;

            if (!otherServer.isConnect)
            {
                MessageBox.Show($"{otherServerName} > DB에 연결되지 않았습니다.");
                return;
            }
            otherServer.ExecuteProcedure(procedureContent);
        }
    }
}