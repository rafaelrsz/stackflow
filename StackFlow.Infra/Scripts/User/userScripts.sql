/****** Object:  StoredProcedure [dbo].[spCheckEmail]    Script Date: 16/11/2023 20:44:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCheckEmail]
	@Email VARCHAR(255)
AS
	SELECT CASE WHEN EXISTS (
		SELECT [Email]
		FROM [User]
		WHERE [Email] = @Email
	)
	THEN CAST(1 AS BIT)
	ELSE CAST(0 AS BIT) END
GO

/****** Object:  StoredProcedure [dbo].[spCheckDocument]    Script Date: 16/11/2023 20:44:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCheckDocument]
	@Document VARCHAR(14)
AS
	SELECT CASE WHEN EXISTS (
		SELECT [Document]
		FROM [User]
		WHERE [Document] = @Document
	)
	THEN CAST(1 AS BIT)
	ELSE CAST(0 AS BIT) END
GO

/****** Object:  StoredProcedure [dbo].[spCreateUser]    Script Date: 16/11/2023 20:44:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCreateUser]
    @Id UNIQUEIDENTIFIER,
    @Name VARCHAR(255),
    @Password VARCHAR(89),
    @AvailableBalance MONEY,
    @Document VARCHAR(14),
    @Role VARCHAR(50) -- Adicionado o atributo 'Role'
AS
BEGIN
    INSERT INTO [User] (Id, Name, Password, AvailableBalance, Document, Role)
    VALUES (@Id, @Name, @Password, @AvailableBalance, @Document, @Role);
END;
GO

/****** Object:  StoredProcedure [dbo].[spDeleteUser]    Script Date: 16/11/2023 20:44:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spDeleteUser]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    DELETE [User]
    WHERE Id = @Id;
END;
GO

/****** Object:  StoredProcedure [dbo].[spGetAllUsers]    Script Date: 16/11/2023 20:44:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetAllUsers]
AS
BEGIN
    SELECT *
    FROM [User];
END;
GO

/****** Object:  StoredProcedure [dbo].[spGetUser]    Script Date: 16/11/2023 20:44:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetUser]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM [User]
    WHERE Id = @Id;
END;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetUserByDocument]
    @Document UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM [User]
    WHERE Id = @Document;
END;
GO

/****** Object:  StoredProcedure [dbo].[spUpdateUser]    Script Date: 16/11/2023 20:44:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUpdateUser]
    @Id UNIQUEIDENTIFIER,
    @Name VARCHAR(255),
    @Password VARCHAR(89),
    @AvailableBalance MONEY,
    @Document VARCHAR(14),
    @Role VARCHAR(50) -- Adicionado o atributo 'Role'
AS
BEGIN
    UPDATE [User]
    SET 
        Name = @Name,
        Password = @Password,
        AvailableBalance = @AvailableBalance,
        Document = @Document,
        Role = @Role
    WHERE Id = @Id;
END;
GO

/****** Object:  StoredProcedure [dbo].[spValidateUserExclusion]    Script Date: 16/11/2023 20:44:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spValidateUserExclusion]
	@Id UNIQUEIDENTIFIER
AS
	SELECT CASE WHEN EXISTS (
		SELECT TOP 1 [Id]
		FROM [Transaction]
		WHERE [UserId] = @Id
	)
	THEN CAST(1 AS BIT)
	ELSE CAST(0 AS BIT) END
GO

CREATE PROCEDURE [spDepositFounds]
  @Id UNIQUEIDENTIFIER,
  @Amount MONEY
AS 
BEGIN
  UPDATE [USER]
  SET AvailableBalance = AvailableBalance + @Amount
  WHERE Id = @Id
END