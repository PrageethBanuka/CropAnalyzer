using Spectre.Console;
using System;
using System.Collections.Generic;

public static class RegionView
{
    public static void Show()
    {
        var regionService = new RegionService(); // Initialize the service

        while (true)
        {
            // Clear the console screen
            Console.Clear();

            // Display the title with Figlet style
            AnsiConsole.Write(
                new FigletText("Crop Analyzer")
                    .Centered()
                    .Color(Color.Green));

            // Prompt for user selection using a menu
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold cyan]Select an option:[/]")
                    .AddChoices("View Regions", "Back"));

            switch (choice)
            {
                case "View Regions":
                    DisplayRegions(regionService);  // Call to display regions
                    break;
                case "Back":
                    return;  // Exit the loop and return to main program
            }
        }
    }

    static void DisplayRegions(RegionService regionService)
    {
        // Clear the console before displaying new data
        AnsiConsole.Clear();

        // Display a rule with the title "Regions"
        AnsiConsole.Write(new Rule("[yellow]Regions[/]").RuleStyle("grey").LeftJustified());

        // Retrieve all regions from the region service
        var regions = regionService.LoadRegions(); // Assuming LoadRegions returns MyLinkedList<Region>

        // Create the table for displaying regions
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn(new TableColumn("Region ID").Centered())
            .AddColumn(new TableColumn("Region").Centered());

        // Get the first node of the linked list
        var currentNode = regions.GetHead();

        // Use a while loop to iterate through the MyLinkedList<Region>
        while (currentNode != null)
        {
            var region = currentNode.Data;  // Access the Data (Region) of the current node
            table.AddRow(region.RegionID.ToString(), region.RegionName);  // Add row for each region
            currentNode = currentNode.Next;  // Move to the next node
        }

        // Display the table on the console
        AnsiConsole.Write(table);

        // Prompt for user to press a key to return to the main menu
        AnsiConsole.MarkupLine("[yellow]Press any key to return...[/]");
        Console.ReadKey();
    }
}