using Microsoft.Data.Sqlite;
using DSALibrary;

public class HarvestService : BaseService<Harvest>
{
    public override void Add(Harvest harvest)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Harvests (FarmerId, CropId, Date, Quantitykg, QualityRating) 
            VALUES (@FarmerId, @CropId, @Date, @Quantitykg, @QualityRating)";
        command.Parameters.AddWithValue("@FarmerId", harvest.FarmerId);
        command.Parameters.AddWithValue("@CropId", harvest.CropId);
        command.Parameters.AddWithValue("@Date", harvest.Date);
        command.Parameters.AddWithValue("@Quantitykg", harvest.Quantitykg);
        command.Parameters.AddWithValue("@QualityRating", harvest.QualityRating);
        command.ExecuteNonQuery();
    }

    public override Harvest GetById(int id)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Harvests WHERE HarvestId = @HarvestId";
        command.Parameters.AddWithValue("@HarvestId", id);
        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Harvest(
                reader.GetInt32(0),
                reader.GetInt32(1),
                reader.GetInt32(2),
                reader.GetString(3),
                reader.GetDouble(4),
                reader.GetString(5)
            );
        }
        return null;
    }

    public override void Update(Harvest harvest)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @"
            UPDATE Harvests 
            SET FarmerID = @FarmerId, CropId = @CropId, Date = @Date, Quantitykg = @Quantitykg, QualityRating = @QualityRating 
            WHERE HarvestId = @HarvestId";
        command.Parameters.AddWithValue("@FarmerId", harvest.FarmerId);
        command.Parameters.AddWithValue("@CropId", harvest.CropId);
        command.Parameters.AddWithValue("@Date", harvest.Date);
        command.Parameters.AddWithValue("@Quantitykg", harvest.Quantitykg);
        command.Parameters.AddWithValue("@QualityRating", harvest.QualityRating);
        command.Parameters.AddWithValue("@HarvestId", harvest.HarvestId);
        command.ExecuteNonQuery();
    }

    public override void Delete(int id)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Harvests WHERE HarvestId = @HarvestId";
        command.Parameters.AddWithValue("@HarvestId", id);
        command.ExecuteNonQuery();
    }
    public MyLinkedList<Harvest> GetHarvestsByFarmerId(int farmerId)
    {
        MyLinkedList<Harvest> harvests = new MyLinkedList<Harvest>();

        using var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Harvests WHERE FarmerId = @FarmerId";
        command.Parameters.AddWithValue("@FarmerId", farmerId);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var harvest = new Harvest(
                reader.GetInt32(0),    // HarvestId
                reader.GetInt32(1),    // FarmerId
                reader.GetInt32(2),    // CropId
                reader.GetString(3),   // Date
                reader.GetDouble(4),   // Quantitykg
                reader.GetString(5)    // QualityRating
            );

            harvests.Add(harvest);
        }

        return harvests;
    }

    public MyLinkedList<Harvest> GetAllHarvests()
    {
        MyLinkedList<Harvest> harvests = new MyLinkedList<Harvest>();

        using var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Harvests";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var harvest = new Harvest(
                reader.GetInt32(0),    // HarvestId
                reader.GetInt32(1),    // FarmerId
                reader.GetInt32(2),    // CropId
                reader.GetString(3),   // Date
                reader.GetDouble(4),   // Quantitykg
                reader.GetString(5)    // QualityRating
            );

            harvests.Add(harvest);
        }

        return harvests;
    }


    // Analysing Methods Implementations
public BST<FarmerHarvest> GetHarvestsByFarmers()
{
    BST<FarmerHarvest> farmerHarvests = new BST<FarmerHarvest>();

    using var connection = GetConnection();
    connection.Open();
    var command = connection.CreateCommand();
    command.CommandText = @"
    SELECT f.FarmerID, f.Name, r.RegionName, h.HarvestID, c.CropName, h.Date, h.Quantitykg, h.QualityRating
    FROM Farmers f
    JOIN Regions r ON f.RegionID = r.RegionID
    JOIN Harvests h ON f.FarmerID = h.FarmerID
    JOIN Crops c ON h.CropID = c.CropID
    ORDER BY f.FarmerID, h.Date;
    ";

    using var reader = command.ExecuteReader();
    while (reader.Read())
    {
        var farmerHarvest = new FarmerHarvest
        {
            FarmerID = reader.GetInt32(0),
            Name = reader.GetString(1),
            RegionName = reader.GetString(2),
            HarvestID = reader.GetInt32(3),
            CropName = reader.GetString(4),
            Date = reader.GetDateTime(5),
            Quantitykg = reader.GetDouble(6),
            QualityRating = reader.GetString(7)
        };

        farmerHarvests.Insert(farmerHarvest);
    }

    return farmerHarvests;
}
    public MyLinkedList<CropHarvest> GetHarvestsByCrops()
    {
        MyLinkedList<CropHarvest> cropHarvests = new MyLinkedList<CropHarvest>();

        using var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @"
        SELECT c.CropID, c.CropName, c.CropCategory, c.AverageYieldPerHectare, h.HarvestID, f.Name AS FarmerName, h.Date, h.Quantitykg, h.QualityRating
        FROM Crops c
        JOIN Harvests h ON c.CropID = h.CropID
        JOIN Farmers f ON h.FarmerID = f.FarmerID
        ORDER BY c.CropID, h.Date;
    ";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var cropHarvest = new CropHarvest
            {
                CropID = reader.GetInt32(0),
                CropName = reader.GetString(1),
                CropCategory = reader.GetString(2),
                AverageYieldPerHectare = reader.GetDouble(3),
                HarvestID = reader.GetInt32(4),
                FarmerName = reader.GetString(5),
                Date = reader.GetDateTime(6),
                Quantitykg = reader.GetDouble(7),
                QualityRating = reader.GetString(8)
            };

            cropHarvests.Add(cropHarvest);
        }

        return cropHarvests;
    }
    public MyLinkedList<RegionHarvest> GetHarvestsByRegions()
    {
        MyLinkedList<RegionHarvest> regionHarvests = new MyLinkedList<RegionHarvest>();

        using var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @"
        SELECT r.RegionID, r.RegionName, h.HarvestID, f.Name AS FarmerName, c.CropName, h.Date, h.Quantitykg, h.QualityRating
        FROM Regions r
        JOIN Farmers f ON r.RegionID = f.RegionID
        JOIN Harvests h ON f.FarmerID = h.FarmerID
        JOIN Crops c ON h.CropID = c.CropID
        ORDER BY r.RegionID, h.Date;
    ";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var regionHarvest = new RegionHarvest
            {
                RegionID = reader.GetInt32(0),
                RegionName = reader.GetString(1),
                HarvestID = reader.GetInt32(2),
                FarmerName = reader.GetString(3),
                CropName = reader.GetString(4),
                Date = reader.GetDateTime(5),
                Quantitykg = reader.GetDouble(6),
                QualityRating = reader.GetString(7)
            };

            regionHarvests.Add(regionHarvest);
        }

        return regionHarvests;
    }
}