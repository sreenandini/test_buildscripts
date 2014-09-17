USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportAutoInstallationToXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportAutoInstallationToXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* Purpose: To export Auto installation details to exchange as an XML 
 *	Change History:
 * exec rsp_ExportAutoInstallationToXML 8
 *	Vineetha Mathew		15-10-2009		created
 */

Create PROCEDURE [dbo].rsp_ExportAutoInstallationToXML
	@Installation_ID int
AS

BEGIN

DECLARE @AutoInstallation XML
--DECLARE @Installation_ID INT
DECLARE @barposid INT
			-- for testing
--			SET @Installation_ID=2
		IF(@Installation_ID>0)
			BEGIN
				SELECT
					@barposid=b.Bar_Position_ID 
				FROM bar_position b 
						join installation i on i.Bar_Position_ID = b.Bar_Position_ID 
				WHERE i.installation_id=@Installation_ID
			--select @barposname ,@barposid
			END
		IF(@barposid>0)
			BEGIN
			SET @AUTOINSTALLATION =	(SELECT
					Bar_position.Bar_position_name ,
					Zone.Zone_name,
					Machine.Machine_stock_no ,
					Machine.Machine_Manufacturers_Serial_No,
					Machine.Machine_Alternative_Serial_Numbers,
					Machine_class.Machine_name,
					Machine_type.Machine_type_code ,
					Manufacturer.Manufacturer_Name ,
					Machine.IsMultiGame,
					Installation_ID,Datapak_ID,
					CONVERT(DATETIME, Installation_Start_Date + ' ' + Installation_Start_Time, 101) AS Installation_Start_Date,
--					Installation_Start_Time,
					CONVERT(DATETIME, Installation_End_Date + ' ' + Installation_End_Time, 101) AS Installation_End_Date,
--					Installation_End_Time,
					Installation_Percentage_Payout,
					Installation_Jackpot_Value,
					Installation_Price_Per_Play,
					Installation_Token_Value,
					HQInstallationID
				FROM INSTALLATION 
					JOIN machine  ON machine.machine_id = installation.machine_id  
					JOIN machine_class  ON machine.machine_class_id = machine_class.machine_class_id
					JOIN machine_type  ON machine_Class.machine_Type_id = machine_type.machine_Type_id 				
					LEFT JOIN manufacturer ON manufacturer.Manufacturer_ID=machine_class.Manufacturer_ID
					JOIN bar_position   ON bar_position.bar_position_id = installation.bar_position_id 
					LEFT JOIN zone   ON zone.zone_id = bar_position.zone_id 
				WHERE INSTALLATION.installation_id=@Installation_ID
				FOR XML PATH('INSTALLATION'), ROOT('AUTOINSTALLATION'),ELEMENTS XSINIL)
				
				
			END	
	SELECT @AUTOINSTALLATION
END
		


GO

