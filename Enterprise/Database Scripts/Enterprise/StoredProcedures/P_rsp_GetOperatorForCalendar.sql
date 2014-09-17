USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetOperatorForCalendar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetOperatorForCalendar]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetOperatorForCalendar
	AS
/*****************************************************************************************************
DESCRIPTION : To get the OperatorName for Calendar  
CREATED DATE: 11.2.2013
MODULE            :Calendar      
CHANGE HISTORY :
*****************************************************************************************************/
	BEGIN
		SELECT
			o.Operator_ID,
			o.Operator_Name			
		FROM
			Operator o
		ORDER BY o.Operator_Name
	END
	
--exec rsp_GetOperatorForCalendar


GO

