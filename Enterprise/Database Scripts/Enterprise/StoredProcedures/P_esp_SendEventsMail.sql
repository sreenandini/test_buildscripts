USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[esp_SendEventsMail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[esp_SendEventsMail]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
 *	this stored procedure is to send mails to the pre-configured groups regarding the events...
 *
 *	Change History:
 *
 *	Sudarsan S			02-08-2008		created
*/
--[esp_SendEventsMail] 20, 10, 2, '2008-07-30 19:12:30.000'
CREATE PROCEDURE [dbo].[esp_SendEventsMail]
@datapak_fault_code	INT,
@datapak_fault_supplementary_code	INT,
@Installation_ID	INT,
@EventDate	DATETIME


AS

BEGIN

DECLARE @Description VARCHAR(100)
DECLARE @Site	VARCHAR(100)
DECLARE @body	VARCHAR(MAX)
DECLARE @Type	VARCHAR(100)
DECLARE @Position VARCHAR(50)
DECLARE @subject	VARCHAR(500)
DECLARE @To_Recipients VARCHAR(500)
DECLARE @CC_Recipients VARCHAR(500)

		IF EXISTS(SELECT 1 FROM dbo.Datapak_Fault WHERE Datapak_Fault_Code = @datapak_fault_code AND Datapak_Fault_Supplementary_Code = @datapak_fault_supplementary_code AND SendMail = 1)
		BEGIN
				SELECT  @Description = CF.Call_Fault_Description, 
						@Type = CF.Call_Fault_Reference,
						@Site = S.Site_Name,
						@Position = BP.Bar_Position_Name,
						@To_Recipients = DF.Mail_TO,
						@CC_Recipients = DF.Mail_CC
				FROM	dbo.Datapak_Fault DF
			INNER JOIN  dbo.Event E ON DF.Datapak_Fault_Code = E.Evt_Fault_Source AND DF.Datapak_Fault_Supplementary_Code = E.Evt_Fault_Type
			 LEFT JOIN	dbo.Call_Fault CF ON DF.Call_Fault_ID = CF.Call_Fault_ID 
			INNER JOIN	dbo.Installation I ON I.Installation_ID = E.Evt_Installation_ID
			INNER JOIN	dbo.Bar_Position BP ON BP.Bar_Position_ID = I.Bar_Position_ID
			INNER JOIN	dbo.Site S ON S.Site_ID = BP.Site_ID
				WHERE	DF.Datapak_Fault_Code = @datapak_fault_code AND DF.Datapak_Fault_Supplementary_Code = @datapak_fault_supplementary_code
						AND E.Evt_Installation_ID = @Installation_ID AND E.Evt_DateTime = @EventDate
						AND DF.SendMail = 1

			SET @subject = '[' + @Site + '] - [' + @Position + '] - [' + @Type + '] - [' + @Description + ']'
			SET @body = 'Hi, <br><br> Please find below the details regarding the event occured in "' + @Site + '" <br><br><br>'

			SET @body = @body + '<table bgcolor="#98AFC7" border="3" cellpadding="2" cellspacing="1">'
			SET @body = @body + '<tr><th>Content</th><th>Details</th></tr>'
			SET @body = @body + '<tr><td>[Date:] - </td><td>' + CONVERT(VARCHAR, @EventDate, 106) + '</td></tr>'
			SET @body = @body + '<tr><td>[Time:] - </td><td>' + CONVERT(VARCHAR, @EventDate, 108) + '</td></tr>'
			SET @body = @body + '<tr><td>[Site Name:] - </td><td>' + @Site + '</td></tr>'
			SET @body = @body + '<tr><td>[Pos Number:] - </td><td>' + @Position + '</td></tr>'
			SET @body = @body + '<tr><td>[Event Type:] - </td><td>' + @Type + '</td></tr>'
			SET @body = @body + '<tr><td>[Event Description:] - </td><td>' + @Description + '</td></tr></table>'

			SET @body = @body + '<br><br><br><br> ' + 'Note: ************Please do not reply to this mail, It is an AUTO GENERATED MAIL. ************'


			IF ISNULL(@To_Recipients, '') = '' AND ISNULL(@CC_Recipients, '') = '' 
			BEGIN
				RETURN 0
			END

			IF (ISNULL(@Description, '') <> '' OR ISNULL(@Type, '') <> '')
			BEGIN
				EXEC msdb.dbo.sp_send_dbmail @recipients=@To_Recipients,
					@copy_recipients = @CC_Recipients,
					@subject = @subject,
					@body = @body,
					@body_format = 'HTML'
			END
		END
END

GO

