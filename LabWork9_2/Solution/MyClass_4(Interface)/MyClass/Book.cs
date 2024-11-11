using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyClass
{
    class Book : Item
    {
        private String author;
        private String title;
        private String publisher;
        private int pages;
        private int year;

        private bool returnSrok;

        private static double price = 9;
        
        static Book()       //статический конструктор
        {
            price = 100;
        }

        public Book(String author, String title, String publisher, int pages, int year)
        {
            this.author = author;
            this.title = title;
            this.publisher = publisher;
            this.pages = pages;
            this.year = year;
        }

        public Book(String author, String title, String publisher, int pages, int year, long invNumber, bool taken) : base (invNumber, taken)
        {
            this.author = author;
            this.title = title;
            this.publisher = publisher;
            this.pages = pages;
            this.year = year;
        }

        public Book(String author, String title)
        {
            this.author = author;
            this.title = title;
        }

        public Book()
        { }

        public void SetBook(String author, String title, String publisher, int pages, int year)
        {
            this.author = author;
            this.title = title;
            this.publisher = publisher;
            this.pages = pages;
            this.year = year;
        }

        public static void SetPrice(double price)
        {
            Book.price = price;
        }

        public override void Show()
        {
            Console.WriteLine("\nКнига:\n Автор: {0}\n Название: {1}\n Год издания: {2}\n {3} стр.\n Стоимость аренды: {4} p. в сутки",
                author, title, year, pages, Book.price);
            base.Show();
        }

        public double PriceBook(int s)
        {
            double cust = s * price;
            return cust;
        }

        public void ReturnSrok()
        {
            returnSrok = true;
        }

        public override void Return()    // операция "вернуть"
        {
            if (returnSrok == true)
                taken = true;
            else
                taken = false;
        }

        //public void TakeItem()
        //{
        //    if (this.IsAvailable())
        //        this.Take();
        //}


    }
}
