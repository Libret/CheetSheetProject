using Model_View_Controller.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Data.SQLite;

namespace Model_View_Controller.Repositories
{
    public class TopicRepository
    {
        private static readonly string stringTopic = "Topic";

        public static void AddNewTopic(string topicName)
        {
            var id = Guid.NewGuid();
            SQLTableManagement.InsertData(stringTopic, "Id, Name", $"\"{id}\", \"{topicName}\"");
        }

        public static List<Topic> GetAllTopics()
        {
            var allTopics = new List<Topic>();
            var sqlite_datareader = SQLTableManagement.ReadData(stringTopic, null);
            while (sqlite_datareader.Read())
            {
                string id = sqlite_datareader.GetString(0);
                string name = sqlite_datareader.GetString(1);
                allTopics.Add(new Topic
                {
                    Id = id,
                    Name = name
                });
            }
            SqliteConnect.CoseConnections(sqlite_datareader);
            return allTopics;
        }

        public static Topic? GetTopic(string id)
        {
            string clause = $"id = \"{id}\"";
            var sqlite_datareader = SQLTableManagement.ReadData(stringTopic, clause);
            while (sqlite_datareader.Read())
            {
                string name = sqlite_datareader.GetString(1);
                SqliteConnect.CoseConnections(sqlite_datareader);
                return new Topic
                {
                    Id = id,
                    Name = name
                };
            }
            SqliteConnect.CoseConnections(sqlite_datareader);
            return null;
        }

        public static Topic? GetTopicWithAllItems(string topicIdForSelect)
        {
            string statement = "SELECT Topic.Id AS TopicId, Topic.Name, " +
                "CheetSheetItem.Id AS ItemId, CheetSheetItem.Name, CheetSheetItem.CodeSnippet, " +
                "CheetSheetItem.AdditionalInfo, " +
                "UsefulLink.Id AS LinkId, UsefulLink.LinkAddress, UsefulLink.LinkOrder\n" +
                "FROM Topic\n" +
                "LEFT JOIN CheetSheetItem ON Topic.Id = CheetSheetItem.TopicId\n" +
                "LEFT JOIN UsefulLink ON CheetSheetItem.Id = UsefulLink.CheetSheetItemId\n" +
                $"WHERE Topic.Id = \"{topicIdForSelect}\";";
            SQLiteDataReader sqlite_datareader = SQLTableManagement.ReadCustomData(statement);
            Topic topic = null;
            var cheatSheetItems = new LinkedList<CheatSheetItem>();

            while (sqlite_datareader.Read())
            {
                var topicId = sqlite_datareader.GetString(0);
                var topicName = sqlite_datareader.GetString(1);

                if(topic == null)
                {
                    topic = new Topic
                    {
                        Id = topicId,
                        Name = topicName
                    };
                }

                CheatSheetItem item = null;
                if (sqlite_datareader[2] != DBNull.Value)
                {
                    var itemId = sqlite_datareader.GetString(2);
                    if(cheatSheetItems.Where(i => i.Id == itemId).Count() > 0)
                    {
                        item = cheatSheetItems.Where(i => i.Id == itemId).First();
                    }
                    else
                    {
                        var itemName = sqlite_datareader.GetString(3);
                        var codeSnippet = sqlite_datareader.GetString(4);
                        var additionalInfo = sqlite_datareader.GetString(5);

                        item = new CheatSheetItem
                        {
                            Id = itemId,
                            Name = itemName,
                            CodeSnippet = codeSnippet,
                            AdditionalInfo = additionalInfo
                        };
                        cheatSheetItems.AddLast(item);
                    }

                    if (item != null)
                    {
                        if (!topic.CheetSheetItems.Contains(item))
                        {
                            topic.CheetSheetItems.Add(item);
                        }
                    }

                    UsefulLink link = null;
                    if (sqlite_datareader[6] != DBNull.Value)
                    {
                        var linkId = sqlite_datareader.GetString(6);
                        var linkAddress = sqlite_datareader.GetString(7);
                        var linkOrder = sqlite_datareader.GetInt32(8);

                        link = new UsefulLink
                        {
                            Id = linkId,
                            LinkAddress = linkAddress,
                            LinkOrder = linkOrder
                        };
                        item.UsefulLinks.Add(link);
                    }
                }
            }
            return topic;
            
        }

        public static void UpdateTopicNameById(string id, string name)
        {
            var setName = $"Name = \"{name}\"";
            var clause = $"Id = \"{id}\"";
            SQLTableManagement.UpdateData(stringTopic, setName, clause);
        }

        public static void DeleteTopicById(string id)
        {
            var clause = $"Id = \"{id}\"";
            SQLTableManagement.DeleteData(stringTopic, clause);
        }

        public static void DeleteTopicByName(string name)
        {
            var clause = $"Name = \"{name}\"";
            SQLTableManagement.DeleteData(stringTopic, clause);
        }
    }
}
