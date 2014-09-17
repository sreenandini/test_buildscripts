USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAuditDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAuditDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

----------------------------------------------------------------------------------------------------    
---      
--- Description: to fetch audit details  
---       
--- Revision History      
---     exec rsp_GetAuditDetails  'Vinal','' ,'',''  
--- Kirupakar    03/03/2010     Created    
--- Vineetha M    02/03/2010    Modified params added       
--- C.Taylor      23/03/2010    modified to suit enterprise version
---                              exec rsp_GetAuditDetails  '01 jan 2010','30 dec 2010',NULL,0,1,'51,52' 
--- C.Taylor      28/03/2010    uses module id as link, not module name 
--- C.Taylor      30/03/2010    displays module name from modules table, not one from table
--- Vineetha M    18/05/2010    Modified uncommented lines  
----------------------------------------------------------------------------------------------------    
CREATE PROCEDURE  rsp_GetAuditDetails    
    
   @FromDate  DATETIME,    
   @ToDate    DATETIME,      
   @ModuleID  varchar(50),  
   @Rows      int,
   @Local     BIT,
   @Sites     VARCHAR(100) = ''
    
AS    
    
BEGIN       
  IF @ModuleID = '0'       
     SET @ModuleID = NULL   
    
   IF (@Rows <> 0 )   
      SET ROWCOUNT @rows
     
 SELECT    
	  AH.Audit_ID as Audit_ID,	
      AH.Audit_User_ID as Audit_User_ID,       
      [Operation] = AH.Audit_Operation_Type,    
      [User Name] = (Select staff_last_name + ','+ staff_first_name  from enterprise..staff where UserTableID = AH.Audit_User_ID),    
      [Date] = AH.Audit_Date,      
      AH.Audit_Module_ID,      
      [Module] = AM.Audit_Module_Name,      
      [Screen] = AH.Audit_Screen_Name,      
      [Description] = AH.Audit_Desc ,     
      AH.Audit_Slot,      
      AH.Audit_Field,     
      AH.Audit_Old_Vl,  
      AH.Audit_New_Vl  
      
      FROM Audit_History AH  

      JOIN Audit_Modules AM
        ON AH.Audit_Module_ID = AM.Audit_Module_ID 

     WHERE      
       AH.Audit_Date BETWEEN @FromDate AND @Todate      
       AND(
            ( @ModuleID IS NULL )
             OR 
            ( @ModuleID IS NOT NULL 
              AND 
              AH.Audit_Module_ID = @ModuleID )
          )        
     
       AND (
             ( @local = 1 )
             OR
             ( @local = 0 
               AND
               1 IN ( SELECT Data FROM fnSplit ( @sites, ',' ) ) )
           )
           
     ORDER BY Audit_Date desc,Audit_ID desc
    
END    
GO

