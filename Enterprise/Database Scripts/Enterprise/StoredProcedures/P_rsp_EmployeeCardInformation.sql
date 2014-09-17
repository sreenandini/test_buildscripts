USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_EmployeeCardInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_EmployeeCardInformation]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
* ************************************************************************************************************
* Revision History
* ************************************************************************************************************
* Anuradha			Created			14 Sep 2012 
*/

--rsp_EmployeeCardInformation '123456789'

CREATE PROCEDURE rsp_EmployeeCardInformation  
(@EmpcardNo VARCHAR(20))  
AS  
BEGIN  
 SET NOCOUNT ON  
   
 SELECT CardType,EmployeeName, tecd.IsActive   
   FROM tblEmployeeCardDetails tecd   
END  

GO

