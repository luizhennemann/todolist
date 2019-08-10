using ToDoApp.Data.Repositories.Interfaces;

namespace ToDoData
{
    public interface IRepositoryFactory
    {
        IToDoItemRepository CreateTodoItemRepository();
    }
}
