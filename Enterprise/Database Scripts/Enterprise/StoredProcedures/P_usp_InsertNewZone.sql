USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_InsertNewZone]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_InsertNewZone]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_InsertNewZone
	@SiteID INT,
	@Standard_Opening_Hours_ID INT,
	@ZoneName VARCHAR(50),
	@PromotionEnabled BIT,
	@ZoneID INT OUTPUT
AS
BEGIN
BEGIN TRANSACTION
	INSERT INTO Zone
	  (
	    -- Zone_ID -- this column value is auto-generated, 
	    Site_ID,
	    Zone_Name,
	    Standard_Opening_Hours_ID,
	    PromotionEnabled
	  )
	VALUES
	  (
	    @SiteID,
	    @ZoneName,
	    @Standard_Opening_Hours_ID,
	    @PromotionEnabled
	  )  
	  
	  IF @@Error <> 0
	  BEGIN
			GOTO ErrorHandler
	  END
	
	SET @ZoneID = CAST(SCOPE_IDENTITY() AS INT)  
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (START)  
 *****************************************************************************************************/  
	EXEC [dbo].[usp_EBS_UpdateZoneDetails] @ZoneId = @ZoneID
		
	IF @@Error <> 0
	BEGIN
		GOTO ErrorHandler
	END
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (END)  
 *****************************************************************************************************/      
	IF @@TRANCOUNT > 0
		COMMIT TRAN 
	SELECT @ZoneID
	RETURN
	ErrorHandler:

		IF @@TRANCOUNT > 0
		BEGIN
			SET @ZoneID = 0
			ROLLBACK TRAN
		END
	
	
END 

/*  
declare @ZoneID int  
exec usp_InsertNewZone 7,0,'[New Zone]',
select @ZoneID
Select * from Zone  

*/
GO

