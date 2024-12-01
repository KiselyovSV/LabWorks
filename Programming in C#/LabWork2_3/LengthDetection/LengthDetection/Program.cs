using System;

namespace LengthDetection
{
    public struct Distance
    {
        public int foot;
        public int inch;       
        
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Distance a = new();
                Distance b = new();
                Distance c = new();
                Console.Write("\nВведите количество футов для дистанции А:\t");
                a.foot = Convert.ToInt32(Console.ReadLine());
                Console.Write("\nВведите количество дюймов для дистанции А:\t");
                a.inch = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nВведите количество футов для дистанции B:\t");
                b.foot = Convert.ToInt32(Console.ReadLine());
                Console.Write("\nВведите количество дюймов для дистанции B:\t");
                b.inch = Convert.ToInt32(Console.ReadLine());
                c.foot = a.foot + b.foot + (a.inch + b.inch) / 12;
                c.inch = (a.inch + b.inch) % 12;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nСумма расстояний А и B равна:\t{c.foot}'-{c.inch}\"");
                Console.ReadKey();
                Console.Clear();
                continue;
            }
        }
    }
}
