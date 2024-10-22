using System;

namespace Greetings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Здравствуйте! Как Вас зовут? Введите своё имя.");
            string? name = Console.ReadLine();
            Console.WriteLine("Приятно познакомиться,{0}! Меня зовут DotNet.", name);
            Console.ReadKey();
        }
    }
}
