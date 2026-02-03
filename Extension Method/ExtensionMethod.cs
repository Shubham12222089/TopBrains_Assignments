public static class ExtennsionMethod
{
    public static string[] DistinctById(this string[] items) // this tells us it is an extension method 
    {
        if(items==null || items.Length==0) return Array.Empty<string>();

        HashSet<string> seenIds = new HashSet<string>();
        List<string> result = new List<string>();

        foreach(string item in items)
        {
            if(string.IsNullOrEmpty(item)) continue;
            string[] parts = item.Split(':',2);
            if (parts.Length!=2) continue;
            string id = parts[0];
            string name = parts[1];

            if(seenIds.Add(id)) result.Add(name); //chks for uniqueness
        }
        return result.ToArray();

    }
}