using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProcedureComparer
{
    public class IniManager
    {


        // ---- ini 파일 의 읽고 쓰기를 위한 API 함수 선언 ----
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(    // ini Read 함수
                    String section,
                    String key,
                    String def,
                    StringBuilder retVal,
                    int size,
                    String filePath);

        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(  // ini Write 함수
                    String section,
                    String key,
                    String val,
                    String filePath);

        /// ini파일에 쓰기
        public void SetIniValue(string path, string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, path);
        }

        /// ini파일에서 읽어 오기
        public string GetIniValue(string path, string section, string key)
        {
            StringBuilder temp = new StringBuilder(2000);
            int i = GetPrivateProfileString(section, key, "", temp, 2000, path);
            return temp.ToString();
        }
    }
}
