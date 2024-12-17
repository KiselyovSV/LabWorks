using System;
using Banking;
using Auditor;


namespace AuditTestHarness
{
/// <summary>
///    Test Harness for the Audit Class.
/// </summary>
public class Test
{
	public static void Main()
	{
			long acc1N = Bank.CreateAccount(2000);
			long acc2N = Bank.CreateAccount(3000);
			BankAccount acc1 = Bank.GetAccount(acc1N);
			BankAccount acc2 = Bank.GetAccount(acc2N);
	    	acc1.AuditTrail("Account1.dat");
			acc2.AuditTrail("Account2.dat");
			acc1.Deposit(500);
			acc2.Deposit(1200);
			acc2.Withdraw(230);
			acc2.Withdraw(138);
			acc1.Deposit(333);
			Bank.CloseAccount(acc1N);
			Bank.CloseAccount(acc2N);

		Audit testAudit = new Audit("AuditTrail.dat");
		BankTransaction testTran = new BankTransaction(500);
		AuditEventArgs testArg = new AuditEventArgs(testTran);
		testAudit.RecordTransaction(null, testArg);

		BankTransaction testTran2 = new BankTransaction(-200);
		AuditEventArgs testArg2 = new AuditEventArgs(testTran2);
		testAudit.RecordTransaction(null, testArg2);

		testAudit.Close();
	}
}
}
