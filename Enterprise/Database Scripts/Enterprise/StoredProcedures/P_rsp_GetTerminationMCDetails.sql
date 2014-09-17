USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetTerminationMCDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetTerminationMCDetails]
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
  Kalaiyarasan.P              21-NOV-2012         Created               This SP is used to get Termination Machine details   
                                                              
--Exec  rsp_GetTerminationMCDetails 'LC001' 
*/  
CREATE PROCEDURE rsp_GetTerminationMCDetails
	@Machine_Stock_No VARCHAR(50)
AS
BEGIN
	SELECT Machine_Termination_Comments,
	       Machine_Termination_Username,
	       Machine_Termination_Reason,
	       Machine_End_Date
	FROM   MACHINE WITH(NOLOCK)
	WHERE  Machine_Stock_No = LTRIM(RTRIM(@Machine_Stock_No))
END


GO

