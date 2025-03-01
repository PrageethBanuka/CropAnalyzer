public class FarmerHarvest : IComparable<FarmerHarvest>
{
    public int FarmerID { get; set; }
    public string Name { get; set; }
    public string RegionName { get; set; }
    public int HarvestID { get; set; }
    public string CropName { get; set; }
    public DateTime Date { get; set; }
    public double Quantitykg { get; set; }
    public string QualityRating { get; set; }

    // Comparison based on FarmerID
    public int CompareTo(FarmerHarvest other)
    {
        if (other == null) return 1;
        return FarmerID.CompareTo(other.FarmerID);
    }

    public override string ToString()
    {
        return $"FarmerID: {FarmerID}, Name: {Name}, Crop: {CropName}, Quantity: {Quantitykg}kg, Quality: {QualityRating}, Date: {Date.ToShortDateString()}";
    }
}