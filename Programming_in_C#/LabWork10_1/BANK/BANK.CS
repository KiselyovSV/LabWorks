using System;
using System.Collections;

public class Bank
{
    private static Hashtable accounts = new Hashtable();
    public static long CreateAccount()
    {
        BankAccount newAcc = new BankAccount();
        long accNo = newAcc.Number();
        accounts[accNo] = newAcc;
        return accNo;
    }
    public static long CreateAccount(AccountType aType, decimal aBal)
    {
        BankAccount newAcc = new BankAccount(aType, aBal);
        long accNo = newAcc.Number();
        accounts[accNo] = newAcc;
        return accNo;
    }
    public static long CreateAccount(AccountType aType)
    {
        BankAccount newAcc = new BankAccount(aType);
        long accNo = newAcc.Number();
        accounts[accNo] = newAcc;
        return accNo;
    }
    public static long CreateAccount(decimal aBal)
    {
        BankAccount newAcc = new BankAccount(aBal);
        long accNo = newAcc.Number();
        accounts[accNo] = newAcc;
        return accNo;
    }

    public static bool CloseAccount(long accNo)
    {
        BankAccount closing =
        accounts[accNo] as BankAccount;
        if (closing != null)
        {
            accounts.Remove(accNo);
            closing.Dispose();
            return true;
        }
        else
        {
            return false;
        }
    }
    public static BankAccount GetAccount(long accNo)
    {
        BankAccount account = accounts[accNo] as BankAccount;
        if(account != null) return account;
        else throw new Exception("null");
    }
}