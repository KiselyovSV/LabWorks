using System;
using System.IO;
using System.Collections;

namespace Banking
{
	public delegate void AuditEventHandler (Object sender, AuditEventArgs data);

	sealed public class BankAccount : IDisposable
	{
		
		private decimal accBal;
	    private Queue tranQueue = new Queue();
		private bool disposed = false;
		private Audit accountAudit = null;
		private event AuditEventHandler AuditingTransaction = null;

		private static long nextNumber = 123;
        public string Holder { get; set; }
        public long Number { get; private set; }
        public AccountType Type { get; private set; }

        //public AccountType Type                       // если делать так, то возникает ошибка. Не подскажете почему?
        //{                                     
        //	get => Type.ToString(); 
        //	private set => Type = value;
        //}


        // Constructors
        internal BankAccount()
		{
            Number = NextNumber();
			Type = AccountType.Checking;
			accBal = 0;
		}

		internal BankAccount(AccountType aType)
		{
            Number = NextNumber();
			Type = aType;
			accBal = 0;
		}

		internal BankAccount(decimal aBal)
		{
            Number = NextNumber();
			Type = AccountType.Checking;
			accBal = aBal;
		}

		internal BankAccount(AccountType aType, decimal aBal)
		{
            Number = NextNumber();
			Type = aType;
			accBal = aBal;
		}

		public void AddOnAuditingTransaction(AuditEventHandler handler)
		{
			this.AuditingTransaction += handler;
		}

		public void RemoveOnAuditingTransaction(AuditEventHandler handler)
		{
			this.AuditingTransaction -= handler;
		}

		protected void OnAuditingTransaction(BankTransaction bankTrans)
		{
			if (this.AuditingTransaction != null)
			{
				AuditEventArgs auditTrans = new AuditEventArgs(bankTrans);
				this.AuditingTransaction(this, auditTrans);
			}
		}

		public void AuditTrail(string auditFileName)
		{
			this.accountAudit = new Audit(auditFileName);
			AuditEventHandler doAuditing = new AuditEventHandler(this.accountAudit.RecordTransaction);
			this.AddOnAuditingTransaction(doAuditing);
		}

		public static bool operator== (BankAccount acc1, BankAccount acc2)
		{
			if((acc1.Type == acc2.Type) && 
			  (acc1.Number == acc2.Number) &&
			  (acc1.accBal == acc2.accBal))
			  return true;
			else
			  return false;
		}

		public static bool operator != (BankAccount acc1, BankAccount acc2)
		{
			return !(acc1 == acc2);
		}

		public override bool Equals(object acc1)
		{
			return this == (BankAccount)acc1;
		}

		public override string ToString()
		{
			string retVal = "Number: " + Number + "\tType: ";
			retVal += (Type == AccountType.Checking) ? "Checking" : "Deposit";
			retVal += "\tBalance: " + accBal;
			retVal += "\tName: " + Holder;
			return retVal;
		}

		public override int GetHashCode()
		{
			return (int)this.Number;
		}
		// Dispose Method
		public void Dispose()
		{
			if (!disposed) 
			{
				if(accountAudit != null) 
					accountAudit.Close();
				GC.SuppressFinalize(this);
				disposed = true;
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
				this.OnAuditingTransaction(tran);
            }
			return sufficientFunds;
		}
    
		public decimal Deposit(decimal amount)
		{
			accBal += amount;
			BankTransaction tran = new BankTransaction(amount);
			tranQueue.Enqueue(tran);
			this.OnAuditingTransaction(tran);
			return accBal;
		}

		public Queue Transactions()
		{
			return tranQueue;
		}
    
		
    
		public decimal Balance()
		{
			return accBal;
		}
    
		
		private static long NextNumber()
		{
			return nextNumber++;
		}

		~BankAccount()
		{
			Dispose();
		}
	}
}