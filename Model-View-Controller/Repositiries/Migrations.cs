using System.Data.SQLite;

namespace Model_View_Controller.Repositiries
{
    public class Migrations
    {
        public static void Run(SQLiteConnection conn)
        {
            SQLTableManagement.CreateTable(conn, "CREATE TABLE Topic (Id VARCHAR(20), Name VARCHAR(200))");
            SQLTableManagement.CreateTable(conn, "CREATE TABLE CheetSheetItem (Id VARCHAR(20), Name VARCAR(200), CodeSnippet TEXT, AdditionalInfo TEXT, TopicId VARCHAR(20))");
            SQLTableManagement.CreateTable(conn, "CREATE TABLE UsefulLink (Id VARCHAR(20), LinkAddress VARCHAR(200), LinkOrder INT, CheetSheetItemId VARCHAR(20)");
            
        }
    }
}
