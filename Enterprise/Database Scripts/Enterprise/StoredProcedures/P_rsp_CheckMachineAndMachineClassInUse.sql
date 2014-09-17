USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckMachineAndMachineClassInUse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckMachineAndMachineClassInUse]
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
  Kalaiyarasan.P              01-NOV-2012         Created               This SP is used to Check  Machine  & Machine_Class   
                  is already exists or not  
--Exec  rsp_CheckMachineAndMachineClassInUse  'LC010',4  
*/    
CREATE PROCEDURE rsp_CheckMachineAndMachineClassInUse  
 @Machine_Stock_No VARCHAR(50),  
 @Machine_ID INT,  
 @ActSerialNo VARCHAR(50),  
 @ActAssetNo VARCHAR(50),  
 @GMUNo VARCHAR(50),  
 @MachineExist BIT OUTPUT,  
 @MachineClassExist BIT OUTPUT  
AS  
BEGIN  
   
 SET @MachineExist = 0  
 SET @MachineClassExist = 0  
   
 IF EXISTS(  
        SELECT TOP 1 MACHINE_ID  
        FROM   MACHINE WITH(NOLOCK)  
        WHERE  Machine_Stock_No = @Machine_Stock_No  
               AND Machine_ID <> @Machine_ID  
               AND (Machine_End_Date IS NULL OR Machine_End_Date='')  
               /*Its not required to check Machine_End_Date,once the machine is terminated, it cannot be reused. */   
    )  
 BEGIN  
     SET @MachineExist = 1  
 END  
   
 IF EXISTS(  
        SELECT TOP 1 MACHINE_ID  
        FROM   MACHINE M WITH(NOLOCK)  
               INNER JOIN Machine_Class MC WITH(NOLOCK)  
                    ON  M.Machine_Class_ID = MC.Machine_Class_ID  
               INNER JOIN Machine_Type MT WITH(NOLOCK)  
                    ON  MC.Machine_Type_ID = MT.Machine_Type_ID  
        WHERE  M.ActSerialNo = @ActSerialNo  
               AND M.ActAssetNo = @ActAssetNo  
               AND GMUNo = @GMUNo  
               AND M.Machine_ID <> @Machine_ID  
               AND ISNULL(MT.IsNonGamingAssetType, 0) = 0  
    )  
 BEGIN  
     SET @MachineClassExist = 1  
 END  
END  
  
  