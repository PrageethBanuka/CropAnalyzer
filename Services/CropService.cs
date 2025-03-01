using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using DSALibrary;

public class CropService : BaseService<Crop>
{
    public override void Add(Crop crop)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Crops (CropName, CropCategory, AverageYieldPerHectare, Season) 
            VALUES (@CropName, @CropCategory, @Yield, @Season)";
        command.Parameters.AddWithValue("@CropName", crop.CropName);
        command.Parameters.AddWithValue("@CropCategory", crop.CropCategory);
        command.Parameters.AddWithValue("@Yield", crop.AverageYieldPerHectare);
        command.Parameters.AddWithValue("@Season", crop.Season);
        command.ExecuteNonQuery();
    }

    public override Crop GetById(int id)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Crops WHERE CropID = @CropID";
        command.Parameters.AddWithValue("@CropID", id);
        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Crop(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetDouble(3),
                reader.GetString(4)
            );
        }
        return null;
    }

    public override void Update(Crop crop)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @"
            UPDATE Crops 
            SET CropName = @CropName, CropCategory = @CropCategory, AverageYieldPerHectare = @Yield, Season = @Season 
            WHERE CropID = @CropID";
        command.Parameters.AddWithValue("@CropName", crop.CropName);
        command.Parameters.AddWithValue("@CropCategory", crop.CropCategory);
        command.Parameters.AddWithValue("@Yield", crop.AverageYieldPerHectare);
        command.Parameters.AddWithValue("@Season", crop.Season);
        command.Parameters.AddWithValue("@CropID", crop.CropID);
        command.ExecuteNonQuery();
    }

    public override void Delete(int id)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Crops WHERE CropID = @CropID";
        command.Parameters.AddWithValue("@CropID", id);
        command.ExecuteNonQuery();
    }
    public MyLinkedList<Crop> GetCropsByFarmerId(int farmerId)
    {
        var crops = new MyLinkedList<Crop>();
        using var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Crops.* FROM Crops
            INNER JOIN Harvests ON Crops.CropID = Harvests.CropId
            WHERE Harvests.FarmerId = @FarmerId";
        command.Parameters.AddWithValue("@FarmerId", farmerId);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            crops.Add(new Crop(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetDouble(3),
                reader.GetString(4)
            ));
        }
        return crops;
    }

    public MyLinkedList<Crop> GetAllCrops()
    {
        var crops = new MyLinkedList<Crop>();
        using var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Crops";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            crops.Add(new Crop(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetDouble(3),
                reader.GetString(4)
            ));
        }
        return crops;
    }
}