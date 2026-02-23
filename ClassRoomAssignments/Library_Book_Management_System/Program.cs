using System;
using System.Collections.Generic;
using System.Linq;

// Book class
public class Book
{
    public string ISBN { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public bool IsAvailable { get; set; }

    public override string ToString()
    {
        return $"ISBN: {ISBN}, Title: {Title}, Author: {Author}, Genre: {Genre}, Available: {IsAvailable}";
    }
}

// Generic catalog class
public class Catalog<T> where T : Book
{
    private List<T> items = new List<T>();
    private HashSet<string> isbnSet = new HashSet<string>();
    private SortedDictionary<string, List<T>> genreIndex = new SortedDictionary<string, List<T>>();

    // Add item with genre indexing
    public bool AddItem(T item)
    {
        // Check ISBN uniqueness
        if (isbnSet.Contains(item.ISBN))
        {
            Console.WriteLine($"Book with ISBN {item.ISBN} already exists.");
            return false;
        }

        // Add to ISBN set
        isbnSet.Add(item.ISBN);

        // Add to items list
        items.Add(item);

        // Add to genre index
        if (!genreIndex.ContainsKey(item.Genre))
        {
            genreIndex[item.Genre] = new List<T>();
        }
        genreIndex[item.Genre].Add(item);

        return true;
    }

    // Remove item
    public bool RemoveItem(string isbn)
    {
        T bookToRemove = items.FirstOrDefault(b => b.ISBN == isbn);
        if (bookToRemove == null)
        {
            return false;
        }

        items.Remove(bookToRemove);
        isbnSet.Remove(isbn);

        if (genreIndex.ContainsKey(bookToRemove.Genre))
        {
            genreIndex[bookToRemove.Genre].Remove(bookToRemove);
            if (genreIndex[bookToRemove.Genre].Count == 0)
            {
                genreIndex.Remove(bookToRemove.Genre);
            }
        }

        return true;
    }

    // Get books by genre using indexer
    public List<T> this[string genre]
    {
        get
        {
            if (genreIndex.ContainsKey(genre))
            {
                return genreIndex[genre];
            }
            return new List<T>();
        }
    }

    // Find books using LINQ and lambda expressions
    public IEnumerable<T> FindBooks(Func<T, bool> predicate)
    {
        return items.Where(predicate);
    }

    // Get all items
    public List<T> GetAllItems()
    {
        return items;
    }

    // Get all genres
    public IEnumerable<string> GetAllGenres()
    {
        return genreIndex.Keys;
    }

    // Get total count
    public int Count
    {
        get { return items.Count; }
    }

    // Check if ISBN exists
    public bool IsbnExists(string isbn)
    {
        return isbnSet.Contains(isbn);
    }

    // Get available books
    public IEnumerable<T> GetAvailableBooks()
    {
        return items.Where(b => b.IsAvailable);
    }

    // Get books by author
    public IEnumerable<T> GetBooksByAuthor(string author)
    {
        return items.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
    }

    // Borrow book
    public bool BorrowBook(string isbn)
    {
        T book = items.FirstOrDefault(b => b.ISBN == isbn);
        if (book != null && book.IsAvailable)
        {
            book.IsAvailable = false;
            return true;
        }
        return false;
    }

    // Return book
    public bool ReturnBook(string isbn)
    {
        T book = items.FirstOrDefault(b => b.ISBN == isbn);
        if (book != null && !book.IsAvailable)
        {
            book.IsAvailable = true;
            return true;
        }
        return false;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Library Book Management System ===\n");

        Catalog<Book> library = new Catalog<Book>();

        // Add books
        Book book1 = new Book
        {
            ISBN = "978-3-16-148410-0",
            Title = "C# Programming",
            Author = "John Sharp",
            Genre = "Programming",
            IsAvailable = true
        };

        Book book2 = new Book
        {
            ISBN = "978-3-16-148410-1",
            Title = "Python Basics",
            Author = "Jane Smith",
            Genre = "Programming",
            IsAvailable = true
        };

        Book book3 = new Book
        {
            ISBN = "978-3-16-148410-2",
            Title = "The Great Novel",
            Author = "John Doe",
            Genre = "Fiction",
            IsAvailable = true
        };

        Book book4 = new Book
        {
            ISBN = "978-3-16-148410-3",
            Title = "Database Design",
            Author = "John Sharp",
            Genre = "Programming",
            IsAvailable = true
        };

        // Test adding books
        Console.WriteLine("--- Adding Books ---");
        Console.WriteLine($"Adding book1: {library.AddItem(book1)}");
        Console.WriteLine($"Adding book2: {library.AddItem(book2)}");
        Console.WriteLine($"Adding book3: {library.AddItem(book3)}");
        Console.WriteLine($"Adding book4: {library.AddItem(book4)}");

        // Test duplicate ISBN
        Console.WriteLine($"\nTrying to add duplicate ISBN: {library.AddItem(book1)}");

        // Test getting books by genre using indexer
        Console.WriteLine("\n--- Books by Genre (Programming) ---");
        List<Book> programmingBooks = library["Programming"];
        Console.WriteLine($"Programming books count: {programmingBooks.Count}");
        foreach (Book book in programmingBooks)
        {
            Console.WriteLine($"  - {book.Title}");
        }

        // Test finding books using predicate
        Console.WriteLine("\n--- Finding books by author containing 'John' ---");
        IEnumerable<Book> johnsBooks = library.FindBooks(b => b.Author.Contains("John"));
        Console.WriteLine($"John's books count: {johnsBooks.Count()}");
        foreach (Book book in johnsBooks)
        {
            Console.WriteLine($"  - {book.Title} by {book.Author}");
        }

        // Test borrowing a book
        Console.WriteLine("\n--- Borrowing Book ---");
        Console.WriteLine($"Borrow C# Programming: {library.BorrowBook("978-3-16-148410-0")}");
        Console.WriteLine($"Book available after borrowing: {book1.IsAvailable}");

        // Test returning a book
        Console.WriteLine("\n--- Returning Book ---");
        Console.WriteLine($"Return C# Programming: {library.ReturnBook("978-3-16-148410-0")}");
        Console.WriteLine($"Book available after returning: {book1.IsAvailable}");

        // Get all genres (sorted)
        Console.WriteLine("\n--- All Genres (Sorted) ---");
        foreach (string genre in library.GetAllGenres())
        {
            Console.WriteLine($"  - {genre}: {library[genre].Count} books");
        }

        // Get available books
        Console.WriteLine("\n--- Available Books ---");
        foreach (Book book in library.GetAvailableBooks())
        {
            Console.WriteLine($"  - {book.Title}");
        }

        // Test finding by price range (using FindBooks with custom predicate)
        Console.WriteLine("\n--- Find Fiction Books ---");
        IEnumerable<Book> fictionBooks = library.FindBooks(b => b.Genre == "Fiction");
        foreach (Book book in fictionBooks)
        {
            Console.WriteLine($"  - {book.Title}");
        }

        Console.WriteLine($"\n--- Total Books in Library: {library.Count} ---");
    }
}
