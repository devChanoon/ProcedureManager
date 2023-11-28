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
                query = query.Replace("@PROCEDURE_NAME", filterText);
                SQLResult sqlResult = ExecuteQuery(query, ResultType.DATA_TABLE);
                return sqlResult.result;
            }
            else
                return null;
        }

        public string GetProcedureContent(string procedureName)
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
                SQLResult sqlResult = ExecuteQuery(query, ResultType.STRING);
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
                string query = @"
SELECT [content]
  FROM procedure_backup
 WHERE id = @BACKUP_ID
";
                query = query.Replace("@BACKUP_ID", backupId);
                SQLResult sqlResult = ExecuteQuery(query, ResultType.STRING);
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
                string query = @"
SELECT id, CONVERT(char(23), insert_time, 21) as insert_time
  FROM procedure_backup
 WHERE name = '@PROCEDURE_NAME'
ORDER BY id desc
";
                query = query.Replace("@PROCEDURE_NAME", procedureName);
                SQLResult sqlResult = ExecuteQuery(query, ResultType.DATA_TABLE);
                return sqlResult.result;
            }
            else
                return null;
        }

        public bool CheckExistBackupTable()
        {
            if (_SqlCommand != null)
            {
                string query = @"
IF EXISTS (select * from sys.tables where name = 'procedure_backup')
BEGIN 
	SELECT 'Y'
END
ELSE
BEGIN
	SELECT 'N'
END
";
                SQLResult sqlResult = ExecuteQuery(query, ResultType.STRING);
                return sqlResult.result != null && sqlResult.result == "Y";
            }
            else
                return false;
        }

        public string CreateBackupTable()
        {
            if (_SqlCommand != null)
            {
                string query = @"
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[procedure_backup](
	[name] [nvarchar](50) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [varchar](3) NULL,
	[content] [nvarchar](max) NULL,
	[insert_time] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[procedure_backup] ADD  CONSTRAINT [DF_procedure_backup_insert_time]  DEFAULT (getdate()) FOR [insert_time]
";
                SQLResult sqlResult = ExecuteQuery(query);
                return sqlResult.errorMessage;
            }
            else
                return "DB에 연결되지 않았습니다.";
        }

        public string InsertBackupProcedureContent(string name, string type, string procedureContent)
        {
            if (_SqlCommand != null)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (string content in procedureContent.Replace("\r\n", "¶").Split("¶").ToArray())
                {   
                    stringBuilder.AppendLine(content.Replace("'", "''"));
                }

                string query = @"
INSERT INTO procedure_backup (name, type, [content])
VALUES ('@PROCEDURE_NAME', '@TYPE', '@PROCEDURE_CONTENT')
";
                query = query.Replace("@PROCEDURE_NAME", name);
                query = query.Replace("@TYPE", type);
                query = query.Replace("@PROCEDURE_CONTENT", stringBuilder.ToString());

                SQLResult sqlResult = ExecuteQuery(query);
                return sqlResult.errorMessage;
            }
            else
                return "DB에 연결되지 않았습니다.";
        }

        public string SetProcedureContent(string procedureContent)
        {
            SQLResult sqlResult = ExecuteQuery(procedureContent);
            return sqlResult.errorMessage;
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

                    if (ds != null)
                    {
                        if (resultType == ResultType.DATA_SET)
                        {
                            sqlResult.result = ds;
                            return sqlResult;
                        }
                        else
                        {
                            if (ds.Tables.Count > 0)
                            {
                                if (resultType == ResultType.DATA_TABLE)
                                {
                                    sqlResult.result = ds.Tables[0];
                                    return sqlResult;
                                }
                                else if (ds.Tables[0].Rows.Count > 0)
                                {
                                    sqlResult.result = ds.Tables[0].Rows[0][0].ToString();
                                    return sqlResult;
                                }
                            }
                        }
                    }
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
