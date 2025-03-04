using Spectre.Console;

public static class AdminDashboard
{
    public static void Show()
    {
        while (true)
        {
            Console.Clear();

            // Title without Padder
            AnsiConsole.Write(
                new FigletText("Admin Dashboard")
                    .Centered()
                    .Color(Color.Green));

            AnsiConsole.Write(
                new Padder(
                    new Rule("[yellow]Harvest Highlights 🔥[/]").Centered(),
                    new Padding(0, 0, 0, 1)));
                    

            DisplayTopFarmer();
            DisplayTopCrop();
            DisplayTopRegion();

            // Menu Options without Padder
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold cyan]Select an option:[/]")
                    .AddChoices("View Farmers", "View Crops", "View Regions", "View Harvests", "Analyse Harvests", "Exit"));

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

                case "Analyse Harvests":
                    AnalyseView.Show();
                    break;

                case "Exit":
                    MainMenu.Show();
                    return;
            }
        }
    }

    private static void DisplayTopFarmer()
    {
        var harvestService = new HarvestService();
        var farmerHarvests = harvestService.GetHarvestsByFarmers().ToList();

        var topFarmer = farmerHarvests
            .OrderByDescending(h => h.Quantitykg)
            .FirstOrDefault();

        if (topFarmer != null)
        {
            AnsiConsole.Write(
                new Padder(
                    new Markup($"👤 [green]Top Farmer:[/] {topFarmer.Name} - {topFarmer.Quantitykg}kg"),
                    new Padding(2, 0, 2, 0)));
        }
        else
        {
            AnsiConsole.MarkupLine("[yellow]No Farmers Found ❌[/]");
        }
    }

    private static void DisplayTopCrop()
    {
        var harvestService = new HarvestService();
        var cropHarvests = harvestService.GetHarvestsByCrops().ToList();

        var topCrop = cropHarvests
            .OrderByDescending(h => h.Quantitykg)
            .FirstOrDefault();

        if (topCrop != null)
        {
            AnsiConsole.Write(
                new Padder(
                    new Markup($"🌾 [green]Top Crop:[/] {topCrop.CropName} - {topCrop.Quantitykg}kg"),
                    new Padding(2, 0, 2, 0)));
        }
        else
        {
            AnsiConsole.MarkupLine("[yellow]No Crops Found ❌[/]");
        }
    }

    private static void DisplayTopRegion()
    {
        var harvestService = new HarvestService();
        var regionHarvests = harvestService.GetHarvestsByRegions().ToList();

        var topRegion = regionHarvests
            .OrderByDescending(h => h.Quantitykg)
            .FirstOrDefault();

        if (topRegion != null)
        {
            AnsiConsole.Write(
                new Padder(
                    new Markup($"🌍 [green]Top Region:[/] {topRegion.RegionName} - {topRegion.Quantitykg}kg"),
                    new Padding(2, 0, 2, 1)));
        }
        else
        {
            AnsiConsole.MarkupLine("[yellow]No Regions Found ❌[/]");
        }
    }
}