using System.Data.SQLite;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Model_View_Controller.Repositories
{
    public class SQLTableManagement
    {
        
        public static void CreateTable(string Createsql)
        {

            SQLiteCommand sqlite_cmd = SqliteConnect.GetSQLiteConnection().CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();

        }

        public static void InsertData(string tableName, string columnNames, string values)
        {
            SQLiteCommand sqlite_cmd = SqliteConnect.GetSQLiteConnection().CreateCommand();
            sqlite_cmd.CommandText = $"INSERT INTO {tableName} ({columnNames}) VALUES({values}); ";
            sqlite_cmd.ExecuteNonQuery();

        }

        public static void UpdateData(string tableName, string setValues, string clause)
        {
            SQLiteCommand sqlite_cmd = SqliteConnect.GetSQLiteConnection().CreateCommand();
            sqlite_cmd.CommandText = $"UPDATE {tableName} SET {setValues} WHERE {clause}; ";
            sqlite_cmd.ExecuteNonQuery();

        }

        public static void DeleteData(string tableName, string clause)
        {
            SQLiteCommand sqlite_cmd = SqliteConnect.GetSQLiteConnection().CreateCommand();
            sqlite_cmd.CommandText = $"DELETE FROM {tableName} WHERE {clause}; ";
            sqlite_cmd.ExecuteNonQuery();

        }

        public static SQLiteDataReader ReadData(string tableName, string? clause)
        {
            SQLiteCommand sqlite_cmd = SqliteConnect.GetSQLiteConnection().CreateCommand();
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

        public static SQLiteDataReader ReadCustomData(string customSelectStatement)
        {
            SQLiteCommand sqliteCommand = SqliteConnect.GetSQLiteConnection().CreateCommand();
            sqliteCommand.CommandText = customSelectStatement;
            return sqliteCommand.ExecuteReader();
        }

    }
}
