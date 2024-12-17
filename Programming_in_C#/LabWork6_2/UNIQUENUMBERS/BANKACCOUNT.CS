
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
   
}
