USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SL_InsertorUpdateRule]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_SL_InsertorUpdateRule]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- usp_SL_InsertorUpdateRule
-- -----------------------------------------------------------------    
--    
-- To Insert/Update rule information for site licensing      
--     
-- -----------------------------------------------------------------        
-- Revision History           
--           
-- 28/03/12 Venkatesan Haridass Created          
-- 
-- =================================================================  
CREATE PROCEDURE [dbo].[usp_SL_InsertorUpdateRule](
	@RuleName VARCHAR(30),
	@ValidationRequired BIT,
	@LockSite BIT,
	@DisableGames BIT,	
	@WarningOnly BIT,
	@AlertRequired BIT,
	@StaffID INT)
AS
BEGIN
	SET NOCOUNT ON

	SET @RuleName = LTRIM(RTRIM(@RuleName))

	IF NOT EXISTS (SELECT 1 FROM SL_Rules WITH (NOLOCK) WHERE RuleName = @RuleName )	
		BEGIN
			INSERT INTO [dbo].[SL_Rules]
					   ([RuleName]
					   ,[ValidationRequired]
					   ,[LockSite]
					   ,[DisableGames]
					   ,[WarningOnly]
					   ,[AlertRequired]
					   ,[CreatedStaffID]
					   ,[ModifiedStaffID]
					   ,[CreatedDateTime]	
					   ,[UpdatedDateTime])
				 VALUES
					   (@RuleName
						,@ValidationRequired
						,@LockSite
						,@DisableGames
						,@WarningOnly
						,@AlertRequired
						,@StaffID
						,@StaffID
						,GETDATE()
						,GETDATE())
				
				IF @@ERROR <> 0
					RETURN 1
				ELSE
					GOTO Success
		END
    ELSE
		BEGIN
	
			--DECLARE @NeedtoExportDisableGames BIT
			--SET @NeedtoExportDisableGames = 0

			--IF NOT EXISTS(SELECT 1 FROM [dbo].[SL_Rules] WHERE [DisableGames] = @DisableGames AND RuleName = @RuleName)
			--	SET @NeedtoExportDisableGames = 1

			UPDATE [dbo].[SL_Rules] 
			SET 
				 [ValidationRequired] = @ValidationRequired
				,[LockSite] = @LockSite
				,[DisableGames] = @DisableGames
				,[WarningOnly] = @WarningOnly
				,[AlertRequired] = @AlertRequired
				,[UpdatedDateTime] = GETDATE()
				,[ModifiedStaffID] = @StaffID
			WHERE 
				LTRIM(RTRIM(RuleName)) = @RuleName
	
			IF @@ROWCOUNT > 0
			BEGIN
				--Send the info to all Sites.  
				--DECLARE @Count INT  
				--DECLARE @Current INT  
				  
				DECLARE @LicenseInfo Table(  
				ID INT IDENTITY(1,1),
				[LicenseInfoID] INT,
				Site_Code Varchar (50)
				)
			   
				INSERT INTO @LicenseInfo   
				SELECT    
					L.[LicenseInfoID],  
					S.[Site_Code]  
				FROM [dbo].[SL_LicenseInfo] L 
				INNER JOIN [dbo].[Site] S
				ON L.Site_ID = S.Site_id
				INNER JOIN [dbo].[SL_Rules] R
				ON L.RuleID = R.RuleID
				WHERE R.RuleName = @RuleName 

				INSERT INTO Export_History (EH_Date, EH_Reference1, EH_Type, EH_Site_Code)  
					SELECT GETDATE(), MAX([LicenseInfoID]), 'SITELICENSING', Site_Code FROM @LicenseInfo GROUP BY Site_Code  

				
					IF @@ERROR <> 0
						RETURN 1
					ELSE
						GOTO Success

			END
		
		END

		--END

Success:
	RETURN 0

END

GO

