using Model_View_Controller.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

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
