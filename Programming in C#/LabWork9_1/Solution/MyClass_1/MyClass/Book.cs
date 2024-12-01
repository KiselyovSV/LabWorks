using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyClass
{
    class Book
    {
        private String author;
        //private String title;
        //private String publisher;
        private int pages;
        //private int year;

        private static decimal price = 9;

        public string Author
        {
            get { return author; }
            set
            {
                if (value.Length > 10)
                    author = "Ошибка! Имя автора должно быть меньше чем 11 символов!";
                 else
                    author = value;
            }
        }

        public int Pages
        {
            get { return pages; }
            set
            {
                if (value < 0)
                    pages = 0;
                else
                    pages = value;
            }
        }

        public String Title { get; set; } // автоматическое свойство
        public String Publisher { get; set; }
        public int Year { get; set; }

        //static Book()       //статический конструктор
        //{
        //    price = 10;
        //}

        //public Book(String author, String title, String publisher, int pages, int year)
        //{
        //    this.author = author;
        //    this.title = title;
        //    this.publisher = publisher;
        //    this.pages = pages;
        //    this.year = year;
        //}

        //public Book(String author, String title)
        //{
        //    this.author = author;
        //    this.title = title;
        //}

        //public Book()
        //{ }

        public void SetBook(String author, String title, String publisher, int pages, int year)
        {
            Author = author;
            Title = title;
            Publisher = publisher;
            Pages = pages;
            Year = year;
        }

        public static void SetPrice(decimal price)
        {
            Book.price = price;
        }

        public void Show()
        {
            Console.WriteLine("\nКнига:\n Автор: {0}\n Название: {1}\n Год издания: {2}\n {3} стр.\n Стоимость аренды: {4} p. в сутки",
                Author, Title, Year, Pages, Book.price);
        }

        public decimal PriceBook(int s)
        {
            decimal cust = s * price;
            return cust;
        }




    }
}
