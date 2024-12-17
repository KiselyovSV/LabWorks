using System;

namespace MyClass
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Book b2 = new Book("Толстой Л.Н.", "Война и мир", "Наука", 823, 2013, 018135, true);
            Book.SetPrice(25);
            b2.СostOfRent(30);
            b2.Show();
            Item item1 = new Item();
            item1.Show();
            Item item3 = new Book("Чехов А.П.","Три сестры","Молодая гвардия", 236, 1985, 018136, true);
            Book b3 = (Book)item3;
            b3.СostOfRent(14);
            b3.TakeBook();
            b3.Show();
            Book b4 = new Book();
            b4.Show();
            Magazine m1 = new Magazine(1,12,"Play Boy",2024, 00153, true);
            m1.СostOfRent(2);
            m1.TakeMagazine();
            m1.ReturnMagazine();
            m1.Show();


        }
    }
}
