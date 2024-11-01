
class BankAccount 
{
    private static long nextAccNo = 123;
    private long accNo;
    private decimal accBal;
    private AccountType accType;

    public void Populate(decimal balance)
    {
        accNo = NextNumber();
        accBal = balance;
        accType = AccountType.Checking;
    }
    
    private static long NextNumber()
    {
        return nextAccNo++;
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
    public decimal Deposit(decimal sum)
    {
        if (sum > 0)
        {
            accBal += sum;
            return accBal;
        }
        else return 0;
    }
    public bool Withdraw(decimal sum)
    {
        bool a = accBal >= sum;
        if (a) accBal -= sum;
        return a;
    }
}
