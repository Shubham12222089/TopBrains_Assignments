using System;
using Services;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ManagementService service = new ManagementService();

            while (true)
            {
                Console.WriteLine("1. Display");
                Console.WriteLine("2. Add");
                Console.WriteLine("3. Update");
                Console.WriteLine("4. Remove");
                Console.WriteLine("5. Exit");

                // TODO: Read user choice

                int choice = 0; // TODO

                switch (choice)
                {
                    case 1:
                        // TODO: Display data
                        break;
                    case 2:
                        // TODO: Add entity
                        break;
                    case 3:
                        // TODO: Update entity
                        break;
                    case 4:
                        // TODO: Remove entity
                        break;
                    case 5:
                        Console.WriteLine("Thank You");
                        return;
                    default:
                        // TODO: Handle invalid choice
                        break;
                }
            }
        }
    }
}
