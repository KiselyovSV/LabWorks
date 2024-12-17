using System;
using System.Net;
using System.Runtime.CompilerServices;

namespace SquareRoot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("\nКвадратное уравнение имеет вид: AX^2+BX+C=0\n Введите значение A:\t");
            int a = int.Parse(Console.ReadLine());
            Console.Write("\n Введите значение B:\t");
            int b = int.Parse(Console.ReadLine());
            Console.Write("\n Введите значение C:\t");
            int c = int.Parse(Console.ReadLine());
            int op = СalcRoot(a, b, c, out int d, out int f);
            switch (op)
            {
                case -1: Console.Write($"\nКорней уравнения с коэффициентами a = {a}, b = {b}, c = {c} нет.");
                    break;
                case 0: Console.Write($"\nКорень уравнения с коэффициентами a = {a}, b = {b}, c = {c} один: x1 = x2 = {d}");
                    break;
                case 1: Console.Write($"\nКорни уравнения с коэффициентами a = {a}, b = {b}, c = {c} равны: x1 = {d}, x2 = {f}.");
                    break;   
            }
             
        }

        static int СalcRoot(int a, int b, int c, out int d, out int f)
        {
            int dis = b * b - 4 * a * c;
            if (dis < 0)
            {
                d = 0;
                f = 0;
                return -1;
            }
            else if (dis == 0)
            {
                d = -(b / (2 * a));
                f = d;
                return 0;
            }
            else
            {
                d = (-b - (int)Math.Sqrt(dis)) / (2 * a);
                f = (-b + (int)Math.Sqrt(dis)) / (2 * a);
                return 1;
            }
        }
    }
}
