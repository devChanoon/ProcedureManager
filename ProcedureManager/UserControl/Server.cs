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
        public CheckContentsDelegate? _CheckContents;
        public SearchTextChangeDelegate? _SearchTextChange;
        public bool isConnect { get { return _SqlManager != null; } }

        private const string INI_FOLDER_NAME = "ini_files";
        private const string SAVE_FOLDER_NAME = "procedure_contents";

        private const string INI_KEY_NAME_ADDRESS = "Address";
        private const string INI_KEY_NAME_NAME = "Name";
        private const string INI_KEY_NAME_ID = "ID";
        private const string INI_KEY_NAME_PW = "PW";

        private const int FOLD_HEIGHT = 40;
        private const int OPEN_HEIGHT = 220;

        private readonly Color _SelectedBackColor = Color.CornflowerBlue;
        private readonly Color _DefaultBackColor = SystemColors.Control;

        private string _ConfigIniPath = string.Empty;
        private string _SaveDbFolderPath = string.Empty;
        private string _ServerName = string.Empty;
        private string _CurrentProcedureName = string.Empty;

        private bool isSelected { get { return gb_ProcedureContent.BackColor == _SelectedBackColor; } }
        private bool isResultOpen { get { return splitContainer1.Height - splitContainer1.SplitterDistance > FOLD_HEIGHT + result1.Margin.Top + result1.Margin.Bottom; } }

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

            result1.btn_Close.Click += Btn_Close_Click;
            ResultFold();
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

                if (_SearchTextChange != null)
                    _SearchTextChange();
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
                    result = ContentSplit(content, backupId);
            }

            return result;
        }

        private string ContentSplit(string text, string? backupId)
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

            string trimText = text.TrimStart();
            int trimIndex = 0;
            foreach (char c in trimText)
            {
                if (c == '\t')
                    trimIndex++;
                else
                    break;
            }

            trimText = trimText.Substring(trimIndex);
            if (trimText.Length >= 2 && (trimText.Substring(0, 2) == "--" || trimText.Substring(0, 2) == "/*"))
                return new Tuple<bool, string>(false, text);

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

        private string GetProcedureContent()
        {
            if (tb_ProcedureContent.SelectionLength > 0)
                return tb_ProcedureContent.Text.Substring(tb_ProcedureContent.SelectionStart, tb_ProcedureContent.SelectionLength);
            else
                return tb_ProcedureContent.Text;
        }

        private void btn_ExecThisServer_Click(object? sender, EventArgs? e)
        {
            ExecuteProcedure(GetProcedureContent());
        }

        public void ExecuteProcedure(string procedureContent)
        {
            if (_SqlManager == null) return;

            string title = $"{tb_Address.Text} > {tb_Name.Text}";
            if (procedureContent.Substring(procedureContent.Length - 2) == "\r\n")
                procedureContent = procedureContent.Substring(0, procedureContent.Length - 2);

            ProcedureList procedureList = _SyntaxAnalyzer.GetProcedureList(procedureContent);
            if (procedureList.Count > 0)
            {
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
            {
                Tuple<DataSet?, string> result = _SqlManager.ExecuteQueryGetDataSet(procedureContent);
                if (result.Item2 != string.Empty)
                {
                    ErrorMessageBox errorMessageBox = new ErrorMessageBox(title, result.Item2, procedureContent);
                    errorMessageBox.Show();
                }
                else
                {
                    result1.SetDataGrid(result.Item1);
                    if (!isResultOpen)
                        ResultOpen();
                }
            }
        }

        private void BackupProcedure(string name, string? currentProcedureContent)
        {
            if (_SqlManager != null)
            {
                string prevProcedureContent = SearchProcedureContent(name);
                if (prevProcedureContent.Length >= 2 && prevProcedureContent.Substring(prevProcedureContent.Length - 2) == "\r\n")
                    prevProcedureContent = prevProcedureContent.Substring(0, prevProcedureContent.Length - 2);

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
                _ExecOtherServer(_ServerName, GetProcedureContent());
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

        private void Btn_Close_Click(object? sender, EventArgs e)
        {
            if (isResultOpen)
                ResultFold();
            else
                ResultOpen();
        }

        public void ResultOpen()
        {
            splitContainer1.SplitterDistance = splitContainer1.Height - OPEN_HEIGHT;
            result1.btn_Close.BackgroundImage = Properties.Resources.double_down_25;
        }

        public void ResultFold()
        {
            splitContainer1.SplitterDistance = splitContainer1.Height - FOLD_HEIGHT;
            result1.btn_Close.BackgroundImage = Properties.Resources.double_up_25;
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (splitContainer1.Height - e.SplitY > FOLD_HEIGHT)
                result1.btn_Close.BackgroundImage = Properties.Resources.double_down_25;
            else
                result1.btn_Close.BackgroundImage = Properties.Resources.double_up_25;
        }


        public void SetLight(bool? isRed)
        {
            if (isRed == null)
                pnl_Status.BackgroundImage = Properties.Resources.gray_circle_30;
            else if (isRed.HasValue && isRed.Value)
                pnl_Status.BackgroundImage = Properties.Resources.red_circle_30;
            else
                pnl_Status.BackgroundImage = Properties.Resources.green_circle_30;
        }

        private void tb_ProcedureContent_TextChanged(object sender, EventArgs e)
        {
            if (_CheckContents != null)
                _CheckContents();
        }
    }
}
