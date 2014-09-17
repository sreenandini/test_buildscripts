USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDefaultVerificationTypes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDefaultVerificationTypes]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================    
-- OUTPUT --Select Component types -- exec rsp_GetDefaultVerificationTypes     
-- Revision History    
-- Renjish 28/05/2010  Created    
-- ======================================================================= 

CREATE PROCEDURE dbo.rsp_GetDefaultVerificationTypes  
AS  

SELECT CVT_Code, CVT_Name FROM CV_Verification_Types
ORDER BY CVT_Name


GO

