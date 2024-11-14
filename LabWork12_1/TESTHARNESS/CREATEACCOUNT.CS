
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

        acc1.Holder = "Sid";
        acc2.Holder = "Ted";

        Console.WriteLine("Account Number is {0}, Account Type is {1}", acc1.Number, acc1.Type);
        Console.WriteLine("Account Number is {0}, Account Type is {1}", acc2.Number, acc2.Type);

        acc1.ToString();
        acc2.ToString();

        Console.WriteLine("acc1 - {0}", acc1);
		Console.WriteLine("acc2 - {0}", acc2);
    }
}
