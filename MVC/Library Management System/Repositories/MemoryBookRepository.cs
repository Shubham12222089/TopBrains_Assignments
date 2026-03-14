using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public class MemoryBookRepository : IBookRepository
    {
        private readonly Dictionary<int, Book> _books;
        private int _nextId;

        public MemoryBookRepository()
        {
            _books = new Dictionary<int, Book>();
            _nextId = 1;

            // Initialize with 3 sample books
            AddBook(new Book { Title = "Clean Code", Author = "Robert C. Martin", Price = 599.99m });
            AddBook(new Book { Title = "Design Patterns", Author = "GoF", Price = 749.99m });
            AddBook(new Book { Title = "Refactoring", Author = "Martin Fowler", Price = 649.99m });
        }

        public List<Book> GetAllBooks()
        {
            return _books.Values.ToList();
        }

        public Book? GetBookById(int id)
        {
            _books.TryGetValue(id, out Book? book);
            return book;
        }

        public void AddBook(Book book)
        {
            book.BookId = _nextId++;
            _books.Add(book.BookId, book);
        }

        public void DeleteBook(int id)
        {
            if (_books.ContainsKey(id))
            {
                _books.Remove(id);
            }
        }
    }
}
