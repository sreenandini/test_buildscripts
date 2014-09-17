USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ecUpdateCompanyAdminDefaults]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ecUpdateCompanyAdminDefaults]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


  
CREATE PROCEDURE dbo.usp_ecUpdateCompanyAdminDefaults
	@Company_ID INT,
	@bAccess_Key_ID BIT,
	@bCompany_Jackpot BIT,
	@bCompany_Percentage_Payout BIT,
	@bCompany_Price_Per_Play BIT,
	@bStaff_ID BIT,
	@bTerms_Group_ID BIT,
	@Value BIGINT,
	@CascadeType INT, --VALUES 0=CASCADE_NONE , 1=CASCADE_DEFAULT , 2=CASCADE_ALL
	@IsDefault BIT,
	@Audit_User_ID INT,
	@Audit_User_Name VARCHAR(50),
	@AuditOperationType VARCHAR(100)
AS
	/*****************************************************************************************************  
DESCRIPTION : PROC Description    
CREATED DATE: PROC CreateDate  
MODULE  : PROC used in Modules   
CHANGE HISTORY :  
------------------------------------------------------------------------------------------------------  
AUTHOR     DESCRIPTON          MODIFIED DATE  
------------------------------------------------------------------------------------------------------  

*****************************************************************************************************/  

BEGIN
	DECLARE @Audit_ModuleID INT 
	SET @Audit_ModuleID = 511
	
	DECLARE @Audit TABLE(
	            ID INT,
	            FieldName VARCHAR(300),
	            OldValue VARCHAR(200),
	            NewValue VARCHAR(200)
	        )
	
	IF @bAccess_Key_ID = 1
	BEGIN
	    UPDATE COMPANY
	    SET    Access_Key_ID = @Value
	           OUTPUT INSERTED.company_id,
	           'Access_Key_ID',
	           DELETED.Access_Key_ID,
	           INSERTED.Access_Key_ID INTO @Audit
	    WHERE  Company_ID = @Company_ID
	END  
	
	IF @bCompany_Jackpot = 1
	BEGIN
	    UPDATE COMPANY
	    SET    Company_Jackpot = @Value
	           OUTPUT INSERTED.company_id,
	           'Company_Jackpot',
	           DELETED.Company_Jackpot,
	           INSERTED.Company_Jackpot INTO @Audit
	    WHERE  Company_ID = @Company_ID
	END  
	
	IF @bCompany_Percentage_Payout = 1
	BEGIN
	    UPDATE COMPANY
	    SET    Company_Percentage_Payout = @Value
	           OUTPUT INSERTED.company_id,
	           'Company_Percentage_Payout',
	           DELETED.Company_Percentage_Payout,
	           INSERTED.Company_Percentage_Payout INTO @Audit
	    WHERE  Company_ID = @Company_ID
	END  
	
	IF @bCompany_Price_Per_Play = 1
	BEGIN
	    UPDATE COMPANY
	    SET    Company_Price_Per_Play = @Value
	           OUTPUT INSERTED.company_id,
	           'Company_Price_Per_Play',
	           DELETED.Company_Price_Per_Play,
	           INSERTED.Company_Price_Per_Play INTO @Audit
	    WHERE  Company_ID = @Company_ID
	END  
	
	IF @bStaff_ID = 1
	BEGIN
	    UPDATE COMPANY
	    SET    Staff_ID = @Value
	           OUTPUT INSERTED.company_id,
	           'Staff_ID',
	           DELETED.Staff_ID,
	           INSERTED.Staff_ID INTO @Audit
	    WHERE  Company_ID = @Company_ID
	END  
	
	IF @bTerms_Group_ID = 1
	BEGIN
	    UPDATE COMPANY
	    SET    Terms_Group_ID = @Value
	           OUTPUT INSERTED.company_id,
	           'Terms_Group_ID',
	           DELETED.Terms_Group_ID,
	           INSERTED.Terms_Group_ID INTO @Audit
	    WHERE  Company_ID = @Company_ID
	END 
	
	
	INSERT INTO Audit_History
	  (
	    Audit_Date,
	    Audit_User_ID,
	    Audit_User_Name,
	    Audit_Module_ID,
	    Audit_Module_Name,
	    Audit_Screen_Name,
	    Audit_Desc,
	    Audit_Field,
	    Audit_Old_Vl,
	    Audit_New_Vl,
	    Audit_Operation_Type,
	    Audit_Slot
	  )
	SELECT GETDATE(),
	       @Audit_User_ID,
	       @Audit_User_Name,
	       @Audit_ModuleID,
	       'Company Administration',
	       'Audit_Company',
	       'Cascade update to; company' + CAST(ID AS VARCHAR(20)) + ', & type ('
	       +
	       CASE 
	            WHEN @CascadeType = 0 THEN 'None'
	            WHEN @CascadeType = 1 THEN 'Default'
	            WHEN @CascadeType = 2 THEN 'All'
	       END 
	       + ')',
	       FieldName,
	       OldValue,
	       NewValue,
	       'MODIFY',
	       ''
	FROM   @Audit
	
	
	--CASCADING TO SUBCOMPANY, SITE, BARPOSITION
	EXEC usp_ecUpdateSubCompanyAdminDefaults @Company_ID,
	     @bAccess_Key_ID,
	     @bCompany_Jackpot,
	     @bCompany_Percentage_Payout,
	     @bCompany_Price_Per_Play,
	     @bStaff_ID,
	     @bTerms_Group_ID,
	     @Value,
	     @CascadeType,
	     1,
	     @IsDefault,
	     @Audit_User_ID,
	     @Audit_User_Name,
	     @AuditOperationType,
		 'Company Administration'
END  


GO

