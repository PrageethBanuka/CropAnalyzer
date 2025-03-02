public class CropHarvest : IComparable<CropHarvest>
{
    public int CropID { get; set; }
    public string CropName { get; set; }
    public string CropCategory { get; set; }
    public double AverageYieldPerHectare { get; set; }
    public int HarvestID { get; set; }
    public string FarmerName { get; set; }
    public DateTime Date { get; set; }
    public double Quantitykg { get; set; }
    public string QualityRating { get; set; }

    // Sorting by Crop Name First then by Date
    public int CompareTo(CropHarvest other)
    {
        if (other == null) return 1;

        int nameComparison = CropName.CompareTo(other.CropName);
        if (nameComparison == 0)
        {
            return CropName.CompareTo(other.CropName);
        }
        return nameComparison;
    }

    public override string ToString()
    {
        return $"Crop: {CropName}, Quantity: {Quantitykg}kg, Date: {Date.ToShortDateString()}";
    }
}