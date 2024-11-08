using System;
using System.IO;
using System.Collections;


class BankAccount: IDisposable
{
	private long accNo;
    private decimal accBal;
    private AccountType accType;
	private Queue tranQueue = new Queue();
    private static long nextNumber = 123;
    bool disposed = false;

	// Constructors
    public BankAccount()
    {
        accNo = NextNumber();
        accType = AccountType.Checking;
	    accBal = 0;
    }

    public BankAccount(AccountType aType)
	{
		accNo = NextNumber();
		accType = aType;
		accBal = 0;
	}

	public BankAccount(decimal aBal)
	{
		accNo = NextNumber();
		accType = AccountType.Checking;
		accBal = aBal;
	}

	public BankAccount(AccountType aType, decimal aBal)
	{
		accNo = NextNumber();
		accType = aType;
		accBal = aBal;
	}
    public void Dispose()
    {
        Dispose(true);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed) return;
        
            if (disposing)
            {
                StreamWriter swFile = File.AppendText(@"E:\LabWorks\LabWork8_3\FINALIZERS\bin\Debug\Transactions.Dat");
                swFile.WriteLine("\nAccount number is {0}", accNo);
                swFile.WriteLine("Account balance is {0}", accBal);
                swFile.WriteLine("Account type is {0}", accType);
                swFile.WriteLine("Транзакции:");
                foreach (BankTransaction tran in tranQueue)
                {
                    swFile.WriteLine($"Дата/Время:{tran.When()}, Сумма транзакции составляет:{tran.Amount()}");
                }
                swFile.Close();
                disposed = true;
                GC.SuppressFinalize(this);
            }
        
    }

    ~ BankAccount()
    {
        Dispose(false);
    }


    public bool Withdraw(decimal amount)
    {
        bool sufficientFunds = accBal >= amount;
        if (sufficientFunds) {
            accBal -= amount;
			BankTransaction tran = new BankTransaction(-amount);
			tranQueue.Enqueue(tran);
        }
        return sufficientFunds;
    }
    
    public decimal Deposit(decimal amount)
    {
        accBal += amount;
		BankTransaction tran = new BankTransaction(amount);
		tranQueue.Enqueue(tran);
        return accBal;
    }

	public Queue Transactions()
	{
		return tranQueue;
	}
    
    public long Number()
    {
        return accNo;
    }
    
    public decimal Balance()
    {
        return accBal;
    }
    
    public string Type()
    {
        return accType.ToString();
    }

    private static long NextNumber()
    {
        return nextNumber++;
    }
}
