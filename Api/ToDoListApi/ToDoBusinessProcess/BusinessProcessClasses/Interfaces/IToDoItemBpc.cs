using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoData.Entities;

namespace ToDoBusinessProcess.BusinessProcessClasses.Interfaces
{
    public interface IToDoItemBpc
    {
        IEnumerable<ToDoItem> GetToDoItemsByUser(string userId);
        ToDoItem GetToDoItemById(int? toDoItemId);
        void InsertToDoItem(ToDoItem toDoItem);
        void DeleteToDoItem(ToDoItem toDoItem);
        void UpdateToDoItem(ToDoItem toDoItem);
        Task<bool> SaveAllAsync();
    }
}
