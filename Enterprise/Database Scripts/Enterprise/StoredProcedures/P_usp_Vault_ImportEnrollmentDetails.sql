USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_Vault_ImportEnrollmentDetails]    Script Date: 10/10/2013 16:07:09 ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
 
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Vault_ImportEnrollmentDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Vault_ImportEnrollmentDetails]
GO

CREATE PROCEDURE [dbo].[usp_Vault_ImportEnrollmentDetails]    
@Xml XML = NULL,      
@IsSuccess INT OUTPUT      
AS      

BEGIN
	
	DECLARE @dochandle INT
	SET @IsSuccess = 0
	EXEC sp_xml_preparedocument @docHandle OUTPUT,      
      @Xml         
      
	DECLARE @tempEnroll TABLE
		(
			Site_ID INT,
			NGAInstallationID INT,
			NGADeviceID INT,
			StartDate DATETIME,
			StartUser INT
		)
		
	INSERT INTO @tempEnroll
	(
		Site_ID,
		NGAInstallationID,
		NGADeviceID,
		StartDate,
		StartUser
	)
	SELECT TNGAI.Site_ID,
		temp.HQ_Installation_No,
		temp.HQ_NGADevice_ID,
		temp.[Start_Date],
		temp.[Start_User]
	FROM OPENXML(@dochandle, '/VaultEnroll/VaultEnrollment_Info',2) 
	WITH
	(
		HQ_Installation_No  INT 'HQ_Installation_No',
		HQ_NGADevice_ID INT 'HQ_NGADevice_ID',
		[Start_Date] DATETIME 'Start_Date',
		[Start_User] INT 'Start_User'			
	)temp
	INNER JOIN dbo.tNGAInstallations TNGAI WITH(NOLOCK)
	ON HQ_Installation_No = TNGAI.Installation_No
		
	EXEC sp_xml_removedocument @dochandle      
 
    BEGIN TRAN 
   
		DECLARE @NGA_ID INT 	
		UPDATE TNGAI 
		SET 
			TNGAI.[Start_Date] = temp.StartDate,
			TNGAI.[Start_User] = temp.StartUser	,
			@NGA_ID= TNGAI.NGADevice_ID
		FROM tNGAInstallations TNGAI
		INNER JOIN @tempEnroll temp ON TNGAI.Installation_No = temp.NGAInstallationID    
	 
		IF @@Error<>0      
		BEGIN      
			GOTO ERR_HANDLE -- failed while updating the records in the Vault table      
		END  
	    
		UPDATE TVD
		SET 
			TVD.Site_ID = temp.Site_ID,
			tvd.Active=1
		FROM tVault_Devices tVD 
		INNER JOIN @tempEnroll temp ON temp.NGADeviceID = tVD.NGADevice_ID 
	 
		 IF @@Error<>0      
		 BEGIN       
			GOTO ERR_HANDLE -- failed while updating the records in the Vault table      
		 END    

		 SET @IsSuccess = 0 
		 
	 COMMIT TRAN   
	 RETURN 
	 
	ERR_HANDLE:
		ROLLBACK TRAN 
		SET @IsSuccess = -1 
END
GO
