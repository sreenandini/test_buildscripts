USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetEmployeeCardTypes]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetEmployeeCardTypes]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*    
* **********************************************************************************************************    
* Revision History         
* Retrieve the Employee Card types      
* Author: Aishwarrya V S 
* **********************************************************************************************************    
*/    
    
    
CREATE PROCEDURE rsp_GetEmployeeCardTypes
AS
BEGIN
	SET NOCOUNT ON    
	
	SELECT GMUModeGroupID,
	       GMUModeGroupName,
	       GMUModeGroupDescription
	FROM   [GMUModeGroup] WITH(NOLOCK)	
END
GO



