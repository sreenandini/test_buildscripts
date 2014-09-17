/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 7/21/2014 2:13:00 PM
 ************************************************************/

 USE  enterprise 
  
 GO 
 
 
 
 /************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 6/23/2014 11:48:48 AM
 ************************************************************/

IF EXISTS (
       SELECT 1
       FROM   sys.objects o
       WHERE  o.[name] = 'usp_ImportEmailAlertAuditDetails'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE usp_ImportEmailAlertAuditDetails
END
GO
 
/*  
* Revision History  
* **************************************************************************************************  
*   
* Anuradha   Created    04 July 2014  
*   
* Import Alert Audit Details

<AlertAudit>
  <EmailAlertAudit>
    <ID>1</ID>
    <Content>Setting Allow_Offline_Redeem had  Orignal Value: True New Value : False</Content>
    <SentStatus>0</SentStatus>
    <Result></Result>
    <SiteCode>1002</SiteCode>
    <SENTDate>1900-01-01T00:00:00</SENTDate>
  </EmailAlertAudit>
</AlertAudit>
*/  
  
CREATE PROCEDURE usp_ImportEmailAlertAuditDetails
	@SiteCode VARCHAR(10),
	@doc VARCHAR(MAX),
	@IsSuccess BIT OUTPUT
AS
BEGIN
	SET NOCOUNT ON  
	
	
	DECLARE @idoc INT  
	EXEC sp_xml_preparedocument @idoc OUTPUT,
	     @doc  
	
	SELECT * INTO #tempAlert
	FROM   OPENXML(@idoc, '/AlertAudit', 2) 
	       WITH 
	       (
	           EID INT './EID',
	           TypeID INT './TypeID',
	           CONTENT VARCHAR(MAX) './Content',
	           SentStatus INT './SentStatus',
	           Result VARCHAR(MAX) './Result',
	           SiteCode VARCHAR(20) './SiteCode',
	           SentDate DATETIME './SentDate'
	       )
	
	SET @IsSuccess = 1  
	
	IF NOT EXISTS (
	       SELECT *
	       FROM   #tempAlert tA
	              JOIN EmailAlertDetails aph 
	                 on  aph.EMD_HQ_ID = ta.EID
	   )
	BEGIN
	    INSERT INTO EmailAlertDetails
	      (
	        -- APH_ID -- this column value is auto-generated,  
	        EMD_HQ_ID,
	        EMD_APH_ID,
	        EMD_Type_ID,
	        EMD_Content,
	        EMD_Sent_Mail_Status,
	        EMD_Sent_Result,
	        EMD_SiteCode,
	        EMD_SentDate
	      )
	    SELECT tA.ID,
	           0,
	           TypeID,
	           CONTENT,
	           SentStatus,
	           Result,
	           SiteCode,
	           SentDate
	    FROM   #tempAlert tA
	END  
	
	EXEC sp_xml_removedocument @idoc 
	DROP TABLE #tempAlert
END
GO


