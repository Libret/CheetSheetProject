using Model_View_Controller.Models;

namespace Model_View_Controller.Repositories
{
    public class CheetSheetItemRepository
    {
        public static void AddNewCheetSheetItem(string cheetSheetItemName, string codeSnippet, string additionalInfo, string topicId)
        {
            var id = Guid.NewGuid();
            SQLTableManagement.InsertData("CheetSheetItem", "Id, Name, CodeSnippet, AdditionalInfo, TopicId", $"\"{id}\", \"{cheetSheetItemName}\", \"{codeSnippet}\", \"{additionalInfo}\", \"{topicId}\"");
        }

        public static List<CheetSheetItem> GetAllCheetSheetItems()
        {
            var allCheetSheetItems = new List<CheetSheetItem>();
            var sqlite_datareader = SQLTableManagement.ReadData("CheetSheetItem");
            while (sqlite_datareader.Read())
            {
                string id = sqlite_datareader.GetString(0);
                string name = sqlite_datareader.GetString(1);
                string codeSnippet = sqlite_datareader.GetString(2);
                string additionalInfo = sqlite_datareader.GetString(3);
                allCheetSheetItems.Add(new CheetSheetItem
                {
                    Id = id,
                    Name = name,
                    CodeSnippet = codeSnippet,
                    AdditionalInfo = additionalInfo,
                });
            }
            SQLTableManagement.GetSQLiteConnection().Close();
            return allCheetSheetItems;
        }
    }
}
