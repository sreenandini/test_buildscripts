USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDefaultComponentTypes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDefaultComponentTypes]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================    
-- OUTPUT --Select Component types -- exec rsp_GetDefaultComponentTypes     
-- Revision History    
-- M Senthil 28/05/2010  Created    
-- ======================================================================= 



CREATE PROCEDURE rsp_GetDefaultComponentTypes  
AS  
SELECT CCT_Code,CCT_Name FROM CV_Component_Types
ORDER BY CCT_Name


GO

