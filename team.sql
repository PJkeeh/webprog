USE [webprog]
GO

INSERT INTO [dbo].[team]
           ([team_id]
           ,[team_name]
           ,[team_description]
           ,[team_stadion_id])
     VALUES
           (<team_id, numeric(18,0),>
           ,<team_name, nchar(50),>
           ,<team_description, text,>
           ,<team_stadion_id, numeric(18,0),>)
GO

