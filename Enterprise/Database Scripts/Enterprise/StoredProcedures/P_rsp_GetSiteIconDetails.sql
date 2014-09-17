USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteIconDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteIconDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*  
 * Revision History  
 *   
 * <Name of the owner>         DateCreated       Type(Created/Modified)  Description  
   Kalaiyarasan.P              07-DEC-2012         Created               This SP is used to get SiteIcon details   
                                                                        
Exec  rsp_GetSiteIconDetails  
*/  
CREATE PROCEDURE rsp_GetSiteIconDetails
AS
BEGIN
	SELECT SiteIconID,
	       Machine_Type_Site_Icon,
	       SiteIconPath
	FROM   SiteIcon
END


GO

