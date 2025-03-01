using Spectre.Console;

public static class FarmerHarvestView
{
    public static void Show(int farmerId)
    {
        Console.Clear();

        AnsiConsole.Write(
            new FigletText("Farmer Harvests")
                .Centered()
                .Color(Color.Green));

        // Use the HarvestService to fetch harvests
        var harvestService = new HarvestService();
        var harvests = harvestService.GetHarvestsByFarmerId(farmerId);

        // Display harvests in a table
        AnsiConsole.Write(new Rule("[yellow]Farmer's Harvests[/]").RuleStyle("grey").LeftJustified());

        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn(new TableColumn("Harvest ID").Centered())
            .AddColumn(new TableColumn("Date").Centered())
            .AddColumn(new TableColumn("Quantity (kg)").Centered())
            .AddColumn(new TableColumn("Quality Rating").Centered());

        // Use a while loop to iterate through the MyLinkedList<Harvest>
        var currentNode = harvests.GetHead(); // Start from the head of the list
        while (currentNode != null)
        {
            var harvest = currentNode.Data;  // Get the harvest data from the current node
            table.AddRow(harvest.HarvestId.ToString(), harvest.Date, harvest.Quantitykg.ToString(), harvest.QualityRating);
            currentNode = currentNode.Next;  // Move to the next node
        }

        AnsiConsole.Write(table);

        // Prompt to go back to dashboard or view crops
        AnsiConsole.MarkupLine("[yellow]Press 'c' to view Crops, 'b' to go back to Dashboard.[/]");
        var input = Console.ReadKey().KeyChar;

        if (input == 'c')
        {
            FarmerCropView.Show(farmerId);  // Navigate to crop view
        }
        else if (input == 'b')
        {
            FarmerDashboard.Show(farmerId);  // Go back to the dashboard
        }
    }
}