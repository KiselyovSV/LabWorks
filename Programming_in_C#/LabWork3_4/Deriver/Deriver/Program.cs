using System;
using System.Linq.Expressions;

namespace Deriver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try 
            {

                while (true)
                {
                    Console.Write("\nВведите первое число для деления:\t");
                    bool a = double.TryParse(Console.ReadLine(), out double num1);
                    if (a is false)
                    {
                        Console.WriteLine("\nВы ввели некорректные данные. Введите число.");
                        Console.ReadLine();
                        Console.Clear();
                        continue;
                    }
                    Console.Write("\nВведите второе число:\t");
                    bool b = double.TryParse(Console.ReadLine(), out double num2);
                    if (b is false)
                    {
                        Console.WriteLine("\nВы ввели некорректные данные. Введите число.");
                        Console.ReadLine();
                        Console.Clear();
                        continue;
                    }
                    if (num2 != 0)
                    {
                        double result = num1 / num2;
                        Console.WriteLine($"\nРезультат деления числа {num1} на число {num2} равен: {result}");
                    }
                    else Console.WriteLine($"\nНа ноль делить нельзя!");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"\nОшибка!{ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        
    }
}
