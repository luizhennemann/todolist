using ToDoBusinessProcess.BusinessProcessClasses.Interfaces;

namespace ToDoBusinessProcess
{
    public interface IBpcFactory
    {
        IToDoItemBpc CreateToDoItemBpc();
    }
}
