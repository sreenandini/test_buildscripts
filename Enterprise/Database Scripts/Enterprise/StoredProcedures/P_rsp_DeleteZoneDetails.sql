USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_DeleteZoneDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_DeleteZoneDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [rsp_DeleteZoneDetails]
@ZoneID INT
AS
BEGIN
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (START)  
 *****************************************************************************************************/  
		EXEC [dbo].[usp_EBS_UpdateZoneDetails] @ZoneId = @ZoneID, @IsDelete = 1
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (END)  
 *****************************************************************************************************/    
	DELETE FROM [Zone] WHERE Zone_ID = @ZoneID
END


GO

