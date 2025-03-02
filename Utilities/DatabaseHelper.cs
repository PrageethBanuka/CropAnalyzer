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
    public static void ResetDatabase()
{
    Console.WriteLine("[yellow]🔄 Resetting Database...[/]");
    
    string dbPath = "Data/CropAnalyzer.db";
    if (File.Exists(dbPath))
    {
        File.Delete(dbPath);
        Console.WriteLine("[red]🗑️ Database Deleted![/]");
    }

    InitializeDatabase(); // Recreate fresh DB
    Console.WriteLine("[green]🎯 DB Setup Complete![/]");
}
}