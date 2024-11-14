using Banking;
using System;

namespace Auditor
{
    public class AuditEventArgs: System.EventArgs
    {
        private readonly BankTransaction transData = null;
        public AuditEventArgs(BankTransaction transaction)
        {
            transData = transaction;
        }
        public BankTransaction getTransaction()
        {
            return transData;
        }

    }
}
