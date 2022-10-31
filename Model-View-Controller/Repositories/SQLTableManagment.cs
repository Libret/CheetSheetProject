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

        public static SQLiteDataReader ReadData(string tableName, string? clause)
        {
            SQLiteCommand sqlite_cmd = GetSQLiteConnection().CreateCommand();
            if(clause == null)
            {
                sqlite_cmd.CommandText = $"SELECT * FROM {tableName}";
            }
            else
            {
                sqlite_cmd.CommandText = $"SELECT * FROM {tableName} WHERE {clause}";

            }
            return sqlite_cmd.ExecuteReader();
            //GetSQLiteConnection().Close();
        }

        public static SQLiteConnection GetSQLiteConnection()
        {
            if (_conn == null)
            {
                _conn = SqliteConnect.CreateConnection();
            }
            return _conn;
        }

    }
}
