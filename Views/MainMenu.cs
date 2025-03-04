using Spectre.Console;

public static class MainMenu
{
    public static void Show()
    {
        LogoAnimation(); // Show the animated logo first

        while (true)
        {
            Console.Clear();

            AnsiConsole.Write(
                new FigletText("Harvest Analyzer")
                    .Centered()
                    .Color(Color.Green));

            AnsiConsole.MarkupLine("[bold yellow]Welcome to Crop Harvest Analyzer 🌾[/]\n");

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold cyan]Select the type of login:[/]")
                    .AddChoices("Admin Login", "Farmer Login", "Exit"));

            switch (choice)
            {
                case "Admin Login":
                    bool isAdmin = AdminLogin.Login();
                    if (isAdmin)
                    {
                        AdminDashboard.Show();
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[bold red]❌ Invalid Admin credentials.[/]");
                    }
                    break;

                case "Farmer Login":
                    int isFarmer = FarmerLogin.Login();
                    if (isFarmer >= 0)
                    {
                        FarmerDashboard.Show(isFarmer);
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[bold red]❌ Invalid Farmer credentials.[/]");
                    }
                    break;

                case "Exit":
                    Console.Clear();
                    AnsiConsole.MarkupLine("[bold green]🧑🏼‍🌾 Happy Farming![/]");
                    AnsiConsole.Write(
                        new FigletText("GoodBye!")
                            .Centered()
                            .Color(Color.Red));
                    return;
            }
        }
    }

    public static void LogoAnimation()
    {
        Console.Clear();
        string[] loadingTexts = {
            "[red]Coffee Runs Through My Circuits ☕️[/]",
            "[green]Harvest Analyzer...[/]",
            "[yellow]Harvesting Insights...[/]",
            "[cyan]Growing Database...[/]",
            "[blue]Analysing Crops...[/]",
            "[green]Farming Success...[/]",
            "[yellow]Almost There...[/]"
        };

        AnsiConsole.MarkupLine("[bold green]Starting System...[/]");
        Thread.Sleep(200);

        for (int i = 0; i < 3; i++)
        {
            foreach (var text in loadingTexts)
            {
                Console.Clear();

                AnsiConsole.Write(
                    new FigletText("Harvest Analyzer")
                        .Centered()
                        .Color(Color.Green));

                AnsiConsole.MarkupLine(text);
                Thread.Sleep(400);
            }
        }

        Console.Clear();

        AnsiConsole.Write(
            new FigletText("Harvest Analyzer")
                .Centered()
                .Color(Color.Green));

        AnsiConsole.MarkupLine("[bold green]System Ready! ✅[/]");
        Thread.Sleep(1500);
    }
}