
using System.Text;

public class Program
{
    static void Main()
    {
        string first = Console.ReadLine()??"";
        string second = Console.ReadLine()??"";

        StringBuilder step1 = new StringBuilder(); // for filtering

     
        foreach (char ch in first)
        {
            char lower = char.ToLower(ch); 

            bool isVowel = "aeiou".Contains(lower); //chk krega bo character vowel to ni
            bool existsInSecond = second.ToLower().Contains(lower);

            if (isVowel || !existsInSecond) // agr vowel nhi h or second word m nhi h to add kro
            {
                step1.Append(ch);
            }
        }

        StringBuilder result = new StringBuilder();

        for (int i = 0; i < step1.Length; i++)
        {
            if (i == 0 || step1[i] != step1[i - 1])
            {
                result.Append(step1[i]);
            }
        }

        Console.WriteLine(result.ToString());
    }
}
