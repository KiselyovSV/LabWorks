
using System;

class BankAccount 
{
    private long accNo;
    private decimal accBal;
    private AccountType accType;
    
    public void Populate(long an, decimal ab)
    {
        accNo = an;
        accBal = ab;
        accType = AccountType.Checking;
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
       return accType.ToString;
    }
}

