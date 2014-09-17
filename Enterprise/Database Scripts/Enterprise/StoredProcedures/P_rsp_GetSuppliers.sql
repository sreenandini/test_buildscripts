USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSuppliers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSuppliers]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================    
-- OUTPUT --Get Company details -- exec rsp_GetSites 2         
-- Revision History    
-- Vineetha Mathew 22/03/2010  Created    
-- ======================================================================= 
CREATE PROCEDURE [dbo].rsp_GetSuppliers
AS
BEGIN



	SELECT Operator_ID, Operator_Name FROM Operator

	ORDER BY  Operator_Name

END

GO

