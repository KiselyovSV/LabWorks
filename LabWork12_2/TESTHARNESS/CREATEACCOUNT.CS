
using System;
using System.Collections;
using Banking;

// Test harness
class CreateAccount
{
    static void Main() 
    {

		// Create two bank accounts. Use Bank.CreateAccount(...) with the same balance and type
		// Store the numbers of these two accounts in long variables accNo1 and accNo2
		long accNo1 = Bank.CreateAccount(AccountType.Checking, 100);
		long accNo2 = Bank.CreateAccount(AccountType.Checking, 100);

		// Create two BankAccount variables, acc1 and acc2. 
		// Use Bank.GetAccount() to populate them with the two accounts just created
		BankAccount acc1 = Bank.GetAccount(accNo1);
		BankAccount acc2 = Bank.GetAccount(accNo2);

		acc1.Deposit(300);
		acc2.Deposit(1800);
		acc2.Deposit(550);
		acc2.Deposit(4800);
		acc2.Withdraw(5600);
		acc1.Withdraw(500);


		Write(acc1);
		Write(acc2);

		// Print the accounts, using ToString
		Console.WriteLine("acc1 - {0}", acc1.ToString());
		Console.WriteLine("acc2 - {0}", acc2.ToString());
    }

	static void Write(BankAccount acc)
    {
        Console.WriteLine("Account number is {0}",  acc.Number);
        Console.WriteLine("Account balance is {0}", acc.Balance());
        Console.WriteLine("Account type is {0}", acc.Type);

		// Print out the transactions (if any)
		Console.WriteLine("Transactions");
		Queue tranQueue = acc.Transactions();
		foreach (BankTransaction tran in tranQueue) {
			Console.WriteLine("Date: {0}\tAmount: {1}", tran.When, tran.Amount);
		}
    }

}
