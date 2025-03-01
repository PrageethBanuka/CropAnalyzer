using DSALibrary; // Assuming DSALibrary contains MyLinkedList, Farmer, and FarmerService
using Spectre.Console;
using System;
using System.Collections.Generic;

public static class FarmerView
{
    private static FarmerService _farmerService = new FarmerService();  // Assuming you have a FarmerService that connects to the database

    public static void Show()
    {
        while (true)
        {
            Console.Clear();
            AnsiConsole.Write(
                new FigletText("Farmer List")
                    .Centered()
                    .Color(Color.Green));

            // Fetch the list of farmers from the database or service
            var farmers = _farmerService.GetAll(); // Assuming you have GetAll() method in FarmerService
            if (farmers.GetHead() == null)  // Check if MyLinkedList is empty
            {
                AnsiConsole.MarkupLine("[red]No farmers found![/]");
                AnsiConsole.MarkupLine("[yellow]Press any key to return...[/]");
                Console.ReadKey();
                return;
            }

            // Display farmer information in a table
            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Name");
            table.AddColumn("Region");
            table.AddColumn("Contact");

            var currentNode = farmers.GetHead();
            while (currentNode != null)
            {
                var farmer = currentNode.Data; // Get farmer from the node
                table.AddRow(farmer.Id.ToString(), farmer.Name, farmer.RegionId.ToString(), farmer.ContactNumber);
                currentNode = currentNode.Next;  // Move to the next node
            }

            AnsiConsole.Render(table);

            // Let user select an action
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold cyan]Select an action:[/]")
                    .AddChoices("View Farmer", "Edit Farmer", "Delete Farmer", "Back"));

            switch (choice)
            {
                case "View Farmer":
                    ViewFarmer(farmers);  // Method to view details of a selected farmer
                    break;
                case "Edit Farmer":
                    EditFarmer(farmers);  // Method to edit selected farmer's details
                    break;
                case "Delete Farmer":
                    DeleteFarmer(farmers);  // Method to delete a selected farmer
                    break;
                case "Back":
                    return;  // Go back to the previous menu
            }
        }
    }

    private static void ViewFarmer(MyLinkedList<Farmer> farmers)
    {
        // Prompt user to select a farmer to view details
        var farmerIds = new List<int>();
        var currentNode = farmers.GetHead();
        while (currentNode != null)
        {
            farmerIds.Add(currentNode.Data.Id); // Collect all farmer IDs
            currentNode = currentNode.Next; // Move to the next node
        }

        var farmerId = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
                .Title("Select a farmer to view details:")
                .AddChoices(farmerIds.ToArray()));

        var selectedFarmer = _farmerService.GetById(farmerId);

        if (selectedFarmer != null)
        {
            AnsiConsole.MarkupLine($"[bold green]Farmer Details:[/]");
            AnsiConsole.MarkupLine($"[cyan]ID:[/] {selectedFarmer.Id}");
            AnsiConsole.MarkupLine($"[cyan]Name:[/] {selectedFarmer.Name}");
            AnsiConsole.MarkupLine($"[cyan]Region ID:[/] {selectedFarmer.RegionId}");
            AnsiConsole.MarkupLine($"[cyan]Contact Number:[/] {selectedFarmer.ContactNumber}");
            AnsiConsole.MarkupLine("[yellow]Press any key to return...[/]");
            Console.ReadKey();
        }
    }

    private static void EditFarmer(MyLinkedList<Farmer> farmers)
    {
        // Prompt user to select a farmer to edit
        var farmerIds = new List<int>();
        var currentNode = farmers.GetHead();
        while (currentNode != null)
        {
            farmerIds.Add(currentNode.Data.Id); // Collect all farmer IDs
            currentNode = currentNode.Next; // Move to the next node
        }

        var farmerId = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
                .Title("Select a farmer to edit:")
                .AddChoices(farmerIds.ToArray()));

        var selectedFarmer = _farmerService.GetById(farmerId);

        if (selectedFarmer != null)
        {
            var newName = AnsiConsole.Ask<string>("Enter new name:", selectedFarmer.Name);
            var newRegionId = AnsiConsole.Ask<int>("Enter new region ID:", selectedFarmer.RegionId);
            var newContactNumber = AnsiConsole.Ask<string>("Enter new contact number:", selectedFarmer.ContactNumber);

            selectedFarmer = new Farmer(
                selectedFarmer.Id,
                newName,
                newRegionId,
                newContactNumber,
                selectedFarmer.Password);

            _farmerService.Update(selectedFarmer);
            AnsiConsole.MarkupLine("[green]Farmer details updated successfully![/]");
            AnsiConsole.MarkupLine("[yellow]Press any key to return...[/]");
            Console.ReadKey();
        }
    }

    private static void DeleteFarmer(MyLinkedList<Farmer> farmers)
    {
        // Prompt user to select a farmer to delete
        var farmerIds = new List<int>();
        var currentNode = farmers.GetHead();
        while (currentNode != null)
        {
            farmerIds.Add(currentNode.Data.Id); // Collect all farmer IDs
            currentNode = currentNode.Next; // Move to the next node
        }

        var farmerId = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
                .Title("Select a farmer to delete:")
                .AddChoices(farmerIds.ToArray()));

        var selectedFarmer = _farmerService.GetById(farmerId);

        if (selectedFarmer != null)
        {
            // Ask for confirmation to delete the selected farmer
            var confirmation = AnsiConsole.Prompt(
                new TextPrompt<string>($"Are you sure you want to delete {selectedFarmer.Name}? (y/n)")
                .AllowEmpty());

            if (confirmation.ToLower() == "y")
            {
                _farmerService.Delete(farmerId);
                AnsiConsole.MarkupLine("[red]Farmer deleted successfully![/]");
                AnsiConsole.MarkupLine("[yellow]Press any key to return...[/]");
                Console.ReadKey();
            }
        }
    }
}