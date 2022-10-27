using System.Data.SQLite;
using System.Data.SqlClient;

namespace Model_View_Controller.Repositiries
{
    public class SQLTableManagement
    {
        public static void CreateTable(SQLiteConnection conn, string Createsql)
        {

            SQLiteCommand sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();

        }

        public static void InsertData(SQLiteConnection conn, string tableName, string columnNames, string values)
        {
            SQLiteCommand sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = $"INSERT INTO {tableName} ({columnNames}) VALUES({values}); ";
            sqlite_cmd.ExecuteNonQuery();

        }

        public static void ReadData(SQLiteConnection conn, string tableName)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM {tableName}";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Console.WriteLine(myreader);
            }
            conn.Close();
        }
    }
}
