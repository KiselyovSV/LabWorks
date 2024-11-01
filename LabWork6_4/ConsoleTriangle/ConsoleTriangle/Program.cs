using System;


namespace ConsoleTriangle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Triangle A = new Triangle(5, 10, 20);
            Print(A);
            Triangle F = new Triangle(15, 10, 20);
            Print(F);
            Triangle G = new Triangle(8, 8, 8);
            Print(G);
            Triangle J = new Triangle(5, -18, 20);
            Print(J);
        }
        public static void Print(Triangle t)
        {
            Console.WriteLine(t.OllSides);
            if (Triangle.Perimeter(t) == 0 || Triangle.Area(t) == 0) Console.WriteLine("Треугольник не существует!");
            else
            {
                Console.WriteLine($"Периметр треугольника равен:{Triangle.Perimeter(t)}.");
                Console.WriteLine($"Площадь треугольника равна:{Triangle.Area(t):F2}.");
            }
        }
    }

}
