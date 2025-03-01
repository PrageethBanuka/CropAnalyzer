using System.Data;

public class Farmer
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int RegionId { get; private set; }
    public string ContactNumber { get; private set; }

    public string Password { get; private set; }

    public Farmer(int id, string name, int regionId, string contactNumber, string password){
        Id = id;
        Name = name;
        RegionId = regionId;
        ContactNumber = contactNumber;
        Password = password;
    }

}