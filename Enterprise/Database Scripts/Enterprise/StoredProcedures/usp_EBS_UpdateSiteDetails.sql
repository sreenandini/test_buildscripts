/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 03/12/2014 12:49:13 PM
 ************************************************************/

USE [Enterprise]
GO


IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_EBS_UpdateSiteDetails]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_EBS_UpdateSiteDetails]
GO

-- exec [dbo].[usp_EBS_UpdateSiteDetails] 2
CREATE PROCEDURE [dbo].[usp_EBS_UpdateSiteDetails](@SiteId INT)
AS
BEGIN
	DECLARE @Site      TABLE (
	            SiteId INT,
	            SiteCode VARCHAR(50),
	            SiteName VARCHAR(50),
	            IsActive BIT
	        )
	
	DECLARE @Value     XML	
	DECLARE @SiteCode  VARCHAR(50)
	
	SELECT @SiteCode = Site_Code
	FROM   dbo.[Site] s WITH(NOLOCK)
	WHERE  S.Site_ID = @SiteId
	
	INSERT INTO @Site
	EXEC [dbo].[rsp_EBS_GetSiteDetails] @SiteCode = @SiteCode	
	
	SELECT @Value = (
	           SELECT *
	           FROM   @Site 
	                  FOR XML PATH('Site'),
	                  TYPE,
	                  ELEMENTS,
	                  ROOT('Sites')
	       )
	
	EXEC [dbo].[usp_EBS_InsertExportHistory] @EH_Type = 'Site',
	     @EH_Value = @Value,
	     @EH_SiteCode = @SiteCode
END
GO