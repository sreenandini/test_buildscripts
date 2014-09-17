USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetOperatorandDepotDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetOperatorandDepotDetails]
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
  Kalaiyarasan.P              25-May-2012         Created               This SP is used to get Operator and      
                                                                        Depot details.  
*/    
CREATE PROCEDURE rsp_GetOperatorandDepotDetails
AS
	SELECT Operator.Operator_ID,
	       Operator.Operator_Name,
	       Depot.Depot_ID,
	       Depot.Depot_Name
	FROM   Depot
	       RIGHT JOIN Operator
	            ON  Depot.Supplier_ID = Operator.Operator_ID


GO

