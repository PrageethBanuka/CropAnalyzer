using Spectre.Console;
using System;
using DSALibrary;

public static class HarvestView
{
    public static void Show()
    {
        var harvestService = new HarvestService(); // Initialize the service
        MyLinkedList<Harvest> harvestList = harvestService.GetAllHarvests();


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
                    .AddChoices("View Harvests", "Add Harvest", "Delete Harvest", "Back"));

            switch (choice)
            {
                case "View Harvests":
                    DisplayHarvests(harvestService, harvestList);  // Call to display harvests
                    AnsiConsole.MarkupLine("[yellow]Press any key to return...[/]");
                    Console.ReadKey();
                    break;
                case "Add Harvest":
                    DisplayHarvests(harvestService, harvestList);
                    AddHarvest(harvestService, harvestList);  // Call to add a harvest
                    break;
                case "Delete Harvest":
                    DisplayHarvests(harvestService, harvestList);
                    DeleteHarvest(harvestService, harvestList);  // Call to delete a harvest
                    break;
                case "Back":
                    return;  // Exit the loop and return to the main program
            }
        }
    }

    static void DisplayHarvests(HarvestService harvestService, MyLinkedList<Harvest> harvestList)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[yellow]Harvest List[/]").RuleStyle("grey").LeftJustified());

        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn(new TableColumn("Harvest ID").Centered())
            .AddColumn(new TableColumn("Farmer ID").Centered())
            .AddColumn(new TableColumn("Crop ID").Centered())
            .AddColumn(new TableColumn("Date").Centered())
            .AddColumn(new TableColumn("Quantity (kg)").Centered())
            .AddColumn(new TableColumn("Quality Rating").Centered());

        var currentNode = harvestList.GetHead();

        while (currentNode != null)
        {
            var harvest = currentNode.Data;
            table.AddRow(
                harvest.HarvestId.ToString(),
                harvest.FarmerId.ToString(),
                harvest.CropId.ToString(),
                harvest.Date,
                harvest.Quantitykg.ToString(),
                harvest.QualityRating
            );

            currentNode = currentNode.Next;
        }

        AnsiConsole.Write(table);

    }

    static void AddHarvest(HarvestService harvestService, MyLinkedList<Harvest> harvestList)
    {
        // Prompt user to enter harvest details
        var farmerId = AnsiConsole.Prompt(new TextPrompt<int>("Enter Farmer ID:"));
        var cropId = AnsiConsole.Prompt(new TextPrompt<int>("Enter Crop ID:"));
        var date = AnsiConsole.Prompt(new TextPrompt<string>("Enter Harvest Date:"));
        var quantitykg = AnsiConsole.Prompt(new TextPrompt<double>("Enter Quantity (kg):"));
        var qualityRating = AnsiConsole.Prompt(new TextPrompt<string>("Enter Quality Rating:"));

        // Create a new Harvest object
        var newHarvest = new Harvest(0, farmerId, cropId, date, quantitykg, qualityRating);

        // Add the harvest to the list and the database
        harvestList.Add(newHarvest);
        harvestService.Add(newHarvest);  // Assuming this saves the harvest to the database

        AnsiConsole.MarkupLine("[green]Harvest added successfully![/]");
        Console.ReadKey();
    }

    static void DeleteHarvest(HarvestService harvestService, MyLinkedList<Harvest> harvestList)
    {
        // Prompt user to enter the Harvest ID to delete
        var harvestId = AnsiConsole.Prompt(new TextPrompt<int>("Enter Harvest ID to delete:"));

        // Find and remove the harvest from the linked list
        var currentNode = harvestList.GetHead();
        while (currentNode != null)
        {
            if (currentNode.Data.HarvestId == harvestId)
            {
                harvestList.Delete(currentNode.Data);  // Delete from linked list
                harvestService.Delete(harvestId);      // Delete from database
                AnsiConsole.MarkupLine("[red]Harvest deleted successfully![/]");
                return;
            }
            currentNode = currentNode.Next;
        }

        AnsiConsole.MarkupLine("[red]Harvest not found![/]");
        Console.ReadKey();
    }
}