USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[esp_InsertSiteAlliance]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[esp_InsertSiteAlliance]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--      
-- Description: Inserts Cross Ticketing Details in SiteAlliance and Export_History tables
--      
-- =======================================================================    
    
-- Object:  StoredProcedure [dbo].[esp_InsertSiteAlliance]      
--  Script Date: 07-May-2012 12:10:28   
--  Created By: Lekha       
--------------------------------------------------------------------------- 

CREATE PROCEDURE [dbo].[esp_InsertSiteAlliance] 
(
    @ClientSiteCode        VARCHAR(50),
    @HostSiteCode          VARCHAR(50),
    @HostSiteURL           VARCHAR(2000),
    @IsCashableRedeemable  BIT,
    @IsPromoRedeemable     BIT
)
AS
	IF NOT EXISTS (
	       SELECT 1
	       FROM   SITE
	       WHERE  TicketingURL = @HostSiteURL
	       AND Site_Code <> @HostSiteCode
	   )
	BEGIN
	    BEGIN TRAN
	    IF NOT EXISTS (
	           SELECT 1
	           FROM   SiteAlliance
	           WHERE  ClientSiteCode = @ClientSiteCode
	                  AND HostSiteCode = @HostSiteCode
	       )
	    BEGIN
	        INSERT INTO 
	               SiteAlliance
	          (
	            ClientSiteCode,
	            HostSiteCode,
	            IsCashableRedeemable,
	            IsPromoRedeemable,
	            LastUpdated
	          )
	        VALUES
	          (
	            @ClientSiteCode,
	            @HostSiteCode,
	            @IsCashableRedeemable,
	            @IsPromoRedeemable,
	            GETDATE()
	          )
	    END

	    ELSE
	    BEGIN
	        UPDATE SiteAlliance
	        SET    IsCashableRedeemable = @IsCashableRedeemable,
	               IsPromoRedeemable = @IsPromoRedeemable,
	               LastUpdated = GETDATE()
	        WHERE  ClientSiteCode = @ClientSiteCode
	               AND HostSiteCode = @HostSiteCode
	    END
	    
	    UPDATE SITE
	    SET    TicketingURL = @HostSiteURL
	    WHERE  Site_Code = @HostSiteCode
	    
	    IF @HostSiteCode IS NOT NULL
	    BEGIN
	        INSERT INTO Export_History
	          (
	            EH_Date,
	            EH_Reference1,
	            EH_Type,
	            EH_Site_Code
	          )
	        VALUES
	          (
	            GETDATE(),
	            'ALL',
	            'CROSSTICKETING',
	            @HostSiteCode
	          )
	    END
	    
	    IF @ClientSiteCode IS NOT NULL
	    BEGIN
	        INSERT INTO Export_History
	          (
	            EH_Date,
	            EH_Reference1,
	            EH_Type,
	            EH_Site_Code
	          )
	        VALUES
	          (
	            GETDATE(),
	            'ALL',
	            'CROSSTICKETING',
	            @ClientSiteCode
	          )
	    END
	END 	   
	ELSE
	BEGIN
	    RETURN 1
	END
	
	IF @@ERROR = 0
	    COMMIT TRAN
	ELSE
	    ROLLBACK TRAN

GO

