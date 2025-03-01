public class Crop
{
    public int CropID { get; private set; }
    public string CropName { get; private set; }
    public string CropCategory { get; private set; }
    public double AverageYieldPerHectare { get; private set; }
    public string Season { get; private set; }

    // Constructor to initialize the Crop object
    public Crop(int cropID, string cropName, string cropCategory, double yield, string season)
    {
        CropID = cropID;
        CropName = cropName;
        CropCategory = cropCategory;
        AverageYieldPerHectare = yield;
        Season = season;
    }

    // Method to update properties (if needed)
    public void Update(string cropName, string cropCategory, double yield, string season)
    {
        CropName = cropName;
        CropCategory = cropCategory;
        AverageYieldPerHectare = yield;
        Season = season;
    }
}