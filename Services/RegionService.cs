using DSALibrary;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

public class RegionService : BaseService<Region>
{
    public override void Add(Region region)
    {
        using var connection = GetConnection();
        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Regions (RegionName) VALUES (@RegionName)";
        command.Parameters.AddWithValue("@RegionName", region.RegionName);
        command.ExecuteNonQuery();
    }

    public override Region GetById(int id)
    {
        using var connection = GetConnection();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Regions WHERE RegionId = @RegionId";
        command.Parameters.AddWithValue("@RegionId", id);
        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Region(reader.GetInt32(0), reader.GetString(1));
        }
        return null;
    }

    public override void Update(Region region)
    {
        using var connection = GetConnection();
        var command = connection.CreateCommand();
        command.CommandText = "UPDATE Regions SET RegionName = @RegionName WHERE RegionId = @RegionId";
        command.Parameters.AddWithValue("@RegionName", region.RegionName);
        command.Parameters.AddWithValue("@RegionId", region.RegionID);
        command.ExecuteNonQuery();
    }

    public override void Delete(int id)
    {
        using var connection = GetConnection();
        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Regions WHERE RegionId = @RegionId";
        command.Parameters.AddWithValue("@RegionId", id);
        command.ExecuteNonQuery();
    }

    public List<Region> GetAllRegions()
    {
        var regions = new List<Region>();
        using var connection = GetConnection();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Regions";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            regions.Add(new Region(reader.GetInt32(0), reader.GetString(1)));
        }
        return regions;
    }


    public MyLinkedList<Region> LoadRegions()
    {
        var regions = new MyLinkedList<Region>();

        // Open the connection explicitly
        using (var connection = GetConnection())
        {
            connection.Open();  // Ensure the connection is open before executing the command

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Regions";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Assuming that the first column is an integer (RegionId) and the second is a string (RegionName)
                    regions.Add(new Region(reader.GetInt32(0), reader.GetString(1)));
                }
            }
        }

        return regions;
    }

}