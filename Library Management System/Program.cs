class Program
{
    static List<dynamic> books = new List<dynamic>();
    static int bookIdCounter = 1;

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n---Book Library Management System---");
            Console.WriteLine("1. Admin");
            Console.WriteLine("2. User");
            Console.WriteLine("3. Exit");
            Console.Write("Enter choice: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AdminMenu();
                    break;
                case 2:
                    UserMenu();
                    break;
                case 3:
                    return;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }


    static void AdminMenu()
    {
        Console.WriteLine("\n--- Admin Menu ---");
        Console.WriteLine("1. Add Book");
        Console.WriteLine("2. Update Book");
        Console.WriteLine("3. Delete Book");
        Console.WriteLine("4. View All Books");
        Console.Write("Enter choice: ");

        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                AddBook();
                break;
            case 2:
                UpdateBook();
                break;
            case 3:
                DeleteBook();
                break;
            case 4:
                ViewBooks();
                break;
            default:
                Console.WriteLine("Invalid choice!");
                break;
        }
    }

  
    static void UserMenu()
    {
        Console.WriteLine("\n--- User Menu ---");
        Console.WriteLine("1. Browse Books");
        Console.WriteLine("2. Search by Book Name");
        Console.WriteLine("3. Search by Publisher");
        Console.WriteLine("4. View Highest Price Book");
        Console.WriteLine("5. View Lowest Price Book");
        Console.Write("Enter choice: ");

        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                ViewBooks();
                break;
            case 2:
                SearchByName();
                break;
            case 3:
                SearchByPublisher();
                break;
            case 4:
                HighestPriceBook();
                break;
            case 5:
                LowestPriceBook();
                break;
            default:
                Console.WriteLine("Invalid choice!");
                break;
        }
    }

  
    static void AddBook()
    {
        dynamic book = new System.Dynamic.ExpandoObject();

        book.Id = bookIdCounter++;
        Console.Write("Enter Book Name: ");
        book.Name = Console.ReadLine();

        Console.Write("Enter Publisher: ");
        book.Publisher = Console.ReadLine();

        Console.Write("Enter Price: ");
        book.Price = double.Parse(Console.ReadLine());

        books.Add(book);
        Console.WriteLine("Book added successfully!");
    }

    static void UpdateBook()
    {
        Console.Write("Enter Book ID to update: ");
        int id = int.Parse(Console.ReadLine());

        var book = books.FirstOrDefault(b => b.Id == id);

        if (book != null)
        {
            Console.Write("Enter New Book Name: ");
            book.Name = Console.ReadLine();

            Console.Write("Enter New Publisher: ");
            book.Publisher = Console.ReadLine();

            Console.Write("Enter New Price: ");
            book.Price = double.Parse(Console.ReadLine());

            Console.WriteLine("Book updated successfully!");
        }
        else
        {
            Console.WriteLine("Book not found!");
        }
    }

    static void DeleteBook()
    {
        Console.Write("Enter Book ID to delete: ");
        int id = int.Parse(Console.ReadLine());

        var book = books.FirstOrDefault(b => b.Id == id);

        if (book != null)
        {
            books.Remove(book);
            Console.WriteLine("Book deleted successfully!");
        }
        else
        {
            Console.WriteLine("Book not found!");
        }
    }

    static void ViewBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books available.");
            return;
        }

        Console.WriteLine("\n--- Book List ---");
        foreach (var book in books)
        {
            Console.WriteLine($"ID: {book.Id}, Name: {book.Name}, Publisher: {book.Publisher}, Price: {book.Price}");
        }
    }

    static void SearchByName()
    {
        Console.Write("Enter Book Name: ");
        string name = Console.ReadLine();

        var result = books.Where(b => b.Name.ToString().Contains(name, StringComparison.OrdinalIgnoreCase));

        DisplayResult(result);
    }   

    static void SearchByPublisher()
    {
        Console.Write("Enter Publisher Name: ");
        string publisher = Console.ReadLine();

        var result = books.Where(b => b.Publisher.ToString().Contains(publisher, StringComparison.OrdinalIgnoreCase));

        DisplayResult(result);
    }

    static void HighestPriceBook()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books available.");
            return;
        }

        var book = books.OrderByDescending(b => b.Price).First();
        Console.WriteLine($"Highest Price Book → {book.Name} | {book.Price}");
    }

    static void LowestPriceBook()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books available.");
            return;
        }

        var book = books.OrderBy(b => b.Price).First();
        Console.WriteLine($"Lowest Price Book → {book.Name} | {book.Price}");
    }

    static void DisplayResult(IEnumerable<dynamic> result)
    {
        if (!result.Any())
        {
            Console.WriteLine("No matching books found.");
            return;
        }

        foreach (var book in result)
        {
            Console.WriteLine($"ID: {book.Id}, Name: {book.Name}, Publisher: {book.Publisher}, Price: {book.Price}");
        }
    }
}




