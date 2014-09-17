USE [Audit]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteAuditDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteAuditDetails]
GO

USE [Audit]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
CREATE PROCEDURE  rsp_GetSiteAuditDetails      
      
   @FromDate	DATETIME,      
   @ToDate		DATETIME,        
   @ModuleName  VARCHAR(50),    
   @Rows		INT,  
   @Sites		VARCHAR(100) = ''  
      
AS      
      
BEGIN         
  IF @ModuleName = '0' 
     SET @ModuleName = NULL     
      
   IF (@Rows <> 0 )     
      SET ROWCOUNT @rows  
       
 SELECT      
	  AH.Audit_ID as Audit_ID,   
      AH.Audit_User_ID as Audit_User_ID,         
      [Operation] = AH.Audit_Operation_Type,      
      [User Name] = (Select staff_last_name + ','+ staff_first_name  from enterprise..staff where usertableID = AH.Audit_User_ID),    
      [Date] = AH.Audit_Date,        
      AH.Audit_Module_ID,        
      [Module] = AH.Audit_Module_Name,        
      [Screen] = AH.Audit_Screen_Name,        
      [Description] = AH.Audit_Desc ,       
      AH.Audit_Slot,        
      AH.Audit_Field,       
      AH.Audit_Old_Vl,    
      AH.Audit_New_Vl,
	  (SELECT Site_Name FROM Enterprise..Site WHERE Site_ID = AH.Site_ID) AS Site_Name
        
      FROM Site_Audit_History AH    
  
	  WHERE        
       AH.Audit_Date BETWEEN @FromDate AND @Todate        
       AND (AH.Audit_Module_Name = @ModuleName OR @ModuleName IS NULL)
	   AND AH.Site_ID IN(
						SELECT (
								SELECT Site_ID 
								FROM Enterprise..Site 
								WHERE Site_Code = Data)
						FROM fnSplit ( @sites, ',' ) 
					 )
      ORDER BY Audit_Date desc ,Audit_ID desc  
END
GO

