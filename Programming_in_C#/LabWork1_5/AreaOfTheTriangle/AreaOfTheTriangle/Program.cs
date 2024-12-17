using System;

namespace AreaOfTheTriangle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("\nДля расчёта площади равностороннего треугольника введите значение периметра:\t");
                bool a = double.TryParse(Console.ReadLine(), out double perim);
                if (a is true)
                {
                    double side = perim / 3;
                    double half = perim / 2;
                    double area = Math.Sqrt(half * (half - side) * (half - side) * (half - side));
                    Console.WriteLine("\nСторона\t   |\t Площадь\n{0,0:F2}\t   |\t {1,0:F2}", side, area);
                    Console.ReadKey();
                    Console.Clear();
                    //continue;
                }
                else Console.WriteLine("\nОшибка. Введено некорректное значение.");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
