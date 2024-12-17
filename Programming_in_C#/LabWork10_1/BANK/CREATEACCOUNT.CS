
using System;
using System.Security.Principal;

class CreateAccount
{
    // Test Harness
    static void Main()
    {
        Bank bank2 = new Bank();
        long acc1AN = Bank.CreateAccount(20000);
        BankAccount acc1 = Bank.GetAccount(acc1AN);
        TestDeposit(acc1);
        TestWithdraw(acc1);
        Write(acc1);
        CloseAcc(acc1AN);
        long acc2AN = Bank.CreateAccount(33000);
        BankAccount acc2 = Bank.GetAccount(acc2AN);
        TestDeposit(acc2);
        TestWithdraw(acc2);
        Write(acc2);
        CloseAcc(acc2AN);
    }

    private static void CloseAcc(long acc1AccNo)
    {
        if (Bank.CloseAccount(acc1AccNo))
        {
            Console.WriteLine("Счёт закрыт.");
        }
        else
        {
            Console.WriteLine("Что-то пошло не так, когда я закрыл счёт");
        }
    }

    static void TestDeposit(BankAccount acc)
	{
		Console.Write("\nВведите сумму для добавления на счёт: ");
		decimal amount = decimal.Parse(Console.ReadLine());
		acc.Deposit(amount);        
	}
    
	static void TestWithdraw(BankAccount acc)
	{
		Console.Write("Введите сумму снятия со счёта: ");
		decimal amount = decimal.Parse(Console.ReadLine());
		acc.Withdraw(amount);   
	}

    static void Write(BankAccount acc)
    {
        Console.WriteLine("Счёт номер: {0}",  acc.Number());
        Console.WriteLine("Балланс счёта: {0}", acc.Balance());
        Console.WriteLine("Тип счёта: {0}", acc.Type());
		Console.WriteLine("Список транзакций:");
		foreach (BankTransaction tran in acc.Transactions()) 
		{
			Console.WriteLine("Дата/Время: {0}\tСумма: {1}", tran.When(), tran.Amount());
		}
    }
}
