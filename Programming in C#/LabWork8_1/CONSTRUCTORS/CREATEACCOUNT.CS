
using System;

class CreateAccount
{
    // Test Harness
    static void Main() 
    {
        BankAccount acc1 = new BankAccount();
        BankAccount acc2 = new BankAccount(AccountType.Deposit);
        BankAccount acc3 = new BankAccount(100);
        BankAccount acc4 = new BankAccount(500, AccountType.Deposit);
        Write(acc1);
        Write(acc2);
        Write(acc3);
        Write(acc4);
    }
      
    static void Write(BankAccount acc)
    {
        Console.WriteLine("\nAccount number is {0}",  acc.Number());
        Console.WriteLine("Account balance is {0}", acc.Balance());
        Console.WriteLine("Account type is {0}", acc.Type());
    }
}
