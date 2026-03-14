

class Program
{
    static void Main()
    {
        string inputFile = "log.txt";    

    
        string[] lines = File.ReadAllLines(inputFile);

        // error.txt file create / overwrite karo
        using (StreamWriter writer = new StreamWriter("error.txt"))
        {
            // Har line ko check karo
            foreach (string line in lines)
            {
                // Agar line me "ERROR" hai
                if (line.Contains("ERROR"))
                {
                    writer.WriteLine(line); // To error.txt me likh do
                }
            }
        }

        Console.WriteLine("ERROR logs extracted successfully.");
    }
}
