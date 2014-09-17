USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateDepotDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateDepotDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_UpdateDepotDetails(
    @Depot_Name                AS VARCHAR(50),
    @Depot_Address             AS NTEXT,
    @Depot_Postcode            AS VARCHAR(10),
    @Depot_Contact_Name        AS VARCHAR(50) = NULL,
    @Supplier_ID               AS INT,
    @Depot_Reference           AS VARCHAR(20) = NULL,
    @Depot_Service             AS BIT = NULL,
    @Depot_Phone_Number        AS VARCHAR(50) = NULL,
    @Depot_Street_Number       AS VARCHAR(15) = NULL,
    @Depot_Province            AS VARCHAR(15) = NULL,
    @Depot_Municipality        AS VARCHAR(40) = NULL,
    @Depot_Cadastral_Code      AS VARCHAR(15) = NULL,
    @Depot_Area                AS INT = NULL,
    @DepotID                   AS INT,
    @Service_Area_Name         AS VARCHAR(16),
    @Service_Area_Description  AS NTEXT,
    @Service_Area_Notes        AS NTEXT,
    @Service_Area_ID           INT,
    @StaffIDs                  VARCHAR(MAX)
)
AS
BEGIN
	DECLARE @Status AS INT
	SET @Status = 0
	
	BEGIN TRAN
	
	/***********************Updating Depot Details*************************/
	BEGIN TRY
		IF EXISTS(
		       SELECT 1
		       FROM   DEPOT WITH(NOLOCK)
		       WHERE  Depot_ID = @DepotID
		   )
		BEGIN
		    UPDATE Depot
		    SET    [Depot_Name] = @Depot_Name,
		           [Depot_Address] = @Depot_Address,
		           [Depot_Postcode] = @Depot_Postcode,
		           [Depot_Contact_Name] = @Depot_Contact_Name,
		           [Supplier_ID] = @Supplier_ID,
		           [Depot_Reference] = @Depot_Reference,
		           [Depot_Service] = @Depot_Service,
		           [Depot_Phone_Number] = @Depot_Phone_Number,
		           [Depot_Street_Number] = @Depot_Street_Number,
		           [Depot_Province] = @Depot_Province,
		           [Depot_Municipality] = @Depot_Municipality,
		           [Depot_Cadastral_Code] = @Depot_Cadastral_Code,
		           [Depot_Area] = @Depot_Area
		    WHERE  Depot_ID = @DepotID
		    
		    UPDATE MeterAnalysis.dbo.Depot
		    SET    [Depot_Name] = @Depot_Name,
		           [Depot_Address] = @Depot_Address,
		           [Depot_Postcode] = @Depot_Postcode,
		           [Depot_Contact_Name] = @Depot_Contact_Name,
		           [Supplier_ID] = @Supplier_ID,
		           [Depot_Reference] = @Depot_Reference,
		           [Depot_Service] = @Depot_Service,
		           [Depot_Phone_Number] = @Depot_Phone_Number,
		           [Depot_Street_Number] = @Depot_Street_Number,
		           [Depot_Province] = @Depot_Province,
		           [Depot_Municipality] = @Depot_Municipality,
		           [Depot_Cadastral_Code] = @Depot_Cadastral_Code,
		           [Depot_Area] = @Depot_Area
		    WHERE  Depot_ID = @DepotID
		    
		    IF @@ERROR <> 0
		    BEGIN
		        SET @Status = 1
		        GOTO Err_Handle
		    END
		END
		ELSE
		BEGIN
		    INSERT INTO MeterAnalysis.dbo.Depot
		      (
		        [Depot_Name],
		        [Depot_Address],
		        [Depot_Postcode],
		        [Depot_Contact_Name],
		        [Supplier_ID],
		        [Depot_Reference],
		        [Depot_Service],
		        [Depot_Phone_Number],
		        [Depot_Street_Number],
		        [Depot_Province],
		        [Depot_Municipality],
		        [Depot_Cadastral_Code],
		        [Depot_Area]
		      )
		    VALUES
		      (
		        @Depot_Name,
		        @Depot_Address,
		        @Depot_Postcode,
		        @Depot_Contact_Name,
		        @Supplier_ID,
		        @Depot_Reference,
		        @Depot_Service,
		        @Depot_Phone_Number,
		        @Depot_Street_Number,
		        @Depot_Province,
		        @Depot_Municipality,
		        @Depot_Cadastral_Code,
		        @Depot_Area
		      )
		      
		        INSERT INTO Depot
		      (
		        [Depot_Name],
		        [Depot_Address],
		        [Depot_Postcode],
		        [Depot_Contact_Name],
		        [Supplier_ID],
		        [Depot_Reference],
		        [Depot_Service],
		        [Depot_Phone_Number],
		        [Depot_Street_Number],
		        [Depot_Province],
		        [Depot_Municipality],
		        [Depot_Cadastral_Code],
		        [Depot_Area]
		      )
		    VALUES
		      (
		        @Depot_Name,
		        @Depot_Address,
		        @Depot_Postcode,
		        @Depot_Contact_Name,
		        @Supplier_ID,
		        @Depot_Reference,
		        @Depot_Service,
		        @Depot_Phone_Number,
		        @Depot_Street_Number,
		        @Depot_Province,
		        @Depot_Municipality,
		        @Depot_Cadastral_Code,
		        @Depot_Area
		      )
		    SELECT @DepotID = SCOPE_IDENTITY()
		    IF @@ERROR <> 0
		    BEGIN
		        SET @Status = 11
		        GOTO Err_Handle
		    END
		END
	END TRY
	BEGIN CATCH
		SET @Status = -1
		GOTO Err_Handle
	END CATCH
	
	/***********************Updating ServiceAreaDetails*************************/
	BEGIN TRY
	if(@Service_Area_Name <> '')
	BEGIN
		IF EXISTS(
		       SELECT 1
		       FROM   Service_Areas WITH(NOLOCK)
		       WHERE  Service_Area_ID = @Service_Area_ID
		   )
		BEGIN
		    UPDATE Service_Areas
		    SET    [Depot_ID] = @DepotID,
		           [Service_Area_Name] = @Service_Area_Name,
		           [Service_Area_Description] = @Service_Area_Description,
		           [Service_Area_Notes] = @Service_Area_Notes
		    WHERE  Service_Area_ID = @Service_Area_ID
		    
		    IF @@ERROR <> 0
		    BEGIN
		        SET @Status = 2
		        GOTO Err_Handle
		    END
		END
		ELSE
		BEGIN
		    INSERT INTO Service_Areas
		      (
		        [Depot_ID],
		        [Service_Area_Name],
		        [Service_Area_Description],
		        [Service_Area_Notes]
		      )
		    VALUES
		      (
		        @DepotID,
		        @Service_Area_Name,
		        @Service_Area_Description,
		        @Service_Area_Notes
		      )
		    IF @@ERROR <> 0
		    BEGIN
		        SET @Status = 22
		        GOTO Err_Handle
		    END
		END
	END
	END TRY
	BEGIN CATCH
		SET @Status = -2
		GOTO Err_Handle
	END CATCH
	
	/********************Updating SiteRepresentative Details****************************/
	BEGIN TRY
	IF(@StaffIDs <> '')
	BEGIN
		IF(@DepotID <> 0)
		BEGIN
			DELETE 
		FROM   dbo.Staff_Depot
		WHERE  Depot_ID = @DepotID 
		
		INSERT INTO dbo.Staff_Depot
		SELECT IntItem,
		       @DepotID
		FROM   dbo.Fn_GetIntTableFromStringList(@StaffIDs)
		END
		ELSE
			BEGIN
				DELETE 
		FROM   dbo.Staff_Depot
		WHERE  Depot_ID = @DepotID 
		
		INSERT INTO dbo.Staff_Depot
		SELECT IntItem,
		       @DepotID
		FROM   dbo.Fn_GetIntTableFromStringList(@StaffIDs)
			END
		
		
		IF @@ERROR <> 0
		BEGIN
		    SET @Status = 3
		    GOTO Err_Handle
		END
	END
	COMMIT TRAN
	END TRY
	BEGIN CATCH
		SET @Status = -3
		GOTO Err_Handle
	END CATCH
	RETURN @Status
	
	
END

Err_Handle:
ROLLBACK TRAN
RETURN @Status

GO

