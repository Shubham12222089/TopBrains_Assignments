
public class Program
{
    public static int SumPositiveUntilZero(int[] nums)
    {
        int sum = 0;

        foreach (int num in nums)
        {
            if (num == 0)
                break;

            if (num < 0)
                continue;

            sum += num;
        }

        return sum;
    }

    public static void Main()
    {
        int n = Convert.ToInt32(Console.ReadLine());
        int[] nums = new int[n];

        for (int i = 0; i < n; i++)
        {
            nums[i] = Convert.ToInt32(Console.ReadLine());
        }

        int result = SumPositiveUntilZero(nums);
        Console.WriteLine("Result: "+ result);
    }
}
