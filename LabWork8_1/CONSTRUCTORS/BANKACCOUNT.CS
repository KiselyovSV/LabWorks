
class BankAccount 
{
	private long accNo;
    private decimal accBal;
    private AccountType accType;
    
    private static long nextNumber = 123;

    public BankAccount(): this (0, AccountType.Checking) { }

    public BankAccount(AccountType accType) : this(0, accType) { }
    
    public BankAccount(decimal accBal): this (accBal, AccountType.Checking) { }
    
    public BankAccount(decimal accBal, AccountType accType)
    {
        accNo = NextNumber();
        this.accBal = accBal;
        this.accType = accType;
    }

    public bool Withdraw(decimal amount)
    {
        bool sufficientFunds = accBal >= amount;
        if (sufficientFunds) {
            accBal -= amount;
        }
        return sufficientFunds;
    }
    
    public decimal Deposit(decimal amount)
    {
        accBal += amount;
        return accBal;
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
}
