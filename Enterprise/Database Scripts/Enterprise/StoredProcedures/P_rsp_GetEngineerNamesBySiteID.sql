USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetEngineerNamesBySiteID]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetEngineerNamesBySiteID]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
    
CREATE PROCEDURE rsp_GetEngineerNamesBySiteID(@Site_ID INT = 0)    
AS    
BEGIN    
 IF @Site_ID <> 0  
  BEGIN  
   SELECT DISTINCT ISNULL(stfD.Staff_ID, stfOp.Staff_ID) AS Staff_ID,    
    CASE     
      WHEN stfD.Staff_First_Name IS NULL THEN stfOp.Staff_First_Name +     
        ' ' + stfOp.Staff_Last_Name + ' - Operator'    
      ELSE stfD.Staff_First_Name + ' ' + stfD.Staff_Last_Name +     
        ' - Depot'    
    END AS Staff_Name    
   FROM   [Site] s WITH (NOLOCK)    
    LEFT JOIN Depot d WITH (NOLOCK)    
      ON  s.Service_Depot_ID = d.Depot_ID    
    LEFT JOIN Staff stfD WITH (NOLOCK)    
      ON  d.Depot_ID = stfD.Depot_ID    
    LEFT JOIN Operator op WITH (NOLOCK)    
      ON  s.Service_Supplier_ID = op.Operator_ID    
    LEFT JOIN Staff stfOp WITH (NOLOCK)    
      ON  op.Operator_ID = stfOp.Supplier_ID    
   WHERE  s.Site_ID = @Site_ID    
    AND stfD.Staff_IsAnEngineer = 1    
    AND stfD.Staff_Terminated = 0    
  END   
 ELSE  
  BEGIN  
		SELECT Staff_ID,   
		Staff_First_Name + ' ' + Staff_Last_Name  as Staff_Name  
		FROM Staff WITH (NOLOCK)   
		WHERE   
		 Staff_IsAnEngineer = 1 and  
		 Staff_Terminated = 0   
		ORDER BY Staff_First_Name,Staff_Last_Name  
  END   
END     