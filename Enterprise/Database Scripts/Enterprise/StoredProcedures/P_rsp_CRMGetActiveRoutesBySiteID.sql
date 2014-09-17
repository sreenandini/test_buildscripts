 USE ENTERPRISE 
 GO
 
 IF EXISTS (SELECT 1 FROM SYS.OBJECTS O WHERE O.NAME='rsp_CRMGetActiveRoutesBySiteID' AND O.TYPE='P')
 BEGIN
    DROP PROCEDURE rsp_CRMGetActiveRoutesBySiteID
 END 
 
 GO
 
 CREATE  PROC dbo.rsp_CRMGetActiveRoutesBySiteID    
@Site_ID int  
AS  
/************************************************************************************  
Used In(Module) : Route Manager  
Created Date :  
Description  : Returns all routes for a site  
======================================================================================  
Modification History  
 Developer        Date  Modification        
======================================================================================  
1) K.Karthicksundar       Created    
2)   
**************************************************************************************/  
BEGIN   
 SELECT Route_ID AS Route_No,Route_Name,Site_ID,Active ,'0' as  CanDelete ,'0' AS Modified  
 FROM dbo.[Route]  
 WHERE Site_id=@Site_ID AND [Active] = 1  
END  