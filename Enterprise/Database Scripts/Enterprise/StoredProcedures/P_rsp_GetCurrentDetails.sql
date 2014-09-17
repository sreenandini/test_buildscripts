USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCurrentDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCurrentDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

    CREATE PROCEDURE rsp_GetCurrentDetails
    @calendarId INT   
    
    AS
/*****************************************************************************************************
DESCRIPTION : To get the Current Week Number  
CREATED DATE: 06.2.2013
MODULE            :Calendar      
CHANGE HISTORY :
*****************************************************************************************************/
BEGIN   
    
SELECT * FROM Calendar_Week cw WHERE cw.Calendar_ID=@calendarId
AND DATEDIFF(d,CONVERT(DATETIME,cw.Calendar_Week_Start_Date, 103),GETDATE())>=0
AND DATEDIFF(d,GETDATE(),CONVERT(DATETIME,cw.Calendar_Week_End_Date, 103) )>=0
END


-- =============================================
-- Example to execute the stored procedure
-- =============================================
-- EXECUTE rsp_GetCurrentCompanyDetails 2
-- GO


GO

