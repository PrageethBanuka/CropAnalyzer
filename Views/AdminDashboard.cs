using Spectre.Console;

public static class AdminDashboard
{
    public static void Show()
    {
        while (true)
        {
            Console.Clear();

            AnsiConsole.Write(
                new FigletText("Admin Dashboard")
                    .Centered()
                    .Color(Color.Green));

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold cyan]Select an option:[/]")
                    .AddChoices("View Farmers", "View Crops", "View Regions", "View Harvests", "Analyse Harvests", "Exit"));

            switch (choice)
            {
                case "View Farmers":
                    FarmerView.Show();  // View farmers
                    break;
                case "View Crops":
                    CropView.Show();  // View crops
                    break;
                case "View Regions":
                    RegionView.Show();  // View regions
                    break;
                case "View Harvests":
                    HarvestView.Show();  // View harvests
                    break;
                case "Analyse Harvests":
                    AnalyseView.Show();  // View alerts
                    break;
                case "Exit":
                    MainMenu.Show(); // Exit
                    return;
            }
        }
    }
}