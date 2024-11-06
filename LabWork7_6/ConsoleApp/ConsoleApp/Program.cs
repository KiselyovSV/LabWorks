using System;
using ConsoleAp;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var books = new List<Book>(100)
            {
            new ("Э.Хэмингуэй","Старик и море", 1952, 150),
            new ("Л.Толстой", "Война и мир", 1869, 1979),
            new ("Ф.Достоевский", "Идиот", 1988, 540),
            new ("М.Шолохов", "Тихий Дон", 1975, 1810),
            new ("Д.Лондон", "Мартин Иден", 1985, 460)
            };
            books.Sort();
            foreach (var item in books)
            {
                item.Show();
            }

            
             
            
            
        }
    }
    
}
