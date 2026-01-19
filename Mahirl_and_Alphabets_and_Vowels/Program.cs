using System;
using System.Collections.Generic;
using System.Text;

class Program
{
    static bool IsVowel(char c)
    {
        c = char.ToLower(c);
        return c == 'a' || c == 'e' || c == 'i' || 
               c == 'o' || c == 'u';
    }

    static void Main()
    {
        string word1 = Console.ReadLine();
        string word2 = Console.ReadLine();

        HashSet<char> set = new HashSet<char>();
        foreach (char c in word2)
        {
            set.Add(char.ToLower(c));
        }

        StringBuilder filtered = new StringBuilder();
        foreach (char c in word1)
        {
            char lower = char.ToLower(c);

            if (IsVowel(c) || !set.Contains(lower))
            {
                filtered.Append(c);
            }
        }

        StringBuilder result = new StringBuilder();
        for (int i = 0; i < filtered.Length; i++)
        {
            if (i == 0 || filtered[i] != filtered[i - 1])
            {
                result.Append(filtered[i]);
            }
        }

        Console.WriteLine(result.ToString());
    }
}
