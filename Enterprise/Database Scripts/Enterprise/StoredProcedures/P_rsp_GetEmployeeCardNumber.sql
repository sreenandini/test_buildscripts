USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetEmployeeCardNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetEmployeeCardNumber]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetEmployeeCardNumber]
AS
BEGIN

SELECT 
DISTINCT (CAST (EmployeeCardNumber as Varchar(20))) as EmployeeCardNumber
FROM
tblEmployeeCardDetails

END

--exec rsp_GetEmployeeCardNumber

GO

