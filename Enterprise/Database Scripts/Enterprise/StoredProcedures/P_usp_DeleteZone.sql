USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_DeleteZone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_DeleteZone]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_DeleteZone
	@ZoneID INT
AS
BEGIN
DECLARE @Site_ID	INT
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (START)  
 *****************************************************************************************************/  
		EXEC [dbo].[usp_EBS_UpdateZoneDetails] @ZoneId = @ZoneID, @IsDelete = 1
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (END)  
 *****************************************************************************************************/ 

SELECT @Site_ID = Site_ID FROM Zone WHERE Zone_ID = @ZoneID   
 
EXEC [dbo].[usp_Export_History] @ZoneID, 'DELETEZONE', @Site_ID

DELETE FROM Zone WHERE Zone_ID=@ZoneID
 
END

GO
