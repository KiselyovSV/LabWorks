namespace StructType
{
    public enum AccountType 
    { 
        Checking, 
        Deposit 
    }
    public struct BankAccount
    {
        public long accNo;          // номер
        public decimal accBal;      // баланс
        public AccountType accType; // тип
    }
    internal class Struct
    {
        static void Main(string[] args)
        {
            BankAccount goldAccount;
            Console.Write("\nВведите номер счёта:");
            goldAccount.accNo = Convert.ToInt64(Console.ReadLine());
            goldAccount.accBal = 3200.00M;
            goldAccount.accType = AccountType.Checking;
            Console.WriteLine($"Acct Number:{goldAccount.accNo}\nAcct Type:{goldAccount.accType}\nAcct Balance:{goldAccount.accBal}");
            Console.ReadKey();

        }
    }
}
