using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProcedureManager
{  

    public class SQLManager
    {
        private SqlConnection _SqlConnection = new SqlConnection();
        private SqlCommand? _SqlCommand = null;
        private QueryManager _queryManager = new QueryManager();

        private string _DbAddress = string.Empty;
        private string _DbName = string.Empty;
        private string _DbId = string.Empty;
        private string _DbPw = string.Empty;

        private enum ResultType
        {
            STRING,
            DATA_TABLE,
            DATA_SET
        }

        private struct SQLResult
        { 
            public string errorMessage;
            public dynamic? result;
        }

        public SQLManager SetDatabaseInfo(string dbAddress, string dbName, string dbId, string dbPw)
        {
            _DbAddress = dbAddress;
            _DbName = dbName;
            _DbId = dbId;
            _DbPw = dbPw;

            return this;
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

        public DataTable? GetProcedureList(string filterText)
        {
            if (_SqlCommand != null)
            {
                SQLResult sqlResult = ExecuteQuery(_queryManager.SelectProcedureList(filterText), ResultType.DATA_TABLE);
                return sqlResult.result;
            }
            else
                return null;
        }

        public string GetProcedureContent(string procedureName)
        {
            if (_SqlCommand != null)
            { 
                SQLResult sqlResult = ExecuteQuery(_queryManager.SelectProcedureContent(procedureName), ResultType.STRING);
                if (sqlResult.result == null)
                    return string.Empty;
                else
                    return sqlResult.result;
            }
            else
                return string.Empty;
        }

        public string GetBackupProcedureContent(string backupId)
        {
            if (_SqlCommand != null)
            {
                SQLResult sqlResult = ExecuteQuery(_queryManager.SelectBackupProcedureContent(backupId), ResultType.STRING);
                if (sqlResult.result == null)
                    return string.Empty;
                else
                    return sqlResult.result;
            }
            else
                return string.Empty;
        }

        public DataTable? GetBackupList(string procedureName)
        {
            if (_SqlCommand != null)
            {
                SQLResult sqlResult = ExecuteQuery(_queryManager.SelectBackupList(procedureName), ResultType.DATA_TABLE);
                return sqlResult.result;
            }
            else
                return null;
        }

        public bool CheckExistBackupTable()
        {
            if (_SqlCommand != null)
            {
                SQLResult sqlResult = ExecuteQuery(_queryManager.SelectExistBackupTable(), ResultType.STRING);
                return sqlResult.result != null && sqlResult.result == "Y";
            }
            else
                return false;
        }

        public string CreateBackupTable()
        {
            if (_SqlCommand != null)
            {
                SQLResult sqlResult = ExecuteQuery(_queryManager.CreateBackupTable());
                return sqlResult.errorMessage;
            }
            else
                return "DB에 연결되지 않았습니다.";
        }

        public string InsertBackupProcedureContent(string name, string procedureContent)
        {
            if (_SqlCommand != null)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (string content in procedureContent.Replace("\r\n", "¶").Split("¶").ToArray())
                {   
                    stringBuilder.AppendLine(content.Replace("'", "''"));
                }
                
                SQLResult sqlResult = ExecuteQuery(_queryManager.InsertBackupProcedureContent(name, stringBuilder.ToString()));
                return sqlResult.errorMessage;
            }
            else
                return "DB에 연결되지 않았습니다.";
        }

        public string SetProcedureContent(string? procedureContent)
        {
            if (procedureContent != null) { 
                SQLResult sqlResult = ExecuteQuery(procedureContent);
                return sqlResult.errorMessage;
            }
            else
                return string.Empty;
        }

        public Tuple<DataSet?, string> ExecuteQueryGetDataSet(string query)
        {
            SQLResult sqlResult = ExecuteQuery(query);
            return new Tuple<DataSet?, string>(sqlResult.result, sqlResult.errorMessage);
        }

        private bool CheckDatabaseInfo()
        {
            return !string.IsNullOrEmpty(_DbAddress) && !string.IsNullOrEmpty(_DbName) && !string.IsNullOrEmpty(_DbId) && !string.IsNullOrEmpty(_DbPw);
        }

        private string GetConnectionString()
        {
            return $"Server={_DbAddress};database={_DbName};uid={_DbId};pwd={_DbPw}";
        }

        private SQLResult ExecuteQuery(string query, ResultType resultType = ResultType.DATA_SET)
        {
            SQLResult sqlResult = new SQLResult();
            sqlResult.result = null;
            sqlResult.errorMessage = string.Empty;

            try
            { 
                if (_SqlCommand != null)
                {
                    _SqlCommand.CommandText = query;

                    SqlDataAdapter sd = new SqlDataAdapter(_SqlCommand);
                    DataSet ds = new DataSet();
                    sd.Fill(ds, "Result");

                    if (resultType == ResultType.DATA_SET)
                        sqlResult.result = ds;
                    else if (resultType == ResultType.DATA_TABLE)
                        sqlResult.result = ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : null;
                    else
                        sqlResult.result = ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0][0].ToString() : null;
                }

                return sqlResult;
            }
            catch (Exception ex)
            {
                sqlResult.errorMessage = ex.Message;
                return sqlResult;
            }
        }
    }
}
