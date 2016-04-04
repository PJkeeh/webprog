USE [webprog]
GO

INSERT INTO [dbo].[ticket_type]
           ([ticket_type_id]
           ,[ticket_type_name]
           ,[ticket_type_hometeam])
     VALUES
           (<ticket_type_id, numeric(18,0),>
           ,<ticket_type_name, nchar(255),>
           ,<ticket_type_hometeam, bit,>)
GO

