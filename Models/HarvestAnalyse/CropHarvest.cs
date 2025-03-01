public class CropHarvest
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
}