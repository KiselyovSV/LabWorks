using System;

namespace BankAcc
{
    class Program
    {
        static void Main(string[] args)
        {
            BankAccount acc1 = new BankAccount();
            acc1.Populate(10000);
            Print(acc1);
            BankAccount acc2 = new BankAccount();
            acc2.Populate(10000);
            Print(acc2);
            acc1.TransferFrom(acc2, 100);
            Print(acc1);
            Print(acc2);
            

        }

        private static void Print(BankAccount a)
        {
            Console.WriteLine($"\nБалланс счета номер {a.Number()} (тип {a.Type()}), составляет {a.Balance()} у.е.");
        }
    }
}