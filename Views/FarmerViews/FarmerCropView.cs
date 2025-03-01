using Spectre.Console;

public static class FarmerCropView
{
    public static void Show(int farmerId)
    {
        Console.Clear();

        AnsiConsole.Write(
            new FigletText("Farmer Crops")
                .Centered()
                .Color(Color.Green));

        // Use the CropService to fetch crops
        var cropService = new CropService();
        var crops = cropService.GetCropsByFarmerId(farmerId);

        // Display crops in a table
        AnsiConsole.Write(new Rule("[yellow]Farmer's Crops[/]").RuleStyle("grey").LeftJustified());

        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn(new TableColumn("Crop ID").Centered())
            .AddColumn(new TableColumn("Crop Name").Centered())
            .AddColumn(new TableColumn("Crop Category").Centered())
            .AddColumn(new TableColumn("Average Yield (kg/ha)").Centered())
            .AddColumn(new TableColumn("Season").Centered());

        // Use a while loop to iterate through the MyLinkedList<Crop>
        var currentNode = crops.GetHead(); // Start from the head of the list
        while (currentNode != null)
        {
            var crop = currentNode.Data;  // Get the crop data from the current node
            table.AddRow(crop.CropID.ToString(), crop.CropName, crop.CropCategory, crop.AverageYieldPerHectare.ToString(), crop.Season);
            currentNode = currentNode.Next;  // Move to the next node
        }

        AnsiConsole.Write(table);

        // Prompt to go back to dashboard or view harvests
        AnsiConsole.MarkupLine("[yellow]Press 'h' to view Harvests, 'b' to go back to Dashboard.[/]");
        var input = Console.ReadKey().KeyChar;

        if (input == 'h')
        {
            FarmerHarvestView.Show(farmerId);  // Navigate to harvest view
        }
        else if (input == 'b')
        {
            FarmerDashboard.Show(farmerId);  // Go back to the dashboard
        }
    }
}