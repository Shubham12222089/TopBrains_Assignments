
class Program
{
    static void Main()
    {
        string input = Console.ReadLine()??"";
        input = input.Trim();
        string cleaned = "";
        for (int i = 0; i < input.Length; i++)
        {
            if (i == 0 || input[i] != input[i - 1])
            {
                cleaned += input[i];
            }
        }

        // Convert to Title Case
        string result = "";
        bool newWord = true;
        for (int i = 0; i < cleaned.Length; i++)
        {
            char ch = cleaned[i];

            if (ch == ' ')
            {
                result += ch;
                newWord = true;
            }
            else
            {
                if (newWord)
                {
                    result += char.ToUpper(ch);
                    newWord = false;
                }
                else
                {
                    result += char.ToLower(ch);
                }
            }
        }
        Console.WriteLine(result);
    }
}
