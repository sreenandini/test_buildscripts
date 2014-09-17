USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSubCompanyCalender]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSubCompanyCalender]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetSubCompanyCalender
@SubCompanyId INT,
@CalendarId INT
AS
/*****************************************************************************************************
DESCRIPTION :To Get SubCompany ID
CREATED DATE: 4.2.2013
MODULE            :Calendar      
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
                                                             
*****************************************************************************************************/

BEGIN
      SET NOCOUNT ON 
      SELECT * FROM Sub_Company_Calendar WHERE Sub_Company_ID=@SubCompanyId AND Calendar_ID=@CalendarId
END

GO

