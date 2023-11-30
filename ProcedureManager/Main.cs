using System.Data;
using System.Diagnostics;
using System.Linq;

namespace ProcedureManager
{
    public partial class Main : Form
    {
        public delegate void ExecOtherServerDelegate(string serverName, string procedureContent);

        private const string SERVER1_NAME = "Server1";
        private const string SERVER2_NAME = "Server2";
        private const string INI_FOLDER_NAME = "ini_files";

        private readonly string CONFIG_INI_PATH = Path.Combine(Application.StartupPath, INI_FOLDER_NAME, "Config.ini");

        private DateTime? _LastTextChangeTime = null;

        public Main()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            InitializeServerInfo();
        }

        private void InitializeServerInfo()
        {
            server1.Initailize(SERVER1_NAME, CONFIG_INI_PATH);
            server1._ExecOtherServer = new ExecOtherServerDelegate(ExecOtherServer);
            server2.Initailize(SERVER2_NAME, CONFIG_INI_PATH);
            server2._ExecOtherServer = new ExecOtherServerDelegate(ExecOtherServer);
        }

        private void lv_ProcedureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_ProcedureList.SelectedItems.Count > 0)
            {
                string? procedureName = lv_ProcedureList.SelectedItems[0].SubItems[0].Text.Trim();
                server1.Search(procedureName);
                server2.Search(procedureName);
            }
        }

        private void btn_OpenWinmerge_Click(object? sender, EventArgs? e)
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
                string winMergePath = new IniManager().GetIniValue(CONFIG_INI_PATH, "Config", "WinMergePath");
                if (winMergePath == string.Empty)
                {
                    MessageBox.Show("WinMerge 프로그램 경로가 설정되지 않았습니다.");
                    ShowWinMergePathSetting();
                    return;
                }

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

        private void btn_WinMergeSetting_Click(object sender, EventArgs e)
        {
            ShowWinMergePathSetting();
        }

        private void ShowWinMergePathSetting()
        {
            WinmergePathSetting formWinmergePathSetting = new WinmergePathSetting(CONFIG_INI_PATH);
            formWinmergePathSetting.ShowDialog();
        }

        private void tb_ProcedureName_TextChanged(object sender, EventArgs e)
        {
            _LastTextChangeTime = DateTime.Now;
            tmr_SearchProcedureName.Enabled = true;
        }

        private List<Tuple<string, string>>? MergeTables(DataTable? table1, DataTable? table2)
        {
            if (table1 == null && table2 == null)
                return null;
            else if (table1 == null || table2 == null)
                return DataTableToListString(table1 ?? table2);
            else
            {
                List<Tuple<string, string>> procedureNameList = DataTableToListString(table1);
                foreach (DataRow dataRow in table2.Rows)
                {
                    string procedureName = dataRow.GetValueToString(0);
                    string type = dataRow.GetValueToString(1);
                    Tuple<string, string> procedure = new Tuple<string, string>(procedureName, type);
                    if (!procedureNameList.Contains(procedure))
                        procedureNameList.Add(procedure);
                }

                return procedureNameList.OrderBy(procedureName => procedureName).ToList();
            }
        }

        private List<Tuple<string, string>> DataTableToListString(DataTable? dataTable)
        {
            List<Tuple<string, string>> list = new List<Tuple<string, string>>();
            if (dataTable == null)
                return list;

            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new Tuple<string, string>(row.GetValueToString(0), row.GetValueToString(1)));
            }

            return list;
        }

        private void SetListViewItems(List<Tuple<string, string>>? itemList)
        {
            lv_ProcedureList.Items.Clear();
            if (itemList != null)
            {
                foreach (Tuple<string, string> item in itemList)
                {
                    if (item.Item1 != null && item.Item1.Trim() != string.Empty)
                    {
                        ListViewItem listViewItem = new ListViewItem(item.Item1);
                        listViewItem.SubItems.Add(item.Item2);
                        lv_ProcedureList.Items.Add(listViewItem);
                    }
                }
            }
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    btn_OpenWinmerge_Click(null, null);
                    break;

                case Keys.F2:
                    tb_ProcedureName.Focus();
                    tb_ProcedureName.SelectAll();
                    break;

                case Keys.F3:
                case Keys.F4:
                    bool isServer1 = e.KeyCode == Keys.F3;
                    Server server = isServer1 ? server1 : server2;
                    if (!server.isConnect)
                        server.btn_Connect_Click(null, null);
                    else
                    {
                        server1.SelectServer(isServer1);
                        server2.SelectServer(!isServer1);
                    }
                    break;

                case Keys.F5:
                case Keys.F6:
                    server1.Execute(e.KeyCode == Keys.F5);
                    server2.Execute(e.KeyCode == Keys.F5);
                    break;
            }
        }

        private void tb_ProcedureName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lv_ProcedureList.Focus();
                lv_ProcedureList.SelectedItems.Clear();
                lv_ProcedureList.SelectedIndices.Clear();

                if (lv_ProcedureList.Items.Count > 0)
                    lv_ProcedureList.Items[0].Selected = true;
            }
        }

        private void tmr_SearchProcedureName_Tick(object sender, EventArgs e)
        {
            if (_LastTextChangeTime != null)
            {
                if ((DateTime.Now - (DateTime)_LastTextChangeTime).TotalMilliseconds > 100)
                {
                    tmr_SearchProcedureName.Enabled = false;
                    string filterText = tb_ProcedureName.Text;
                    DataTable? dataTable1 = server1.SearchProcedureNames(filterText);
                    DataTable? dataTable2 = server2.SearchProcedureNames(filterText);
                    SetListViewItems(MergeTables(dataTable1, dataTable2));
                }
            }
        }
    }
}