using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Data.Repositories.Interfaces;
using ToDoBusinessProcess.BusinessProcessClasses.Interfaces;
using ToDoData;
using ToDoData.Entities;

namespace ToDoBusinessProcess.BusinessProcessClasses.Implementations
{
    public class ToDoItemBpc : IToDoItemBpc
    {
        private readonly ILogger _logger;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IToDoItemRepository _toDoItemRepository;

        public ToDoItemBpc(IRepositoryFactory repositoryFactory, ILoggerFactory loggerFactory)
        {
            if (repositoryFactory == null) throw new ArgumentNullException(nameof(repositoryFactory));
            if (loggerFactory == null) throw new ArgumentNullException(nameof(loggerFactory));

            _logger = loggerFactory.CreateLogger(nameof(ToDoItemBpc));

            _repositoryFactory = repositoryFactory;
            _toDoItemRepository = _repositoryFactory.CreateTodoItemRepository();
        }

        public void DeleteToDoItem(ToDoItem toDoItem)
        {
            if (toDoItem == null) throw new ArgumentNullException(nameof(toDoItem));

            try
            {
                _toDoItemRepository.DeleteToDoItem(toDoItem);
            }
            catch (Exception e)
            {
                var errorMessage = $"Something went wrong when deleting item id: {toDoItem.ToDoItemId} - {e.Message}";

                _logger.LogError(errorMessage);

                throw new Exception(errorMessage);
            }
        }

        public ToDoItem GetToDoItemById(int? toDoItemId)
        {
            try
            {
                var toDoItem = _toDoItemRepository.GetToDoItemById(toDoItemId);

                return toDoItem;
            }
            catch (Exception e)
            {
                var errorMessage = $"Something went wrong when retrieving item: {toDoItemId} - {e.Message}";

                _logger.LogError(errorMessage);

                throw new Exception(errorMessage);
            }
        }

        public IEnumerable<ToDoItem> GetToDoItemsByUser(string userId)
        {
            try
            {
                var toDoItems = _toDoItemRepository.GetToDoItemsByUser(userId);

                return toDoItems;
            }
            catch (Exception e)
            {
                var errorMessage = $"Something went wrong when retrieving items for user: {userId} - {e.Message}";

                _logger.LogError(errorMessage);

                throw new Exception(errorMessage);
            }
        }

        public void InsertToDoItem(ToDoItem toDoItem)
        {
            if (toDoItem == null) throw new ArgumentNullException(nameof(toDoItem));

            try
            {
                _toDoItemRepository.InsertToDoItem(toDoItem);
            }
            catch (Exception e)
            {
                var errorMessage = $"Something went wrong when adding item, item id: {toDoItem.ToDoItemId} - {e.Message}";

                _logger.LogError(errorMessage);

                throw new Exception(errorMessage);
            }
        }

        public void UpdateToDoItem(ToDoItem toDoItem)
        {
            if (toDoItem == null) throw new ArgumentNullException(nameof(toDoItem));

            try
            {
                _toDoItemRepository.UpdateToDoItem(toDoItem);
            }
            catch (Exception e)
            {
                var errorMessage = $"Something went wrong when updating item - itemId: {toDoItem.ToDoItemId} - {e.Message}";

                _logger.LogError(errorMessage);

                throw new Exception(errorMessage);
            }
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _toDoItemRepository.SaveAllAsync();
        }
    }
}
