/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 8/21/2014 11:17:03 AM
 ************************************************************/

USE enterprise 
 GO
 
 
 
 /************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 6/23/2014 11:48:48 AM
 ************************************************************/

IF EXISTS (
       SELECT 1
       FROM   sys.objects o
       WHERE  o.[name] = 'rsp_GetEmailAlertAuditDetails'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE rsp_GetEmailAlertAuditDetails
END
GO
 
/*  
* Revision History  
* **************************************************************************************************  
*   
* Anuradha   Created    04 July 2014  
*   
* get the email alert details with status   
* 
* rsp_getEmailAlertAuditDetails 1,'1555',0
*/  
  
CREATE PROCEDURE rsp_GetEmailAlertAuditDetails
	@AlertTypeId INT = NULL,
	@SiteCode VARCHAR(20) = NULL,
	@IsProcessed BIT = NULL
AS
BEGIN
	SET NOCOUNT ON  
	
	IF @IsProcessed IS NULL  SET  @IsProcessed = 0
	SELECT
		   at.AlertType_Name AS AlertType,
	       ead.EMD_Content AS CONTENT,
	       S.Site_Name AS SiteName,
	       S.Site_Code AS SiteCode,
	       ISNULL(ead.EMD_Sent_Mail_Status, '0') AS [Status],
	       ead.EMD_SentDate AS [Date],
	       ISNULL(ead.EMD_Sent_Result, '') AS Result
	FROM   EmailAlertDetails ead
	       INNER JOIN AlertType at
	            ON  at.AlertType_ID = ead.EMD_Type_ID
	       INNER JOIN [Site] S
	            ON  S.site_code = ead.EMD_SiteCode
	WHERE  S.Site_Code = @SiteCode
	       AND at.AlertType_ID = @AlertTypeID
	       AND (@IsProcessed= 0 or (@IsProcessed <>0  and ead.EMD_Sent_Mail_Status = 100))
END  
  
  GO
  
  