USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateMachineType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateMachineType]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
* Revision History  
*   
* <Name of the owner>         DateCreated       Type(Created/Modified)  Description  
  Kalaiyarasan.P              23-Nov-2012         Created               This SP is used to update Machine Type details
  --Exec  usp_UpdateMachineType                                                                   
*/  
  
CREATE PROCEDURE usp_UpdateMachineType
	@Machine_Type_ID INT OUTPUT,
	@Depreciation_Policy_ID INT,
	@Machine_Type_Code VARCHAR(50),
	@Machine_Type_Description VARCHAR(50),
	@Machine_Type_Icon_ref INT,
	@Machine_Type_Site_Icon VARCHAR(10),
	@Machine_Type_Income_Ledger_Code VARCHAR(20),
	@Machine_Type_AMEDIS_ID VARCHAR(50),
	@IsNonGamingAssetType INT
AS
BEGIN
	IF NOT EXISTS(
	       SELECT 1
	       FROM   Machine_Type 
	       WHERE  Machine_Type_ID = @Machine_Type_ID
	   )
	BEGIN
	    INSERT INTO Machine_Type
	      (
	        Depreciation_Policy_ID,
	        Machine_Type_Code,
	        Machine_Type_Description,
	        IsNonGamingAssetType,
	        Machine_Type_AMEDIS_ID,
	        Machine_Type_Income_Ledger_Code,
	        Machine_Type_Site_Icon,
	        Machine_Type_Icon_ref
	      )
	    VALUES
	      (
	        @Depreciation_Policy_ID,
	        @Machine_Type_Code,
	        @Machine_Type_Description,
	        @IsNonGamingAssetType,
	        @Machine_Type_AMEDIS_ID,
	        @Machine_Type_Income_Ledger_Code,
	        @Machine_Type_Site_Icon,
	        @Machine_Type_Icon_ref
	      )
	      
	      
	      INSERT INTO MeterAnalysis.dbo.Machine_Type
	      (
	        Depreciation_Policy_ID,
	        Machine_Type_Code,
	        Machine_Type_Description,
	        IsNonGamingAssetType,
	        Machine_Type_AMEDIS_ID,
	        Machine_Type_Income_Ledger_Code,
	        Machine_Type_Site_Icon,
	        Machine_Type_Icon_ref
	      )
	    VALUES
	      (
	        @Depreciation_Policy_ID,
	        @Machine_Type_Code,
	        @Machine_Type_Description,
	        @IsNonGamingAssetType,
	        @Machine_Type_AMEDIS_ID,
	        @Machine_Type_Income_Ledger_Code,
	        @Machine_Type_Site_Icon,
	        @Machine_Type_Icon_ref
	      )
	      
	    SELECT @Machine_Type_ID=SCOPE_IDENTITY()
	   
	END
	ELSE
	BEGIN
	    UPDATE Machine_Type
	    SET    Depreciation_Policy_ID = @Depreciation_Policy_ID,
	           Machine_Type_Code = @Machine_Type_Code,
	           Machine_Type_Description = @Machine_Type_Description,
	           IsNonGamingAssetType = @IsNonGamingAssetType,
	           Machine_Type_AMEDIS_ID = @Machine_Type_AMEDIS_ID,
	           Machine_Type_Income_Ledger_Code =@Machine_Type_Income_Ledger_Code,
	           Machine_Type_Site_Icon = @Machine_Type_Site_Icon,
	           Machine_Type_Icon_ref = @Machine_Type_Icon_ref
	    WHERE  Machine_Type_ID = @Machine_Type_ID
	    
	    UPDATE MeterAnalysis.dbo.Machine_Type
	    SET    Depreciation_Policy_ID = @Depreciation_Policy_ID,
	           Machine_Type_Code = @Machine_Type_Code,
	           Machine_Type_Description = @Machine_Type_Description,
	           IsNonGamingAssetType = @IsNonGamingAssetType,
	           Machine_Type_AMEDIS_ID = @Machine_Type_AMEDIS_ID,
	           Machine_Type_Income_Ledger_Code =@Machine_Type_Income_Ledger_Code,
	           Machine_Type_Site_Icon = @Machine_Type_Site_Icon,
	           Machine_Type_Icon_ref = @Machine_Type_Icon_ref
	    WHERE  Machine_Type_ID = @Machine_Type_ID
	    
	END
END

GO

