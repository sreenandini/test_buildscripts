USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateOperatorDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateOperatorDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_UpdateOperatorDetails(
    @Operator_ID                AS INT,
    @Calendar_ID                AS INT,
    @Operator_Name              AS VARCHAR(50),
    @Operator_Address           AS NTEXT,
    @Operator_PostCode          AS VARCHAR(50),
    @Operator_Depot_Phone       AS VARCHAR(15),
    @Operator_Fax               AS VARCHAR(15),
    @Operator_EMail             AS VARCHAR(100),
    @Operator_Contact           AS VARCHAR(50),
    @Operator_Invoice_Address   AS NTEXT,
    @Operator_Invoice_Postcode  AS VARCHAR(50),
    @Operator_Invoice_Name      AS VARCHAR(50),
    @Operator_Start_Date        AS VARCHAR(30),
    @Operator_End_Date          AS VARCHAR(30),
    @Operator_AMEDIS_Code       AS VARCHAR(4),
    @Operator_Logo_Reference    AS VARCHAR(50),
    @Operator_Account_Name      AS VARCHAR(32),
    @Operator_Sort_Code         AS VARCHAR(8),
    @Operator_Account_No        AS VARCHAR(12)
)
AS
BEGIN
	IF EXISTS (
	       SELECT 1
	       FROM   Operator WITH(NOLOCK)
	       WHERE  Operator_ID = @Operator_ID
	   )
	BEGIN
	    UPDATE Operator
	    SET    [Calendar_ID] = @Calendar_ID,
	           [Operator_Name] = @Operator_Name,
	           [Operator_Address] = @Operator_Address,
	           [Operator_PostCode] = @Operator_PostCode,
	           [Operator_Depot_Phone] = @Operator_Depot_Phone,
	           [Operator_Fax] = @Operator_Fax,
	           [Operator_EMail] = @Operator_EMail,
	           [Operator_Contact] = @Operator_Contact,
	           [Operator_Invoice_Address] = @Operator_Invoice_Address,
	           [Operator_Invoice_Postcode] = @Operator_Invoice_Postcode,
	           [Operator_Invoice_Name] = @Operator_Invoice_Name,
	           [Operator_Start_Date] = @Operator_Start_Date,
	           [Operator_End_Date] = @Operator_End_Date,
	           [Operator_AMEDIS_Code] = @Operator_AMEDIS_Code,
	           [Operator_Logo_Reference] = @Operator_Logo_Reference,
	           [Operator_Account_Name] = @Operator_Account_Name,
	           [Operator_Sort_Code] = @Operator_Sort_Code,
	           [Operator_Account_No] = @Operator_Account_No
	    WHERE  Operator_ID = @Operator_ID
	    
	    UPDATE MeterAnalysis.dbo.Operator
	    SET    [Calendar_ID] = @Calendar_ID,
	           [Operator_Name] = @Operator_Name,
	           [Operator_Address] = @Operator_Address,
	           [Operator_PostCode] = @Operator_PostCode,
	           [Operator_Depot_Phone] = @Operator_Depot_Phone,
	           [Operator_Fax] = @Operator_Fax,
	           [Operator_EMail] = @Operator_EMail,
	           [Operator_Contact] = @Operator_Contact,
	           [Operator_Invoice_Address] = @Operator_Invoice_Address,
	           [Operator_Invoice_Postcode] = @Operator_Invoice_Postcode,
	           [Operator_Invoice_Name] = @Operator_Invoice_Name,
	           [Operator_Start_Date] = @Operator_Start_Date,
	           [Operator_End_Date] = @Operator_End_Date,
	           [Operator_AMEDIS_Code] = @Operator_AMEDIS_Code,
	           [Operator_Logo_Reference] = @Operator_Logo_Reference,
	           [Operator_Account_Name] = @Operator_Account_Name,
	           [Operator_Sort_Code] = @Operator_Sort_Code,
	           [Operator_Account_No] = @Operator_Account_No
	    WHERE  Operator_ID = @Operator_ID
	END
	ELSE
	BEGIN
	    INSERT INTO Operator
	      (
	        Calendar_ID,
	        Operator_Name,
	        Operator_Address,
	        Operator_PostCode,
	        Operator_Depot_Phone,
	        Operator_Fax,
	        Operator_EMail,
	        Operator_Contact,
	        Operator_Invoice_Address,
	        Operator_Invoice_Postcode,
	        Operator_Invoice_Name,
	        Operator_Start_Date,
	        Operator_End_Date,
	        Operator_AMEDIS_Code,
	        Operator_Logo_Reference,
	        Operator_Account_Name,
	        Operator_Sort_Code,
	        Operator_Account_No
	      )
	    VALUES
	      (
	        @Calendar_ID,
	        @Operator_Name,
	        @Operator_Address,
	        @Operator_PostCode,
	        @Operator_Depot_Phone,
	        @Operator_Fax,
	        @Operator_EMail,
	        @Operator_Contact,
	        @Operator_Invoice_Address,
	        @Operator_Invoice_Postcode,
	        @Operator_Invoice_Name,
	        @Operator_Start_Date,
	        @Operator_End_Date,
	        @Operator_AMEDIS_Code,
	        @Operator_Logo_Reference,
	        @Operator_Account_Name,
	        @Operator_Sort_Code,
	        @Operator_Account_No
	      )
	      
	      INSERT INTO MeterAnalysis.dbo.Operator
	      (
	        Calendar_ID,
	        Operator_Name,
	        Operator_Address,
	        Operator_PostCode,
	        Operator_Depot_Phone,
	        Operator_Fax,
	        Operator_EMail,
	        Operator_Contact,
	        Operator_Invoice_Address,
	        Operator_Invoice_Postcode,
	        Operator_Invoice_Name,
	        Operator_Start_Date,
	        Operator_End_Date,
	        Operator_AMEDIS_Code,
	        Operator_Logo_Reference,
	        Operator_Account_Name,
	        Operator_Sort_Code,
	        Operator_Account_No
	      )
	    VALUES
	      (
	        @Calendar_ID,
	        @Operator_Name,
	        @Operator_Address,
	        @Operator_PostCode,
	        @Operator_Depot_Phone,
	        @Operator_Fax,
	        @Operator_EMail,
	        @Operator_Contact,
	        @Operator_Invoice_Address,
	        @Operator_Invoice_Postcode,
	        @Operator_Invoice_Name,
	        @Operator_Start_Date,
	        @Operator_End_Date,
	        @Operator_AMEDIS_Code,
	        @Operator_Logo_Reference,
	        @Operator_Account_Name,
	        @Operator_Sort_Code,
	        @Operator_Account_No
	      )
	END
END

GO

