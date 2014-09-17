USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertMachineComponentDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertMachineComponentDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------   
--  
-- Description: Insert Machine Comp Details.  
--   
-- Inputs:     
--  
-- Outputs:       
--  
-- =======================================================================  
--   
-- Revision History  
--   
-- Renjish     29-05-2010   Created  
---------------------------------------------------------------------------   
CREATE PROCEDURE dbo.usp_InsertMachineComponentDetails
	@MachineID AS INT,
	@CompTypeID AS INT,
	@CompDetID AS INT	
AS     
  
BEGIN  
  
	DECLARE @MachSerNo VARCHAR(50)
	SELECT @MachSerNo = Machine_Manufacturers_Serial_No FROM dbo.Machine WHERE Machine_ID = @MachineID

	IF ISNULL(@MachSerNo, '') <> ''
	BEGIN
		IF EXISTS(SELECT CVMCD_CCD_ID FROM dbo.CV_Machine_Component_Details WHERE CVMCD_Machine_Serial_No = @MachSerNo AND
		CVMCD_CCT_Code = @CompTypeID)
		BEGIN
			UPDATE dbo.CV_Machine_Component_Details SET CVMCD_CCD_ID = @CompDetID
			WHERE CVMCD_Machine_Serial_No = @MachSerNo AND CVMCD_CCT_Code = @CompTypeID
		END
		ELSE
		BEGIN
			INSERT INTO dbo.CV_Machine_Component_Details(CVMCD_Machine_Serial_No, CVMCD_CCT_Code, CVMCD_CCD_ID)
			VALUES(@MachSerNo, @CompTypeID, @CompDetID)
		END
	END
END


GO

