using System;
using Microsoft.Data.Sqlite;
using Spectre.Console;

public static class AdminLogin
{
    public static bool Login()
    {
        Console.Clear();
        AnsiConsole.Write(
            new FigletText("Admin Login")
                .Centered()
                .Color(Color.Green));

        AnsiConsole.MarkupLine("[bold yellow]Welcome back Admin! 🔐[/]\n");

        string username = AnsiConsole.Prompt(
            new TextPrompt<string>("[bold cyan]👤 Enter Admin Username:[/]")
            .PromptStyle("green")
            .ValidationErrorMessage("[bold red]Username cannot be empty![/]")
            .Validate(x => !string.IsNullOrWhiteSpace(x)));

        string password = AnsiConsole.Prompt(
            new TextPrompt<string>("[bold cyan]🔑 Enter Admin Password:[/]")
            .Secret()
            .PromptStyle("green")
            .ValidationErrorMessage("[bold red]Password cannot be empty![/]")
            .Validate(x => !string.IsNullOrWhiteSpace(x)));

        // Fake loading effect 🔄
        AnsiConsole.Status()
            .Start("Verifying Admin Credentials...", ctx =>
            {
                Thread.Sleep(1500); // Wait effect
            });

        if (ValidateAdminCredentials(username, password))
        {
            AnsiConsole.MarkupLine("[bold green]✅ Login Successful! Welcome Admin.[/]");
            Thread.Sleep(1000);
            return true;
        }
        else
        {
            AnsiConsole.MarkupLine("[bold red]❌ Invalid credentials. Please Try Again![/]");
            AnsiConsole.Write(new Rule("[bold yellow]⚠️ Attempt Failed[/]").Centered());
            Thread.Sleep(1000);
            return false;
        }
    }

    private static bool ValidateAdminCredentials(string username, string password)
    {
        using (var connection = DatabaseHelper.GetConnection())
        {
            try
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Admins WHERE Username = @Username AND Password = @Password";

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    long count = (long)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[bold red]🚨 Error: {0}[/]", ex.Message);
                return false;
            }
        }
    }
}