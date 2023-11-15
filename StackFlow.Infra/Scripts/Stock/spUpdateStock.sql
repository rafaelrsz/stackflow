CREATE PROCEDURE spUpdateStock
    @Id UNIQUEIDENTIFIER,
    @Symbol VARCHAR(10),
    @Name VARCHAR(255),
    @Price MONEY,
    @Sector VARCHAR(255),
    @AvailableQuantity INT
AS
BEGIN
    UPDATE [Stock]
    SET 
        Name = @Name,
        Price = @Price,
        Sector = @Sector,
        AvailableQuantity = @AvailableQuantity,
        Symbol = @Symbol
    WHERE Id = @Id;
END;