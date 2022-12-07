using Microsoft.AspNetCore.Mvc;
using Model_View_Controller.Models;
using Model_View_Controller.Repositories;

namespace Model_View_Controller.Controllers
{
    [Route("api/topic")]
    public class TopicController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public List<Topic> GetAllTopics()
        {
            return TopicRepository.GetAllTopics();
        }

        [HttpGet("{topicId}")]
        public Topic? GetDitaledTopicData(string topicId)
        {
            return TopicRepository.GetTopicWithAllItems(topicId);
        }

        [HttpPost]
        public void CreateNewTopic([FromBody] Topic topic)
        {
            TopicRepository.AddNewTopic(topic.Name);
        }

        [HttpDelete("{id}")]
        public void DeleteTopic(string id)
        {
            TopicRepository.DeleteTopicById(id);
        }

        [HttpPut("{id}")]
        public Topic? UpdateTopic(string id, [FromBody] string name)
        {
            TopicRepository.UpdateTopicNameById(id, name);
            return TopicRepository.GetTopicWithAllItems(id);
        }
    }
}
