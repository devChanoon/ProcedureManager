using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcedureComparer
{
    public partial class FormWinmergePathSetting : Form
    {
        private const string WINMERGE_PROGRAM_NAME = "WinMergeU.exe";
        private const string DEFAULT_WINMERGE_PATH = @"C:\Program Files\WinMerge";
        private const string INI_KEY_NAME_SECTION = "Config";
        private const string INI_KEY_NAME_PATH = "WinMergePath";

        private string _ConfigIniPath = string.Empty;
        private string _CurrentPath = string.Empty;

        private IniManager _IniManager = new IniManager();

        public FormWinmergePathSetting(string configIniPath)
        {
            InitializeComponent();
            _ConfigIniPath = configIniPath;
        }

        private void FormWinmergePathSetting_Load(object sender, EventArgs e)
        {
            _CurrentPath = _IniManager.GetIniValue(_ConfigIniPath, INI_KEY_NAME_SECTION, INI_KEY_NAME_PATH);

            if (CheckWinMergePath(_CurrentPath))
            {
                tb_Path.Text = _CurrentPath;
            }
            else
            {
                _IniManager.SetIniValue(_ConfigIniPath, INI_KEY_NAME_SECTION, INI_KEY_NAME_PATH, string.Empty);
                _CurrentPath = string.Empty;
            }
        }

        private bool CheckWinMergePath(string path)
        {
            return File.Exists(path);
        }

        private void btn_Path_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "실행 파일 (*.exe)|*.exe";
            openFileDialog.InitialDirectory = DEFAULT_WINMERGE_PATH;
            if (CheckWinMergePath(_CurrentPath))
            {
                openFileDialog.InitialDirectory = _CurrentPath;
            }

            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                if (WINMERGE_PROGRAM_NAME != ShortNameFromFile(openFileDialog.FileName))
                    MessageBox.Show($"선택한 파일이 WinMerge 프로그램이 아닙니다.\r\n대상 프로그램명 : {WINMERGE_PROGRAM_NAME}", "안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    tb_Path.Text = openFileDialog.FileName;
            }
        }

        private string ShortNameFromFile(string fileName)
        {
            int num = fileName.LastIndexOfAny(new char[2] { '\\', ':' }, fileName.Length - 1, fileName.Length);
            if (num > 0)
            {
                return fileName.Substring(num + 1, fileName.Length - num - 1);
            }

            return fileName;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (tb_Path.Text != string.Empty)
            {
                _IniManager.SetIniValue(_ConfigIniPath, INI_KEY_NAME_SECTION, INI_KEY_NAME_PATH, tb_Path.Text);
                MessageBox.Show("경로가 저장 되었습니다.");
            }
            else
                MessageBox.Show("경로가 설정되지 않았습니다.");
        }
    }
}
