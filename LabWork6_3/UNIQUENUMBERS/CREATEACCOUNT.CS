using System;

class CreateAccount
{
    static void Main() 
    {
        BankAccount berts = NewBankAccount();
        Write(berts);
        TestDeposit(berts);
        Write(berts);
        TestWithdraw(berts);
        Write(berts);

        BankAccount freds = NewBankAccount();
        Write(freds);
        TestDeposit(freds);
        Write(freds);
        TestWithdraw(freds);
        Write(freds);
        
    }
    
    static BankAccount NewBankAccount()
    {       
        BankAccount created = new BankAccount();

        //Console.Write("Enter the account number   : ");
        //long number = long.Parse(Console.ReadLine());
        //long number = BankAccount.NextNumber();

        Console.Write("\nEnter the account balance! : ");
        decimal balance = decimal.Parse(Console.ReadLine());
        
        //created.accNo = number;
        //created.accBal = balance;
        //created.accType = AccountType.Checking;
        created.Populate(balance);
        
        return created;
    }
    
    static void Write(BankAccount toWrite)
    {
        Console.WriteLine("Account number is {0}",  toWrite.Number());
        Console.WriteLine("Account balance is {0}", toWrite.Balance());
        Console.WriteLine("Account type is {0}", toWrite.Type());
    }
    static void TestDeposit(BankAccount ac)
    {
        Console.Write($"Введите сумму, которую Вы хотите положить на счёт:\t");
        decimal sum = decimal.Parse(Console.ReadLine());
        ac.Deposit(sum);
    }
    static void TestWithdraw(BankAccount ac)
    {
        Console.Write($"Введите сумму, которую Вы хотите снять со счётa:\t");
        decimal sum = decimal.Parse(Console.ReadLine());
        if (!ac.Withdraw(sum))
        {
            Console.WriteLine($"Недостаточно средств.");
        }
    }
}
