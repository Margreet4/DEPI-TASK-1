using System;
using System.Collections.Generic;

// Parent
public class BankAccount
{
    public string FullName { get; set; }
    public decimal Balance { get; set; }
    public decimal AccountNumber { get; set; }

    public virtual void ShowAccountDetails()
    {
        Console.WriteLine($"Name: {FullName}, AccountNumber: {AccountNumber}, Balance: {Balance}");
    }

    public virtual void CalculateInterest()
    {
        Console.WriteLine("This account does not calculate interest.");
    }
}

// Child 1
public class SavingAccount : BankAccount
{
    public decimal InterestRate { get; set; }

    public override void CalculateInterest()
    {
        decimal interest = Balance * InterestRate / 100;
        Console.WriteLine($"Interest for saving account: {interest}");
    }
}

// Child 2
public class CurrentAccount : BankAccount
{
    public decimal OverdraftLimit { get; set; }

    public override void CalculateInterest()
    {
        Console.WriteLine("Current account does not earn interest.");
    }
}

class Program
{
    static void Main()
    {
        SavingAccount saving = new SavingAccount
        {
            FullName = "Margreet",
            AccountNumber = 1001,
            Balance = 500000,
            InterestRate = 4
        };

        CurrentAccount current = new CurrentAccount
        {
            FullName = "Posy",
            AccountNumber = 1005,
            Balance = 55200,
            OverdraftLimit = 1000
        };

        List<BankAccount> accounts = new List<BankAccount>();
        accounts.Add(saving);
        accounts.Add(current);

        foreach (BankAccount acc in accounts)
        {
            acc.ShowAccountDetails();
            acc.CalculateInterest();
            Console.WriteLine("--------------");
        }
    }
}
