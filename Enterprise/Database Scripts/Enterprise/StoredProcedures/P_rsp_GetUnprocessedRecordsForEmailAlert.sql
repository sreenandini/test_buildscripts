/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 6/23/2014 11:48:48 AM
 ************************************************************/
USE[Enterprise]
GO

IF EXISTS (SELECT 1 FROM sys.objects o WHERE o.[name]='rsp_GetUnprocessedRecordsForEmailAlert' AND o.[type]='P')
BEGIN
	DROP PROCEDURE rsp_GetUnprocessedRecordsForEmailAlert
END
GO

/*  
* Revision History  
*   
* Anuradha   Created    29 May 2014  
*   
* get the records to send the email alert  
*/  
--exec rsp_GetUnprocessedRecordsForEmailAlert '1001',10  
  
CREATE PROCEDURE rsp_GetUnprocessedRecordsForEmailAlert(@SiteCode VARCHAR(20), @RecordsCount INT)
AS
BEGIN
	SET NOCOUNT ON   
	
	SELECT TOP(@RecordsCount) 
	       EMD_ID,
	       at.AlertType_Name,
	       EMD_Content,
	       EMD_Sent_Mail_Status,
	       EMD_Sent_Result,	       
	       S.Site_Code AS EMD_SiteCode,
	       s.site_name AS SiteName
	FROM   EmailAlertDetails APH 
	       WITH (NOLOCK)
	       INNER JOIN [Site] S
	            ON  EMD_SiteCode = S.Site_Code
	            INNER JOIN AlertType at ON at.AlertType_ID = aph.EMD_Type_ID
	WHERE  aph.EMD_SiteCode = @sitecode
	       AND ISNULL(EMD_Sent_Mail_Status, 0) < 100
	ORDER BY
	       EMD_ID
END  

GO
