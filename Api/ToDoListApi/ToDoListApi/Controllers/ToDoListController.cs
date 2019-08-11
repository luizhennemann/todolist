using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoBusinessProcess;
using ToDoBusinessProcess.BusinessProcessClasses.Interfaces;
using ToDoData.Entities;

namespace ToDoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<ToDoListController> _logger;
        private readonly IBpcFactory _bpcFactory;
        private readonly IToDoItemBpc _toDoItemBpc;

        public ToDoListController(IBpcFactory bpcFactory, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<ToDoListController>();

            _bpcFactory = bpcFactory;
            _toDoItemBpc = _bpcFactory.CreateToDoItemBpc();
        }

        [HttpGet("gettodoitemsbyuser/{userId}", Name = "ToDoItemsByUser")]
        public ActionResult<List<ToDoItem>> GetToDoItemsByUser(string userId)
        {
            try
            {
                var toDoItems = _toDoItemBpc.GetToDoItemsByUser(userId);

                if (toDoItems.Count() == 0)
                {
                    return NotFound($"No items found for user: {userId}");
                }

                return Ok(toDoItems);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong when getting tasks by user. userId: {userId} - {e.Message}");
            }

            return BadRequest();
        }

        [HttpGet("{itemId}")]
        public ActionResult<string> Get(int itemId)
        {
            try
            {
                var toDoItem = _toDoItemBpc.GetToDoItemById(itemId);

                if (toDoItem == null)
                {
                    return NotFound($"No item found for id: {itemId}");
                }

                return Ok(toDoItem);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong when getting task by id. itemId: {itemId} - {e.Message}");
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ToDoItem toDoItem)
        {
            try
            {
                _toDoItemBpc.InsertToDoItem(toDoItem);

                if (await _toDoItemBpc.SaveAllAsync())
                {
                    var uri = Url.Link("ToDoItemsByUser", new { userId = toDoItem.UserId });

                    return Created(uri, toDoItem);
                }
                else
                {
                    _logger.LogWarning($"Could not save task in the database. itemId: {toDoItem.ToDoItemId}");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong when saving the task. itemId: {toDoItem.ToDoItemId} - {e.Message}");
            }

            return BadRequest();
        }

        [HttpPut("{itemId}")]
        public async Task<IActionResult> Put(int itemId, [FromBody] ToDoItem toDoItem)
        {
            try
            {
                var oldToDoItem = _toDoItemBpc.GetToDoItemById(itemId);

                if (oldToDoItem == null)
                {
                    return NotFound($"No item found for id: {itemId}");
                }
                else
                {
                    oldToDoItem.Description = toDoItem.Description ?? oldToDoItem.Description;
                    oldToDoItem.ExpectedDate = toDoItem.ExpectedDate;
                    oldToDoItem.Done = toDoItem.Done;
                    oldToDoItem.LastUpdateDate = DateTime.Now;

                    _toDoItemBpc.UpdateToDoItem(oldToDoItem);

                    if (await _toDoItemBpc.SaveAllAsync())
                    {
                        return Ok(oldToDoItem);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong when updating task. itemId: {toDoItem} - {e.Message}");
            }

            return BadRequest();
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> Delete(int itemId)
        {
            try
            {
                var toDoItem = _toDoItemBpc.GetToDoItemById(itemId);

                if (toDoItem == null)
                {
                    return NotFound($"No item found for id: {itemId}");
                }
                else
                {
                    _toDoItemBpc.DeleteToDoItem(toDoItem);

                    if (await _toDoItemBpc.SaveAllAsync())
                    {
                        return Ok();
                    }
                }

            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong when deleting task. itemId: {itemId} - {e.Message}");
            }

            return BadRequest();
        }
    }
}
