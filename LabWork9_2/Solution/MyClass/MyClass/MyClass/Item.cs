using System;
using System.Numerics;

namespace MyClass
{
    internal class Item
    {
        private long invNumber;
        private bool taken;

        internal long InvNumber
        {
            get => invNumber;
            set => invNumber = value;
        }

        internal bool IsAvailable
        {
            get => taken;
            private set => taken = value;
        }

        public Item(): this(0, true) { }
          
        public Item(long invNumber, bool taken)
        {
            InvNumber = invNumber;
            IsAvailable = taken;
        }

        // Операция "взять"
        private void Take()
        {
            taken = false;
        }
        // Операция "вернуть"
        private void Return()
        {
            taken = true;
        }
        public void Show() => Console.WriteLine("\nСостояние единицы хранения:\nИнвентарный номер: {0}\nНаличие: {1}", invNumber, taken);
         



    }

}
