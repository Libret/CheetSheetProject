using System;
using System.Data.Entity;
using System.Data.SQLite;

namespace Model_View_Controller.Repositories
{
    public class SqliteConnect
    {
        public static SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=database.db; Version = 3; New = True; Compress = True;");
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }
            return sqlite_conn;
        }

        public static void CoseConnections(SQLiteDataReader sqlite_datareader)
        {
            sqlite_datareader.Close();
            SQLTableManagement.GetSQLiteConnection().Close();
        }
    }
}
