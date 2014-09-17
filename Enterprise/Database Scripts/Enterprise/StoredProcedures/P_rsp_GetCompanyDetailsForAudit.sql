USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCompanyDetailsForAudit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCompanyDetailsForAudit]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =======================================================================      
-- OUTPUT --Get Company details 
--To exec
-- Revision History      
-- =======================================================================   
create PROCEDURE rsp_GetCompanyDetailsForAudit  
(   
 @company    INT = 0  ,
 @CompanyName  VARCHAR(50) OUTPUT
)  
AS  
BEGIN  
  
  
  
 SELECT @CompanyName=Company_Name  
 FROM Company   
 WHERE Company_ID=@company 
   
END


GO

