
using System.Text.Json;
public record Student(string Name, int Score);
public static class StudentProcessor
{
    public static string BuildAndSerialize(string[] items, int minScore)
    {
        if (items == null || items.Length == 0) return "[]";
      
        List<Student> students = new List<Student>();

        foreach (string item in items)
        {
            if (string.IsNullOrWhiteSpace(item)) continue;

            string[] parts = item.Split(':', 2);

            if (parts.Length != 2) continue;

            if (!int.TryParse(parts[1], out int score)) continue;

            students.Add(new Student(parts[0], score));
        }

        var result = students.Where(s => s.Score >= minScore).OrderByDescending(s => s.Score).ThenBy(s => s.Name).ToList();

        return JsonSerializer.Serialize(result);
    }
}
