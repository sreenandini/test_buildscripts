USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_BMC_Purger]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_BMC_Purger]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC usp_BMC_Purger
AS 
BEGIN
	
	DECLARE @NumberOFDays INT

	SELECT @NumberOFDays= setting_value FROM SETTING (NOLOCK)
	WHERE Setting_name = 'BMC_Purger_DAYS'
	
	IF ISNULL(@NumberOFDays,0)=0
	BEGIN
		 SET @NumberOFDays=30 
		 EXEC usp_EditSetting 0,'BMC_Purger_DAYS' , @NumberOFDays
	END  

	
	DELETE FROM import_history
	WHERE  IH_Processed_Date <= DATEADD(DAY,-(@NumberOFDays), GETDATE()) 
	and IH_STATUS=100

	DELETE FROM   Export_History
	WHERE  EH_Export_Date   <= DATEADD(DAY, -(@NumberOFDays), GETDATE()) 
	and EH_STATUS=100

	DELETE FROM SiteStatusHistory
	WHERE  updateTimeStamp  <= DATEADD(DAY, -(@NumberOFDays), GETDATE())
	
	
END

GO

