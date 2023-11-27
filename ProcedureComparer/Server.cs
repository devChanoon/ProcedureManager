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

using static ProcedureComparer.FormMain;
using System.IO;

namespace ProcedureComparer
{
    public partial class Server : UserControl
    {
        SQLManager? _SqlManager = null;
        IniManager _IniManager = new IniManager();

        private const string INI_FOLDER_NAME = "ini_files";
        private const string SAVE_FOLDER_NAME = "procedure_contents";
        private const string BACKUP_FOLDER_NAME = "backups";

        private string _ConfigIniPath = string.Empty;
        private string _SaveDbFolderPath = string.Empty;
        private string _SaveDbBackupFolderPath = string.Empty;
        private string _ServerName = string.Empty;
        private string _CurrentProcedureName = string.Empty;
        private string _CurrentProcedureType = string.Empty;

        private const string INI_KEY_NAME_ADDRESS = "Address";
        private const string INI_KEY_NAME_NAME = "Name";
        private const string INI_KEY_NAME_ID = "ID";
        private const string INI_KEY_NAME_PW = "PW";

        public ExecOtherServerDelegate? _ExecOtherServer;

        public bool isConnect { get { return _SqlManager != null; } }

        public Server()
        {
            InitializeComponent();
            tb_ProcedureContent.MaxLength = int.MaxValue;
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

        public void btn_Connect_Click(object sender, EventArgs e)
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
                _SaveDbBackupFolderPath = Path.Combine(_SaveDbFolderPath, BACKUP_FOLDER_NAME);
                CheckDirectory(_SaveDbFolderPath);
                CheckDirectory(_SaveDbBackupFolderPath);

                ChangeServerInfoEnabled(false);
                ChangeServerFunctionEnabeld(true);
                SaveServerInfo();
            }
        }

        public void Disconnect()
        {
            if (_SqlManager != null)
            {
                _SqlManager.Disconnect();
                _SqlManager = null;

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

        private string SearchProcedureContent(string procedureName, string type)
        {
            string result = string.Empty;
            if (_SqlManager != null)
            {
                string? content = _SqlManager.GetProcedureContent(procedureName);
                if (content != null)
                    result = ContentSplit(content, type);
                else
                    result = string.Empty;
            }

            return result;
        }

        public DataTable? SearchProcedureNames(string filterText)
        {
            if (_SqlManager == null) return null;

            return _SqlManager.GetProcedureNames(filterText);
        }

        public void Search(string? procedureName, string? type)
        {
            if (_SqlManager == null || procedureName == null || type == null) return;

            _CurrentProcedureName = procedureName;
            _CurrentProcedureType = type;
            tb_ProcedureContent.Text = SearchProcedureContent(procedureName, type);
        }

        private string ContentSplit(string text, string type)
        {
            string[] textArray = text.Replace("\\r\\n", "¶").Split('¶').ToArray();
            bool findCreateText = false;

            StringBuilder stringBuilder = new StringBuilder();
            foreach (string str in textArray)
            {
                if (!findCreateText)
                {
                    Tuple<bool, string> tuple = CheckCreateProcedure(str, type);
                    findCreateText = tuple.Item1;
                    stringBuilder.AppendLine(tuple.Item2);
                }
                else
                    stringBuilder.AppendLine(str);
            }
            return stringBuilder.ToString().Replace("\r\r", "\r");
        }

        private Tuple<bool, string> CheckCreateProcedure(string text, string type)
        {
            const string FILTER_TEXT = "CREATE";
            const string REPLACE_TEXT = "ALTER";
            string targetText = type == "P" ? "PROC" : "FUNC";
            int filterStringStartIndex = -1;
            bool matched = false;

            string _text = text;
            string upperText = _text.ToUpper();
            filterStringStartIndex = upperText.IndexOf(FILTER_TEXT);
            if (filterStringStartIndex > -1)
            {
                upperText = upperText.Substring(filterStringStartIndex + FILTER_TEXT.Length);
                if (upperText.Length > targetText.Length && upperText.Trim().Substring(0, targetText.Length) == targetText)
                {
                    matched = true;
                    _text = text.Substring(0, filterStringStartIndex);
                    _text += REPLACE_TEXT;
                    _text += text.Substring(filterStringStartIndex + FILTER_TEXT.Length);
                }
            }

            return new Tuple<bool, string>(matched, _text);
        }

        private void btn_Exec_Click(object sender, EventArgs e)
        {
            ExecuteProcedure(tb_ProcedureContent.Text);
        }

        public void ExecuteProcedure(string procedureContent)
        {
            if (_SqlManager == null) return;

            BackupProcedure();
            string title = $"{tb_Address.Text} > {tb_Name.Text}";
            string errorMessage = _SqlManager.SetProcedureContent(procedureContent);
            if (errorMessage != string.Empty)
            {
                MessageBox.Show(errorMessage, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("실행이 완료 되었습니다.", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Search(_CurrentProcedureName, _CurrentProcedureType);
                SendKeys.Send("{F6}");
            }
        }

        private void BackupProcedure()
        {
            SaveProcedureContent(_SaveDbBackupFolderPath, SearchProcedureContent(_CurrentProcedureName, _CurrentProcedureType));
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
            string fileName = $"{_CurrentProcedureName}_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.txt";
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

        private void btn_ExecOtherServer_Click(object sender, EventArgs e)
        {
            if (_ExecOtherServer != null)
                _ExecOtherServer(_ServerName, tb_ProcedureContent.Text);
        }

        private void btn_OpenBackupFolder_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = "Explorer.exe",
                    Arguments = $"\"{_SaveDbBackupFolderPath}\""
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
