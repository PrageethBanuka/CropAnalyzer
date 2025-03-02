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

        var harvestService = new HarvestService();
        var farmerHarvests = harvestService.GetHarvestsByFarmers().ToList();

        // QuickSort by Quantity
        var quantityArray = farmerHarvests.Select(h => h.Quantitykg).ToArray();
        SortingAlgorithms.QuickSort(quantityArray, 0, quantityArray.Length - 1);

        // Create a table for displaying sorted results
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn("Farmer Name")
            .AddColumn("Total Harvests (kg)")
            .AddColumn("Latest Harvest Date");
        
        

        foreach (var harvest in farmerHarvests.OrderByDescending(h => h.Quantitykg))
        {
            table.AddRow(harvest.Name, harvest.Quantitykg.ToString(), harvest.Date.ToShortDateString());
        }

        AnsiConsole.Write(table);
        int i = 1;
        // Extract and display top 5 results
        var top5Farmers = farmerHarvests.OrderByDescending(h => h.Quantitykg).Take(5).ToList();
        AnsiConsole.Write(new Rule("[yellow]Top 5 Farmers[/]").LeftJustified());
        foreach (var harvest in top5Farmers)
        {
            AnsiConsole.MarkupLine($"[green]{harvest.Name}[/] - {harvest.Quantitykg}kg");
        }

        AnsiConsole.MarkupLine("[yellow]Press any key to go back to Analysis Options.[/]");
        Console.ReadKey();
        Show();
    }

    private static void InOrderDisplay(BSTNode<FarmerHarvest> node, Table table)
    {
        if (node != null)
        {
            InOrderDisplay(node.Left, table);
            table.AddRow(node.Data.Name, node.Data.Quantitykg.ToString(), node.Data.Date.ToShortDateString());
            InOrderDisplay(node.Right, table);
        }
    }

    // Display harvest analysis by crops
    private static void AnalyseByCrops()
    {
        Console.Clear();
        AnsiConsole.Write(new Rule("Harvests by Crops").RuleStyle("grey").LeftJustified());

        var harvestService = new HarvestService();
        var cropHarvests = harvestService.GetHarvestsByCrops().ToList();

        // QuickSort by Quantity
        var quantityArray = cropHarvests.Select(h => h.Quantitykg).ToArray();
        SortingAlgorithms.QuickSort(quantityArray, 0, quantityArray.Length - 1);

        // Create a table for displaying sorted results
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn("Crop Name")
            .AddColumn("Total Harvests (kg)")
            .AddColumn("Latest Harvest Date");

        foreach (var harvest in cropHarvests.OrderByDescending(h => h.Quantitykg))
        {
            table.AddRow(harvest.CropName, harvest.Quantitykg.ToString(), harvest.Date.ToShortDateString());
        }

        AnsiConsole.Write(table);

        // Extract and display top 5 results
        var top5Crops = cropHarvests.OrderByDescending(h => h.Quantitykg).Take(5).ToList();
        AnsiConsole.Write(new Rule("[yellow]Top 5 Crops[/]").LeftJustified());
        foreach (var harvest in top5Crops)
        {
            AnsiConsole.MarkupLine($"[green]{harvest.CropName}[/] - {harvest.Quantitykg}kg");
        }

        AnsiConsole.MarkupLine("[yellow]Press any key to go back to Analysis Options.[/]");
        Console.ReadKey();
        Show();
    }
    private static void InOrderDisplay(BSTNode<CropHarvest> node, Table table)
    {
        if (node != null)
        {
            InOrderDisplay(node.Left, table);
            table.AddRow(node.Data.CropName, node.Data.Quantitykg.ToString(), node.Data.Date.ToShortDateString());
            InOrderDisplay(node.Right, table);
        }
    }

    // Display harvest analysis by regions
    private static void AnalyseByRegions()
    {
        Console.Clear();
        AnsiConsole.Write(new Rule("Harvests by Regions").RuleStyle("grey").LeftJustified());

        var harvestService = new HarvestService();
        var regionHarvests = harvestService.GetHarvestsByRegions().ToList();

        // QuickSort by Quantity
        var quantityArray = regionHarvests.Select(h => h.Quantitykg).ToArray();
        SortingAlgorithms.QuickSort(quantityArray, 0, quantityArray.Length - 1);

        // Create a table for displaying sorted results
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn("Region Name")
            .AddColumn("Total Harvests (kg)")
            .AddColumn("Latest Harvest Date");

        foreach (var harvest in regionHarvests.OrderByDescending(h => h.Quantitykg))
        {
            table.AddRow(harvest.RegionName, harvest.Quantitykg.ToString(), harvest.Date.ToShortDateString());
        }

        AnsiConsole.Write(table);

        // Extract and display top 5 results
        var top5Regions = regionHarvests.OrderByDescending(h => h.Quantitykg).Take(5).ToList();
        AnsiConsole.Write(new Rule("[yellow]Top 5 Regions[/]").LeftJustified());
        foreach (var harvest in top5Regions)
        {
            AnsiConsole.MarkupLine($"[green]{harvest.RegionName}[/] - {harvest.Quantitykg}kg");
        }

        AnsiConsole.MarkupLine("[yellow]Press any key to go back to Analysis Options.[/]");
        Console.ReadKey();
        Show();
    }
    private static void InOrderDisplay(BSTNode<RegionHarvest> node, Table table)
    {
        if (node != null)
        {
            InOrderDisplay(node.Left, table);
            table.AddRow(node.Data.RegionName, node.Data.Quantitykg.ToString(), node.Data.Date.ToShortDateString());
            InOrderDisplay(node.Right, table);
        }
    }
}