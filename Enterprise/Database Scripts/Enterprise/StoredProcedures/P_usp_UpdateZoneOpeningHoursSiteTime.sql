USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateZoneOpeningHoursSiteTime]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateZoneOpeningHoursSiteTime]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
CREATE  PROCEDURE [dbo].[usp_UpdateZoneOpeningHoursSiteTime]   
(  
 @ZoneID int,   
 @ZoneStatusOUT int = 0 Output     
)  
  
AS  
  
BEGIN  

IF EXISTS (SELECT 1 FROM [Zone] INNER JOIN Site ON [Zone].Site_ID = Site.Site_ID Where Zone_ID = @ZoneID)  
BEGIN  

 SET @ZoneStatusOUT = 1

 UPDATE ZONE  
 SET  
  Zone_Open_Monday = S.Site_Open_Monday,
  Zone_Open_Tuesday = S.Site_Open_Tuesday,  
  Zone_Open_Wednesday = S.Site_Open_Wednesday,  
  Zone_Open_Thursday = S.Site_Open_Thursday,  
  Zone_Open_Friday = S.Site_Open_Friday,  
  Zone_Open_Saturday = S.Site_Open_Saturday ,  
  Zone_Open_Sunday = S.Site_Open_Sunday 
 FROM SITE S
 INNER JOIN [ZONE] ON [Zone].Site_ID = S.Site_ID 
 Where [ZONE].Zone_ID = @ZoneID

END
END


GO

