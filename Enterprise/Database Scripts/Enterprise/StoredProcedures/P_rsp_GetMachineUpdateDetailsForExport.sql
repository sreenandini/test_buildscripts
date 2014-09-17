USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetMachineUpdateDetailsForExport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetMachineUpdateDetailsForExport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
CREATE PROCEDURE dbo.rsp_GetMachineUpdateDetailsForExport  
@Mac_ID INT  
AS  
BEGIN  
SELECT Machine_ID,
Machine_Stock_No,  
Machine_Manufacturers_Serial_No,  
Machine_Alternative_Serial_Numbers,  
IsTITOEnabled,  
IsAFTEnabled,  
IsNonCashVoucherEnabled ,  
ActAssetNo,  
GMUNo,  
Stacker_ID,
IsDefaultAssetDetail,
Base_Denom,
Percentage_Payout,
IsGameCappingEnabled,
isnull(AssetDisplayName,'') as AssetDisplayName ,
Machine_Occupancy_Hour 
FROM dbo.Machine M   
WHERE M.Machine_ID = @Mac_ID  
FOR XML PATH ('MACHINE') ,ELEMENTS XSINIL,ROOT('MACHINE_DETAILS')  
END

GO

