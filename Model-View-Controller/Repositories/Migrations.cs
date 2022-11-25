using System.Data.SQLite;

namespace Model_View_Controller.Repositories
{
    public class Migrations
    {
        public static void Run()
        {
            //SQLTableManagement.CreateTable("CREATE TABLE Topic (Id VARCHAR(20) NOT NULL, Name VARCHAR(200), PRIMARY KEY (Id))");
            //SQLTableManagement.CreateTable("CREATE TABLE CheetSheetItem (Id VARCHAR(20) NOT NULL, Name VARCAR(200), CodeSnippet TEXT, AdditionalInfo TEXT, TopicId VARCHAR(20), PRIMARY KEY (Id), FOREIGN KEY (TopicId) REFERENCES Topic(Id))");
            //SQLTableManagement.CreateTable("CREATE TABLE UsefulLink (Id VARCHAR(20) NOT NULL, LinkAddress VARCHAR(200), LinkOrder INT, CheetSheetItemId VARCHAR(20), PRIMARY KEY(Id), FOREIGN KEY (CheetSheetItemId) REFERENCES CheetSheetItem(Id))");

        }
    }
}
