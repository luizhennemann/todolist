using System;
using System.Collections.Generic;
using ToDoData.Entities;
using System.Threading.Tasks;

namespace ToDoApp.Data.Repositories.Interfaces
{
    public interface IToDoItemRepository : IDisposable
    {
        IEnumerable<ToDoItem> GetToDoItemsByUser(string userId);
        ToDoItem GetToDoItemById(int? toDoItemId);
        void InsertToDoItem(ToDoItem toDoItem);
        void DeleteToDoItem(ToDoItem toDoItem);
        void UpdateToDoItem(ToDoItem toDoItem);
        Task<bool> SaveAllAsync();
    }
}
