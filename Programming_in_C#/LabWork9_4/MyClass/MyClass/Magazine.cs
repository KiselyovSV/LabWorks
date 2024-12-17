using System;

namespace MyClass
{
    internal class Magazine: Item, IPubs
    {
        private int volume;                 // том
        private int number;                 // номер
        private string title;               // название
        private int year;                   // год выпуска
        
        private static double price = 5;     // стоимость аренды в сутки
        private int days;                    // количество дней
        private double costOfRent;           // общая стоимость аренды
        public bool IfSubs {  get; set; }
        public void Subs()
        {
            Console.WriteLine($"Подписка на журнал {title}: {IfSubs}.");
        }


        public Magazine() : this(0,0,"", 0, 0, false) { }
        public Magazine(int volume, int number, string title, int year, long InvNumber, bool IsAvailable)
            : base(InvNumber, IsAvailable)
        {
            this.volume = volume;
            this.title = title;
            this.number = number;
            this.year = year;
        }
        static Magazine()
        {
            price = 8;
        }

        public static void SetPrice(double price)
        {
            Magazine.price = price;
        }

        new public void Show()
        {
            Console.WriteLine("\nТом журнала: {0}\nНомер: {1}\nНазвание: {2}\nГод выпуска: {3}\n" +
                "Стоимость аренды в сутки: {4} руб.\nОбщая стоимость аренды за {5} дней составляет: {6} руб.\n" +
                "Состояние единицы хранения:\nИнвентарный номер: {7}\nНаличие: {8}", volume, number, title, year,
                price, days, costOfRent, InvNumber, IsAvailable);
            Subs();
        }

        public void СostOfRent(int days)
        {
            this.days = days;
            costOfRent = price * days;
        }

        public void TakeMagazine()
        {
            if (IsAvailable) Take();

        }

        public override void Return()
        {
            IsAvailable = true;
        }

    }
}
