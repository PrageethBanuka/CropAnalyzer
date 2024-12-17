INSERT INTO Regions (RegionName)
VALUES
('Northern Province'),
('Southern Province'),
('Eastern Province'),
('Western Province');

INSERT INTO Farmers (Name, RegionID, ContactNumber, Password)
VALUES
('John Doe', 1, '0771234567', 'password123'),
('Jane Smith', 2, '0712345678', 'securepass'),
('Michael Brown', 3, '0789876543', 'mypass'),
('Emily Davis', 4, '0705432167', 'farm@2024');

INSERT INTO Crops (CropName, CropCategory, AverageYieldPerHectare, Season)
VALUES
('Rice', 'Grain', 6.5, 'Wet Season'),
('Tomatoes', 'Vegetable', 25.0, 'Dry Season'),
('Corn', 'Grain', 8.0, 'Dry Season'),
('Bananas', 'Fruit', 20.0, 'Year Round'),
('Carrots', 'Vegetable', 30.0, 'Cool Season');

INSERT INTO Harvests (FarmerID, CropID, Date, Quantitykg, QualityRating)
VALUES
(1, 1, '2024-12-01', 1000, 'A'),
(1, 2, '2024-11-15', 500, 'B'),
(2, 3, '2024-12-05', 800, 'A+'),
(3, 4, '2024-10-20', 1200, 'B+'),
(4, 5, '2024-09-30', 600, 'A');

INSERT INTO Alerts (HarvestID, AlertMessage, AlertDate)
VALUES
(1, 'Low-quality rating detected for rice harvest.', '2024-12-02'),
(2, 'Unseasonal tomatoes yield reported.', '2024-11-16'),
(4, 'Banana harvest exceeds expected quantity.', '2024-10-21'),
(5, 'Carrots quality exceeds market standards.', '2024-10-01');