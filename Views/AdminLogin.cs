using System;
using Microsoft.Data.Sqlite;
using Spectre.Console;

public static class AdminLogin
{
    // Use the helper class for database connection
    public static bool Login()
    {
        Console.Clear();
        AnsiConsole.Write(
            new FigletText("Admin Login")
                .Centered()
                .Color(Color.Green));

        // Get username and password from user input
        string username = AnsiConsole.Prompt(new TextPrompt<string>("[bold cyan]Enter Admin Username:[/]"));
        string password = AnsiConsole.Prompt(new TextPrompt<string>("[bold cyan]Enter Admin Password:[/]").Secret());

        // Validate credentials from the database
        if (ValidateAdminCredentials(username, password))
        {
            AnsiConsole.MarkupLine("[bold green]Admin login successful![/]");
            return true;
        }
        else
        {
            AnsiConsole.MarkupLine("[bold red]Invalid credentials. Try again![/]");
            return false;
        }
    }

    // Method to validate admin credentials from the database
    private static bool ValidateAdminCredentials(string username, string password)
    {
        // Get a connection to the database
        using (var connection = DatabaseHelper.GetConnection())
        {
            try
            {
                connection.Open();

                // Prepare the SQL query to check if the username and password match
                string query = "SELECT COUNT(*) FROM Admins WHERE Username = @Username AND Password = @Password";

                // Execute the query
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = query;

                    // Bind the parameters to the query
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    // Execute the query and get the result
                    long count = (long)cmd.ExecuteScalar();  // This will return the number of matching rows

                    if (count > 0)
                    {
                        return true;  // Admin found with matching credentials
                    }
                    else
                    {
                        return false;  // No matching credentials found
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[bold red]Error: {0}[/]", ex.Message);
                return false;
            }
        }
    }
}