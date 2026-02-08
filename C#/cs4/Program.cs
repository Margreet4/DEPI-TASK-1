using System;
using System.Collections.Generic;


public static class Bank
{
    public static string BName = "MEBank";
    public static int BranchCode = 1;
    public static List<Customers> CustomersList = new List<Customers>();
}


public class Customers
{
    private static int counter = 1;

    public int Id { get; private set; }
    public string FullName { get; set; }
    public string NationalId { get; set; }
    public string BirthDate { get; set; }

    public List<Account> Accounts { get; private set; } = new List<Account>();

    public Customers(string name, string NID, string BD)
    {
        Id = counter++;
        FullName = name;
        NationalId = NID;
        BirthDate = BD;
    }

 
    public class CustomerAccounts
    {
        private Customers parent;

        public CustomerAccounts(Customers parent)
        {
            this.parent = parent;
        }

        
        public void Update(string newName, string newBirthDate)
        {
            parent.FullName = newName;
            parent.BirthDate = newBirthDate;
            Console.WriteLine($"Updated: Name = {parent.FullName}, BirthDate = {parent.BirthDate}");
        }

   
        public void Remove()
        {
            bool canRemove = true;
            foreach (var acc in parent.Accounts)
            {
                if (acc.Balance > 0)
                {
                    canRemove = false;
                    break;
                }
            }

            if (canRemove)
            {
                Bank.CustomersList.Remove(parent);
                Console.WriteLine("Customer removed successfully!");
            }
            else
            {
                Console.WriteLine("Cannot remove customer: Some accounts have balance.");
            }
        }

        // البحث بالاسم
        public bool SearchByName(string name)
        {
            return parent.FullName == name;
        }

        
        public bool SearchByNationalId(string nid)
        {
            return parent.NationalId == nid;
        }

      
        public void ShowAccounts()
        {
            foreach (var acc in parent.Accounts)
            {
                Console.WriteLine(acc);
            }
        }

        
        public decimal TotalBalance()
        {
            decimal total = 0;
            foreach (var acc in parent.Accounts)
            {
                total += acc.Balance;
            }
            return total;
        }
    }
}

// Abstract Account Class
public abstract class Account
{
    private static int accCounter = 1000;
    public int AccountNumber { get; private set; }
    public decimal Balance { get; protected set; }
    public DateTime DateOpened { get; private set; }
    public List<string> TransactionHistory { get; private set; } = new List<string>();

    public Account()
    {
        AccountNumber = accCounter++;
        Balance = 0;
        DateOpened = DateTime.Now;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
        TransactionHistory.Add($"Deposit: +{amount:C} | Balance: {Balance:C}");
    }

    public virtual void Withdraw(decimal amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
            TransactionHistory.Add($"Withdraw: -{amount:C} | Balance: {Balance:C}");
        }
        else
        {
            Console.WriteLine("Insufficient balance!");
        }
    }

    public void Transfer(Account to, decimal amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
            to.Balance += amount;
            TransactionHistory.Add($"Transfer: -{amount:C} to Acc {to.AccountNumber} | Balance: {Balance:C}");
            to.TransactionHistory.Add($"Transfer: +{amount:C} from Acc {AccountNumber} | Balance: {to.Balance:C}");
        }
        else
        {
            Console.WriteLine("Insufficient balance for transfer!");
        }
    }

    public void ShowTransactions()
    {
        Console.WriteLine($"Transactions for Account {AccountNumber}:");
        foreach (var t in TransactionHistory)
        {
            Console.WriteLine(t);
        }
    }

    public override string ToString()
    {
        return $"Acc#: {AccountNumber} | Balance: {Balance:C} | Opened: {DateOpened:d}";
    }
}


public class SavingsAccount : Account
{
    public decimal InterestRate { get; set; } 

    public SavingsAccount(decimal rate)
    {
        InterestRate = rate;
    }

    public decimal CalculateMonthlyInterest()
    {
        return Balance * InterestRate / 12;
    }

    public void ApplyMonthlyInterest()
    {
        decimal interest = CalculateMonthlyInterest();
        Deposit(interest);
    }

    public override string ToString()
    {
        return $"[Savings] {base.ToString()} | InterestRate: {InterestRate:P}";
    }
}

public class CurrentAccount : Account
{
    public decimal OverdraftLimit { get; set; }

    public CurrentAccount(decimal overdraft)
    {
        OverdraftLimit = overdraft;
    }

    public override void Withdraw(decimal amount)
    {
        if (Balance + OverdraftLimit >= amount)
        {
            Balance -= amount;
            TransactionHistory.Add($"Withdraw: -{amount:C} | Balance: {Balance:C}");
        }
        else
        {
            Console.WriteLine("Exceeded overdraft limit!");
        }
    }

    public override string ToString()
    {
        return $"[Current] {base.ToString()} | OverdraftLimit: {OverdraftLimit:C}";
    }
}


class Program
{
    static void Main()
    {
      
        var customer1 = new Customers("Ali", "123456789", "01-01-1990");
        var customer2 = new Customers("Sara", "987654321", "05-05-1995");

        Bank.CustomersList.Add(customer1);
        Bank.CustomersList.Add(customer2);

      
        var savings1 = new SavingsAccount(0.05m);
        var current1 = new CurrentAccount(1000);

        customer1.Accounts.Add(savings1);
        customer1.Accounts.Add(current1);

        var savings2 = new SavingsAccount(0.03m);
        customer2.Accounts.Add(savings2);

        
        savings1.Deposit(5000);
        current1.Deposit(2000);
        current1.Withdraw(2500); 

        savings1.Transfer(current1, 1000);

        
        savings1.ApplyMonthlyInterest();

  
        var accManager = new Customers.CustomerAccounts(customer1);
        accManager.ShowAccounts();

      
        Console.WriteLine($"Total Balance for {customer1.FullName}: {accManager.TotalBalance():C}");

       
        savings1.ShowTransactions();
        current1.ShowTransactions();

       
        accManager.Update("Ali Rehman", "02-02-1990");

      
        accManager.Remove();
    }
}
