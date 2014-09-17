USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDepreciationDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDepreciationDetails]
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
  Kalaiyarasan.P              18-OCT-2012         Created               This SP is used to get Depreciation details   
                                                                        based on Machine_ID. 
--Exec  rsp_GetDepreciationDetails  111
*/  
CREATE PROCEDURE rsp_GetDepreciationDetails
	@Machine_ID INT
AS
BEGIN
SELECT ISNULL(Machine_End_Date,'') Machine_End_Date,
       Machine_Original_Purchase_Price,
       Machine_Depreciation_Start_Date,
       MACHINE.Depreciation_Policy_ID,
       MACHINE.Machine_Class_ID,
       Depreciation_Policy_Details_ID,
       Depreciation_Policy_Residual_Value,
       Depreciation_Policy_Details_Duration,
       Depreciation_Policy_Details_Percentage
FROM   MACHINE WITH(NOLOCK)
       LEFT JOIN Depreciation_Policy WITH(NOLOCK)
            ON  MACHINE.Depreciation_Policy_ID = Depreciation_Policy.Depreciation_Policy_ID
       LEFT JOIN Depreciation_Policy_Details WITH(NOLOCK)
            ON  Depreciation_Policy.Depreciation_Policy_ID = 
                Depreciation_Policy_Details.Depreciation_Policy_ID
WHERE  MACHINE.Machine_ID = @Machine_ID
ORDER BY
       Depreciation_Policy_Details_Period
END

GO

