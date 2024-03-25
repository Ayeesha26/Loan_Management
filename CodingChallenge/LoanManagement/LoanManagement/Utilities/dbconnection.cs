using System;
using System.Data.SqlClient;

public class DBUtil
{
    // Connection string for the SQL Server database
    private const string ConnectionString = "Server=DESKTOP-RIQ1MAN;Database=LoanManagement;Integrated Security=true";

    // Method to establish a connection to the database and return Connection reference
    public SqlConnection GetDBConn()
    {
        try
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            Console.WriteLine("Database connection established successfully.");
            return connection;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error establishing database connection: {ex.Message}");
            throw;
        }
    }
}