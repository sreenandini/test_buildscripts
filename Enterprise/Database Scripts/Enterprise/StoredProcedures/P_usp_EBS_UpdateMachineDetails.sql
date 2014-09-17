/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 03/05/2014 8:42:27 PM
 ************************************************************/

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_EBS_UpdateMachineDetails]    Script Date: 03/05/2014 16:43:13 ******/
IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_EBS_UpdateMachineDetails]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_EBS_UpdateMachineDetails] 
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_EBS_UpdateMachineDetails]    Script Date: 03/05/2014 16:43:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
--[usp_EBS_UpdateMachineDetails] 3779

--Select * from Ebs_Export_History

CREATE PROCEDURE [dbo].[usp_EBS_UpdateMachineDetails](@MachineID INT)
AS
BEGIN
	DECLARE @Machine  TABLE (
	            MachineID VARCHAR(50),
	            Area Varchar(50),
	            MachineType VARCHAR(50),
	            Bank VARCHAR(50),
	            [GameName] VARCHAR(50),
	            DenominationID VARCHAR(50),
	            ManufacturerId VARCHAR(50),
	            CasinoID VARCHAR(50),
	            ZoneID VARCHAR(50),
	            IsActive BIT,
	            MachineLoc VARCHAR(50)
	        )
	
	DECLARE @Value    XML	
	
	INSERT INTO @Machine
	EXEC [dbo].[rsp_EBS_GetMachines] @MachineIDActual = @MachineID
	
	DECLARE @Site_code VARCHAR(50)
	
	SELECT @Site_code = Site_code FROM Site JOIN  @Machine
	ON CasinoID = Site.Site_Code
	 
	SELECT @Value = (
	           SELECT *
	           FROM   @Machine 
	                  FOR XML PATH('Machine'),
	                  TYPE,
	                  ELEMENTS,
	                  ROOT('Machines')
	       )
	
	IF ISNULL(@Site_code,'')<>''
	BEGIN
		EXEC [dbo].[usp_EBS_InsertExportHistory] @EH_Type = 'MACHINE',
			 @EH_Value = @Value, @EH_SiteCode = @Site_code
	END
END
GO


