using System.Data.SQLite;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Model_View_Controller.Repositories
{
    public class SQLTableManagement
    {
        private static SQLiteConnection _conn;
        public static void CreateTable(string Createsql)
        {

            SQLiteCommand sqlite_cmd = GetSQLiteConnection().CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();

        }

        public static void InsertData(string tableName, string columnNames, string values)
        {
            SQLiteCommand sqlite_cmd = GetSQLiteConnection().CreateCommand();
            sqlite_cmd.CommandText = $"INSERT INTO {tableName} ({columnNames}) VALUES({values}); ";
            sqlite_cmd.ExecuteNonQuery();

        }

        public static void ReadData(string tableName)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = GetSQLiteConnection().CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM {tableName}";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Console.WriteLine(myreader);
            }
            GetSQLiteConnection().Close();
        }

        private static SQLiteConnection GetSQLiteConnection()
        {
            if (_conn == null)
            {
                _conn = SqliteConnect.CreateConnection();
            }
            return _conn;
        }

    }
}
