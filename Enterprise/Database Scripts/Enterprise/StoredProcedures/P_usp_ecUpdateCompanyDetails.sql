USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ecUpdateCompanyDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ecUpdateCompanyDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dbo.usp_ecUpdateCompanyDetails
	@Company_ID INT,
	@Company_Name VARCHAR(50),
	@Company_Switchboard_Phone_No VARCHAR(15),
	@Company_Address_1 VARCHAR(50),
	@Company_Address_2 VARCHAR(50),
	@Company_Address_3 VARCHAR(50),
	@Company_Address_4 VARCHAR(50),
	@Company_Address_5 VARCHAR(50),
	@Company_Postcode VARCHAR(15),
	@Company_Contact_Name VARCHAR(50),
	@Company_Contact_Phone_No VARCHAR(15),
	@Company_Contact_Email_Address VARCHAR(50),
	@Company_Invoice_Name VARCHAR(50),
	@Company_Invoice_Address NTEXT,
	@Company_Invoice_Postcode VARCHAR(15)
AS
	/*****************************************************************************************************
DESCRIPTION	: PROC Description  
CREATED DATE: PROC CreateDate
MODULE		: PROC used in Modules	
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR					DESCRIPTON										MODIFIED DATE
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/

BEGIN
	IF EXISTS (
	       SELECT '1'
	       FROM   Company WITH(NOLOCK)
	       WHERE  Company_Name = @Company_Name
	              AND Company_ID <> @Company_ID
	   )
	BEGIN
	    SELECT -1 AS CompanyID
	    RETURN
	END 
	
	IF @Company_ID <> 0
	BEGIN
	    UPDATE Company
	    SET    Company_Name = @Company_Name,
	           Company_Postcode = @Company_Postcode,
	           Company_Switchboard_Phone_No = @Company_Switchboard_Phone_No,
	           Company_Contact_Name = @Company_Contact_Name,
	           Company_Contact_Phone_No = @Company_Contact_Phone_No,
	           Company_Contact_Email_Address = @Company_Contact_Email_Address,
	           Company_Invoice_Address = @Company_Invoice_Address,
	           Company_Invoice_Postcode = @Company_Invoice_Postcode,
	           Company_Invoice_Name = @Company_Invoice_Name,
	           Company_Address_1 = @Company_Address_1,
	           Company_Address_2 = @Company_Address_2,
	           Company_Address_3 = @Company_Address_3,
	           Company_Address_4 = @Company_Address_4,
	           Company_Address_5 = @Company_Address_5
	    WHERE  Company_ID = @Company_ID
	    
	     UPDATE MeterAnalysis.dbo.Company
	    SET    Company_Name = @Company_Name,
	           Company_Postcode = @Company_Postcode,
	           Company_Switchboard_Phone_No = @Company_Switchboard_Phone_No,
	           Company_Contact_Name = @Company_Contact_Name,
	           Company_Contact_Phone_No = @Company_Contact_Phone_No,
	           Company_Contact_Email_Address = @Company_Contact_Email_Address,
	           Company_Invoice_Address = @Company_Invoice_Address,
	           Company_Invoice_Postcode = @Company_Invoice_Postcode,
	           Company_Invoice_Name = @Company_Invoice_Name,
	           Company_Address_1 = @Company_Address_1,
	           Company_Address_2 = @Company_Address_2,
	           Company_Address_3 = @Company_Address_3,
	           Company_Address_4 = @Company_Address_4,
	           Company_Address_5 = @Company_Address_5
	    WHERE  Company_ID = @Company_ID
	END
	ELSE
	BEGIN
	    INSERT INTO Company
	      (
	        Company_Name,
	        Company_Postcode,
	        Company_Switchboard_Phone_No,
	        Company_Contact_Name,
	        Company_Contact_Phone_No,
	        Company_Contact_Email_Address,
	        Company_Invoice_Address,
	        Company_Invoice_Postcode,
	        Company_Invoice_Name,
	        Company_Address_1,
	        Company_Address_2,
	        Company_Address_3,
	        Company_Address_4,
	        Company_Address_5
	      )
	    VALUES
	      (
	        @Company_Name,
	        @Company_Postcode,
	        @Company_Switchboard_Phone_No,
	        @Company_Contact_Name,
	        @Company_Contact_Phone_No,
	        @Company_Contact_Email_Address,
	        @Company_Invoice_Address,
	        @Company_Invoice_Postcode,
	        @Company_Invoice_Name,
	        @Company_Address_1,
	        @Company_Address_2,
	        @Company_Address_3,
	        @Company_Address_4,
	        @Company_Address_5
	      )
	      
	       INSERT INTO MeterAnalysis.dbo.Company
	      (
	        Company_Name,
	        Company_Postcode,
	        Company_Switchboard_Phone_No,
	        Company_Contact_Name,
	        Company_Contact_Phone_No,
	        Company_Contact_Email_Address,
	        Company_Invoice_Address,
	        Company_Invoice_Postcode,
	        Company_Invoice_Name,
	        Company_Address_1,
	        Company_Address_2,
	        Company_Address_3,
	        Company_Address_4,
	        Company_Address_5
	      )
	    VALUES
	      (
	        @Company_Name,
	        @Company_Postcode,
	        @Company_Switchboard_Phone_No,
	        @Company_Contact_Name,
	        @Company_Contact_Phone_No,
	        @Company_Contact_Email_Address,
	        @Company_Invoice_Address,
	        @Company_Invoice_Postcode,
	        @Company_Invoice_Name,
	        @Company_Address_1,
	        @Company_Address_2,
	        @Company_Address_3,
	        @Company_Address_4,
	        @Company_Address_5
	      )
	END
	
	IF (@Company_ID > 0)
	    SELECT @Company_ID AS CompanyID
	ELSE
	    SELECT CAST(SCOPE_IDENTITY() AS INT) AS CompanyID
END

GO

