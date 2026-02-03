public class Solution
{
    public static void Main()
    {
        int[] a = { 1, 3, 5, 7 };
        int[] b = { 2, 4, 6, 8 };

        int[] result = MergeSortedArrays(a, b);

        foreach (var x in result)
        {
            Console.Write(x + " ");
        }
        string[] a2 = { "apple", "banana" };
        string[] b2 = { "apricot", "orange" };
        Console.WriteLine();
        string[] result2 = MergeSortedArrays(a2, b2);

        foreach (var x in result2)
        {
            Console.Write(x + " ");
        }


    }

    public static T[] MergeSortedArrays<T>(T[] a, T[] b) where T : IComparable<T>
    {
        int n = a.Length;
        int m = b.Length;

        T[] merged = new T[n + m];

        int i = 0, j = 0, k = 0;

        while (i < n && j < m)
        {
            if (a[i].CompareTo(b[j]) <= 0)
                merged[k++] = a[i++];
            else
                merged[k++] = b[j++];
        }

        while (i < n)
            merged[k++] = a[i++];

        while (j < m)
            merged[k++] = b[j++];

        return merged;
    }
}
