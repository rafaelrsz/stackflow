CREATE PROCEDURE spCreateStock
    @Id UNIQUEIDENTIFIER,
    @Symbol VARCHAR(10),
    @Name VARCHAR(255),
    @Price MONEY,
    @Sector VARCHAR(255),
    @AvailableQuantity INT
AS
BEGIN
    INSERT INTO [Stock] (Id, Symbol, Name, Price, Sector, AvailableQuantity)
    VALUES (@Id, @Symbol, @Name, @Price, @Sector, @AvailableQuantity);
END;