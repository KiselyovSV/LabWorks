using System;

namespace MyClass
{
    internal class Book
    {
        private string author;               // автор
        private string title;                // название
        private string publisher;            // издательство
        private int pages;                   // количество страниц
        private int year;                    // год издания
        private static double price = 9;     // стоимость аренды в сутки
        internal int days;                    // количество дней
        internal double costOfRent;           // общая стоимость аренды

        public Book(): this("","","",0,0) { } 
        public Book(string author, string title, string publisher, int pages, int year)
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

        public void Show()
        {
          Console.WriteLine("\nАвтор книги: {0}\nНазвание: {1}\nИздательство: {2}\nГод издания: {3}\n{4} стр.\n" +
              "Стоимость аренды в сутки: {5} руб.\nОбщая стоимость аренды за {6} дней составляет: {7} руб.", author, title, publisher, year, pages, price, days, costOfRent);
        }
         
        public void СostOfRent(int days)
        {
            this.days = days;
            costOfRent = price * days;
        }

    }
}
