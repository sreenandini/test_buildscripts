USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetInstallations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetInstallations]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 -- =======================================================================  
-- OUTPUT           
-- =======================================================================  
-- Revision History  
--   
-- Poorna 
--------------------------------------------------------------------------- 
Create Proc [dbo].[usp_GetInstallations](@Site_Id int)  
--With ENCRYPTION  
As  
Begin  
 Select   
   I.Bar_Position_ID    as Bar_Pos_No ,  
   I.Machine_ID     as Machine_No,   
   M.Machine_Stock_No     as Stock_No,  
   MC.Machine_Name      as [Name],  
   I.Datapak_ID     as Datapak_No,  
   ID.Installation_Datapak_variant    as Datapak_Variant,  
   I.Installation_ID    as HQ_Installation_No ,  
   CONVERT(DATETIME, I.Installation_Start_Date + ' ' + I.Installation_Start_Time, 101) as Start_Date ,
--   I.Installation_Start_Time  as Start_Time ,  
   CONVERT(DATETIME, I.Installation_End_Date + ' ' + I.Installation_End_Time, 101)  as End_Date ,  
--   I.Installation_End_Time   as End_Time ,  
   I.Installation_Percentage_Payout as Anticipated_Percentage_Payout ,  
   I.Installation_Price_Per_Play  as Installation_Price_Of_Play ,  
   I.Installation_Jackpot_Value  as Installation_Jackpot ,  
   I.Installation_Initial_SC_Coins_In as Installation_Initial_Meters_Coins_In ,  
   I.Installation_Initial_SC_Coins_Out as Installation_Initial_Meters_Coins_Out ,  
   I.Installation_Initial_Coins_Drop as Installation_Initial_Meters_Coin_Drop ,  
   I.Installation_Initial_ExternalCredit as Installation_Initial_Meters_External_Credit ,  
   I.Installation_Initial_GamesBet as Installation_Initial_Meters_Games_Bet ,  
   I.Installation_Initial_GamesWon as Installation_Initial_Meters_Games_Won ,  
   I.Installation_Initial_Notes as Installation_Initial_Meters_Notes ,  
   I.Installation_Initial_Handpay as Installation_Initial_Meters_Handpay ,  
   I.Installation_Token_Value  as Installation_Token_Value   ,
   ISNULL(I.IsAuxSerialPortEnabled,0)  as IsAuxSerialPortEnabled,
   ISNULL(I.IsGatSerialPortEnabled,0)  as IsGatSerialPortEnabled,
   ISNULL(I.IsSlotLinePortEnabled,0) as IsSlotLinePortEnabled,
   ISNULL(I.Port_Disabled_Status,0) as Port_Disabled_Status
  from Installation I   
 left join Bar_Position B on I.Bar_Position_Id=B.Bar_Position_Id  
 left join Installation_Datapak ID on ID.Installation_Id = I.Installation_Id  
 join Machine M on M.Machine_Id = I.Machine_Id  
 join Machine_Class  MC on MC.Machine_Class_Id= M. Machine_Class_Id  
 Join Site S on B.Site_Id = S.Site_Id  
 where S.Site_Code=@Site_Id --and I.Installation_End_Date is null  
  For XML Path('Installation'),ROOT('Installations'),TYPE , Elements XSINIL  
End  
  

GO

