/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 7/16/2014 12:19:47 PM
 ************************************************************/

USE Enterprise
GO

IF EXISTS (
       SELECT 1
       FROM   sys.objects o
       WHERE  o.[name] = 'rsp_GetUnprocessedRecordsForAlert'
              AND o.[type] = 'p'
   )
BEGIN
    DROP PROCEDURE rsp_GetUnprocessedRecordsForAlert
END

GO
/*
* Revision History
* 
* Anuradha			Created				29 May 2014
* 
* get the records to send alert
*/


CREATE PROCEDURE rsp_GetUnprocessedRecordsForAlert(@SiteCode VARCHAR(8000), @RecordsCount INT)  
AS  
BEGIN  
 SET NOCOUNT ON   
   
   
 SELECT TOP (@RecordsCount)  
     aph.APH_ID,  
        aph.APH_Site_Code,  
        aph.APH_Type,  
        aph.APH_Message,  
        aph.APH_Status,  
        aph.APE_Received_Date,  
        aph.APH_Processed_Date,  
        aph.APH_Result  
 FROM   AlertProcessHistory aph  
 INNER JOIN [Site] s ON s.site_code = aph.APH_Site_Code  
 WHERE  s.site_code =@SiteCode  
 AND ISNULL (aph.APH_Status,0) <> 100   
END  
GO