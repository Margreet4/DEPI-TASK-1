using System;

public class Account
{
    public const string BankCode = "BNK001";
    public readonly DateTime CreatedDate;

    private int _accountNumber;
    private string _fullName;
    private string _nationalID;
    private string _phoneNumber;
    private string _address;
    private decimal _balance;

    // Properties with validation
    public string FullName
    {
        get { return _fullName; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Full name must not be empty");
            _fullName = value;
        }
    }

    public string NationalID
    {
        get { return _nationalID; }
        set
        {
            if (value == null || value.Length != 14)
                throw new ArgumentException("National ID must be 14 digits");
            _nationalID = value;
        }
    }

    public string PhoneNumber
    {
        get { return _phoneNumber; }
        set
        {
            if (value == null || value.Length != 11 || !value.StartsWith("01"))
                throw new ArgumentException("Phone number must start with '01' and be 11 digits");
            _phoneNumber = value;
        }
    }

    public string Address
    {
        get { return _address; }
        set { _address = value; }
    }

    public decimal Balance
    {
        get { return _balance; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Balance must be >= 0");
            _balance = value;
        }
    }

    // Default Constructor
    public Account()
    {
        CreatedDate = DateTime.Now;
        _accountNumber = 15;
        FullName = "Margo";
        NationalID = "30500285650000";
        PhoneNumber = "01203855000";
        Address = "Ismailia";
        Balance = 0;
    }

    // Parameterized Constructor
    public Account(int accountNumber, string fullName, string nationalID, string phoneNumber, string address, decimal balance)
    {
        CreatedDate = DateTime.Now;
        _accountNumber = accountNumber;
        FullName = fullName;
        NationalID = nationalID;
        PhoneNumber = phoneNumber;
        Address = address;
        Balance = balance;
    }

    // Overloaded Constructor
    public Account(string fullName, string phoneNumber, decimal balance, string address)
    {
        CreatedDate = DateTime.Now;
        _accountNumber = 0;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Balance = balance;
        Address = address;
        NationalID = "Not Assigned";
    }

    // Show account details
    public void ShowAccountDetails()
    {
        Console.WriteLine("All Account Details:");
        Console.WriteLine($"Bank Code     : {BankCode}");
        Console.WriteLine($"Account No    : {_accountNumber}");
        Console.WriteLine($"Full Name     : {FullName}");
        Console.WriteLine($"National ID   : {NationalID}");
        Console.WriteLine($"Phone Number  : {PhoneNumber}");
        Console.WriteLine($"Address       : {Address}");
        Console.WriteLine($"Balance       : {Balance}");
        Console.WriteLine($"Created Date  : {CreatedDate}");
        Console.WriteLine("------------------------------");
    }

    // Validation methods
    public bool IsValidNationalID()
    {
        return !string.IsNullOrEmpty(NationalID) && NationalID.Length == 14;
    }

    public bool IsValidPhoneNumber()
    {
        return !string.IsNullOrEmpty(PhoneNumber) && PhoneNumber.Length == 11 ;
    }
}

// Main program
class Program
{
    static void Main()
    {
        // Default constructor
        Account acc1 = new Account();
        acc1.ShowAccountDetails();

        // Parameterized constructor
        Account acc2 = new Account(2, "Margreet Emil", "30500285650001", "01234568901", "Ismailia", 5000m);
        acc2.ShowAccountDetails();

        // Validation Methods
        Console.WriteLine($"acc2 NationalID Valid? {acc2.IsValidNationalID()}");
        Console.WriteLine($"acc2 PhoneNumber Valid? {acc2.IsValidPhoneNumber()}");

        
    }
}
