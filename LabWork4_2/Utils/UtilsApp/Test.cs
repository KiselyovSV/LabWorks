using System;
using App1;

namespace UtilsApp
{
    internal class Test
    {
        static void Main(string[] args)
        {
            // Вводим значения двух чисел
            Console.Write($"\nВведите значение первого числа:\t");
            int num1 = int.Parse(Console.ReadLine());
            Console.Write($"\nВведите значение второго числа:\t");
            int num2 = int.Parse(Console.ReadLine());

            // Тестируем метод Utils.Greater()
            Console.Write($"\nБольшее из этих чисел:{Utils.Greater(num1, num2),11}");
            Console.ReadLine();

            // Тестирование метода Utils.Swap()
            int a = 4;
            int b = 33;
            Console.Write($"\nДо метода Swap() переменные A = {a}, B = {b}");
            Utils.Swap( ref a,ref b );
            Console.Write($"\nПосле метода Swap() переменные A = {a}, B = {b}");
            Console.ReadLine();

        }
    }
}
