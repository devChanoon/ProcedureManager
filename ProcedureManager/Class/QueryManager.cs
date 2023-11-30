using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProcedureManager
{
    public class QueryManager
    {
        public string SelectProcedureList(string filterText)
        {
            string query = @"
SELECT a.name as procedure_name
     , a.type as type
  FROM sys.objects a
 WHERE a.type_desc in ('SQL_SCALAR_FUNCTION', 'SQL_TABLE_VALUED_FUNCTION', 'SQL_STORED_PROCEDURE')
   AND a.name like '%@PROCEDURE_NAME%'
ORDER BY a.name
";
            return query.Replace("@PROCEDURE_NAME", filterText);
        }

        public string SelectProcedureContent(string procedureName)
        {
            string query = @"
SELECT REPLACE(b.definition, char(10), '\r\n')
  FROM sys.objects a
       INNER JOIN sys.sql_modules b ON a.object_id = b.object_id
 WHERE a.type_desc in ('SQL_SCALAR_FUNCTION', 'SQL_TABLE_VALUED_FUNCTION', 'SQL_STORED_PROCEDURE')
   AND a.name = '@PROCEDURE_NAME'
";
            return query.Replace("@PROCEDURE_NAME", procedureName);
        }

        public string SelectBackupProcedureContent(string backupId)
        {
            string query = @"
SELECT [content]
  FROM procedure_backup
 WHERE id = @BACKUP_ID
";
            return query.Replace("@BACKUP_ID", backupId);
        }

        public string SelectBackupList(string procedureName)
        {
            string query = @"
SELECT id, CONVERT(char(23), insert_time, 21) as insert_time
  FROM procedure_backup
 WHERE name = '@PROCEDURE_NAME'
ORDER BY id desc
";
            return query.Replace("@PROCEDURE_NAME", procedureName);
        }

        public string SelectExistBackupTable()
        {
            return @"
IF EXISTS (select * from sys.tables where name = 'procedure_backup')
BEGIN 
	SELECT 'Y'
END
ELSE
BEGIN
	SELECT 'N'
END
";
        }

        public string CreateBackupTable()
        {
            return @"
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
        }

        public string InsertBackupProcedureContent(string procedureName, string procedureContent)
        {
            string query = @"
INSERT INTO procedure_backup (name, type, [content])
SELECT name, type, '@PROCEDURE_CONTENT'
  FROM sys.objects
 WHERE name = '@PROCEDURE_NAME'
";
            query = query.Replace("@PROCEDURE_NAME", procedureName);
            return query.Replace("@PROCEDURE_CONTENT", procedureContent);
        }
    }
}
