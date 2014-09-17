USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EscalateServiceCall]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EscalateServiceCall]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------- 
--
-- Description: Escalates a service call
--
-- Inputs:     Notes, User
-- Outputs:     
--
-- =======================================================================
-- 
-- Revision History
-- 
-- Sudarsan S	 24/08/2009   Created
--------------------------------------------------------------------------- 
CREATE PROCEDURE [dbo].[usp_EscalateServiceCall] 
	@Service_ID	VARCHAR(20),
	@UserID		INT
AS

BEGIN

	DECLARE @User	VARCHAR(50)
	DECLARE @Notes	VARCHAR(500)
	DECLARE @DownTime	VARCHAR(20)
	DECLARE @SLAMin	INT
	DECLARE @DownMin	INT
	DECLARE @DepotID	INT
	DECLARE @Site	VARCHAR(100)
	DECLARE @Date	VARCHAR(30)
	DECLARE @Position	VARCHAR(10)
	DECLARE @Mail	VARCHAR(1000)
	DECLARE @subject	VARCHAR(500)
	DECLARE @body	VARCHAR(5000)
	DECLARE @Desc	VARCHAR(500)


	SELECT @DownTime =	CASE WHEN ISNULL(SSOH.Standard_Opening_Hours_ID, 0) > 0 THEN
						dbo.fnServiceGetDownTime(CONVERT(DATETIME, S.Service_Received, 101), SSOH.Standard_Opening_Hours_Open_Monday, 
						SSOH.Standard_Opening_Hours_Open_Tuesday, SSOH.Standard_Opening_Hours_Open_Wednesday, SSOH.Standard_Opening_Hours_Open_Thursday, 
						SSOH.Standard_Opening_Hours_Open_Friday, SSOH.Standard_Opening_Hours_Open_Saturday, SSOH.Standard_Opening_Hours_Open_Sunday, NULL)
					ELSE
						dbo.fnServiceGetDownTime(CONVERT(DATETIME, S.Service_Received, 101), St.Site_Open_Monday, St.Site_Open_Tuesday, 
						St.Site_Open_Wednesday, St.Site_Open_Thursday, St.Site_Open_Friday, St.Site_Open_Saturday, St.Site_Open_Sunday,NULL)
					END,
			@SLAMin = ISNULL(SA.SLA_Contract_Response, 0),
			@DepotID = St.Depot_ID,
			@Site = St.Site_Name,
			@Date = S.Service_Received,
			@Position = B.Bar_Position_name
	FROM dbo.Service S 
INNER JOIN dbo.Site St ON S.Site_ID = St.Site_ID
INNER JOIN dbo.Bar_Position B ON S.Bar_Position_ID = B.Bar_Position_ID
LEFT JOIN dbo.Standard_Opening_Hours SSOH ON St.Standard_Opening_Hours_ID = SSOH.Standard_Opening_Hours_ID
LEFT JOIN dbo.Sub_Company SC ON SC.Sub_Company_ID = St.Sub_Company_ID
LEFT JOIN dbo.SLA_Contract SA ON SA.SLA_Contract_ID = SC.SLA
	WHERE Service_Allocated_Job_No = CAST(SUBSTRING(@Service_ID, 1, CHARINDEX('/', @Service_ID) - 1) AS INT)

	SELECT @DownMin = CASE WHEN @DownTime <> '00:00' THEN (CAST(LEFT(@DownTime,2) AS INT) * 60) + CAST(RIGHT(@DownTime,2) AS INT)
						ELSE 0 END

	IF @DownMin < @SLAMin
		RETURN -5

	SELECT @Mail = S.Email_Address + ';' FROM dbo.Staff S INNER JOIN dbo.Staff_Depot D ON S.Staff_ID = D.Staff_ID WHERE D.Depot_ID = @DepotID

	IF ISNULL(REPLACE(@Mail, ';', ''), '') = ''
		RETURN -10

	SELECT @User = UserName FROM dbo.[User] WHERE SecurityUserID = @UserID

	IF ISNULL(@User, '') = ''
		RETURN -15

	SET @Notes = 'Call escalated by ' + @User

	UPDATE dbo.Service SET IsEscalated = 1 WHERE Service_Allocated_Job_No = CAST(SUBSTRING(@Service_ID, 1, CHARINDEX('/', @Service_ID) - 1) AS INT)

	EXEC dbo.usp_InsertServiceNotes @Service_ID, @Notes, @User

	SELECT @Desc = C.Call_Fault_Description
			FROM dbo.Service S
	INNER JOIN dbo.Datapak_Fault D ON S.Service_GMU_Source_ID = D.Datapak_Fault_Code AND S.Service_GMU_Type_ID = D.Datapak_Fault_Supplementary_Code
	INNER JOIN dbo.Call_Fault C ON C.Call_Fault_ID = D.Call_Fault_ID
		WHERE S.Service_Allocated_Job_No = CAST(SUBSTRING(@Service_ID, 1, CHARINDEX('/', @Service_ID) - 1) AS INT)

	SET @subject = 'Escalation of service Call: ' + SUBSTRING(@Service_ID, 1, CHARINDEX('/', @Service_ID) - 1)
	SET @body = 'Hi, <br><br> The below mentioned service call in "' + @Site + '" is escalated to you. <br><br><br>'

	SET @body = @body + '<table bgcolor="#98AFC7" border="3" cellpadding="2" cellspacing="1">'
	SET @body = @body + '<tr><th>Content</th><th>Details</th></tr>'
	SET @body = @body + '<tr><td>[Job ID:] - </td><td>' + SUBSTRING(@Service_ID, 1, CHARINDEX('/', @Service_ID) - 1) + '</td></tr>'
	SET @body = @body + '<tr><td>[Site Name:] - </td><td>' + @Site + '</td></tr>'
	SET @body = @body + '<tr><td>[Pos Number:] - </td><td>' + @Position + '</td></tr>'
	SET @body = @body + '<tr><td>[Description:] - </td><td>' + @Desc + '</td></tr>'
	SET @body = @body + '<tr><td>[Service Received:] - </td><td>' + @Date + '</td></tr></table>'

	SET @body = @body + '<br><br><br><br> ' + 'Note: ************Please do not reply to this mail, It is an AUTO GENERATED MAIL. ************'

	EXEC msdb.dbo.sp_send_dbmail @recipients=@Mail,
	@subject = @subject,
	@body = @body,
	@body_format = 'HTML'

	RETURN @@ERROR

END


GO

