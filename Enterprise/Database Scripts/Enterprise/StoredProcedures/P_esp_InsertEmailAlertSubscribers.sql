/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 9/3/2014 5:55:53 PM
 ************************************************************/

USE Enterprise
GO

IF EXISTS (
       SELECT 1
       FROM   sys.objects o
       WHERE  o.[name] = 'esp_InsertEmailAlertSubscribers'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE esp_InsertEmailAlertSubscribers
END

GO


/*
* Revision History	
* *********************************************************************************************************
* 
* Anuradha				Created				26 june 2014
*/

CREATE PROCEDURE esp_InsertEmailAlertSubscribers
	@inputxml VARCHAR(MAX),
	@SubList VARCHAR(MAX),
	@UserName VARCHAR(50),
	@UserId INT,
	@Oldvalue VARCHAR(500),
	@NewValue VARCHAR(500)
AS
BEGIN
	DECLARE @idoc INT  
	
	EXEC sp_xml_preparedocument @idoc OUTPUT,
	     @inputxml   
	
	SELECT * INTO #tempAlert
	FROM   OPENXML(@idoc, 'MailSubscribers/EmailAlertEntity', 2) 
	       WITH (
	           TypeID INT './AlertTypeId',
	           [Subject] VARCHAR(500) './Subject',
	           Frommail VARCHAR(MAX) './FromMail',
	           Tomail VARCHAR(MAX) './ToMail',
	           CCmail VARCHAR(MAX) './CCMail',
	           Bccmail VARCHAR(MAX) './BCCMail'
	       ) 
	
	DECLARE @Tomail VARCHAR(MAX)
	
	SELECT @tomail = tomail
	FROM   #tempAlert
	
	EXEC sp_xml_removedocument @idoc
	EXEC sp_xml_preparedocument @idoc OUTPUT,
	     @SubList      
	
	SELECT * INTO #tempAlertLink
	FROM   OPENXML(@idoc, 'LinkList/AlertLink', 2) 
	       WITH (
	           TypeID INT './AlertTypeID',
	           SubscriberID VARCHAR(500) './SubscriberID'
	       ) 
	
	IF NOT EXISTS (
	       SELECT 1
	       FROM   EmailSubscriberDetails esd
	       WHERE  LTRIM(RTRIM(REPLACE(esd.AS_To_Mail, ';', '')))  IN (SELECT 
	                                                                         DATA
	                                                                  FROM   dbo.fnSplit(@tomail, ';'))
	   )
	BEGIN
	    INSERT INTO EmailSubscriberDetails
	      (
	        -- AS_ID -- this column value is auto-generated,
	        AS_From_Mail,
	        AS_Subject,
	        AS_To_Mail,
	        AS_CC_Mail,
	        AS_BCC_Mail
	      )
	    SELECT DISTINCT ta.Frommail,
	           ta.[Subject],
	           Tomail,
	           ta.CCmail,
	           ta.Bccmail
	    FROM   #tempAlert ta
	END
	
	----DELETE
	----FROM   EmailSubscriberAlertlnk
	----WHERE  ESD_ID  IN (SELECT As_Id
	----                      FROM   EmailSubscriberDetails esd
	----                      WHERE  LTRIM(RTRIM(REPLACE(esd.AS_To_Mail, ';', '')))
	----                             IN (SELECT subscriberID
	----                                 FROM   #tempAlertLink))
	--SELECT *
	--FROM   #tempAlertLink ta
	--       Left  JOIN EmailSubscriberAlertlnk esa
	--            ON  ta.Typeid = esa.AT_ID
	
	--         Left JOIN EmailSubscriberDetails esd 
	--                   ON LTRIM(RTRIM(REPLACE(esd.AS_To_Mail, ';', '')))= @tomail  
	--                     WHERE  ta.Typeid IS NOT NULL
	--              AND esd_id IS NULL             
	
	--IF EXISTS (
	--       SELECT *
	--       FROM   #tempAlertLink ta
	--              LEFT  JOIN EmailSubscriberAlertlnk esa
	--                   ON  ta.Typeid = esa.AT_ID
	--                   Left JOIN EmailSubscriberDetails esd 
	--                   ON LTRIM(RTRIM(REPLACE(esd.AS_To_Mail, ';', '')))= @tomail
	                  
	--       WHERE  ta.Typeid IS NOT NULL
	--              AND esd_id IS NULL
	--   )
	--BEGIN
	--    PRINT 'exists'
	
	--SELECT * FROM EmailSubscriberAlertlnk esa LEFT  JOIN #tempAlertLink tal
	--ON esa.AT_ID =tal.TypeId WHERE esa.ESD_ID IS null
	
	--DELETE FROM EmailSubscriberAlertlnk 
	--WHERE exists (SELECT * FROM EmailSubscriberAlertlnk esa LEFT  JOIN #tempAlertLink tal
	--ON esa.AT_ID =tal.TypeId WHERE esa.ESD_ID IS NULL)
	
	TRUNCATE TABLE emailsubscriberAlertLnk
	    INSERT INTO EmailSubscriberAlertlnk
	    SELECT esd.AS_ID,
	           tal.TypeID
	    FROM   #tempAlertLink tal
	           LEFT  JOIN EmailSubscriberAlertlnk esa
	                ON  tal.Typeid = esa.AT_ID
	           LEFT JOIN EmailSubscriberDetails esd
	                ON  LTRIM(RTRIM(REPLACE(esd.AS_To_Mail, ';', ''))) = tal.SubscriberID
	    --WHERE  tal.Typeid IS NOT NULL
	    --       AND esd_id IS NULL
	
	
	
	PRINT 'inserted subscriber link'
	SELECT Site_code,
	       Site_id,
	       site_name INTO #tempSites
	FROM   SITE
	
	INSERT INTO Export_History
	  (
	    EH_Reference1,
	    EH_Date,
	    EH_Type,
	    EH_Site_Code
	  )
	SELECT 0,
	       GETDATE(),
	       'MailSubscribers',
	       Site_Code
	FROM   #tempSites
	
	
	EXEC usp_InsertAuditData @User_ID = @UserId,
	     @User_Name = @UserName,
	     @Module_ID = 35,
	     @Module_Name = 'MailSubscribers',
	     @Screen_Name = 'Service Calls',
	     @Slot = '',
	     @Aud_Field = 'MailSubscribers',
	     @Old_Value = @Oldvalue,
	     @New_Value = @NewValue,
	     @Aud_Desc = 'Mail Subscribers details inserted successfully',
	     @Operation_Type = 'Add'
END
GO
