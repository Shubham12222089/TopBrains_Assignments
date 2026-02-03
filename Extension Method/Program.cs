class Program
{
    
    public static void Main()
    {
        string[] items =
        {
            "1:Rahul",
            "2:Roni",
            "3:Sahil",
            "3:Arjun",
            "2:Sehdev"
        };
        string[] distinctNames = items.DistinctById(); // can write bcz of extension method
        foreach (var name in distinctNames)
        {
            Console.WriteLine(name);
        }
    }
}