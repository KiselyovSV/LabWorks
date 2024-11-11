using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyClass
{
    class Program
    {
        static void Main(string[] args)
        {
            Book b1 = new Book();
            b1.SetBook("Пушкин А.С.", "Капитанская дочка", "Вильямс", 123, 2012);
          //  Book.SetPrice(12);
            b1.Show();

            Console.WriteLine("\n Итоговая стоимость аренды: {0} p.", b1.PriceBook(3));

            Book b2 = new Book("Толстой Л.Н.", "Война и мир", "Наука и жизнь", 1234, 2013, 101, true);
            b2.TakeItem();
            b2.Show();
      //      b2.ReturnSrok();
            b2.Return();
            b2.Show();

     //       Item it = new Item();

            Book b3 = new Book("Лермонтов М.Ю.", "Мцыри");
            b3.Show();

            Magazine mag1 = new Magazine("О природе", 5,"Земля и мы", 2014, 1235, true);
            mag1.TakeItem();
            mag1.Show();

            Console.WriteLine("\n Тестирование полиморфизма");

            Item it;
            it = b2;
            it.TakeItem();
            it.Return();
            it.Show();
            it = mag1;
            it.TakeItem();
            it.Return();
            it.Show();
        }
    }
}
