using System;
class Program
{
    static void Swap_using_ref(ref int a,ref int b)
    {
        int temp=a;
        a=b;
        b=temp;
    }
    static void Swap_using_out(int c,int d,out int x,out int y)
    {
        x=d;
        y=c;
    }
    static void Main()
    {
        int a = 10, b = 20;
        int c=10,d=20;
        int x,y;
        Console.WriteLine($"Before Swap: a = {a}, b = {b}");
        Swap_using_ref(ref a, ref b);
        Console.WriteLine($"After Swap:  a = {a}, b = {b}");

        Console.WriteLine("Using Out Keyword");
        Console.WriteLine($"Before Swap: x = {c}, y = {d}");
        Swap_using_out(c, d, out x, out y);
        Console.WriteLine($"After Swap:  x = {x}, y = {y}");
    }
}