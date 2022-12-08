﻿using Microsoft.AspNetCore.Mvc;
using Model_View_Controller.Models;
using Model_View_Controller.Repositories;

namespace Model_View_Controller.Controllers
{
    [Route("api/[controller]")]
    public class CheatSheetItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public List<CheatSheetItem> GetAllItems()
        {
            return CheatSheetItemRepository.GetAllItems();
        }

        [HttpGet("{id}")]
        public CheatSheetItem? GetCheatSheetItem(string id)
        {
            return CheatSheetItemRepository.GetCheetSheetItem(id);
        }

        [HttpGet("topics/{topicId}")]
        public List<CheatSheetItem> GetItemsByTopicId(string topicId)
        {
            return CheatSheetItemRepository.GetAllItemsByTopicId(topicId);
        }

        [HttpPost]
        public void CreateNewItem([FromBody] CheatSheetItem item, [FromQuery] string? topicId)
        {
            CheatSheetItemRepository.AddNewCheetSheetItem(item, topicId);
        }
        
        [HttpPut("{id}")]
        public CheatSheetItem? UpdateItem(string id, [FromBody] CheatSheetItem item)
        {
            CheatSheetItemRepository.UpdateItemById(id, item);
            return CheatSheetItemRepository.GetCheetSheetItem(id);
        }

        [HttpDelete("{id}")]
        public void DeleteItemById(string id)
        {
            CheatSheetItemRepository.DeleteItemById(id);
        }
    }
}