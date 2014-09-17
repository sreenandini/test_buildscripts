USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetDepreciationPolicyInfo]    Script Date: 07/02/2013 19:06:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDepreciationPolicyInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDepreciationPolicyInfo]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetDepreciationPolicyInfo]    Script Date: 07/02/2013 19:06:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[rsp_GetDepreciationPolicyInfo]
(@MachineID INT)
 AS BEGIN
    	
    
SELECT Machine_End_Date,
       Machine_Original_Purchase_Price,
       Machine_Depreciation_Start_Date,
       MACHINE.Depreciation_Policy_ID,
       MACHINE.Machine_Class_ID,
       Depreciation_Policy_Details_ID,
       Depreciation_Policy_Residual_Value,
       Depreciation_Policy_Details_Duration,
       Depreciation_Policy_Details_Percentage
FROM   (
           (
               MACHINE LEFT JOIN Depreciation_Policy ON MACHINE.Depreciation_Policy_ID 
               = Depreciation_Policy.Depreciation_Policy_ID
           ) LEFT JOIN Depreciation_Policy_Details ON Depreciation_Policy.Depreciation_Policy_ID 
           = Depreciation_Policy_Details.Depreciation_Policy_ID
       )
WHERE  MACHINE.Machine_ID =  @MachineID 
ORDER BY
       Depreciation_Policy_Details_Period
       
 END

GO


