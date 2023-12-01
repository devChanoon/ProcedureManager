using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Reflection;

namespace ProcedureManager
{
    public static class Extentions
    {
        public static bool isValid(this DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public static bool isValid(this DataSet dataSet)
        {
            if (dataSet != null && dataSet.Tables.Count > 0)
                return true;
            else
                return false;
        }

        public static string GetValueToString(this DataRow dataRow, string columnName)
        {
            try
            {
                string? value = dataRow[columnName].ToString();
                return value ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetValueToString(this DataRow dataRow, int columnIndex)
        {
            try
            {
                string? value = dataRow[columnIndex].ToString();
                return value ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetValue(this Dictionary<string, string> dictionary, string key)
        {
            if (dictionary.ContainsKey(key))
                return dictionary[key];
            else
                return string.Empty;
        }
    }
}
