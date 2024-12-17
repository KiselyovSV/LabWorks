
    using System;
    using System.IO;

	namespace Banking
	{
		public class BankTransaction
		{
        public decimal Amount { get; }

        public DateTime When { get; }
        

        public BankTransaction(decimal tranAmount)
			{
				Amount = tranAmount;
				When = DateTime.Now;
			}

			
			
			
		}
	}
