using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LengthDetection
{
    public class Distance
    {
        public int foot;
        public double inch;

        public Distance() : this(0, 0.0) { }
        public Distance(int foot, double inch)
        {
            this.foot = foot;
            this.inch = inch;
        }
        public static Distance operator + (Distance a) => a;
        public static Distance operator - (Distance a) => new Distance(-a.foot, -a.inch);
        public static Distance operator + (Distance a, Distance b)
        {
            int sum = a.foot + b.foot + (int)(a.inch + b.inch)/12;
            double sum2 = (a.inch + b.inch)%12;
            return new Distance(sum, sum2);
        }

        public static Distance operator - (Distance a, Distance b)
        {
            return a + (-b);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Distance a = new();
                Distance b = new();
                Console.Write("\nВведите количество футов для дистанции А:\t");
                a.foot = Convert.ToInt32(Console.ReadLine());
                Console.Write("\nВведите количество дюймов для дистанции А:\t");
                a.inch = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nВведите количество футов для дистанции B:\t");
                b.foot = Convert.ToInt32(Console.ReadLine());
                Console.Write("\nВведите количество дюймов для дистанции B:\t");
                b.inch = Convert.ToInt32(Console.ReadLine());
                Distance c = a + b;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nСумма расстояний А и B равна:\t{c.foot}'-{c.inch}\"");
                Console.ReadKey();
                Console.Clear();
                continue;
            }
        }
    }
}
