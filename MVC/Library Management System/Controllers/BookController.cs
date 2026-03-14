using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _repo;

        public BookController(IBookRepository repo)
        {
            _repo = repo;
        }

        // GET: Book/List
        public IActionResult List()
        {
            var books = _repo.GetAllBooks();
            return View(books);
        }

        // GET: Book/Details/5
        public IActionResult Details(int id)
        {
            var book = _repo.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _repo.AddBook(book);
                return RedirectToAction(nameof(List));
            }
            return View(book);
        }

        // GET: Book/Delete/5
        public IActionResult Delete(int id)
        {
            var book = _repo.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.DeleteBook(id);
            return RedirectToAction(nameof(List));
        }
    }
}
