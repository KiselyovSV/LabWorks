using System;
using System.Numerics;

namespace MyClass
{
    internal class Book: Item
    {
        private string author;               // автор
        private string title;                // название
        private string publisher;            // издательство
        private int pages;                   // количество страниц
        private int year;                    // год издания

        private static double price = 9;     // стоимость аренды в сутки
        private int days;                    // количество дней
        private double costOfRent;           // общая стоимость аренды
        private bool returnSrok;


        public Book() :this ("", "", "", 0, 0, 0, false) { } 
        public Book(string author, string title, string publisher, int pages, int year, long InvNumber, bool IsAvailable)
            :base (InvNumber, IsAvailable)
        {
            this.author = author;
            this.title = title;
            this.publisher = publisher;
            this.pages = pages;
            this.year = year;
        }
        static Book()
        {
            price = 10;
        }

        public static void SetPrice(double price)
        {
            Book.price = price;
        }

        new public void Show()
        {
            Console.WriteLine("\nАвтор книги: {0}\nНазвание: {1}\nИздательство: {2}\nГод издания: {3}\n{4} стр.\n" +
                "Стоимость аренды в сутки: {5} руб.\nОбщая стоимость аренды за {6} дней составляет: {7} руб.\n" +
                "Состояние единицы хранения:\nИнвентарный номер: {8}\nНаличие: {9}", author, title, publisher, year, 
                pages, price, days, costOfRent, InvNumber, IsAvailable);
        }

        public void СostOfRent(int days)
        {
            this.days = days;
            costOfRent = price * days;
        }

        public void TakeBook()
        {
            if (IsAvailable) Take();

        }
        public override void Return()
        {
            if (!IsAvailable || returnSrok) IsAvailable = true;
        }
   
        public void ReturnSrok()
        {
            returnSrok = true;
        }
    }
}
