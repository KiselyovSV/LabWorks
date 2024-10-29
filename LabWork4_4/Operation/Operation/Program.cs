using System;
using Op;

namespace Operation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nЕсли Вы хотите рассчитать площадь треугольника, то нажмите \"1\". \nЕсли нужна площадь равностороннего треугольника, то нажмите \"2\".");
            int option = int.Parse(Console.ReadLine());
            if (option == 1)
            {
                Console.Write("\nВведите длину стороны А:\t");
                double a = double.Parse(Console.ReadLine());
                Console.Write("\nВведите длину стороны B:\t");
                double b = double.Parse(Console.ReadLine());
                Console.Write("\nВведите длину стороны C:\t");
                double c = double.Parse(Console.ReadLine());
                double s = Op.Operation.AreaOfTheTriangle(a, b, c, out bool d);
                Console.WriteLine($"\nТреугольник существует: {d}. Площадь треугольника равна: {s:F2}.");
                Console.ReadLine();
            }

            else if (option == 2)
            {
                Console.Write("\nВведите длину стороны треугольника:\t");
                double a = double.Parse(Console.ReadLine());
                double s = Op.Operation.AreaOfTheTriangle(a, out bool d);
                Console.WriteLine($"\nТреугольник существует: {d}. Площадь треугольника равна: {s:F2}.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("\nВы ввели неверное значение.");
                Console.ReadLine();
            }
            
        }

    }
}
