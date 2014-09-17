USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_InsertComponentType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_InsertComponentType]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================    
-- OUTPUT --insert Component type -- exec rsp_InsertComponentType     
-- Revision History    
-- M Senthil 28/05/2010  Created    
-- ======================================================================= 


CREATE PROCEDURE rsp_InsertComponentType
(@CompName VARCHAR(50),@CompDesc VARCHAR(50))
AS
INSERT CV_Component_Types(CCT_Name,CCT_Description) VALUES(@CompName,@CompDesc)

GO

