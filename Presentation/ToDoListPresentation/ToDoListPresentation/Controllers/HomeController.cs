using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListPresentation.Models;
using ToDoListPresentation.Service;

namespace ToDoListPresentation.Controllers
{
    public class HomeController : Controller
    {
        private string _menuVisibility = "hidden";
        private readonly IToDoListService _toDoListService;

        public HomeController(IToDoListService toDoListService)
        {
            _toDoListService = toDoListService;
        }

        public IActionResult Index(bool displayInvalidLoginMessage = false)
        {
            ViewData["Hidden"] = _menuVisibility;

            TempData["userId"] = string.Empty;
            TempData["name"] = string.Empty;
            TempData["password"] = string.Empty;

            if (displayInvalidLoginMessage)
            {
                ModelState.AddModelError(string.Empty, "Invalid user/password.");
            }

            return View();
        }

        public async Task<IActionResult> ToDoList(string name, string password)
        {
            var user = GetUser(name, password);

            if (user == null)
            {
                return RedirectToAction("Index", new { displayInvalidLoginMessage = true });
            }

            TempData["userId"] = user.UserId;
            TempData["name"] = user.Name;
            TempData["password"] = user.Password;

            _menuVisibility = "visible";

            var toDoItems = await _toDoListService.GetToDoItemsByUser(user.UserId);

            return View(toDoItems.OrderBy(x => x.Description).OrderByDescending(y => y.ExpectedDate).OrderBy(x => x.Done));
        }

        public IActionResult ToDoListLogged()
        {
            return RedirectToAction(nameof(ToDoList), new { name = TempData["name"], password = TempData["password"] });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ToDoItemId,Description,ExpectedDate,Done,UserId")] ToDoItemModel toDoItem)
        {
            if (ModelState.IsValid)
            {
                toDoItem.UserId = TempData["userId"] as string;

                var response = await _toDoListService.InsertToDoItem(toDoItem);

                return RedirectToAction(nameof(ToDoListLogged));
            }

            return View(toDoItem);
        }

        public async Task<IActionResult> Edit(int itemId)
        {
            var toDoItem = await _toDoListService.GetToDoItemById(itemId);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int toDoItemId, [Bind("ToDoItemId,Description,ExpectedDate,Done,UserId")] ToDoItemModel toDoItem)
        {
            if (toDoItemId != toDoItem.ToDoItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                toDoItem.UserId = TempData["userId"] as string;

                var toDoItemUpdated = await _toDoListService.UpdateToDoItem(toDoItemId, toDoItem);

                if (toDoItemUpdated != null)
                {
                    return RedirectToAction(nameof(ToDoListLogged));
                }
            }

            return View(toDoItem);
        }

        public async Task<IActionResult> Details(int itemId)
        {
            var toDoItem = await _toDoListService.GetToDoItemById(itemId);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem);
        }

        public async Task<IActionResult> Delete(int itemId)
        {
            var toDoItem = await _toDoListService.GetToDoItemById(itemId);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int toDoItemId)
        {
            _toDoListService.DeleteToDoItem(toDoItemId);

            return RedirectToAction(nameof(ToDoListLogged));
        }

        private User GetUser(string name, string password)
        {
            var users = new List<User>
            {
                new User { UserId = "Id1", Name = "deloitte", Password = "123" },
                new User { UserId = "Id2", Name = "luizmarcelo", Password = "789" }
            };

            var user = users.Where(x => x.Name == name && x.Password == password).FirstOrDefault();

            return user;
        }
    }
}
