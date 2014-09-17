  /************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 6/24/2014 6:08:38 PM
 ************************************************************/

USE Enterprise
GO

IF EXISTS (
       SELECT 1
       FROM   sys.objects o
       WHERE  o.[name] = 'rsp_GetAlertTypes'
              AND o.[type] = 'p'
   )
BEGIN
    DROP PROCEDURE rsp_GetAlertTypes
END

GO
/*
* Revision History
* 
* Anuradha			Created				29 May 2014
* 
* Get teh alert types
*/

CREATE PROCEDURE rsp_GetAlertTypes  
AS   
  
BEGIN  
 SET NOCOUNT ON  
   
 SELECT  
  at.AlertType_ID AS ID,  
  at.AlertType_Name AS AlertTypeName  
 FROM  
  AlertType at  
END  
  