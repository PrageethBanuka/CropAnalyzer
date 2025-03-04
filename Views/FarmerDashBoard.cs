using Spectre.Console;

public static class FarmerDashboard
{
    public static void Show(int farmerId) // Accept farmerId as a parameter
    {
        while (true)
        {
            Console.Clear();

            AnsiConsole.Write(
                new FigletText("Farmer Dashboard")
                    .Centered()
                    .Color(Color.Green));

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold cyan]Select an option:[/]")
                    .AddChoices("View My Harvests", "View My Crops", "Exit"));

            switch (choice)
            {
                case "View My Harvests":
                    FarmerHarvestView.Show(farmerId);  // Pass the farmerId to view harvests
                    break;
                case "View My Crops":
                    FarmerCropView.Show(farmerId);  // Pass the farmerId to view crops (assuming it's implemented)
                    break;
                case "Exit":
                    MainMenu.Show(); // Exit
                    return;
            }
        }
    }
}