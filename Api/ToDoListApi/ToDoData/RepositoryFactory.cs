using ToDoApp.Data.Repositories.Implementations;
using ToDoApp.Data.Repositories.Interfaces;

namespace ToDoData
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly ToDoListContext _context;

        public RepositoryFactory(ToDoListContext context)
        {
            _context = context;
        }
        public IToDoItemRepository CreateTodoItemRepository()
        {
            return new ToDoItemRepository(_context);
        }
    }
}
