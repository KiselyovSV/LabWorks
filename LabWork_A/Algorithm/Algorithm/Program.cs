using System;

namespace Algorithm
{
    internal class Program
    {
        public static void Main()
        {
            Console.Write("\nВведите число:\t");
            double num = double.Parse(Console.ReadLine());
            double root = Program.SquareRoot(num);
            Console.WriteLine($"\nКорень квадратный числа {root*root} равен {root:F2}.");
           
        }
        public static double SquareRoot(double target)
        {
            double x = 1;
            double oldx;
            do
            {
                oldx = x;
                x = (x + target / x) / 2;
            }
            while (oldx != x);
            return x;
        }
    }
}
