USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ListCalender]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ListCalender]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_ListCalender
      
AS
/*****************************************************************************************************
DESCRIPTION : To List the Calender  
CREATED DATE: 28/01/2013


------------------------------------------------------------------------------------------------------
AUTHOR						MODIFIED DATE		DESCRIPTON                                                        
------------------------------------------------------------------------------------------------------
                                                              
*****************************************************************************************************/

BEGIN
      SET NOCOUNT ON 
      SELECT
      	c.Calendar_ID,
      	c.Calendar_Description
      	
      FROM
      	Calendar c
END

GO

