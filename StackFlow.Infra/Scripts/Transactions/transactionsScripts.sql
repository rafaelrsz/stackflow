/****** Object:  StoredProcedure [dbo].[spGetTransactionsByUser]    Script Date: 16/11/2023 20:44:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetTransactionsByUser]
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM [Transaction]
    WHERE UserId = @UserId;
END;
GO

/****** Object:  StoredProcedure [dbo].[spGetAllTransactions]    Script Date: 16/11/2023 20:44:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetAllTransactions]
AS
BEGIN
    SELECT *
    FROM [Transaction];
END;
GO

/****** Object:  StoredProcedure [dbo].[spGetTransaction]    Script Date: 16/11/2023 20:44:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetTransaction]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM [Transaction]
    WHERE Id = @Id;
END;
GO

/****** Object:  StoredProcedure [dbo].[spInsertTransaction]    Script Date: 16/11/2023 20:44:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInsertTransaction]
    @Id UNIQUEIDENTIFIER,
    @Amount INT,
    @TransactionType INT,
    @TotalPrice MONEY,
    @StockId UNIQUEIDENTIFIER,
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO [Transaction] (Id, Amount, TransactionType, TotalPrice, StockId, UserId)
    VALUES (@Id, @Amount, @TransactionType, @TotalPrice, @StockId, @UserId);
END;
GO
