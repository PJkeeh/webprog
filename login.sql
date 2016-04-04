USE [webprog]
GO

INSERT INTO [dbo].[login]
           ([login]
           ,[password])
     VALUES
           (<login, nchar(50),>
           ,<password, nchar(255),>)
GO

