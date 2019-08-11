using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Data.Repositories.Interfaces;
using ToDoData;
using ToDoData.Entities;

namespace ToDoApp.Data.Repositories.Implementations
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        private readonly ToDoListContext _context;
        private bool disposed = false;

        public ToDoItemRepository(ToDoListContext context)
        {
            _context = context;
        }

        public void DeleteToDoItem(ToDoItem toDoItem)
        {
            _context.ToDoItems.Remove(toDoItem);
        }

        public ToDoItem GetToDoItemById(int? toDoItemId)
        {
            return _context.ToDoItems.Find(toDoItemId);
        }

        public List<ToDoItem> GetToDoItemsByUser(string userId)
        {
            return _context.ToDoItems.Where(x => x.UserId == userId).ToList();
        }

        public void InsertToDoItem(ToDoItem toDoItem)
        {
            _context.ToDoItems.Add(toDoItem);
        }

        public void UpdateToDoItem(ToDoItem toDoItem)
        {
            _context.Update(toDoItem);
        }

        public async Task<bool> SaveAllAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
