using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListPresentation.Models;

namespace ToDoListPresentation.Service
{
    public interface IToDoListService
    {
        Task<List<ToDoItemModel>> GetToDoItemsByUser(string userId);

        Task<ToDoItemModel> GetToDoItemById(int itemId);

        Task<ToDoItemModel> InsertToDoItem(ToDoItemModel toDoItem);

        Task<ToDoItemModel> UpdateToDoItem(int itemId, ToDoItemModel toDoItem);

        void DeleteToDoItem(int itemId);
    }
}
