USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAuditEnterpriseModules]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAuditEnterpriseModules]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------                           
--                          
-- Description: Get the data from Audit_Modules table 
--                          
-- Inputs:      NONE                    
--                          
-- Outputs:     Audit Modules
--                          

-- =======================================================================                          
--                           
-- Revision History                          
--                           
-- P SaravanaKumar     28/05/2010     Created    
---------------------------------------------------------------------------                    

                             
CREATE PROCEDURE [dbo].[rsp_GetAuditEnterpriseModules]     
AS                    
BEGIN    

	SELECT 
	DISTINCT Audit_Module_Name, Audit_Module_ID 
	FROM Audit_Modules 
	ORDER BY Audit_Module_Name ASC

END
GO

