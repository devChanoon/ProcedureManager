using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProcedureComparer
{  

    public class SQLManager
    {
        private SqlConnection _SqlConnection = new SqlConnection();
        private SqlCommand? _SqlCommand = null;

        private string _DbAddress = string.Empty;
        private string _DbName = string.Empty;
        private string _DbId = string.Empty;
        private string _DbPw = string.Empty;

        public SQLManager SetDatabaseInfo(string dbAddress, string dbName, string dbId, string dbPw)
        {
            _DbAddress = dbAddress;
            _DbName = dbName;
            _DbId = dbId;
            _DbPw = dbPw;

            return this;
        }

        private bool CheckDatabaseInfo()
        {
            return !string.IsNullOrEmpty(_DbAddress) && !string.IsNullOrEmpty(_DbName) && !string.IsNullOrEmpty(_DbId) && !string.IsNullOrEmpty(_DbPw);
        }

        private string GetConnectionString()
        {
            return $"Server={_DbAddress};database={_DbName};uid={_DbId};pwd={_DbPw}";
        }

        public string Connect()
        {
            if (!CheckDatabaseInfo())
                return "입력되지 않은 DB 정보가 있습니다.";

            string errorMessage = string.Empty;
            try
            {
                _SqlConnection.ConnectionString = GetConnectionString();
                _SqlConnection.Open();

                _SqlCommand = new SqlCommand();
                _SqlCommand.Connection = _SqlConnection;
                _SqlCommand.CommandTimeout = 0;
            }
            catch (Exception ex)
            {
                _SqlConnection.Close();
                errorMessage = ex.Message;
            }

            return errorMessage;
        }
        public bool Disconnect()
        {
            try
            {
                _SqlConnection.Close();
                _SqlCommand = null;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public DataTable? GetProcedureNames(string filterText)
        {
            if (_SqlCommand != null)
            {
                string query = @"
SELECT a.name as procedure_name
     , a.type as type
  FROM sys.objects a
       INNER JOIN sys.sql_modules b ON a.object_id = b.object_id
 WHERE a.type_desc in ('SQL_SCALAR_FUNCTION', 'SQL_TABLE_VALUED_FUNCTION', 'SQL_STORED_PROCEDURE')
   AND a.name like '%@PROCEDURE_NAME%'
ORDER BY a.name
";
                _SqlCommand.CommandText = query.Replace("@PROCEDURE_NAME", filterText);

                SqlDataAdapter sd = new SqlDataAdapter(_SqlCommand);
                DataSet ds = new DataSet();
                sd.Fill(ds, "Result");

                if (ds == null || ds.Tables.Count == 0)
                    return null;
                else
                    return ds.Tables[0];
            }
            else
                return null;
        }

        public string? GetProcedureContent(string procedureName)
        {
            if (_SqlCommand != null)
            { 
                string query = @"
SELECT REPLACE(b.definition, char(10), '\r\n')
  FROM sys.objects a
       INNER JOIN sys.sql_modules b ON a.object_id = b.object_id
 WHERE a.type_desc in ('SQL_SCALAR_FUNCTION', 'SQL_TABLE_VALUED_FUNCTION', 'SQL_STORED_PROCEDURE')
   AND a.name = '@PROCEDURE_NAME'
";

                query = query.Replace("@PROCEDURE_NAME", procedureName);
                _SqlCommand.CommandText = query;

                SqlDataAdapter sd = new SqlDataAdapter(_SqlCommand);
                DataSet ds = new DataSet();
                sd.Fill(ds, "Result");

                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    return string.Empty;
                else
                    return ds.Tables[0].Rows[0][0].ToString();
            }
            else
                return string.Empty;
        }

        public string SetProcedureContent(string query)
        {
            try
            { 
                if (_SqlCommand != null)
                {
                    _SqlCommand.CommandText = query;

                    SqlDataAdapter sd = new SqlDataAdapter(_SqlCommand);
                    DataSet ds = new DataSet();
                    sd.Fill(ds, "Result");
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
