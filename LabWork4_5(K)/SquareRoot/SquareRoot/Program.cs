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
            var roots = Program.СalcRoot(a, b, c);
            switch (roots.o)
            {
                case -1: Console.Write($"\nКорней уравнения с коэффициентами a = {a}, b = {b}, c = {c} нет.");
                    break;
                case 0: Console.Write($"\nКорень уравнения с коэффициентами a = {a}, b = {b}, c = {c} один: x1 = x2 = {roots.d}");
                    break;
                case 1: Console.Write($"\nКорни уравнения с коэффициентами a = {a}, b = {b}, c = {c} равны: x1 = {roots.d}, x2 = {roots.f}.");
                    break;   
            }
             
        }

        static (int d, int f, int o) СalcRoot(int a, int b, int c)
        {
            int dis = b * b - 4 * a * c;
            if (dis < 0)
            {
                var roots = (d:0, f:0, o:-1);
                return roots;
            }
            else if (dis == 0)
            {
                var roots = (d:0, f:0, o:0);
                roots.d = -(b / (2 * a));
                roots.f = roots.d;
                return roots;
            }
            else
            {
                var roots = (d:0, f:0, o:1);
                roots.d = (-b - (int)Math.Sqrt(dis)) / (2 * a);
                roots.f = (-b + (int)Math.Sqrt(dis)) / (2 * a);
                return roots;
            }
        }
    }
}
