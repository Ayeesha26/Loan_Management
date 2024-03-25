using System;

public class Customer
{
    // Properties
    public int CustomerID { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public int CreditScore { get; set; }

    // Default Constructor
    public Customer()
    {
        CustomerID = 0;
        Name = string.Empty;
        EmailAddress = string.Empty;
        PhoneNumber = string.Empty;
        Address = string.Empty;
        CreditScore = 0;
    }

    // Parameterized Constructor
    public Customer(int customerID, string name, string emailAddress, string phoneNumber, string address, int creditScore)
    {
        CustomerID = customerID;
        Name = name;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        Address = address;
        CreditScore = creditScore;
    }

    // Method to print customer information
    public void PrintCustomerInfo()
    {
        Console.WriteLine($"Customer ID: {CustomerID}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Email Address: {EmailAddress}");
        Console.WriteLine($"Phone Number: {PhoneNumber}");
        Console.WriteLine($"Address: {Address}");
        Console.WriteLine($"Credit Score: {CreditScore}");
    }
}
