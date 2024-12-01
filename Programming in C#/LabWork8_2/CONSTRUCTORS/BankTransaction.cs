using System;


internal class BankTransaction
{
    private readonly decimal amount;
    private readonly DateTime when;

    public decimal Amount { get { return amount; } }
    public DateTime When { get { return when; } }

    public BankTransaction(decimal tranAmount)
    {
        when = DateTime.Now;
        amount = tranAmount;
    }






}

