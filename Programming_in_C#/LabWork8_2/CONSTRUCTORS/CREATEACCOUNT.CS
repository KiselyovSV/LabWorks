
using System;

class CreateAccount
{
    // Test Harness
    static void Main() 
    {
        BankAccount acc1 = new BankAccount(1000);
        BankAccount acc2 = new BankAccount(2000);
        BankAccount acc3 = new BankAccount(5000);
        BankAccount acc4 = new BankAccount(10000);
        acc1.Deposit(500);
        acc3.Withdraw(99);
        acc4.Withdraw(900);
        acc2.Deposit(600);
        acc3.Deposit(15800);
        acc1.Withdraw(999);
        acc4.Deposit(30);
        acc2.Withdraw(100000);
        acc1.Withdraw(3000);
        Write(acc1);
        Write(acc2);
        Write(acc3);
        Write(acc4);
    }
      
    static void Write(BankAccount acc)
    {
        Console.WriteLine("Account number is {0}",  acc.Number());
        Console.WriteLine("Account balance is {0}", acc.Balance());
        Console.WriteLine("Account type is {0}", acc.Type());
        Console.WriteLine("Транзакции:");
        foreach (BankTransaction tran in acc.Transactions())
        {
            Console.WriteLine("Дата/Время: {0}\tСумма транзакции: {1}",
   tran.When, tran.Amount);
        }  
        Console.WriteLine();
    }
}
