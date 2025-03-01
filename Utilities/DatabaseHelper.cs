using Microsoft.Data.Sqlite;

public static class DatabaseHelper
{
    public static SqliteConnection GetConnection()
    {
        var connectionString = "Data Source=Data/CropAnalyzer.db";
        return new SqliteConnection(connectionString);
    }

    public static void InitializeDatabase()
    {
        using var connection = GetConnection();
        connection.Open();

        // Execute Schema.sql to create tables
        var schema = System.IO.File.ReadAllText("Data/Schema.sql");
        var command = connection.CreateCommand();
        command.CommandText = schema;
        command.ExecuteNonQuery();
    }
}