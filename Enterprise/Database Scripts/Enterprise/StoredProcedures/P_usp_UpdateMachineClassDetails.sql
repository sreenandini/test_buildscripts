USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateMachineClassDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateMachineClassDetails]
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
  Kalaiyarasan.P              27-NOV-2012         Created               This SP is used to Update Machine Class details
  --Exec  usp_UpdateMachineClassDetails                                                                     
*/  
  
CREATE PROCEDURE usp_UpdateMachineClassDetails
	@Machine_Name VARCHAR(50),
	@Manufacturer_ID INT,
	@Machine_Class_Model_Code VARCHAR(50),
	@Machine_Class_DeListed BIT,
	@Machine_Class_Test_Machine BIT,
	@Machine_Class_Category VARCHAR(50),
	@Depreciation_Policy_ID INT,
	@Depreciation_Policy_Use_Default BIT,
	@Machine_Class_Release_Date VARCHAR(30),
	@Machine_Type_ID INT,
	@UserID INT,
	@Audit_Module_ID INT,
	@Audit_Screen_Name VARCHAR(50),
	@Machine_Class_ID INT OUTPUT
AS
BEGIN
	DECLARE @Audit_Desc        AS VARCHAR(200),	       
	        @Currentdt         DATETIME 
	        
	IF NOT EXISTS(
	       SELECT 1
	       FROM   Machine_Class
	       WHERE  Machine_Class_ID = @Machine_Class_ID
	   )
	BEGIN
	    INSERT INTO Machine_Class
	      (
	        Machine_Name,
	        Manufacturer_ID,
	        Machine_Class_Model_Code,
	        Machine_Class_DeListed,
	        Machine_Class_Test_Machine,
	        Machine_Class_Category,
	        Depreciation_Policy_ID,
	        Depreciation_Policy_Use_Default,
	        Machine_Class_Release_Date,
	        Machine_Type_ID
	      )
	    VALUES
	      (
	        @Machine_Name,
	        @Manufacturer_ID,
	        @Machine_Class_Model_Code,
	        @Machine_Class_DeListed,
	        @Machine_Class_Test_Machine,
	        @Machine_Class_Category,
	        @Depreciation_Policy_ID,
	        @Depreciation_Policy_Use_Default,
	        @Machine_Class_Release_Date,
	        @Machine_Type_ID
	      
	      )
	      
	      INSERT INTO MeterAnalysis.dbo.Machine_Class
	      (
	        Machine_Name,
	        Manufacturer_ID,
	        Machine_Class_Model_Code,
	        Machine_Class_DeListed,
	        Machine_Class_Test_Machine,
	        Machine_Class_Category,
	        Depreciation_Policy_ID,
	        Depreciation_Policy_Use_Default,
	        Machine_Class_Release_Date,
	        Machine_Type_ID
	      )
	    VALUES
	      (
	        @Machine_Name,
	        @Manufacturer_ID,
	        @Machine_Class_Model_Code,
	        @Machine_Class_DeListed,
	        @Machine_Class_Test_Machine,
	        @Machine_Class_Category,
	        @Depreciation_Policy_ID,
	        @Depreciation_Policy_Use_Default,
	        @Machine_Class_Release_Date,
	        @Machine_Type_ID
	      
	      )
	    SELECT @Machine_Class_ID = SCOPE_IDENTITY()
	    
	    SET @Audit_Desc = 'Machine Model Adminstration Added: ' + CAST(@Machine_Class_ID AS VARCHAR)
	        SET @Currentdt = GETDATE()
	        EXEC usp_CreateAuditHistory
	             @Currentdt,
	             @UserID,
	             NULL,
	             @Audit_Module_ID,
	             'Machine Class',
	             @Audit_Screen_Name,
	             @Audit_Desc,
	             NULL,
	             @Machine_Name,
	             '',
	             '',
	             'ADD'
	END
	ELSE
	BEGIN
	    UPDATE Machine_Class
	    SET    Machine_Name = @Machine_Name,
	           Manufacturer_ID = @Manufacturer_ID,
	           Machine_Class_Model_Code = @Machine_Class_Model_Code,
	           Machine_Class_DeListed = @Machine_Class_DeListed,
	           Machine_Class_Test_Machine = @Machine_Class_Test_Machine,
	           Machine_Class_Category = @Machine_Class_Category,
	           Depreciation_Policy_ID = @Depreciation_Policy_ID,
	           Depreciation_Policy_Use_Default = @Depreciation_Policy_Use_Default,
	           Machine_Class_Release_Date = @Machine_Class_Release_Date,
	           Machine_Type_ID = @Machine_Type_ID
	    WHERE  Machine_Class_ID = @Machine_Class_ID
	    
	    UPDATE MeterAnalysis.dbo.Machine_Class
	    SET    Machine_Name = @Machine_Name,
	           Manufacturer_ID = @Manufacturer_ID,
	           Machine_Class_Model_Code = @Machine_Class_Model_Code,
	           Machine_Class_DeListed = @Machine_Class_DeListed,
	           Machine_Class_Test_Machine = @Machine_Class_Test_Machine,
	           Machine_Class_Category = @Machine_Class_Category,
	           Depreciation_Policy_ID = @Depreciation_Policy_ID,
	           Depreciation_Policy_Use_Default = @Depreciation_Policy_Use_Default,
	           Machine_Class_Release_Date = @Machine_Class_Release_Date,
	           Machine_Type_ID = @Machine_Type_ID
	    WHERE  Machine_Class_ID = @Machine_Class_ID
	    
	    SET @Audit_Desc = 'Machine Class Modified: ' + CAST(@Machine_Class_ID AS VARCHAR)
	        SET @Currentdt = GETDATE()
	        EXEC usp_CreateAuditHistory 
	             @Currentdt,
	             @UserID,
	             NULL,
	             @Audit_Module_ID,
	             'Machine Class',
	             @Audit_Screen_Name,
	             @Audit_Desc,
	             NULL,
	             @Machine_Name,
	             '',
	             '',
	             'MODIFY'
	END
	IF (@@ROWCOUNT > 0)
	    BEGIN
	        --Export to Exchange
	        INSERT INTO dbo.Export_History
	          (
	            EH_Date,
	            EH_Reference1,
	            EH_Type,
	            EH_Site_Code
	          )
	        SELECT GETDATE(),
	               @Machine_Class_ID,
	               'MODEL',
	               Site_Code
	        FROM   SITE WITH(NOLOCK)
	        WHERE  Site_Enabled = 1
	               AND Sitestatus = 'FULLYCONFIGURED'
	    END
END

GO

