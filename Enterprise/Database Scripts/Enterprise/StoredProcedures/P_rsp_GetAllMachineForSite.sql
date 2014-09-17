USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAllMachineForSite]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAllMachineForSite]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




--------------------------------------------------------------------------  
--------------------------------------------------------------------------   
---  
--- Description: Get Get All Sit eFor Verification.  
---  
--- Inputs:      see inputs  
---  
--- Outputs:       
---   
--- =======================================================================  
---   
--- Revision History  
---   
--- Senthil  07/06/10     Created   
---------------------------------------------------------------------------   
CREATE PROCEDURE dbo.rsp_GetAllMachineForSite 
(@SiteId INT)  
AS  
BEGIN  

IF @SiteId <> 999999
	BEGIN
		SELECT DISTINCT M.Machine_Manufacturers_Serial_No FROM SITE S
		INNER JOIN dbo.Bar_Position BP  on S.Site_ID = BP.Site_ID
		INNER JOIN dbo.installation I on BP.Bar_Position_ID = I.Bar_Position_ID
		INNER JOIN dbo.Machine M ON M.Machine_ID = I.Machine_ID
		INNER JOIN Machine_Class MC ON M.Machine_Class_ID = MC.Machine_Class_ID
		INNER JOIN MAchine_Type MT ON MT.Machine_Type_ID = MC.Machine_Type_ID
		WHERE MT.IsNonGamingAssetType <> 1
		AND S.Site_ID = @SiteId AND I.Installation_End_Date IS NULL
	END
	ELSE
	BEGIN
		SELECT Machine_Manufacturers_Serial_No FROM dbo.Machine M 
		INNER JOIN Machine_Class MC ON M.Machine_Class_ID = MC.Machine_Class_ID
		INNER JOIN MAchine_Type MT ON MT.Machine_Type_ID = MC.Machine_Type_ID
		WHERE MT.IsNonGamingAssetType <> 1
	END
END  
  

GO

