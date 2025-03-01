using System;
using Microsoft.Data.Sqlite;
using Spectre.Console;

public static class FarmerLogin
{
    // Use the helper class for database connection
    public static int Login()
    {
        Console.Clear();
        AnsiConsole.Write(
            new FigletText("Farmer Login")
                .Centered()
                .Color(Color.Green));

        // Get username and password from user input
        string username = AnsiConsole.Prompt(new TextPrompt<string>("[bold cyan]Enter Farmer Username:[/]"));
        string password = AnsiConsole.Prompt(new TextPrompt<string>("[bold cyan]Enter Farmer Password:[/]").Secret());

        // Validate credentials from the database and get the farmer ID
        int farmerId = ValidateFarmerCredentials(username, password);
        
        if (farmerId != -1)  // If valid, farmerId will not be -1
        {
            AnsiConsole.MarkupLine("[bold green]Farmer login successful![/]");
            return farmerId;  // Return the farmerId after successful login
        }
        else
        {
            AnsiConsole.MarkupLine("[bold red]Invalid credentials. Try again![/]");
            return -1;  // Return -1 to indicate failed login
        }
    }

    // Method to validate farmer credentials from the database and return the farmer ID
    private static int ValidateFarmerCredentials(string username, string password)
    {
        // Get a connection to the database
        using (var connection = DatabaseHelper.GetConnection())
        {
            try
            {
                connection.Open();

                // Prepare the SQL query to check if the username and password match
                string query = "SELECT FarmerId FROM Farmers WHERE Name = @Username AND Password = @Password";

                // Execute the query
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = query;

                    // Bind the parameters to the query
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    // Execute the query and get the result
                    var result = cmd.ExecuteScalar();  // This will return the FarmerId if a match is found

                    if (result != null)
                    {
                        return Convert.ToInt32(result);  // Return the FarmerId of the matching farmer
                    }
                    else
                    {
                        return -1;  // No matching credentials found, return -1
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[bold red]Error: {0}[/]", ex.Message);
                return -1;  // Return -1 in case of error
            }
        }
    }
}