USE [Enterprise]
GO

IF EXISTS (
       SELECT TOP 1 1
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[esp_InsertEmployeeCardDetails]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[esp_InsertEmployeeCardDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
* **********************************************************************************************************
* Revision History
* 
* Store the Employee Card Details	
* Anuradha		Created		07/06/2012		
* Aishwarrya	Modified	16/09/2014 --> Modified Export history related code
	
* 
* **********************************************************************************************************
*/
CREATE PROCEDURE [dbo].[esp_InsertEmployeeCardDetails](
    @EmpCardNumber  VARCHAR(20),
    @EmployeeName   VARCHAR(50),
    @UserID         INT,
    @IsActive       BIT,
    @CreatedBy      VARCHAR(50),
    @Result         INT = 0 OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON       
	
	DECLARE @PrevMasterCardStatus BIT 	
	
	IF NOT EXISTS (
	       SELECT TOP 1 1
	       FROM   dbo.tblEmployeeCardDetails tecd WITH(NOLOCK)
	       WHERE  tecd.EmployeeCardNumber = @EmpCardNumber
	   )
	BEGIN
	    INSERT INTO [Enterprise].[dbo].[tblEmployeeCardDetails]
	      (
	        [EmployeeCardNumber],
	        [IsActive],
	        [CreatedOn],
	        CreatedBy
	      )
	    VALUES
	      (
	        @EmpCardNumber,
	        @IsActive,
	        GETDATE(),
	        @CreatedBy
	      )
	    
	    SET @Result = 1
	END
	ELSE
	BEGIN
	    UPDATE dbo.tblEmployeeCardDetails
	    SET    -- EmpID = ? -- this column value is auto-generated,        	                   
	           IsActive = @IsActive,
	           EmployeeName = @EmployeeName,
	           UserID = @UserID,
	           LastModifedDateTime = GETDATE()
	    WHERE  EmployeeCardNumber = @EmpCardNumber   
	    
	    UPDATE dbo.[User]
	    SET    EmpCardNumber = @EmpCardNumber,
	           IsActiveCard = @IsActive
	    WHERE  SecurityUserID = @UserID
	    
	    
	    SELECT site_code INTO #Site
	    FROM   SITE S
	           INNER JOIN dbo.UserSite_lnk usl WITH(NOLOCK)
	                ON  usl.SiteID = s.Site_ID
	           INNER JOIN dbo.[USER] U WITH(NOLOCK)
	                ON  u.SecurityUserID = usl.SecurityUserID
	           INNER JOIN dbo.tblEmployeeCardDetails tecd WITH(NOLOCK)
	                ON  tecd.UserID = u.SecurityUserID
	    WHERE  tecd.EmployeeCardNumber = @EmpCardNumber    
	    
	    
	    INSERT INTO dbo.Export_History
	      (
	        EH_Date,
	        EH_Reference1,
	        EH_Type,
	        EH_Site_Code
	      )
	    SELECT GETDATE(),
	           @UserID,
	           'UserDetails',
	           site_code
	    FROM   #Site
	    
	    
	    SET @Result = 1
	END 
	
	DROP TABLE #Site
	RETURN @result
END
GO

