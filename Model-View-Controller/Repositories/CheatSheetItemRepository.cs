using Model_View_Controller.Models;
using System.Data.SQLite;

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

        public static CheatSheetItem? GetItemWithAllLinks(string itemIdForSelect)
        {
            var statement = "SELECT CheetSheetItem.Id AS ItemId, CheetSheetItem.Name, " +
                "CheetSheetItem.CodeSnippet, CheetSheetItem.AdditionalInfo, " +
                "UsefulLink.Id As ItemId, UsefulLink.LinkAddress, UsefulLink.LinkOrder\r\n" +
                "FROM CheetSheetItem\r\n" +
                "LEFT JOIN UsefulLink ON CheetSheetItem.Id = UsefulLink.CheetSheetItemId\r\n" +
                $"WHERE CheetSheetItem.Id = \"{itemIdForSelect}\";";
            SQLiteDataReader sqlite_datareader = SQLTableManagement.ReadCustomData(statement);
            CheatSheetItem item = null;

            while (sqlite_datareader.Read())
            {
                var itemId = sqlite_datareader.GetString(0);

                if (item == null)
                {
                    var itemName = sqlite_datareader.GetString(1);
                    string codeSnippet = null;
                    if (sqlite_datareader[2] != DBNull.Value) 
                    { codeSnippet = sqlite_datareader.GetString(2); }
                    string additionalInfo = null;
                    if (sqlite_datareader[3] != DBNull.Value)
                    { additionalInfo = sqlite_datareader.GetString(3); }

                    item = new CheatSheetItem
                    {
                        Id = itemId,
                        Name = itemName,
                        CodeSnippet = codeSnippet,
                        AdditionalInfo = additionalInfo
                    };
                }

                UsefulLink link = null;
                if (sqlite_datareader[4] != DBNull.Value)
                {
                    var linkId = sqlite_datareader.GetString(4);
                    var linkAddress = sqlite_datareader.GetString(5);
                    var linkOrder = sqlite_datareader.GetInt32(6);
                    link = new UsefulLink
                    {
                        Id = linkId,
                        LinkAddress = linkAddress,
                        LinkOrder = linkOrder
                    };
                    item.UsefulLinks.Add(link);
                }
            }
            return item;
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
