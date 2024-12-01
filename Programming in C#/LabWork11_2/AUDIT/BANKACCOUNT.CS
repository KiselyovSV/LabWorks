using System;
using System.IO;
using System.Collections;
using Auditor;

namespace Banking
{
	public delegate void AuditEventHandler (object sender, AuditEventArgs data);

    sealed public class BankAccount : IDisposable
	{
		private long accNo;
		private decimal accBal;
		private AccountType accType;
		private Queue tranQueue = new Queue();
		private bool disposed = false;
    
		private static long nextNumber = 123;

        private Audit accountAudit;

        private event AuditEventHandler AuditingTransaction = null;

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

		public static bool operator== (BankAccount acc1, BankAccount acc2)
		{
			if((acc1.accType == acc2.accType) && 
			  (acc1.accNo == acc2.accNo) &&
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
			string retVal = "Number: " + this.accNo + "\tType: ";
			retVal += (this.accType == AccountType.Checking) ? "Checking" : "Deposit";
			retVal += "\tBalance: " + this.accBal;
			return retVal;
		}
		
		public override int GetHashCode()
		{
			return (int)this.accNo;
		}
		// Dispose Method
		public void Dispose()
		{
			if (!disposed) 
			{
				GC.SuppressFinalize(this);
				disposed = true;
                accountAudit.Close();
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
	}
}