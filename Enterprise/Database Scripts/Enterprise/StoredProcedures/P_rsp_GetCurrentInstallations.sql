USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCurrentInstallations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCurrentInstallations]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------   
---  
--- Description: returns details of current bar positions with installation links  
---  
--- Inputs:      see inputs  
---  
--- Outputs:     (0)   - no error ..   
---              OTHER - SQL error   
---   
--- =======================================================================  
---   
--- Revision History  
---   
--- C.Taylor   Jan 2008     Created 
--- Anuradha J	08 July 2008 Modified  - Included bar position id to retrieve 
--- particular installation details.  
---Sudarshan S 30/10/2009	Modified	Check for Multigame
--- Madhu      29/12/2009	Added to retrive Installation Status  
---------------------------------------------------------------------------  

CREATE PROCEDURE rsp_GetCurrentInstallations
  
  @site_id int,
  @BarPosID int = 0
  
AS  
  
 IF @BarPosID = 0  SET @BarPosID = NULL

 SELECT Bar_Position.Bar_Position_ID,   
 cast(Bar_Position.Bar_Position_Name as int) AS Bar_Position,   
 Bar_Position.Bar_Position_Name,
 Bar_Position.Bar_Position_End_Date,   
 Bar_Pos_Type_Code = Bar_Pos_Type.Machine_Type_Code,   
 Bar_Position.Bar_Position_Supplier_Position_Code,   
 Bar_Position.Bar_Position_Supplier_Site_Code,   
 Bar_Position.Bar_Position_Use_Terms,   
 Bar_Position.Bar_Position_Net_Target,   
 Bar_Position.Bar_Position_Price_Per_Play,   
 Bar_Position.Bar_Position_Category,   
 Bar_Position.Bar_Position_Company_Target,   
 Bar_Position.Bar_Position_Collection_Day,   
 Bar_Position.Bar_Position_Company_Position_Code,   
 Bar_Position.Bar_Position_Jackpot,   
 Bar_Position.Bar_Position_Percentage_Payout,   
 Bar_Position.Bar_Position_Machine_Enabled,  
 Bar_Position.Bar_Position_Note_Acceptor_Enabled,   
 [Zone].Zone_Name,   
 Installation.installation_token_value,    
 Installation.Installation_ID,   
 Installation.Installation_Start_Date,   
 Installation.Installation_End_Date,   
 Installation.Installation_Change_Flag,   
 Installation.Installation_BACTA_Code_Override,   
 Installation.Installation_Price_Per_Play,   
 Installation.Installation_Jackpot_Value,   
 Installation.Installation_Percentage_Payout,   
 Installation.Datapak_ID,   
 Installation_RDC_Datapak_Version,   
 Installation_RDC_Datapak_Type,    
 Machine.Machine_ID,
-- CASE WHEN COUNT(MGMD_ID) OVER(PARTITION BY MGMD_Installation_ID) > 0 THEN
--	'MULTI GAME'
--	ELSE
-- Machine_Class.Machine_Name END AS Machine_Name,
 CASE WHEN
 Machine.IsMultiGame = 1
  THEN 
  ISNULL(MGMP.MultiGameName,'MULTI GAME')  
  ELSE 
  Machine_Class.Machine_Name
   END 
   AS Machine_Name,
 Machine.Machine_Stock_No,   
 Machine.Machine_Test,   
 Machine.Machine_Manufacturers_Serial_No,   
 Machine.Machine_Alternative_Serial_Numbers,   
 Machine_Class.Machine_BACTA_Code,   
 Machine.Machine_Due_In_Stock,   
 Machine.Machine_Due_In_Stock_Date,   
 Machine_Class.Machine_Class_Model_Code,   
 Machine.Depreciation_Policy_ID,    
 Machine_Type.Machine_Type_Code,  
 Operator.Operator_Name,   
 Depot.Depot_Name,   
 Depot.Depot_ID,   
 Installation.Installation_RDC_Machine_Code,   
 Installation.Installation_RDC_Secondary_Machine_Code,    
 Standard_Opening_Hours.Standard_Opening_Hours_ID,   
 Standard_Opening_Hours_Description,    
 Installation_Datapak_SecondaryMachineCode_Version,   
 (case Installation_Datapak_SecondaryMachineCode_CashOrToken WHEN '' THEN NULL ELSE Installation_Datapak_SecondaryMachineCode_CashOrToken END) AS Installation_Datapak_SecondaryMachineCode_CashOrToken,   
 Installation_Datapak_SecondaryMachineCode_PercentagePayout,   
 Installation_Datapak_SecondaryMachineCode_Type,   
 Installation_Datapak_SecondaryMachineCode_PriceOfPlay,   
 Installation_Datapak_SecondaryMachineCode_CoinSystem,   
 Installation_Datapak_SecondaryMachineCode_Dataport,   
 Installation_Datapak_SecondaryMachineCode_Jackpot,   
 Installation_Datapak_FirmwareVersion,   
 Installation_Datapak_FirmwareRevision,
 isnull(Installation_Status,'') as Installation_Status,
 Manufacturer.Manufacturer_Name AS Manufacturer_Name,
 Machine.GMUNo    

   FROM Bar_Position WITH (NOLOCK)   
  
LEFT JOIN [Zone] WITH (NOLOCK)   
       ON Bar_Position.Zone_ID = [Zone].Zone_ID  
  
     JOIN Site WITH (NOLOCK)   
       ON Bar_Position.Site_ID = Site.Site_ID  
  
LEFT JOIN Installation WITH (NOLOCK)   
       ON ( Bar_Position.Bar_Position_ID = Installation.Bar_Position_ID  
      AND installation.installation_id in ( select max(tmpinst.installation_id)   
                                              from installation tmpinst  
                                              join bar_position tmpbp  
                                                on tmpbp.bar_position_id = tmpinst.bar_position_id   
                                             WHERE tmpinst.installation_end_date is null   
                                               and tmpbp.site_id = @site_id  
                                          group by tmpbp.bar_position_name )   
          )  
  
LEFT JOIN Machine WITH (NOLOCK)   
       ON Installation.Machine_ID = Machine.Machine_ID  
  
LEFT JOIN Machine_Class WITH (NOLOCK)   
       ON Machine.Machine_Class_ID = Machine_Class.Machine_Class_ID  
  LEFT JOIN MultiGameMapping MGMP WITH (NOLOCK)
       ON MGMP.MachineID=Machine.Machine_ID
LEFT JOIN Machine_Type WITH (NOLOCK)   
       ON Machine_Class.Machine_Type_ID = Machine_Type.Machine_Type_ID  
  
LEFT JOIN Machine_Type AS Bar_Pos_Type WITH (NOLOCK)   
       ON Bar_Position.Machine_Type_ID = Bar_Pos_Type.Machine_Type_ID  
  
LEFT JOIN Depot WITH (NOLOCK)   
       ON Bar_Position.Depot_ID = Depot.Depot_ID  
  
LEFT JOIN Operator WITH (NOLOCK)   
       ON Depot.Supplier_ID = Operator.Operator_ID  
  
LEFT JOIN Standard_Opening_Hours WITH (NOLOCK)   
       ON Site.Standard_Opening_Hours_ID = Standard_Opening_Hours.Standard_Opening_Hours_ID  
  
LEFT JOIN Installation_Datapak WITH (NOLOCK)   
       ON Installation.Installation_ID = Installation_Datapak.Installation_ID 

LEFT JOIN dbo.Manufacturer WITH (NOLOCK)
	   ON Manufacturer.Manufacturer_ID = Machine_Class.Manufacturer_ID
 
    WHERE Bar_Position.Site_ID = @site_id  
AND ( ( @BarPosID IS NULL )   
         OR
           ( @BarPosID IS NOT NULL 
             AND
             Bar_Position.Bar_Position_ID= @BarPosID 
           )
)
AND LTRIM(RTRIM(ISNULL(Bar_Position.Bar_Position_End_Date,''))) = '' 
  
 ORDER BY cast(Bar_Position_Name as int),     
          installation.Installation_id desc,    
          Bar_Position.Bar_Position_ID,     
          Installation_Start_Date DESC     


GO

