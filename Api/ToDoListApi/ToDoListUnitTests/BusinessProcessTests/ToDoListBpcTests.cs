using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoApp.Data.Repositories.Interfaces;
using ToDoBusinessProcess.BusinessProcessClasses.Implementations;
using ToDoBusinessProcess.BusinessProcessClasses.Interfaces;
using ToDoData;
using ToDoData.Entities;
using Xunit;

namespace ToDoListUnitTests.BusinessProcessTests
{
    public class ToDoListBpcTests
    {
        private readonly IToDoItemBpc _sut;
        private readonly Mock<IRepositoryFactory> _mockRepositoryFactory;
        private readonly Mock<ILoggerFactory> _mockLoggerFactory;
        private readonly Mock<IToDoItemRepository> _mockToDoItemRepository;
        private readonly Mock<ILogger> _mockLogger;

        private string AnyException = "Any exception";

        public ToDoListBpcTests()
        {
            _mockRepositoryFactory = new Mock<IRepositoryFactory>();
            _mockLoggerFactory = new Mock<ILoggerFactory>();
            _mockToDoItemRepository = new Mock<IToDoItemRepository>();
            _mockLogger = new Mock<ILogger>();

            _mockRepositoryFactory.Setup(x => x.CreateTodoItemRepository()).Returns(_mockToDoItemRepository.Object);
            _mockLoggerFactory.Setup(x => x.CreateLogger(nameof(ToDoItemBpc))).Returns(_mockLogger.Object);

            _sut = new ToDoItemBpc(_mockRepositoryFactory.Object, _mockLoggerFactory.Object);
        }

        [Fact]
        public void When_arguments_are_null_then_throw_argument_null_exception()
        {
            Assert.Throws<ArgumentNullException>(() => new ToDoItemBpc(null, _mockLoggerFactory.Object));
            Assert.Throws<ArgumentNullException>(() => new ToDoItemBpc(_mockRepositoryFactory.Object, null));
        }

        [Fact]
        public void When_deleting_task_then_call_repository_delete_method_once()
        {
            _sut.DeleteToDoItem(new ToDoItem());

            _mockToDoItemRepository.Verify(x => x.DeleteToDoItem(It.IsAny<ToDoItem>()), Times.Once);
        }

        [Fact]
        public void When_deleting_task_and_argument_is_null_throw_null_argument_exception()
        {
            Assert.Throws<ArgumentNullException>(() => _sut.DeleteToDoItem(null));
        }

        [Fact]
        public void When_deleting_task_and_something_went_wrong_raise_a_specific_exception()
        {
            _mockToDoItemRepository.Setup(x => x.DeleteToDoItem(It.IsAny<ToDoItem>())).Throws(new Exception(AnyException));

            var exception = Assert.Throws<Exception>(() => _sut.DeleteToDoItem(new ToDoItem { ToDoItemId = 1 }));

            Assert.Equal($"Something went wrong when deleting item id: 1 - {AnyException}", exception.Message);
        }

        [Fact]
        public void When_getting_task_by_id_then_return_task()
        {
            var toDoItem = new ToDoItem { Description = "Any description" };

            _mockToDoItemRepository.Setup(x => x.GetToDoItemById(It.IsAny<int>())).Returns(toDoItem);

            var result = _sut.GetToDoItemById(It.IsAny<int>());

            Assert.NotNull(result);
            Assert.Equal(result.Description, toDoItem.Description);

            _mockToDoItemRepository.Verify(x => x.GetToDoItemById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void When_getting_task_by_id_and_an_exception_occurs_then_return_specific_message()
        {
            _mockToDoItemRepository.Setup(x => x.GetToDoItemById(It.IsAny<int>())).Throws(new Exception(AnyException));

            var exception = Assert.Throws<Exception>(() => _sut.GetToDoItemById(It.IsAny<int>()));

            Assert.Equal($"Something went wrong when retrieving item: 0 - {AnyException}", exception.Message);

            _mockToDoItemRepository.Verify(x => x.GetToDoItemById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void When_getting_task_by_user_id_then_return_a_list_of_tasks()
        {
            var toDoItems = new List<ToDoItem> { new ToDoItem { ToDoItemId = 1 }, new ToDoItem { ToDoItemId = 2 } };

            _mockToDoItemRepository.Setup(x => x.GetToDoItemsByUser(It.IsAny<string>())).Returns(toDoItems);

            var result = _sut.GetToDoItemsByUser(It.IsAny<string>());

            Assert.Equal(toDoItems.Count, result.ToList().Count);
            Assert.Collection(result,
                    elem1 => { Assert.Equal(1, elem1.ToDoItemId); },
                    elem2 => { Assert.Equal(2, elem2.ToDoItemId); }
                );

            _mockToDoItemRepository.Verify(x => x.GetToDoItemsByUser(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void When_getting_task_by_user_id_and_error_occurs_then_raise_exception_an_specific_message()
        {
            _mockToDoItemRepository.Setup(x => x.GetToDoItemsByUser(It.IsAny<string>())).Throws(new Exception(AnyException));

            var exception = Assert.Throws<Exception>(() => _sut.GetToDoItemsByUser(It.IsAny<string>()));

            Assert.Equal($"Something went wrong when retrieving items for user:  - {AnyException}", exception.Message);

            _mockToDoItemRepository.Verify(x => x.GetToDoItemsByUser(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void When_inserting_new_task_and_argument_is_null_throw_null_argument_exception()
        {
            Assert.Throws<ArgumentNullException>(() => _sut.InsertToDoItem(null));
        }

        [Fact]
        public void When_adding_new_task_then_call_repository_insert_method_once()
        {
            _sut.InsertToDoItem(new ToDoItem());

            _mockToDoItemRepository.Verify(x => x.InsertToDoItem(It.IsAny<ToDoItem>()), Times.Once);
        }

        [Fact]
        public void When_adding_new_task_and_something_went_wrong_then_raise_an_exception_and_specific_message()
        {
            _mockToDoItemRepository.Setup(x => x.InsertToDoItem(It.IsAny<ToDoItem>())).Throws(new Exception(AnyException));

            var exception = Assert.Throws<Exception>(() => _sut.InsertToDoItem(new ToDoItem { ToDoItemId = 1 }));

            Assert.Equal($"Something went wrong when adding item, item id: 1 - {AnyException}", exception.Message);

            _mockToDoItemRepository.Verify(x => x.InsertToDoItem(It.IsAny<ToDoItem>()), Times.Once);
        }

        [Fact]
        public void When_updating_task_and_argument_is_null_throw_null_argument_exception()
        {
            Assert.Throws<ArgumentNullException>(() => _sut.UpdateToDoItem(null));
        }

        [Fact]
        public void When_updating_task_then_call_repository_method_once()
        {
            _sut.UpdateToDoItem(new ToDoItem());

            _mockToDoItemRepository.Verify(x => x.UpdateToDoItem(It.IsAny<ToDoItem>()), Times.Once);
        }

        [Fact]
        public void When_updating_task_and_error_occurs_then_raise_an_exception_and_specific_message()
        {
            _mockToDoItemRepository.Setup(x => x.UpdateToDoItem(It.IsAny<ToDoItem>())).Throws(new Exception(AnyException));

            var exception = Assert.Throws<Exception>(() => _sut.UpdateToDoItem(new ToDoItem()));

            Assert.Equal($"Something went wrong when updating item - itemId: 0 - {AnyException}", exception.Message);

            _mockToDoItemRepository.Verify(x => x.UpdateToDoItem(It.IsAny<ToDoItem>()), Times.Once);
        }
    }
}
