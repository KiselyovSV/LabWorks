using System;
using System.Numerics;

namespace MyClass
{
    abstract class Item
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
            set => taken = value;
        }

        public Item(): this(0, true) { }
          
        public Item(long invNumber, bool taken)
        {
            InvNumber = invNumber;
            IsAvailable = taken;
        }

        // Операция "взять"
        private protected void Take()
        {
            IsAvailable = false;
        }
        // Операция "вернуть"
        abstract public void Return();
        
        public void Show() => Console.WriteLine("\nСостояние единицы хранения:\nИнвентарный номер: {0}\nНаличие: {1}", invNumber, taken);
         



    }

}
