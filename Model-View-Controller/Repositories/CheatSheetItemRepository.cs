using Model_View_Controller.Models;

namespace Model_View_Controller.Repositories
{
    public class CheatSheetItemRepository
    {
        private static readonly string stringCheetSheetItem = "CheetSheetItem";

        public static void AddNewCheetSheetItem(CheatSheetItem cheetSheetItem, string? topicId)
        {
            var id = Guid.NewGuid();
            var columnNames = "Id, Name, CodeSnippet, AdditionalInfo, TopicId";
            var columnValues = $"\"{id}\", \"{cheetSheetItem.Name}\", \"{cheetSheetItem.CodeSnippet}\", \"{cheetSheetItem.AdditionalInfo}\", \"{topicId}\"";

            SQLTableManagement.InsertData(stringCheetSheetItem, columnNames, columnValues);
            
        }

        public static List<CheatSheetItem> GetAllItems()
        {
            var allCheetSheetItems = new List<CheatSheetItem>();
            var sqlite_datareader = SQLTableManagement.ReadData(stringCheetSheetItem, null);
            while (sqlite_datareader.Read())
            {
                string id = sqlite_datareader.GetString(0);
                string name = sqlite_datareader.GetString(1);
                string codeSnippet = sqlite_datareader.GetString(2);
                string additionalInfo = sqlite_datareader.GetString(3);
                string topicId = sqlite_datareader.GetString(4);
                allCheetSheetItems.Add(new CheatSheetItem
                {
                    Id = id,
                    Name = name,
                    CodeSnippet = codeSnippet,
                    AdditionalInfo = additionalInfo,
                });
            }
            SqliteConnect.CoseConnections(sqlite_datareader);
            return allCheetSheetItems;
        }

        public static List<CheatSheetItem> GetAllItemsByTopicId(string topicId)
        {
            var allCheetSheetItemsForTopic = new List<CheatSheetItem>();
            var clause = $"TopicId = \"{topicId}\"";
            var sqlite_datareader = SQLTableManagement.ReadData(stringCheetSheetItem, clause);
            while (sqlite_datareader.Read())
            {
                string id = sqlite_datareader.GetString(0);
                string name = sqlite_datareader.GetString(1);
                string codeSnippet = sqlite_datareader.GetString(2);
                string additionalInfo = sqlite_datareader.GetString(3);
                
                allCheetSheetItemsForTopic.Add(new CheatSheetItem
                {
                    Id = id,
                    Name = name,
                    CodeSnippet = codeSnippet,
                    AdditionalInfo = additionalInfo,
                });
            }
            SqliteConnect.CoseConnections(sqlite_datareader);
            return allCheetSheetItemsForTopic;
        }

        public static CheatSheetItem? GetCheetSheetItem(string id)
        {
            string clause = $"id = \"{id}\"";
            var sqlite_datareader = SQLTableManagement.ReadData(stringCheetSheetItem, clause);
            while (sqlite_datareader.Read())
            {
                string name = sqlite_datareader.GetString(1);
                string codeSnippet = sqlite_datareader.GetString(2);
                string additionalInfo = sqlite_datareader.GetString(3);
                SqliteConnect.CoseConnections(sqlite_datareader);
                return new CheatSheetItem
                {
                    Id = id,
                    Name = name,
                    CodeSnippet = codeSnippet,
                    AdditionalInfo = additionalInfo,
                };
            }
            SqliteConnect.CoseConnections(sqlite_datareader);
            return null;
        }

        public static void UpdateItemById(string id, CheatSheetItem cheetSheetItem)
        {
            var clause = $"Id = \"{id}\"";
            var setItem = "";
            if(cheetSheetItem.Name != null)
            {
                setItem += $"Name = \"{cheetSheetItem.Name}\", ";
            }
            setItem += $"CodeSnippet = \"{cheetSheetItem.CodeSnippet}\", ";
            setItem += $"AdditionalInfo = \"{cheetSheetItem.AdditionalInfo}\"";
            SQLTableManagement.UpdateData(stringCheetSheetItem, setItem, clause);
        }

        public static void DeleteItemById(string id)
        {
            var clause = $"Id = \"{id}\"";
            SQLTableManagement.DeleteData(stringCheetSheetItem, clause);
        }

        public static void DeleteItemByName(string name)
        {
            var clause = $"Name = \"{name}\"";
            SQLTableManagement.DeleteData(stringCheetSheetItem, clause);
        }
    }
}
