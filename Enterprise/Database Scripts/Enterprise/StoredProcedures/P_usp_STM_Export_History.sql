USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_STM_Export_History]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_STM_Export_History]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_STM_Export_History 
				@Type varchar(50),
				@ClientID int,                --default 1
				@Site_Code varchar(50),
				@XmlMessage Xml   
				
AS 
BEGIN
INSERT INTO STM_Export_History
           ([Type]
           ,ClientID
           ,Site_Code
           ,[Message]		         
           ,Received_Date)
     VALUES
           (@Type
           ,@ClientID
           ,@Site_Code
           ,@XmlMessage           
           ,GETDATE())
END

GO

