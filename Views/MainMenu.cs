using Spectre.Console;

public static class MainMenu
{
    public static void Show()
    {
        while (true)
        {
            Console.Clear();

            AnsiConsole.Write(
                new FigletText("Crop Analyzer")
                    .Centered()
                    .Color(Color.Green));

            // Present the login options: Admin or Farmer
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold cyan]Select the type of login:[/]")
                    .AddChoices("Admin Login", "Farmer Login", "Exit"));

            switch (choice)
            {
                case "Admin Login":
                    bool isAdmin = AdminLogin.Login();  // Prompt for Admin login
                    if (isAdmin)
                    {
                        AdminDashboard.Show();  // Admin Dashboard after successful login
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[bold red]Invalid Admin credentials.[/]");
                    }
                    break;

                case "Farmer Login":
                    int isFarmer = FarmerLogin.Login();  // Prompt for Farmer login
                    if (isFarmer >= 0)
                    {
                        FarmerDashboard.Show(isFarmer);  // Farmer Dashboard after successful login
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[bold red]Invalid Farmer credentials.[/]");
                    }
                    break;

                case "Exit":
                    Console.Clear();
                    AnsiConsole.MarkupLine("[bold green]🧑🏼‍🌾 Happy Farming.[/]");
                    AnsiConsole.Write(
                        new FigletText("GoodBye!")
                            .Centered()
                            .Color(Color.Red));
                    return;  // Exit the program
            }
        }
    }
}