CREATE PROCEDURE spDeleteStock
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    DELETE [Stock]
    WHERE Id = @Id;
END;