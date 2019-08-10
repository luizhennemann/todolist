using Microsoft.Extensions.Logging;
using ToDoBusinessProcess.BusinessProcessClasses.Implementations;
using ToDoBusinessProcess.BusinessProcessClasses.Interfaces;
using ToDoData;

namespace ToDoBusinessProcess
{
    public class BpcFactory : IBpcFactory
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly ILoggerFactory _loggerFactory;

        public BpcFactory(IRepositoryFactory repositoryFactory, ILoggerFactory loggerFactory)
        {
            _repositoryFactory = repositoryFactory;
            _loggerFactory = loggerFactory;
        }

        public IToDoItemBpc CreateToDoItemBpc()
        {
            return new ToDoItemBpc(_repositoryFactory, _loggerFactory);
        }
    }
}
