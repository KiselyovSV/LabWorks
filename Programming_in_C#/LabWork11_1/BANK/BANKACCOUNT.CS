using System;
using System.IO;
using System.Collections;

namespace Banking
{
	sealed public class BankAccount : IDisposable
	{
		private long accNo;
		private decimal accBal;
		private AccountType accType;
		private Queue tranQueue = new Queue();
		private bool disposed = false;
    
		private static long nextNumber = 123;

		// Constructors
		internal BankAccount()
		{
			accNo = NextNumber();
			accType = AccountType.Checking;
			accBal = 0;
		}

		internal BankAccount(AccountType aType)
		{
			accNo = NextNumber();
			accType = aType;
			accBal = 0;
		}

		internal BankAccount(decimal aBal)
		{
			accNo = NextNumber();
			accType = AccountType.Checking;
			accBal = aBal;
		}

		internal BankAccount(AccountType aType, decimal aBal)
		{
			accNo = NextNumber();
			accType = aType;
			accBal = aBal;
		}

		// Dispose Method

		public void Dispose()
		{
			if (!disposed) 
			{
				StreamWriter swFile = File.AppendText("Transactions.Dat");
				swFile.WriteLine("Account number is {0}",  accNo);
				swFile.WriteLine("Account balance is {0}", accBal);
				swFile.WriteLine("Account type is {0}", accType);
				swFile.WriteLine("Transactions:");
				foreach(BankTransaction tran in tranQueue) 
				{
					swFile.WriteLine("Date/Time: {0}\tAmount: {1}", tran.When(), tran.Amount());
				}
				swFile.Close();
				disposed = true;
				GC.SuppressFinalize(this);
			}
		}

		public bool Withdraw(decimal amount)
		{
			bool sufficientFunds = accBal >= amount;
			if (sufficientFunds) 
			{
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

		~BankAccount()
		{
			Dispose();
		}
		public static bool operator ==(BankAccount acc1, BankAccount acc2)
		{
			if (acc1.accNo == acc2.accNo && acc1.accBal == acc2.accBal && acc1.accType == acc2.accType) return true;
			else return false;

		}
		public static bool operator !=(BankAccount acc1, BankAccount acc2)
		{
			return !(acc1 == acc2);

		}
		public override bool Equals(object obj)
		{
			if (obj is BankAccount acc) return (this == acc);
			else return false;
			
		}
		public override string ToString()
		{
			string retVal = "����� �����: " + this.accNo + "\t���: ";
			retVal += (this.accType == AccountType.Checking) ?
			"��������" : "�������";
			retVal += "\t������: " + this.accBal;
			return retVal;
		}
		public override int GetHashCode()
		{
			return (int)this.accNo;
		}
	}
}