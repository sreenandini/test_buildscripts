USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetOperatorForPosition]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetOperatorForPosition]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetOperatorForPosition
AS
/*****************************************************************************************************
DESCRIPTION : To display the Operator Name   
CREATED DATE: 30.1.2013
MODULE      : BarPosition      
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR						MODIFIED DATE		DESCRIPTON                                                        
------------------------------------------------------------------------------------------------------
                                                              
*****************************************************************************************************/

BEGIN
      SET NOCOUNT ON 
      SELECT * from operator where LTRIM(RTRIM((ISNULL(operator_end_date,'')))) =''  ORDER BY Operator_Name ASC
END

GO

