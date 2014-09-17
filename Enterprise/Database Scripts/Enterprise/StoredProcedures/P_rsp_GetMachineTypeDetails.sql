USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetMachineTypeDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetMachineTypeDetails]
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
  Kalaiyarasan.P              18-Sep-2012         Created               This SP is used to get Machine Type details
  --Exec  rsp_GetMachineTypeDetails                                                                   
*/  
  
CREATE PROCEDURE rsp_GetMachineTypeDetails
	@Machine_Type_ID INT = NULL
AS
BEGIN
	SELECT MT.Machine_Type_ID,
	       MT.Depreciation_Policy_ID,
	       MT.Machine_Type_Code,
	       MT.Machine_Type_Description,
	       MT.Machine_Type_Category,
	       MT.IsNonGamingAssetType,
	       MT.Machine_Type_AMEDIS_ID,
	       MT.Machine_Type_Income_Ledger_Code,
	       COALESCE(MT.Machine_Type_Site_Icon, 'SLOT') AS Machine_Type_Site_Icon,
	       MT.Machine_Type_Icon_ref,
	       SI.SiteIconPath
	FROM   Machine_Type MT WITH(NOLOCK)
	       LEFT JOIN SiteIcon SI
	            ON  COALESCE(MT.Machine_Type_Site_Icon, 'SLOT') = SI.Machine_Type_Site_Icon
	WHERE  Machine_Type_ID = COALESCE(@Machine_Type_ID, Machine_Type_ID)
	ORDER BY
	       Machine_Type_Code
END

GO

