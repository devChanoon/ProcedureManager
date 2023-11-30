using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using static ProcedureManager.Main;

namespace ProcedureManager
{
    public partial class Server : UserControl
    {
        private SQLManager? _SqlManager = null;
        private IniManager _IniManager = new IniManager();
        private SyntaxAnalyzer _SyntaxAnalyzer = new SyntaxAnalyzer();

        public ExecOtherServerDelegate? _ExecOtherServer;
        public bool isConnect { get { return _SqlManager != null; } }

        private const string INI_FOLDER_NAME = "ini_files";
        private const string SAVE_FOLDER_NAME = "procedure_contents";

        private const string INI_KEY_NAME_ADDRESS = "Address";
        private const string INI_KEY_NAME_NAME = "Name";
        private const string INI_KEY_NAME_ID = "ID";
        private const string INI_KEY_NAME_PW = "PW";

        private readonly Color _SelectedBackColor = Color.CornflowerBlue;
        private readonly Color _DefaultBackColor = SystemColors.Control;

        private string _ConfigIniPath = string.Empty;
        private string _SaveDbFolderPath = string.Empty;
        private string _ServerName = string.Empty;
        private string _CurrentProcedureName = string.Empty;

        private bool isSelected { get { return gb_ProcedureContent.BackColor == _SelectedBackColor; } }

        public Server()
        {
            InitializeComponent();
            InitializeBackupList(null);
            tb_ProcedureContent.MaxLength = int.MaxValue;
        }

        public void SelectServer(bool isSelect)
        {
            gb_ProcedureContent.BackColor = isSelect ? _SelectedBackColor : _DefaultBackColor;
            btn_ExecThisServer.Text = string.Format("This Server{0}", isSelect ? " (F5)" : "");
            btn_ExecOtherServer.Text = string.Format("Other Server{0}", isSelect ? " (F6)" : "");
            if (isSelect)
            {
                tb_ProcedureContent.SelectionStart = 0;
                tb_ProcedureContent.Focus();
            }
        }

        private void InitializeBackupList(DataTable? dataTable)
        {
            cb_BackupList.Items.Clear();
            cb_BackupList.Items.Add(string.Empty);
            if (dataTable != null)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    cb_BackupList.Items.Add(new Tuple<string, string>(dataRow.GetValueToString("id"), dataRow.GetValueToString("insert_time")));
                }
            }
        }

        private void CheckDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public void Initailize(string serverName, string configIniPath)
        {
            CheckDirectory(CreatePath(INI_FOLDER_NAME));
            CheckDirectory(CreatePath(SAVE_FOLDER_NAME));

            _ServerName = serverName;
            gb_ServerName.Text = $"{_ServerName} Info";
            _ConfigIniPath = configIniPath;

            ChangeConnectButtonText();

            tb_Address.Text = _IniManager.GetIniValue(_ConfigIniPath, _ServerName, INI_KEY_NAME_ADDRESS);
            tb_Name.Text = _IniManager.GetIniValue(_ConfigIniPath, _ServerName, INI_KEY_NAME_NAME);
            tb_ID.Text = _IniManager.GetIniValue(_ConfigIniPath, _ServerName, INI_KEY_NAME_ID);
            tb_PW.Text = _IniManager.GetIniValue(_ConfigIniPath, _ServerName, INI_KEY_NAME_PW);
        }

        private string CreatePath(params string[] paths)
        {
            string resultPath = Application.StartupPath;
            foreach (string path in paths)
            {
                resultPath = Path.Combine(resultPath, path);
            }
            return resultPath;
        }

        public void btn_Connect_Click(object? sender, EventArgs? e)
        {
            if (_SqlManager == null)
                Connect();
            else
                Disconnect();

            ChangeConnectButtonText();
        }

        private void Connect()
        {
            _SqlManager = new SQLManager();
            string result = _SqlManager.SetDatabaseInfo(tb_Address.Text, tb_Name.Text, tb_ID.Text, tb_PW.Text).Connect();
            if (result != string.Empty)
            {
                MessageBox.Show(result);
                _SqlManager = null;
            }
            else
            {
                _SaveDbFolderPath = CreatePath(SAVE_FOLDER_NAME, $"{tb_Address.Text}_{tb_Name.Text}");
                CheckDirectory(_SaveDbFolderPath);

                ChangeServerInfoEnabled(false);
                ChangeServerFunctionEnabeld(true);
                SaveServerInfo();

                gb_ProcedureContent.Text = $"Procedure Content (F{Convert.ToInt32(_ServerName.Replace("Server", "")) + 2})";

                if (!_SqlManager.CheckExistBackupTable())
                {
                    string errorMessage = _SqlManager.CreateBackupTable();
                    if (errorMessage != string.Empty)
                        MessageBox.Show($"Backup 테이블 생성에 실패했습니다.\r\n{errorMessage}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void Disconnect()
        {
            if (_SqlManager != null)
            {
                _SqlManager.Disconnect();
                _SqlManager = null;

                gb_ProcedureContent.Text = "Procedure Content";

                ChangeServerInfoEnabled(true);
                ChangeServerFunctionEnabeld(false);
                DeleteFiles();
            }
        }

        private void SaveServerInfo()
        {
            _IniManager.SetIniValue(_ConfigIniPath, _ServerName, INI_KEY_NAME_ADDRESS, tb_Address.Text);
            _IniManager.SetIniValue(_ConfigIniPath, _ServerName, INI_KEY_NAME_NAME, tb_Name.Text);
            _IniManager.SetIniValue(_ConfigIniPath, _ServerName, INI_KEY_NAME_ID, tb_ID.Text);
            _IniManager.SetIniValue(_ConfigIniPath, _ServerName, INI_KEY_NAME_PW, tb_PW.Text);
        }

        private void ChangeServerInfoEnabled(bool enabled)
        {
            tb_Address.Enabled = enabled;
            tb_Name.Enabled = enabled;
            tb_ID.Enabled = enabled;
            tb_PW.Enabled = enabled;
        }

        private void ChangeServerFunctionEnabeld(bool enabled)
        {
            tableLayoutPanel1.Enabled = enabled;
        }

        private void ChangeConnectButtonText()
        {
            btn_Connect.Text = _SqlManager == null ? $"Connect\r\n(F{Convert.ToInt32(_ServerName.Replace("Server", "")) + 2})" : "Disconnect";
        }

        public DataTable? SearchProcedureNames(string filterText)
        {
            if (_SqlManager == null) return null;

            return _SqlManager.GetProcedureList(filterText);
        }

        public void Search(string? procedureName, string? backupId = null)
        {
            if (_SqlManager == null || procedureName == null) return;

            _CurrentProcedureName = procedureName;
            tb_ProcedureContent.Text = SearchProcedureContent(procedureName, backupId);
        }

        private string SearchProcedureContent(string procedureName, string? backupId = null)
        {
            string result = string.Empty;
            if (_SqlManager != null)
            {
                if (backupId == null)
                    InitializeBackupList(_SqlManager.GetBackupList(procedureName));

                string content = backupId == null ? _SqlManager.GetProcedureContent(procedureName) : _SqlManager.GetBackupProcedureContent(backupId);
                if (content != string.Empty)
                    result = ContentSplit(content);
            }

            return result;
        }

        private string ContentSplit(string text)
        {
            string[] textArray = text.Replace("\\r\\n", "¶").Split('¶').ToArray();
            bool findCreateText = false;

            StringBuilder stringBuilder = new StringBuilder();
            foreach (string str in textArray)
            {
                if (!findCreateText)
                {
                    Tuple<bool, string> tuple = CheckCreateProcedure(str);
                    findCreateText = tuple.Item1;
                    stringBuilder.AppendLine(tuple.Item2);
                }
                else
                    stringBuilder.AppendLine(str);
            }
            return stringBuilder.ToString().Replace("\r\r", "\r");
        }

        private Tuple<bool, string> CheckCreateProcedure(string text)
        {
            const string FILTER_TEXT = "CREATE";
            const string REPLACE_TEXT = "ALTER";

            Tuple<int, string, string> result = _SyntaxAnalyzer.GetTargetSyntaxIndex(new List<string> { FILTER_TEXT }, text);
            int filterTextStartIndex = result.Item1;
            string _text = result.Item2;
            bool matched = false;
            if (filterTextStartIndex > -1)
            {
                Tuple<int, string> checkResult = _SyntaxAnalyzer.CheckFirstWord(new List<string> { "PROC", "FUNC" }, _text);
                if (checkResult.Item1 > -1)
                {
                    matched = true;
                    _text = text.Substring(0, filterTextStartIndex);
                    _text += REPLACE_TEXT;
                    _text += text.Substring(filterTextStartIndex + FILTER_TEXT.Length);
                }
            }

            return new Tuple<bool, string>(matched, _text);
        }

        public void Execute(bool isThisServer)
        {
            if (isSelected)
            {
                if (isThisServer)
                    btn_ExecThisServer_Click(null, null);
                else
                    btn_ExecOtherServer_Click(null, null);
            }
        }

        private void btn_ExecThisServer_Click(object? sender, EventArgs? e)
        {
            ExecuteProcedure(tb_ProcedureContent.Text);
        }

        public void ExecuteProcedure(string procedureContent)
        {
            if (_SqlManager == null) return;

            ProcedureList procedureList = _SyntaxAnalyzer.GetProcedureList(procedureContent);
            if (procedureList.Count > 0)
            {
                string title = $"{tb_Address.Text} > {tb_Name.Text}";

                bool executeSearch = false;
                List<Tuple<string, string?>> errorList = new List<Tuple<string, string?>>();
                for (int i = 0; i < procedureList.Count; i++)
                {
                    Tuple<string?, string?> procedure = procedureList.GetPair(i);
                    if (procedure.Item1 != null)
                    {
                        if (!executeSearch && _CurrentProcedureName == procedure.Item1)
                            executeSearch = true;

                        BackupProcedure(procedure.Item1, procedure.Item2);
                    }

                    string errorMessage = _SqlManager.SetProcedureContent(procedure.Item2);
                    if (errorMessage != string.Empty)
                        errorList.Add(new Tuple<string, string?>(errorMessage, procedure.Item2));
                }

                if (errorList.Count == 0)
                {
                    MessageBox.Show("실행이 완료 되었습니다.", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (executeSearch)
                        Search(_CurrentProcedureName);
                }
                else
                {
                    foreach (Tuple<string, string?> error in errorList)
                    {
                        ErrorMessageBox errorMessageBox = new ErrorMessageBox(title, error.Item1, error.Item2);
                        errorMessageBox.Show();
                    }
                }
            }
            else
                _SqlManager.SetProcedureContent(procedureContent);
        }

        private void BackupProcedure(string name, string? currentProcedureContent)
        {
            if (_SqlManager != null)
            {
                string prevProcedureContent = SearchProcedureContent(name);
                if (prevProcedureContent != currentProcedureContent)
                {
                    string errorMessage = _SqlManager.InsertBackupProcedureContent(name, prevProcedureContent);
                    if (errorMessage != string.Empty)
                        MessageBox.Show(errorMessage, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        public string SaveProcedure()
        {
            if (!string.IsNullOrEmpty(_CurrentProcedureName))
                return SaveProcedureContent(_SaveDbFolderPath, tb_ProcedureContent.Text);
            else
                return string.Empty;
        }

        private string SaveProcedureContent(string path, string procedureContent)
        {
            string fileName = $"{_CurrentProcedureName} ({DateTime.Now.ToString("yyyyMMddHHmmssfff")}).txt";
            string saveFilePath = Path.Combine(path, fileName);
            File.WriteAllText(saveFilePath, procedureContent);

            return saveFilePath;
        }

        private void DeleteFiles()
        {
            foreach (FileInfo fileInfo in new DirectoryInfo(_SaveDbFolderPath).GetFiles())
            {
                File.Delete(fileInfo.FullName);
            }
        }

        private void btn_ExecOtherServer_Click(object? sender, EventArgs? e)
        {
            if (_ExecOtherServer != null)
                _ExecOtherServer(_ServerName, tb_ProcedureContent.Text);
        }

        private void cb_BackupList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? backupId = null;
            if (cb_BackupList.SelectedItem.GetType() != typeof(string))
                backupId = ((Tuple<string, string>)cb_BackupList.SelectedItem).Item1;

            Search(_CurrentProcedureName, backupId);
        }

        private void btn_RefreshBackupList_Click(object sender, EventArgs e)
        {
            if (_SqlManager != null)
                InitializeBackupList(_SqlManager.GetBackupList(_CurrentProcedureName));
        }
    }
}
