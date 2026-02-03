
public class Program
{
    public static string ConvertToMinutesSeconds(int totalSeconds)
    {
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        return minutes + ":" + seconds.ToString("D2"); //"D2" ensures leading zero like 02
    }

    public static void Main()
    {
        int totalSeconds = Convert.ToInt32(Console.ReadLine());
        string result = ConvertToMinutesSeconds(totalSeconds);
        Console.WriteLine(result);
    }
}

