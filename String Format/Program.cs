class Program
{
    static void Main()
    {
        string[] items =
        {
            "Arjun:90",
            "Babbu:75",
            "Chaman:90",
            "Drubh:60"
        };

        string json = StudentProcessor.BuildAndSerialize(items, 80);
        Console.WriteLine(json);
    }
}
