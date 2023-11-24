CREATE PROCEDURE spGetAllStocks
AS
BEGIN
    SELECT *
    FROM [Stock]
END;

CREATE PROCEDURE spGetSocksByUser
  @UserId UNIQUEIDENTIFIER
AS
BEGIN
  SELECT S.Id, S.Name, S.Symbol, S.Price, S.Sector, SUM (CASE
  WHEN T.Transactiontype = 2 THEN 0 - S.Price
  ELSE S.Price
  END) Quantidade
  FROM [Stock] S
  JOIN [Transaction] T ON (S.Id = T.StockId)
  JOIN [User] U ON (T.UserId = U.Id)
  WHERE U.Id = @UserId
  GROUP BY S.Id, S.Name, S.Symbol, S.Price, S.Sector
END