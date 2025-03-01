using System;
using Spectre.Console;
using DSALibrary;  // Ensure this namespace is included for HarvestService and related classes

public static class AnalyseView
{
    // Method to show the analysis view
    public static void Show()
    {
        Console.Clear();

        AnsiConsole.Write(
            new FigletText("Analysis View")
                .Centered()
                .Color(Color.Green));

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold cyan]Select an analysis category:[/]")
                .AddChoices("By Farmers", "By Crops", "By Regions", "Back"));

        switch (choice)
        {
            case "By Farmers":
                AnalyseByFarmers();  // Show analysis by farmers
                break;
            case "By Crops":
                AnalyseByCrops();    // Show analysis by crops
                break;
            case "By Regions":
                AnalyseByRegions();  // Show analysis by regions
                break;
            case "Back":
                AdminDashboard.Show();  // Go back to Admin Dashboard
                break;
        }
    }

    // Display harvest analysis by farmers
    private static void AnalyseByFarmers()
    {
        Console.Clear();
        AnsiConsole.Write(new Rule("Harvests by Farmers").RuleStyle("grey").LeftJustified());

        // Instantiate HarvestService and fetch data
        var harvestService = new HarvestService();
        var farmerHarvests = harvestService.GetHarvestsByFarmers();

        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn("Farmer Name")
            .AddColumn("Total Harvests (kg)");

        var node = farmerHarvests.GetHead();
        while (node != null)
        {
            var farmer = node.Data;
            table.AddRow(farmer.Name, farmer.Quantitykg.ToString()); // Assuming `TotalHarvest` is the `Quantitykg` field
            node = node.Next;
        }

        AnsiConsole.Write(table);

        // Allow user to navigate back
        AnsiConsole.MarkupLine("[yellow]Press any key to go back to the analysis options.[/]");
        Console.ReadKey();
        Show();  // Return to analysis view
    }

    // Display harvest analysis by crops
    private static void AnalyseByCrops()
    {
        Console.Clear();
        AnsiConsole.Write(new Rule("Harvests by Crops").RuleStyle("grey").LeftJustified());

        // Instantiate HarvestService and fetch data
        var harvestService = new HarvestService();
        var cropHarvests = harvestService.GetHarvestsByCrops();

        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn("Crop Name")
            .AddColumn("Total Harvests (kg)");

        var node = cropHarvests.GetHead();
        while (node != null)
        {
            var crop = node.Data;
            table.AddRow(crop.CropName, crop.Quantitykg.ToString()); // Assuming `TotalHarvest` is the `Quantitykg` field
            node = node.Next;
        }

        AnsiConsole.Write(table);

        // Allow user to navigate back
        AnsiConsole.MarkupLine("[yellow]Press any key to go back to the analysis options.[/]");
        Console.ReadKey();
        Show();  // Return to analysis view
    }

    // Display harvest analysis by regions
    private static void AnalyseByRegions()
    {
        Console.Clear();
        AnsiConsole.Write(new Rule("Harvests by Regions").RuleStyle("grey").LeftJustified());

        // Instantiate HarvestService and fetch data
        var harvestService = new HarvestService();
        var regionHarvests = harvestService.GetHarvestsByRegions();

        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn("Region")
            .AddColumn("Total Harvests (kg)");

        var node = regionHarvests.GetHead();
        while (node != null)
        {
            var region = node.Data;
            table.AddRow(region.RegionName, region.Quantitykg.ToString()); // Assuming `TotalHarvest` is the `Quantitykg` field
            node = node.Next;
        }

        AnsiConsole.Write(table);

        // Allow user to navigate back
        AnsiConsole.MarkupLine("[yellow]Press any key to go back to the analysis options.[/]");
        Console.ReadKey();
        Show();  // Return to analysis view
    }
}