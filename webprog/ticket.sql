USE [webprog]
GO

INSERT INTO [dbo].[ticket]
           ([ticket_id]
           ,[ticket_type]
           ,[match_id]
           ,[login])
     VALUES
           (<ticket_id, numeric(18,0),>
           ,<ticket_type, numeric(18,0),>
           ,<match_id, numeric(18,0),>
           ,<login, nchar(50),>)
GO

