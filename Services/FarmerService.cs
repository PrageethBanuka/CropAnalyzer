using DSALibrary;
public class FarmerService : BaseService<Farmer>
{
    public override void Add(Farmer farmer)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Farmers (Name, RegionID, ContactNumber, Password) VALUES (@Name, @RegionID, @ContactNumber, @Password)";
        command.Parameters.AddWithValue("@Name", farmer.Name);
        command.Parameters.AddWithValue("@RegionID", farmer.RegionId);
        command.Parameters.AddWithValue("@ContactNumber", farmer.ContactNumber);
        command.Parameters.AddWithValue("@Password", farmer.Password);
        command.ExecuteNonQuery();
    }
    public MyLinkedList<Farmer> GetAll()
    {
        var farmers = new MyLinkedList<Farmer>();

        using var connection = GetConnection();
        connection.Open();  // Ensure the connection is open before executing the command

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Farmers";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var farmer = new Farmer(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetInt32(2),
                reader.GetString(3),
                reader.GetString(4)
            );
            farmers.Add(farmer);
        }

        return farmers;
    }

    public override Farmer GetById(int id)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Farmers WHERE FarmerID = @FarmerID";
        command.Parameters.AddWithValue("@FarmerID", id);
        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Farmer(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetInt32(2),
                reader.GetString(3),
                reader.GetString(4));
        }
        return null;
    }

    public override void Update(Farmer farmer)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "UPDATE Farmers SET Name = @Name, RegionID = @RegionID, ContactNumber = @ContactNumber, Password = @Password WHERE FarmerID = @FarmerID";
        command.Parameters.AddWithValue("@Name", farmer.Name);
        command.Parameters.AddWithValue("@RegionID", farmer.RegionId);
        command.Parameters.AddWithValue("@ContactNumber", farmer.ContactNumber);
        command.Parameters.AddWithValue("@Password", farmer.Password);
        command.Parameters.AddWithValue("@FarmerID", farmer.Id);
        command.ExecuteNonQuery();
    }

    public override void Delete(int id)
    {
        using var connection = GetConnection();
        connection.Open();

        // Step 1: Delete related alerts from the Alerts table
        var deleteAlertsCommand = connection.CreateCommand();
        deleteAlertsCommand.CommandText = "DELETE FROM Alerts WHERE HarvestID IN (SELECT HarvestID FROM Harvests WHERE FarmerID = @FarmerID)";
        deleteAlertsCommand.Parameters.AddWithValue("@FarmerID", id);
        deleteAlertsCommand.ExecuteNonQuery();

        // Step 2: Delete related harvests from the Harvests table
        var deleteHarvestsCommand = connection.CreateCommand();
        deleteHarvestsCommand.CommandText = "DELETE FROM Harvests WHERE FarmerID = @FarmerID";
        deleteHarvestsCommand.Parameters.AddWithValue("@FarmerID", id);
        deleteHarvestsCommand.ExecuteNonQuery();

        // Step 3: Delete the farmer from the Farmers table
        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Farmers WHERE FarmerID = @FarmerID";
        command.Parameters.AddWithValue("@FarmerID", id);
        command.ExecuteNonQuery();
    }
}