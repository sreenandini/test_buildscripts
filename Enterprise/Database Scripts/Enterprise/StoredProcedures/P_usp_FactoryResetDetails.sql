USE [Enterprise]
GO
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'usp_FactoryResetDetails'
   )
    DROP PROCEDURE dbo.usp_FactoryResetDetails
GO

CREATE PROCEDURE dbo.usp_FactoryResetDetails
	@RemoveDetail BIT,
	@Site_Code VARCHAR(50),	
	@Status INT OUTPUT
AS
--/*****************************************************************************************************
--DESCRIPTION    :	To generates or removes reset details into tmpResetDetails based on @Site_Code
--					@RemoveDetail = 0 - Generates the common data based on @Site_Code and inserts
--									in [Factory_Reset_Details] table
--										(Called in initial stage)
--									1 - Deletes [Factory_Reset_Details] based on @Site_Code
--										(Called in final stage)
--CREATED DATE   : 
--MODULE		 : Called in usp_FactoryResetProcess for Fatory Reset
--Example		 : To Populate data
--				   =============================================
--				   EXECUTE dbo.usp_FactoryResetDetails 0,'1002',@Status OUTPUT			 
--				   GO
--=============================================
--CHANGE HISTORY :
--------------------------------------------------------------------------------------------------------
--AUTHOR						MODIFIED DATE		DESCRIPTON
--------------------------------------------------------------------------------------------------------

--*****************************************************************************************************/
BEGIN
	SET NOCOUNT ON
	
	--To Populate data into [Factory_Reset_Details]
	IF @RemoveDetail = 0
	BEGIN
	    INSERT [Factory_Reset_Details]
	    SELECT BarPos.Site_ID AS Site_ID,
	           St.Site_Code AS Site_Code,
	           Inst.Bar_Position_ID AS Position_ID,
	           BarPos.Bar_Position_Name AS Position_Name,
	           Inst.Installation_ID AS Installation_ID,
	           Inst.Machine_ID AS Machine_ID,
	           Mac.Machine_Stock_No AS Stock_No,
	           Mac.Machine_Class_ID AS Machine_Class_ID,
	           Inst.Installation_Start_Date AS Installation_Start_Date,
	           Inst.Installation_Start_Time AS Installation_Start_Time,
	           Inst.Installation_End_Date AS Installation_End_Date,
	           Inst.Installation_End_Time AS Installation_End_Time,
	           ZN.Zone_ID AS Zone_ID,
	           Col.Collection_ID AS Collection_ID,
	           Col.Batch_ID AS Batch_ID,
	           LiqDet.LiquidationId AS LiquidationId,
			   0 AS Vault_Transaction_ID,
               0 AS Vault_Drop_ID
	    FROM   Installation Inst
	           INNER JOIN Bar_Position BarPos
	                ON  BarPos.Bar_Position_ID = Inst.Bar_Position_ID
	           INNER JOIN [Machine] Mac
	                ON  Mac.Machine_ID = Inst.Machine_ID
	           INNER JOIN [Site] St
	                ON  St.Site_ID = BarPos.Site_ID
	           LEFT JOIN LiquidationDetails LiqDet
	                ON  LiqDet.SiteId = St.Site_ID
	           LEFT JOIN LiquidationShareDetails LiqShDet
	                ON  LiqShDet.LiquidationId = LiqDet.LiquidationId
	           LEFT JOIN [Collection] Col
	                ON  Col.Installation_ID = Inst.Installation_ID
	           LEFT JOIN Batch Bt
	                ON  Bt.Batch_ID = Col.Batch_ID
	           LEFT JOIN Zone Zn
	                ON  Zn.Zone_ID = BarPos.Zone_ID
	    WHERE  St.Site_Code = @Site_Code
	    ORDER BY
	           Mac.Machine_Stock_No
	    
	    --To Get NGA system
	    INSERT [Factory_Reset_Details]
	    SELECT St.Site_ID AS Site_ID,
	           St.Site_Code AS Site_Code,
	           '' AS Position_ID,
	           '' AS Position_Name,
	           '' AS Installation_ID,
	           Mac.Machine_ID AS Machine_ID,
	           Mac.Machine_Stock_No AS Stock_No,
	           '' AS Machine_Class_ID,
	           '' AS Installation_Start_Date,
	           '' AS Installation_Start_Time,
	           '' AS Installation_End_Date,
	           '' AS Installation_End_Time,
	           '' AS Zone_ID,
	           '' AS Collection_ID,
	           '' AS Batch_ID,
	           '' AS LiquidationId,
			   0 AS Vault_Transaction_ID,
               0 AS Vault_Drop_ID
	    FROM   [Site] St
	           INNER JOIN [Machine] Mac
	                ON  St.NGA_Machine_ID = Mac.Machine_ID
	    WHERE  St.Site_Code = @Site_Code
	    
	    --To Get Vault TransactionID
	    INSERT [Factory_Reset_Details]  
	    SELECT St.Site_ID AS Site_ID,  
               St.Site_Code AS Site_Code,  
               '' AS Position_ID,  
               '' AS Position_Name,  
               '' AS Installation_ID,  
               '' AS Machine_ID,  
               '' AS Stock_No,  
               '' AS Machine_Class_ID,  
               '' AS Installation_Start_Date,  
               '' AS Installation_Start_Time,  
               '' AS Installation_End_Date,  
               '' AS Installation_End_Time,  
               '' AS Zone_ID,  
               '' AS Collection_ID,  
               '' AS Batch_ID,  
               '' AS LiquidationId,
               Vc.Transaction_ID AS Vault_Transaction_ID,
               0 AS Vault_Drop_ID
	    FROM   [Site] St  
            INNER JOIN tvault_Transactions Vt
                 ON  Vt.site_id= St.Site_ID
            INNER JOIN tVault_CassetteTransactions Vc
				 ON Vc.Transaction_ID = Vt.Transactions_ID
	
		--To Get Vault Drop_ID		
	    INSERT [Factory_Reset_Details]  
	    SELECT St.Site_ID AS Site_ID,  
               St.Site_Code AS Site_Code,  
               '' AS Position_ID,  
               '' AS Position_Name,  
               '' AS Installation_ID,  
               '' AS Machine_ID,  
               '' AS Stock_No,  
               '' AS Machine_Class_ID,  
               '' AS Installation_Start_Date,  
               '' AS Installation_Start_Time,  
               '' AS Installation_End_Date,  
               '' AS Installation_End_Time,  
               '' AS Zone_ID,  
               '' AS Collection_ID,  
               '' AS Batch_ID,  
               '' AS LiquidationId,
               0 AS Vault_Transaction_ID,
               Vd.Drop_ID AS Vault_Drop_ID
	    FROM   [Site] St  
            INNER JOIN tVault_Drops Vd ON Vd.Site_ID = St.Site_ID
            
	    IF @@ERROR <> 0
	        SET @Status = -1
	    ELSE
	        SET @Status = 0
	END
	--To delete populates data from [Factory_Reset_Details]
	ELSE
	BEGIN
	    IF EXISTS (
	           SELECT 1
	           FROM   sys.objects
	           WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[Factory_Reset_Details]')
	                  AND TYPE IN (N'U')
	       )
	    BEGIN
	    	BEGIN TRY	    		
	    		DELETE 
				FROM   Factory_Reset_Details
				WHERE  Site_Code = @Site_Code
				SET @Status = 0
	    	END TRY
	    	BEGIN CATCH
	    		SELECT @Status = ERROR_NUMBER()
	    	END CATCH	            
	    END
	END
END
GO