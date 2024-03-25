using System;

public class Loan
{
    // Properties
    public int LoanId { get; set; }
    public Customer Customer { get; set; }
    public decimal PrincipalAmount { get; set; }
    public decimal InterestRate { get; set; }
    public int LoanTerm { get; set; }
    public string LoanType { get; set; }
    public string LoanStatus { get; set; }

    // Default Constructor
    public Loan()
    {
        LoanId = 0;
        Customer = null; // Assuming Customer object will be set later
        PrincipalAmount = 0;
        InterestRate = 0;
        LoanTerm = 0;
        LoanType = string.Empty;
        LoanStatus = "Pending"; // Default status is pending
    }

    // Parameterized Constructor
    public Loan(int loanId, Customer customer, decimal principalAmount, decimal interestRate, int loanTerm, string loanType, string loanStatus)
    {
        LoanId = loanId;
        Customer = customer;
        PrincipalAmount = principalAmount;
        InterestRate = interestRate;
        LoanTerm = loanTerm;
        LoanType = loanType;
        LoanStatus = loanStatus;
    }

    // Method to print loan details
    public void PrintLoanDetails()
    {
        Console.WriteLine($"Loan ID: {LoanId}");
        Console.WriteLine($"Customer ID: {Customer.CustomerID}");
        Console.WriteLine($"Principal Amount: {PrincipalAmount}");
        Console.WriteLine($"Interest Rate: {InterestRate}");
        Console.WriteLine($"Loan Term: {LoanTerm}");
        Console.WriteLine($"Loan Type: {LoanType}");
        Console.WriteLine($"Loan Status: {LoanStatus}");
    }
}
