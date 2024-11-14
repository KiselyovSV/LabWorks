
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

		// Print the accounts, using ToString
		Console.WriteLine("acc1 - {0}", acc1);
		Console.WriteLine("acc2 - {0}", acc2);

		for (int i = 0; i < 5; i++)
		{
			acc1.Deposit(100);
			acc1.Withdraw(50);
		}

		Write(acc1);
		
    }

	static void Write(BankAccount acc)
    {
        Console.WriteLine("Account number is {0}",  acc.Number);
        Console.WriteLine("Account balance is {0}", acc.Balance());
        Console.WriteLine("Account type is {0}", acc.Type);

		// Print out the transactions (if any)
		Console.WriteLine("Transactions");
		Queue tranQueue = acc.Transactions();

		// Replace this loop with a for loop that uses an indexer
		for (int counter = 0; counter < tranQueue.Count; counter++)
        {
                BankTransaction tran = acc[counter];
                Console.WriteLine("Date: {0}\tAmount: {1}", tran.When, tran.Amount);
		}
    }

}
