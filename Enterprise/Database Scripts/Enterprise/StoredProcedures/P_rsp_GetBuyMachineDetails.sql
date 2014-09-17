USE [Enterprise]
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetBuyMachineDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetBuyMachineDetails]
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
  Kalaiyarasan.P              27-SEP-2012         Created               This SP is used to get Machine details   
                                                                        based on Machine_ID. 
--Exec  rsp_GetBuyMachineDetails  20,''
*/  
CREATE PROCEDURE rsp_GetBuyMachineDetails
	@Machine_ID INT,
	@TemplateName varchar(50)
AS
BEGIN
	DECLARE @Installation_ID AS INT
	SELECT @Installation_ID = Installation_ID
	FROM   Installation WITH(NOLOCK)
	WHERE  Machine_ID = @Machine_ID
	       AND Installation_End_Date IS NULL
	 
	 IF(@Machine_ID <>0)
BEGIN  
   
SELECT tgt.GameTypeCode,  
     M.IsAFTEnabled,  
     M.Depot_ID,  
     M.Operator_ID,  
     M.CMPGameType,  
     M.Staff_ID,  
     M.Machine_Connection_Type,  
     M.Machine_CIV_Change_Reason,  
     M.Machine_ModelTypeID,  
     M.Stacker_Id,  
     CAST(M.IsTITOEnabled AS BIT) AS IsTITOEnabled,  
     CAST(M.IsNonCashVoucherEnabled AS BIT) AS IsNonCashVoucherEnabled,  
     M.IsMultiGame,  
     CASE WHEN M.IsMultiGame = 1 THEN ISNULL(MGM.MultiGameName,'MULTI GAME')
     ELSE '-'
     END AS MultiGameName, 
     M.Machine_Status_Flag,  
     M.Machine_Stock_No,  
     ISNULL(M.Machine_End_Date,'') Machine_End_Date,  
     M.Machine_Sales_Invoice_Number,  
     M.Machine_Sale_Price,  
     M.Machine_Sold_To,  
     M.Machine_Type_Of_Sale,  
     M.ActAssetNo,  
     M.ActSerialNo,  
     M.GMUNo,  
	 M.Machine_Memo,  
     M.Machine_Purchased_From,  
     M.Machine_Alternative_Serial_Numbers,  
     M.Machine_MAC_Address,  
     M.Machine_Depreciation_Start_Date,  
     M.Machine_Start_Date,  
     M.Machine_Original_Purchase_Price,  
     M.Machine_Purchase_Invoice_Number,  
     M.Depreciation_Policy_ID,  
     M.Depreciation_Policy_Use_Default,          
     M.IsDefaultAssetDetail,  
     M.IsGameCappingEnabled, 
     M.AssetDisplayName,  
	 M.GetGameDetails,  
     M.Base_Denom,  
     M.Percentage_Payout,  
     M.Machine_Occupancy_Hour AS Machine_Class_Occupancy_Games_Per_Hour,    
     MC.Depreciation_Policy_ID AS Class_Depreciation,    
     MC.Machine_Class_Category_ID AS Machine_Class_Category_ID,    
     MT.Depreciation_Policy_ID AS Type_Depreciation,    
     MC.Machine_Name,    
     MC.Machine_BACTA_Code,    
     MC.Manufacturer_ID AS Manufacturer_ID,    
     MC.Validation_Length AS Validation_Length,    
     ST.Staff_Last_Name AS Staff_Sold_Staff_Last_Name,    
     ST.Staff_First_Name AS Staff_Sold_Staff_First_Name,    
     Old_Machine.Machine_ID AS Old_Machine_ID,    
     Old_Machine.Machine_Start_Date AS Old_Machine_Start_Date,    
     Old_Machine_Class.Machine_Name AS Old_Machine_Name,    
     Game_Title_ID AS MG_Game_ID,    
     
     @Installation_ID AS Installation_ID    
 FROM   MACHINE M WITH(NOLOCK)    
		LEFT OUTER JOIN [MultiGameMapping] MGM  WITH(NOLOCK) 
		ON m.Machine_ID = mgm.MachineID
        INNER JOIN Machine_Class MC WITH(NOLOCK)    
             ON  M.Machine_Class_ID = MC.Machine_Class_ID    
        LEFT JOIN Game_Title GT WITH(NOLOCK)    
             ON  GT.Game_Title = MC.Machine_Name    
        INNER JOIN Machine_Type MT WITH(NOLOCK)    
             ON  MC.Machine_Type_ID = MT.Machine_Type_ID    
        LEFT JOIN Staff ST WITH(NOLOCK)    
             ON  M.Staff_ID_Deleted = ST.Staff_ID    
        LEFT JOIN MACHINE AS Old_Machine WITH(NOLOCK)    
             ON  M.Machine_Previous_Machine_ID = Old_Machine.Machine_ID    
        LEFT JOIN Machine_Class AS Old_Machine_Class WITH(NOLOCK)    
             ON  Old_Machine.Machine_Class_ID = Old_Machine_Class.Machine_Class_ID    
        LEFT OUTER JOIN TblCMPGameTypes tGT WITH(NOLOCK)    
             ON M.CMPGameType = tGT.GamePrefix                 
  WHERE  M.Machine_ID = @Machine_ID    
END    
ELSE  
 BEGIN  
  SELECT tgt.GameTypeCode,  
        M.IsAFTEnabled,  
        M.Depot_ID,  
        M.Operator_ID,  
        M.CMPGameType,  
        M.Staff_ID,  
        M.Machine_Connection_Type,  
        M.Machine_CIV_Change_Reason,  
        M.Machine_ModelTypeID,  
        M.Stacker_Id,  
        CAST(M.IsTITOEnabled AS BIT) AS IsTITOEnabled,  
        CAST(M.IsNonCashVoucherEnabled AS BIT) AS IsNonCashVoucherEnabled,  
        M.IsMultiGame,  
        M.Machine_Status_Flag,  
        M.Machine_Stock_No,  
        ISNULL(M.Machine_End_Date,'') Machine_End_Date,  
        M.Machine_Sales_Invoice_Number,  
        M.Machine_Sale_Price,  
        M.Machine_Sold_To,  
        M.Machine_Type_Of_Sale,  
        M.ActAssetNo,  
        M.ActSerialNo,  
        M.GMUNo,  
        M.Machine_Memo, 
        M.Machine_Purchased_From,  
        M.Machine_Alternative_Serial_Numbers,  
        M.Machine_MAC_Address,  
        M.Machine_Depreciation_Start_Date,  
        M.Machine_Start_Date,  
        M.Machine_Original_Purchase_Price,  
        M.Machine_Purchase_Invoice_Number,  
        M.Depreciation_Policy_ID,  
        M.Depreciation_Policy_Use_Default,  
        M.IsDefaultAssetDetail, 
        M.IsGameCappingEnabled,    
        M.GetGameDetails,  
        M.Base_Denom,  
        M.Percentage_Payout,  
        M.Machine_Occupancy_Hour AS Machine_Class_Occupancy_Games_Per_Hour,     
        MC.Depreciation_Policy_ID AS Class_Depreciation,    
        MC.Machine_Class_Category_ID AS Machine_Class_Category_ID,    
        MT.Depreciation_Policy_ID AS Type_Depreciation,    
        MC.Machine_Name,    
        MC.Machine_BACTA_Code,    
        MC.Manufacturer_ID AS Manufacturer_ID,    
        MC.Validation_Length AS Validation_Length,    
        ST.Staff_Last_Name AS Staff_Sold_Staff_Last_Name,    
        ST.Staff_First_Name AS Staff_Sold_Staff_First_Name,    
        Old_Machine.Machine_ID AS Old_Machine_ID,    
        Old_Machine.Machine_Start_Date AS Old_Machine_Start_Date,    
        Old_Machine_Class.Machine_Name AS Old_Machine_Name,    
        Game_Title_ID AS MG_Game_ID,
        M.AssetDisplayName,
        @Installation_ID AS Installation_ID    
 FROM   dbo.AssetCreationTemplate M WITH(NOLOCK)    
        INNER JOIN TemplateMachine_Class MC WITH(NOLOCK)    
             ON  M.TemplateMachine_Class_ID = MC.TemplateMachine_Class_ID    
        LEFT JOIN Game_Title GT WITH(NOLOCK)    
             ON  GT.Game_Title = MC.Machine_Name    
        INNER JOIN Machine_Type MT WITH(NOLOCK)    
             ON  MC.Machine_Type_ID = MT.Machine_Type_ID    
        LEFT JOIN Staff ST WITH(NOLOCK)    
             ON  M.Staff_ID_Deleted = ST.Staff_ID    
        LEFT JOIN MACHINE AS Old_Machine WITH(NOLOCK)    
             ON  M.Machine_Previous_Machine_ID = Old_Machine.Machine_ID    
        LEFT JOIN Machine_Class AS Old_Machine_Class WITH(NOLOCK)    
             ON  Old_Machine.Machine_Class_ID = Old_Machine_Class.Machine_Class_ID    
        LEFT OUTER JOIN TblCMPGameTypes tGT WITH(NOLOCK)    
             ON M.CMPGameType = tGT.GamePrefix                 
 WHERE  M.TemplateName = @TemplateName   
 END  
END
GO

