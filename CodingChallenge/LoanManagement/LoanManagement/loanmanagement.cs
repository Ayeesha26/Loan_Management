using System;
using System.Collections.Generic;
using System.Data.SqlClient;

class NewLoanManagement
    {
    static void Main(string[] args)
        {
            ILoanRepositoryImpl loanRepositoryImpl = new ILoanRepositoryImpl();

            void ApplyLoan(Loan loan)
            {
                Console.WriteLine("Applying for loan...");
                string connectionString = "Server=DESKTOP-RIQ1MAN;Database=LoanManagement;Integrated Security=true";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Loans (LoanId, CustomerId, PrincipalAmount, InterestRate, LoanTerm, LoanStatus) " +
                                   "VALUES (@LoanId, @CustomerId, @PrincipalAmount, @InterestRate, @LoanTerm, @LoanStatus)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@LoanId", loan.LoanId);
                    command.Parameters.AddWithValue("@CustomerId", loan.Customer.CustomerID);
                    command.Parameters.AddWithValue("@PrincipalAmount", loan.PrincipalAmount);
                    command.Parameters.AddWithValue("@InterestRate", loan.InterestRate);
                    command.Parameters.AddWithValue("@LoanTerm", loan.LoanTerm);
                    command.Parameters.AddWithValue("@LoanStatus", loan.LoanStatus);

                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Loan applied successfully.");
                }
            }

            decimal CalculateInterest(int loanId)
            {
                Console.WriteLine("Calculating interest...");
                string connectionString = "Server=DESKTOP-RIQ1MAN;Database=LoanManagement;Integrated Security=true";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // SQL query to fetch the loan details based on loanId
                        string query = "SELECT PrincipalAmount, InterestRate, LoanTerm FROM Loans WHERE LoanId = @LoanId";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@LoanId", loanId);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            // Retrieve loan details from the database
                            decimal principalAmount = reader.GetDecimal(0);
                            decimal interestRate = reader.GetDecimal(1);
                            int loanTerm = reader.GetInt32(2);

                            // Calculate interest
                            decimal interest = (principalAmount * interestRate * loanTerm) / 12;

                            // Close the reader
                            reader.Close();

                            // Return the calculated interest
                            return interest;
                        }
                        else
                        {
                            // If no loan found with the given loanId, throw an exception or return a default value
                            throw new Exception("Loan not found.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions
                    Console.WriteLine($"Error in calculating interest");
                    // You might want to throw the exception further to handle it in the calling code
                    throw;
                }
            }

            decimal CalculateInterestWithParams(decimal principalAmount, decimal interestRate, int loanTerm)
            {
                string connectionString = "Server=DESKTOP-RIQ1MAN;Database=LoanManagement;Integrated Security=true";
                // Implementation to calculate interest for a loan by providing parameters
                Console.WriteLine("Calculating interest for loan with parameters...");
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // SQL query to fetch the interest rate from the database based on loan parameters
                        string query = "SELECT InterestRate FROM InterestRates WHERE PrincipalAmount = @PrincipalAmount " +
                                       "AND LoanTerm = @LoanTerm";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@PrincipalAmount", principalAmount);
                        command.Parameters.AddWithValue("@LoanTerm", loanTerm);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            // Retrieve interest rate from the database
                            decimal dbInterestRate = reader.GetDecimal(0);

                            // Close the reader
                            reader.Close();

                            // Calculate interest using the fetched interest rate
                            decimal interest = (principalAmount * dbInterestRate * loanTerm) / 12;

                            // Return the calculated interest
                            return interest;
                        }
                        else
                        {
                            // If no interest rate found for the given loan parameters, throw an exception or return a default value
                            throw new Exception("Interest rate not found for the given loan parameters.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions
                    Console.WriteLine($"Error in calculating interest");
                    // You might want to throw the exception further to handle it in the calling code
                    throw;
                }
            }

            void LoanStatus(int loanId)
            {
                Console.WriteLine("Updating loan status...");
                string connectionString = "Server=DESKTOP-RIQ1MAN;Database=LoanManagement;Integrated Security=true";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // SQL query to update the loan status in the database
                        string query = "UPDATE Loans SET LoanStatus = @LoanStatus WHERE LoanId = @LoanId";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@LoanId", loanId);
                        // Set the new loan status, for example, "Approved" or "Rejected"
                        command.Parameters.AddWithValue("@LoanStatus", "Approved");

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Loan status updated successfully
                            Console.WriteLine("Loan status updated successfully.");
                        }
                        else
                        {
                            // No rows affected, loan not found or status already updated
                            Console.WriteLine("Loan not found or status already updated.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions
                    Console.WriteLine($"Error updating loan status: {ex.Message}");
                    // You might want to throw the exception further to handle it in the calling code
                    throw;
                }
            }

            decimal CalculateEMI(int loanId)
            {

                Console.WriteLine("Calculating EMI...");
                string connectionString = "Server=DESKTOP-RIQ1MAN;Database=LoanManagement;Integrated Security=true";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // SQL query to fetch loan details from the database
                        string query = "SELECT PrincipalAmount, InterestRate, LoanTerm FROM Loans WHERE LoanId = @LoanId";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@LoanId", loanId);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            // Retrieve loan details from the database
                            decimal principalAmount = reader.GetDecimal(0);
                            decimal interestRate = reader.GetDecimal(1);
                            int loanTerm = reader.GetInt32(2);

                            // Close the reader
                            reader.Close();

                            // Calculate EMI using the loan details
                            decimal emi = CalculateEMIWithParams(principalAmount, interestRate, loanTerm);
                            Console.WriteLine($"EMI calculated: {emi}");
                            return emi;
                        }
                        else
                        {
                            // Loan not found
                            Console.WriteLine("Loan not found.");
                            return 0.0m;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions
                    Console.WriteLine($"Error calculating EMI: {ex.Message}");
                    // You might want to throw the exception further to handle it in the calling code
                    throw;
                }
            }

            decimal CalculateEMIWithParams(decimal principalAmount, decimal interestRate, int loanTerm)
            {
                // Implementation to calculate EMI for a loan by providing parameters
                Console.WriteLine("Calculating EMI for loan with parameters...");
                decimal monthlyInterestRate = interestRate / 12 / 100;
                decimal emi = (principalAmount * monthlyInterestRate * (decimal)Math.Pow(1 + (double)monthlyInterestRate, loanTerm)) /
                              ((decimal)Math.Pow(1 + (double)monthlyInterestRate, loanTerm) - 1);
                return emi;
            }

            void LoanRepayment(int loanId, decimal amount)
            {
                string connectionString = "Server=DESKTOP-RIQ1MAN;Database=LoanManagement;Integrated Security=true";
                Console.WriteLine($"Repaying loan with ID: {loanId}. Amount: {amount}");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Loans SET RepaymentAmount = RepaymentAmount + @Amount WHERE LoanId = @LoanId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@LoanId", loanId);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception($"Loan with ID {loanId} not found.");
                    }
                }
            }

            List<Loan> GetAllLoan()
            {

                Console.WriteLine("Retrieving all loans...");
                List<Loan> loans = new List<Loan>();
                string connectionString = "Server=DESKTOP-RIQ1MAN;Database=LoanManagement;Integrated Security=true";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Loans";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // Create Loan objects from the data retrieved from the database
                        Loan loan = new Loan
                        {
                            // Populate properties from the database columns
                            LoanId = Convert.ToInt32(reader["LoanId"]),
                            // Populate other properties accordingly
                        };
                        loans.Add(loan);
                    }
                }

                return loans;
            }

            Loan GetLoanById(int loanId)
            {
                Console.WriteLine($"Retrieving loan with ID: {loanId}");
                Loan loan = null;
                string connectionString = "Server=DESKTOP-RIQ1MAN;Database=LoanManagement;Integrated Security=true";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Loans WHERE LoanId = @LoanId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@LoanId", loanId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        // Create a Loan object from the data retrieved from the database
                        loan = new Loan
                        {
                            // Populate properties from the database columns
                            LoanId = Convert.ToInt32(reader["LoanId"]),
                            // Populate other properties accordingly
                        };
                    }
                }

                return loan;
            }
        }
}