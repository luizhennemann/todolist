using Microsoft.Extensions.Logging;
using SpanJson;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ToDoListPresentation.Models;

namespace ToDoListPresentation.Service
{
    public class ToDoListService : IToDoListService
    {
        private readonly ILogger<ToDoListService> _logger;
        private readonly HttpClient _client;

        public ToDoListService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ToDoListService>();

            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:8888")
            };
        }

        public async Task<List<ToDoItemModel>> GetToDoItemsByUser(string userId)
        {
            var toDoItems = new List<ToDoItemModel>();

            try
            {
                _logger.LogInformation($"Calling api/todolist/gettodoitemsbyuser");

                var response = await _client.GetAsync($"/api/todolist/gettodoitemsbyuser/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    toDoItems = JsonSerializer.Generic.Utf16.Deserialize<List<ToDoItemModel>>(result);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong when calling api/todolist/gettodoitemsbyuser. {e.Message}");
            }

            return toDoItems;
        }

        public async Task<ToDoItemModel> GetToDoItemById(int itemId)
        {
            var toDoItem = new ToDoItemModel();

            try
            {
                _logger.LogInformation($"Calling api/todolist/get");

                var response = await _client.GetAsync($"/api/todolist/{itemId}");

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    toDoItem = JsonSerializer.Generic.Utf16.Deserialize<ToDoItemModel>(result);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong when calling api/todolist/get. {e.Message}");
            }

            return toDoItem;
        }

        public async Task<ToDoItemModel> InsertToDoItem(ToDoItemModel toDoItem)
        {
            var toDoItemAdded = new ToDoItemModel();

            try
            {
                _logger.LogInformation($"Calling api/todolist/post");

                var response = await _client.PostAsJsonAsync("/api/todolist", toDoItem);

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    toDoItem = JsonSerializer.Generic.Utf16.Deserialize<ToDoItemModel>(result);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong when calling api/todolist/post. {e.Message}");
            }

            return toDoItemAdded;
        }

        public async Task<ToDoItemModel> UpdateToDoItem(int itemId, ToDoItemModel toDoItem)
        {
            var toDoItemUpdated = new ToDoItemModel();

            try
            {
                _logger.LogInformation($"Calling api/todolist/put");

                var response = await _client.PutAsJsonAsync($"/api/todolist/{itemId}", toDoItem);

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    toDoItemUpdated = JsonSerializer.Generic.Utf16.Deserialize<ToDoItemModel>(result);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong when calling api/todolist/put. {e.Message}");
            }

            return toDoItemUpdated;
        }

        public async void DeleteToDoItem(int itemId)
        {
            try
            {
                _logger.LogInformation($"Calling api/todolist/delete");

                await _client.DeleteAsync($"/api/todolist/{itemId}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong when calling api/todolist/delete. {e.Message}");
            }
            
        }
    }
}
