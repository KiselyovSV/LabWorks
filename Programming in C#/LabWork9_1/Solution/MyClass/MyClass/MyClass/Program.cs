using System;

namespace MyClass
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Book b2 = new Book("Толстой Л.Н.", "Война и мир", "Наука", 823, 2013);
            Book.SetPrice(25);
            b2.СostOfRent(30);
            b2.Show();
            Console.ReadKey();
        }
    }
}
