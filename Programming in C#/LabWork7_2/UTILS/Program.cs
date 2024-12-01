using System;
using UtilsNS;

namespace UtilsApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nВведите текст:");
            string s = Console.ReadLine();
            Utils.Reverse(ref s);
            Console.WriteLine(s);
            
        }
    }
}
