using System;
using System.IO;
using Auditor;

namespace Banking
{
    public class Audit
    {
        private bool closed = false;
        private string filename;
        private StreamWriter auditFile;
        public Audit(string fileToUse)
        {
            filename = fileToUse;
            auditFile = File.AppendText(fileToUse);
        }
        public void RecordTransaction(object sender, AuditEventArgs eventData)
        {
            BankTransaction tempTrans = eventData.getTransaction();
            if (tempTrans != null)
                auditFile.WriteLine("Amount: {0}\tDate: {1}",
                tempTrans.Amount(), tempTrans.When());
        }
        public void Close()
        {
            if (!closed)
            {
                auditFile.Close();
                closed = true;
            }
        }




    }
}