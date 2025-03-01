using Spectre.Console;

public static class AlertView
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

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold cyan]Select an option:[/]")
                    .AddChoices("View Farmers", "View Crops", "View Regions", "View Harvests", "View Alerts", "Exit"));

            switch (choice)
            {
                case "View Farmers":
                    FarmerView.Show();
                    break;
                case "View Crops":
                    CropView.Show();
                    break;
                case "View Regions":
                    RegionView.Show();
                    break;
                case "View Harvests":
                    HarvestView.Show();
                    break;
                case "View Alerts":
                    AlertView.Show();
                    break;
                case "Exit":
                    return;
            }
        }
    }
}