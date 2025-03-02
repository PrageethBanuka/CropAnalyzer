public class RegionHarvest : IComparable<RegionHarvest>
{
    public int RegionID { get; set; }
    public string RegionName { get; set; }
    public int HarvestID { get; set; }
    public string FarmerName { get; set; }
    public string CropName { get; set; }
    public DateTime Date { get; set; }
    public double Quantitykg { get; set; }
    public string QualityRating { get; set; }

    // Sorting by Region Name First then by Date
    public int CompareTo(RegionHarvest other)
    {
        if (other == null) return 1;

        int regionComparison = RegionName.CompareTo(other.RegionName);
        if (regionComparison == 0)
        {
            return RegionName.CompareTo(other.RegionName);
        }
        return regionComparison;
    }

    public override string ToString()
    {
        return $"Region: {RegionName}, Quantity: {Quantitykg}kg, Date: {Date.ToShortDateString()}";
    }
}