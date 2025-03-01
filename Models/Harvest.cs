public class Harvest
{
    public int HarvestId { get; private set; }
    public int FarmerId { get; private set; }
    public int CropId { get; private set; }
    public string Date { get; private set; }
    public double Quantitykg { get; private set; }
    public string QualityRating { get; private set; }

    public Harvest(int harvestID, int farmerID, int cropID, string date, double quantity, string qualityRating)
    {
        HarvestId = harvestID;
        FarmerId = farmerID;
        CropId = cropID;
        Date = date;
        Quantitykg = quantity;
        QualityRating = qualityRating;
    }

    // Method to update the quantity of the harvest
    public void UpdateQuantity(double newQuantity)
    {
        Quantitykg = newQuantity;
    }

    // Method to update the quality rating of the harvest
    public void UpdateQualityRating(string newRating)
    {
        QualityRating = newRating;
    }

    // Add other methods as necessary for updating other fields
}