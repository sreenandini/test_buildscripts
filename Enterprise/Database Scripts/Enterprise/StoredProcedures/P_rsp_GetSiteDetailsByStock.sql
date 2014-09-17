USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteDetailsByStock]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteDetailsByStock]
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
  Kalaiyarasan.P              10-Sep-2012         Created               This SP is used to get Sitedetails based on M/C  
  --Exec  rsp_GetSiteDetailsByStock 9,0                                                                    
*/  
  
CREATE PROCEDURE  rsp_GetSiteDetailsByStock @Machine_ID INT,@IsNonGamingAsset BIT
AS
BEGIN
	IF @IsNonGamingAsset=0 
	BEGIN
	SELECT Site_Name, Site_Code, Bar_Position_Name,Site_ZonaRice FROM Installation WITH(NOLOCK)
	INNER JOIN Bar_Position WITH(NOLOCK) ON Installation.Bar_Position_ID = Bar_Position.Bar_Position_ID 
	INNER JOIN Site WITH(NOLOCK) ON Bar_Position.Site_ID = Site.Site_ID
	WHERE Installation.Machine_ID =@Machine_ID  AND COALESCE(Installation_End_Date,'')='' AND Installation_ID IS NOT NULL	
	END
ELSE
	BEGIN
	SELECT Site_Name, Site_Code,CAST ('' as Varchar(50)) as Bar_Position_Name,Site_ZonaRice FROM Site WITH(NOLOCK) INNER JOIN Machine WITH(NOLOCK) ON Machine.Machine_ID = Site.NGA_Machine_ID 
	WHERE Machine.Machine_ID = @Machine_ID

	END
END


GO

