USE [Audit]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteAuditHistoryDetailsInXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteAuditHistoryDetailsInXML]
GO

USE [Audit]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*  
 * this stored procedure is to export the Site Audit History details to the corresponding Exchange  
 * Change History:     
 * Anil		   18 April 2011  Created 
*/  
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
 
CREATE PROCEDURE  [dbo].[rsp_GetSiteAuditHistoryDetailsInXML]      
(       
   @Site_Code VARCHAR(100) = NULL 
)
      
AS      
      
BEGIN         
 
      DECLARE @siteid INT
	  DECLARE @xml XML

	  SELECT @Siteid = site_id FROM Enterprise..Site WHERE site_code = @Site_Code	
	  
	  SET @xml = (SELECT      
	  AH.Audit_ID as Audit_ID,   
      AH.Audit_User_ID as Audit_User_ID,         
      Audit_Operation_Type = AH.Audit_Operation_Type,      
      Audit_User_Name = (Select staff_last_name + ','+ staff_first_name  from enterprise..staff where usertableID = AH.Audit_User_ID),    
      Audit_Date = AH.Audit_Date,        
      AH.Audit_Module_ID,        
      Audit_Module_Name = AH.Audit_Module_Name,        
      Audit_Screen_Name = AH.Audit_Screen_Name,        
      Audit_Desc = AH.Audit_Desc ,       
      AH.Audit_Slot,        
      AH.Audit_Field,       
      AH.Audit_Old_Vl,    
      AH.Audit_New_Vl,
	  (SELECT Site_Name FROM Enterprise..Site WHERE Site_ID = AH.Site_ID) AS Site_Name        
      FROM Site_Audit_History AH      
	  WHERE AH.Site_ID = @Siteid																		 						 
      ORDER BY Audit_Date desc ,Audit_ID desc
	  FOR XML PATH ('Audit_History'), ELEMENTS, ROOT('Audit_Historys'))
	  
	  SELECT @xml 	
END

GO

