USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateMachineStatusToInStock]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateMachineStatusToInStock]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ********************************************************************************************************
-- StoredProcedure usp_UpdateMachineStatusToInStock
-- --------------------------------------------------------------------------------------------------------
--
-- To update the Machine Status to In Stock
--
-- --------------------------------------------------------------------------------------------------------
-- Revision History 
-- 
-- 07/10/2010		Yoganandh P		Created
-- 15/10/2010		Yoganandh P		Insert Installation Removal record for AAMS
***********************************************************************************************************/

CREATE PROCEDURE usp_UpdateMachineStatusToInStock
(	
	@Machine_ID INT
)
AS
BEGIN
	UPDATE 
		Machine 
	SET 
		Machine_Status_Flag = 0, Machine_Transit_Site_Code = NULL
	WHERE 
		Machine_Id = @Machine_ID

	--Insert a record for AAMS
	EXEC dbo.usp_InsertBMCBASExportRecord @Machine_ID, 3, 306, NULL, 4, 'Installation Removal.'        
END


GO

