using System;

public class Region
{
    public int RegionID { get; private set; }
    public string RegionName { get; private set; }

    // Constructor to initialize the Region object
    public Region(int regionID, string regionName)
    {
        RegionID = regionID;
        RegionName = regionName;
    }
}