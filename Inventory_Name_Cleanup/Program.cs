using System;
using System.Text.RegularExpressions;
using System.Text;
using System.Globalization;
class Program
{
    static void Main(string[] args)
    {
        string input = Console.ReadLine();
        
        StringBuilder sb = new StringBuilder();
        for(int i = 0; i < input.Length; i++)
        {
            if(i==0 || input[i] != input[i - 1])
            {
                sb.Append(input[i]);
            }
        }
        string cleaned = sb.ToString().Trim();
        cleaned = Regex.Replace(cleaned, @"\s+", " ");

        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
        string result = textInfo.ToTitleCase(cleaned.ToLower());

        Console.WriteLine(result);

        
    }
}