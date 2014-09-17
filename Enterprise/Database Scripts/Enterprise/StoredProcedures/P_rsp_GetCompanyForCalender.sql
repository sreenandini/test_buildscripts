USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCompanyForCalender]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCompanyForCalender]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetCompanyForCalender
AS
/*****************************************************************************************************
DESCRIPTION : To Display Company Name  
CREATED DATE: 31.1.2013
MODULE            : Bar Position      
CHANGE HISTORY :
                                                            
*****************************************************************************************************/

BEGIN
      SET NOCOUNT ON 
SELECT 
Company.Company_Name, 
Company.Company_ID, 
Sub_Company.Sub_Company_Name, 
Sub_Company.Sub_Company_ID, 
Sub_Company.Calendar_ID 
FROM Sub_Company 
INNER JOIN Company ON 
Sub_Company.Company_ID = Company.Company_ID 
ORDER BY Company.Company_Name ASC, 
Company.Company_ID ASC, 
Sub_Company.Sub_Company_Name ASC, 
Sub_Company.Sub_Company_ID ASC
END

GO

