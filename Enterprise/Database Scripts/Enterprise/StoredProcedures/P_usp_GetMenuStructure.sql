USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetMenuStructure]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetMenuStructure]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[usp_GetMenuStructure] 

@MS_Order int,
@userID int

AS
DECLARE @userReference int
BEGIN
	
	SET @userReference=(Select coalesce(usp_Reference,0) From User_Security_Profile Where usp_type=1 and usp_user_id=@userID)
	
	Select 
		MS_ID,
		MS_Order,
		MS_Type,
		MS_Menu_ID,
		MS_Application,
		MS_Additional,
		MS_Caption,
		MS_AppID,
		MS_IconName,
		MS_Heading,
		MS_Status,
		MS_Profile_ID,
		MS_Report_Type,
		MS_ProcedureUsed
	from Server_Reports_Menu_Structure where MS_Order=@MS_Order and MS_Profile_ID=@userReference and ltrim(rtrim(MS_Status))='Active' Order By MS_ID

END


GO

