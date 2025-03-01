using Spectre.Console;
using System;

public static class CropView
{
    private static readonly CropService _cropService = new CropService();

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
                    .AddChoices("View Crops", "Add Crop", "Delete Crop", "Update Crop", "Back"));

            switch (choice)
            {
                case "View Crops":
                    ViewCrops();
                    // Prompt to return
                    AnsiConsole.MarkupLine("[yellow]Press any key to return...[/]");
                    Console.ReadKey();
                    break;
                case "Add Crop":
                    ViewCrops();
                    AddCrop();
                    break;
                case "Delete Crop":
                    ViewCrops();
                    DeleteCrop();
                    break;
                case "Update Crop":
                    ViewCrops();
                    UpdateCrop();
                    break;
                case "Back":
                    return;
            }
        }
    }

    private static void ViewCrops()
    {
        var crops = _cropService.GetAllCrops(); // Returns MyLinkedList<Crop>

        if (crops.GetHead() == null) // Check if the list is empty
        {
            AnsiConsole.MarkupLine("[red]No crops found![/]");
            return;
        }

        // Create a table with appropriate columns
        var table = new Table();

        // Define columns
        table.AddColumn("CropID", col => col.Centered().PadLeft(2));
        table.AddColumn("Crop Name", col => col.Centered().PadLeft(2));
        table.AddColumn("Category", col => col.Centered().PadLeft(2));
        table.AddColumn("Average Yield", col => col.Centered().PadLeft(2));
        table.AddColumn("Season", col => col.Centered().PadLeft(2));

        // Traverse the linked list and add rows to the table
        var currentNode = crops.GetHead();
        while (currentNode != null)
        {
            var crop = currentNode.Data; // Get crop from the node
            table.AddRow(crop.CropID.ToString(), crop.CropName, crop.CropCategory, crop.AverageYieldPerHectare.ToString(), crop.Season);
            currentNode = currentNode.Next; // Move to the next node
        }

        // Render the table to the console
        AnsiConsole.Render(table);


    }

    private static void AddCrop()
    {
        var cropName = AnsiConsole.Prompt(new TextPrompt<string>("Enter crop name:"));
        var cropCategory = AnsiConsole.Prompt(new TextPrompt<string>("Enter crop category:"));
        var yield = AnsiConsole.Prompt(new TextPrompt<double>("Enter average yield per hectare:"));
        var season = AnsiConsole.Prompt(new TextPrompt<string>("Enter season:"));

        var crop = new Crop(0, cropName, cropCategory, yield, season);
        _cropService.Add(crop);

        AnsiConsole.MarkupLine("[green]Crop added successfully![/]");
        AnsiConsole.MarkupLine("[yellow]Press any key to return...[/]");
        Console.ReadKey();
    }

    private static void DeleteCrop()
    {
        var crops = _cropService.GetAllCrops();
        if (crops.GetHead() == null) // Check if the list is empty
        {
            AnsiConsole.MarkupLine("[red]No crops available to delete![/]");
            return;
        }

        // Manually extract CropIDs from the linked list
        var cropIds = new List<int>();
        var currentNode = crops.GetHead();
        while (currentNode != null)
        {
            cropIds.Add(currentNode.Data.CropID); // Add CropID to the list
            currentNode = currentNode.Next; // Move to the next node
        }

        var cropId = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
                .Title("Select a crop to delete:")
                .AddChoices(cropIds.ToArray())); // Use the list of CropIDs

        _cropService.Delete(cropId);
        AnsiConsole.MarkupLine("[red]Crop deleted successfully![/]");
        AnsiConsole.MarkupLine("[yellow]Press any key to return...[/]");
        Console.ReadKey();
    }

    private static void UpdateCrop()
    {
        var crops = _cropService.GetAllCrops();
        if (crops.GetHead() == null) // Check if the list is empty
        {
            AnsiConsole.MarkupLine("[red]No crops available to update![/]");
            return;
        }

        // Manually extract CropIDs from the linked list
        var cropIds = new List<int>();
        var currentNode = crops.GetHead();
        while (currentNode != null)
        {
            cropIds.Add(currentNode.Data.CropID); // Add CropID to the list
            currentNode = currentNode.Next; // Move to the next node
        }

        var cropId = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
                .Title("Select a crop to update:")
                .AddChoices(cropIds.ToArray())); // Use the list of CropIDs

        var selectedCrop = _cropService.GetById(cropId);

        var cropName = AnsiConsole.Prompt(new TextPrompt<string>($"Enter new name for {selectedCrop.CropName}:"));
        var cropCategory = AnsiConsole.Prompt(new TextPrompt<string>($"Enter new category for {selectedCrop.CropCategory}:"));
        var yield = AnsiConsole.Prompt(new TextPrompt<double>($"Enter new average yield for {selectedCrop.CropName}:"));
        var season = AnsiConsole.Prompt(new TextPrompt<string>($"Enter new season for {selectedCrop.CropName}:"));

        selectedCrop.Update(cropName, cropCategory, yield, season);

        _cropService.Update(selectedCrop);

        AnsiConsole.MarkupLine("[green]Crop updated successfully![/]");
        AnsiConsole.MarkupLine("[yellow]Press any key to return...[/]");
        Console.ReadKey();
    }
}