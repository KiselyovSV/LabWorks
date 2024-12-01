using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyClass
{
    abstract class Item : IComparable
    {
        protected long invNumber; // инвентарный номер — целое число
        protected bool taken;    // хранит состояние объекта - взят ли на руки

        public Item(long invNumber, bool taken)
        {
            this.invNumber = invNumber;
            this.taken = taken;
        }

        public Item()
        {
            this.taken = true;
        }
           
   public bool IsAvailable()    // истина, если этот предмет имеется в библиотеке
      {
          if (taken == true)
              return true;
          else
              return false;
      }

   public long GetInvNumber()   // инвентарный номер  
      {
          return invNumber;
      }


   private void Take()      // операция "взять"
        {
            taken = false;
        }

  abstract public  void Return();    // операция "вернуть"
       //{
       //   // taken = true;
       //}

   public void TakeItem()
   {
       if (this.IsAvailable())
           this.Take();
   }


   public virtual void Show()
   {
       Console.WriteLine("Состояние хранения:\n Инвентарный номер: {0}\n Наличие: {1}", invNumber, taken);
   }


   int IComparable.CompareTo(object obj)
   {
       Item it = (Item)obj;
       if (this.invNumber == it.invNumber) return 0;
       else if (this.invNumber > it.invNumber) return 1;
       else return -1;
   }


    }
}
