USE [webprog]
GO

INSERT INTO [dbo].[ticket_team]
           ([ticket_type_id]
           ,[team_id]
           ,[ticket_price]
           ,[ticket_amount])
     VALUES
           (<ticket_type_id, numeric(18,0),>
           ,<team_id, numeric(18,0),>
           ,<ticket_price, real,>
           ,<ticket_amount, numeric(18,0),>)
GO

