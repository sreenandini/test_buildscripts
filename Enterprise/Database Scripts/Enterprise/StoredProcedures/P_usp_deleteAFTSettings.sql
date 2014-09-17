USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_deleteAFTSettings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_deleteAFTSettings]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_deleteAFTSettings (@SiteID INT, @Val VARCHAR(1000))
AS
BEGIN
	
	DELETE FROM AFTSetting WHERE DENOM IN (SELECT DATA FROM dbo.fnSplit(@Val,',')) AND SiteCode = @SiteId
	
END 
DECLARE @Site_Code VARCHAR (50)

	SELECT @Site_Code = Site_Code FROM dbo.[Site] WHERE Site_ID = @SiteID 
   
	 Insert into dbo.Export_History(EH_Date,EH_Reference1,EH_Type, EH_Site_Code) 
	 SELECT GETDATE(),DATA,'AFTSETTINGS', @Site_Code FROM dbo.fnSplit(@Val,',')  
         
         INSERT INTO dbo.Export_History
		(EH_Date,
		EH_Reference1,
		EH_Type, 
		EH_Site_Code) 
	(SELECT 
			GETDATE(),
			I.Installation_ID,
			'AFTENABLEDISABLE', 
			@Site_Code
	FROM Installation I
		INNER JOIN Bar_Position BP ON BP.Bar_Position_ID = I.Bar_Position_ID
	WHERE 
		I.Installation_End_Date IS NULL
		AND BP.Site_ID =  @SiteID
	)
	






	

GO