using System;
using Microsoft.Data.Sqlite;
using Spectre.Console;

public static class FarmerLogin
{
    public static int Login()
    {
        Console.Clear();
        AnsiConsole.Write(
            new FigletText("Farmer Login")
                .Centered()
                .Color(Color.Green));

        AnsiConsole.MarkupLine("[bold yellow]🌿 Welcome Farmer! Let's Grow Together.[/]\n");

        string username = AnsiConsole.Prompt(
            new TextPrompt<string>("[bold cyan]👨‍🌾 Enter Farmer Username:[/]")
            .PromptStyle("green")
            .ValidationErrorMessage("[bold red]Username cannot be empty![/]")
            .Validate(x => !string.IsNullOrWhiteSpace(x)));

        string password = AnsiConsole.Prompt(
            new TextPrompt<string>("[bold cyan]🔑 Enter Farmer Password:[/]")
            .Secret()
            .PromptStyle("green")
            .ValidationErrorMessage("[bold red]Password cannot be empty![/]")
            .Validate(x => !string.IsNullOrWhiteSpace(x)));

        // Fake Loading Animation 🔄
        AnsiConsole.Status()
            .Start("Authenticating Farmer Credentials...", ctx =>
            {
                Thread.Sleep(1200); 
            });

        int farmerId = ValidateFarmerCredentials(username, password);

        if (farmerId != -1)
        {
            AnsiConsole.MarkupLine("[bold green]✅ Welcome Farmer {0}![/]", username);
            Thread.Sleep(1000);
            return farmerId;
        }
        else
        {
            AnsiConsole.MarkupLine("[bold red]❌ Incorrect Credentials, Try Again![/]");
            AnsiConsole.Write(new Rule("[bold yellow]⚠️ Access Denied[/]").Centered());
            Thread.Sleep(1000);
            return -1;
        }
    }

    private static int ValidateFarmerCredentials(string username, string password)
    {
        using (var connection = DatabaseHelper.GetConnection())
        {
            try
            {
                connection.Open();
                string query = "SELECT FarmerId FROM Farmers WHERE Name = @Username AND Password = @Password";

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    var result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[bold red]🚨 Error: {0}[/]", ex.Message);
                return -1;
            }
        }
    }
}