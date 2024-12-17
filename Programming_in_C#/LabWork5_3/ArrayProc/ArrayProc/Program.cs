using System;
using System.Diagnostics.CodeAnalysis;

namespace ArrayProc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] mas = Input();
            int sum = Sum(mas);
            int aver = Average(mas);
            var quan = Quantity(mas);
            var seo = SumEEndO(mas);
            var f = Find(mas);
            Print(sum, aver, quan, seo, f);
        }

        private static void Print(int sum, int aver, (int pos, int neg) quan, (int even, int odd) seo, (int min, int max) f)
        {
            Console.WriteLine($"\nСумма всех членов массива равна: {sum}\n" +
                $"Среднее значение массива равно: {aver}\n" +
                $"Сумма отрицательных чисел равна: {quan.neg}, сумма положительных: {quan.pos}\n" +
                $"Сумма четных чисел равна: {seo.even}, сумма нечетных: {seo.odd}\n" +
                $"Максимальный элемент массива: {f.max}, минимальный: {f.min}.");
        }

        static (int min, int max) Find(int[,] mas)
        {
            var min = int.MaxValue;
            var max = int.MinValue;
            for (int i = 0; i < mas.GetLength(0);i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    if (mas[i, j] < min) min = mas[i, j];
                    if (mas[i, j] > max) max = mas[i, j];
                }
            }
            return (min, max);
        }

        static (int even, int odd) SumEEndO(int[,] mas)
        {
            var res = (even: 0, odd: 0);
            foreach (int x in mas)
            {
                if (x % 2 != 0) res.odd += x;
                else res.even += x;
            }
            return res;
        }

        static (int pos, int neg) Quantity(int[,] mas)
        {
            var res = (positive: 0, negative: 0);
            foreach (int x in mas)
            {
                if (x < 0) res.negative += x; 
                else if (x > 0) res.positive += x;
            }
            return res;
        }

        static int Average(int[,] mas)
        {
            int sum = 0;
            foreach (int i in mas)
            {
                sum += i;
            }
            int average = sum / mas.Length;
            return average;
        }

        private static int Sum(int[,] mas)
        {
            int sum = 0;
            foreach (int i in mas)
            {
                sum += i;
            }
            return sum;
        }

        private static int[,] Input()
        {
            int[,] mas = new int[2, 2];
            Console.Write("\nВведите значение элемента двухмерного массива с индексом 0,0:\t");
            mas[0, 0] = int.Parse(Console.ReadLine());
            Console.Write("\nВведите значение элемента двухмерного массива с индексом 0,1:\t");
            mas[0, 1] = int.Parse(Console.ReadLine());
            Console.Write("\nВведите значение элемента двухмерного массива с индексом 1,0:\t");
            mas[1, 0] = int.Parse(Console.ReadLine());
            Console.Write("\nВведите значение элемента двухмерного массива с индексом 1,1:\t");
            mas[1, 1] = int.Parse(Console.ReadLine());
            return mas;
        }
    }
}
