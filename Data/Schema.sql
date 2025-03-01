CREATE TABLE Farmers (
    FarmerID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    RegionID INTEGER NOT NULL,
    ContactNumber TEXT,
    Password TEXT
);

CREATE TABLE Regions (
    RegionID INTEGER PRIMARY KEY AUTOINCREMENT,
    RegionName TEXT NOT NULL
);

CREATE TABLE Crops (
    CropID INTEGER PRIMARY KEY AUTOINCREMENT,
    CropName TEXT NOT NULL,
    CropCategory TEXT,
    AverageYieldPerHectare REAL,
    Season TEXT
);

CREATE TABLE Harvests (
    HarvestID INTEGER PRIMARY KEY AUTOINCREMENT,
    FarmerID INTEGER NOT NULL,
    CropID INTEGER NOT NULL,
    Date DATE NOT NULL,
    Quantitykg REAL,
    QualityRating TEXT,
    FOREIGN KEY (FarmerID) REFERENCES Farmers(FarmerID),
    FOREIGN KEY (CropID) REFERENCES Crops(CropID)
);

CREATE TABLE Alerts (
    AlertID INTEGER PRIMARY KEY AUTOINCREMENT,
    HarvestID INTEGER NOT NULL,
    AlertMessage TEXT,
    AlertDate DATE,
    FOREIGN KEY (HarvestID) REFERENCES Harvests(HarvestID)
);
