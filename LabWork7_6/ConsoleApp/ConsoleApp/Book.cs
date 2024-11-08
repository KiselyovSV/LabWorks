using System;
using System.Xml.Linq;

namespace ConsoleAp
{
    internal class Book: IComparable<Book>
    {
        private string author;
        private string title;
        private int year;
        private int pages;

        public Book(string author, string title, int year, int pages)
        {
            this.author = author;
            this.title = title;
            this.year = year;
            this.pages = pages;
        }
        public void Show()
        {
            Console.WriteLine($"\nНазвание книги:{title},\nАвтор:{author},\nГод издания:{year},\n{pages} стр.");
        }
        //public int CompareTo(object? obj)
        //{
        //    Book? it = obj as Book;
        //    if (it == null)
        //    {
        //        Console.WriteLine("Параметр не соответствует классу \"Book\".");
        //        return 2;
        //    }
        //    if (this.year == it.year) return 0;
        //    else if (this.year > it.year) return 1;
        //    else return -1;
        //}
        //public int CompareTo(object? o)
        //{
        //    if (o is Book book) return year.CompareTo(book.year);
        //    else throw new ArgumentException("Некорректное значение параметра");
        //}

        public int CompareTo(Book? person)
        {
            if (person is null) throw new ArgumentException("Некорректное значение параметра");
            return year - person.year;
        }
    }
}
