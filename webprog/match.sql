USE [webprog]
GO

INSERT INTO [dbo].[match]
           ([match_id]
           ,[match_hometeam_id]
           ,[match_date]
           ,[match_awayteam_id])
     VALUES
           (<match_id, numeric(18,0),>
           ,<match_hometeam_id, numeric(18,0),>
           ,<match_date, date,>
           ,<match_awayteam_id, numeric(18,0),>)
GO

