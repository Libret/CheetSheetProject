namespace Model_View_Controller.Repositories
{
    public class TopicRepository
    {
        public static void AddNewTopic(string topicName)
        {
            var Id = Guid.NewGuid();
            SQLTableManagement.InsertData("Topic", "Id, Name", $"\"{Id}\", \"{topicName}\"");
        }
    }
}
