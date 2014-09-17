USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_UpdateZoneName]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_UpdateZoneName]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

GO

CREATE PROCEDURE usp_UpdateZoneName
	@ZoneName VARCHAR(50),
	@ZoneID INT,
	@SiteID INT,
	@PromotionEnabled bit,
	@OpenHourID INT
AS
BEGIN

/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (START)  
 *****************************************************************************************************/  
  DECLARE @_Modified TABLE (  
            OldFlag VARCHAR(50),  
            NewFlag VARCHAR(50),  
            FlagChanged AS (CASE WHEN OldFlag = NewFlag THEN 0 ELSE 1 END)  
     )  
 /*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (END)  
 *****************************************************************************************************/ 
 
	UPDATE Zone
	SET    Zone_Name = @ZoneName,
	       Site_ID = @SiteID,
	       PromotionEnabled=@PromotionEnabled,
	       Standard_Opening_Hours_ID = @OpenHourID
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (START)  
 *****************************************************************************************************/  
		OUTPUT   
            DELETED.Zone_Name,  
            INSERTED.Zone_Name  
               INTO @_Modified  
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (END)  
 *****************************************************************************************************/  
	WHERE  Zone_ID = @ZoneID
	       AND Site_ID = @SiteID
	       
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (START)  
 *****************************************************************************************************/  
IF EXISTS(  
       SELECT 1  
       FROM   @_Modified m  
       WHERE  m.FlagChanged = 1  
                  )  
     BEGIN  
		EXEC [dbo].[usp_EBS_UpdateZoneDetails] @ZoneId = @ZoneID
     END   
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (END)  
 *****************************************************************************************************/  
END
GO


