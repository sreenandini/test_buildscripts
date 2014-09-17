USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ecUpdateSubCompanyAdminDefaults]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ecUpdateSubCompanyAdminDefaults]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dbo.usp_ecUpdateSubCompanyAdminDefaults
	@Company_ID INT,
	@bAccess_Key_ID BIT,
	@bCompany_Jackpot BIT,
	@bCompany_Percentage_Payout BIT,
	@bCompany_Price_Per_Play BIT,
	@bStaff_ID BIT,
	@bTerms_Group_ID BIT,
	@Value BIGINT,
	@CascadeType INT, --VALUES 0=CASCADE_NONE , 1=CASCADE_DEFAULT , 2=CASCADE_ALL
	@Level INT, --	@Level INT -- 1 Company , 2 Sub_Company
	@IsDefault BIT,
	@Audit_User_ID INT,
	@Audit_User_Name VARCHAR(50),
	@AuditOperationType VARCHAR(100),
	@Audit_ModuleName VARCHAR(50),
	@SubCompanyID  INT = 0
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
	DECLARE @Audit_ModuleID INT 
	SET @Audit_ModuleID = 507
	IF @SubCompanyID = 0
		SET @SubCompanyID = NULL
	DECLARE @Audit TABLE(
	            ID INT,
	            FieldName VARCHAR(100),
	            TYPE VARCHAR(200),
	            Cascade_Level_AuditDesc VARCHAR(500),
	            NewValue VARCHAR(100),
	             OldValue VARCHAR(100)
	        )
	
	
	SELECT @CascadeType CascadeType
	IF @CascadeType = 2
	    SET @CascadeType = NULL
	
	IF @bAccess_Key_ID = 1
	BEGIN
	    IF @CascadeType = 0
	       AND @Level = 1
	    BEGIN
	        UPDATE Sub_Company
	        SET    Access_Key_ID_default = 0
	               OUTPUT INSERTED.Sub_Company_ID,
	               'Access_Key_ID_default',
	               'SUBCOMPANY',
	               'Cascade.Sub Company',
	               INSERTED.Access_Key_ID_default,
	               DELETED.Access_Key_ID_default
	               INTO @Audit
	        WHERE  Company_ID = @Company_ID AND Sub_Company.Sub_Company_ID = ISNULL(@SubCompanyID, Sub_Company.Sub_Company_ID)
	               AND Access_Key_ID_default = 1
	    END
	    ELSE
	    BEGIN
	        UPDATE Sub_Company
	        SET    Access_Key_ID = @Value,
	               Access_Key_ID_default = @IsDefault
	               OUTPUT INSERTED.Sub_Company_ID,
	               'Access_Key_ID',
	               'SUBCOMPANY',
	               'Cascade.Sub Company',
	               INSERTED.Access_Key_ID,
	               DELETED.Access_Key_ID
	               INTO @Audit
	        WHERE  Company_ID = @Company_ID AND Sub_Company.Sub_Company_ID = ISNULL(@SubCompanyID, Sub_Company.Sub_Company_ID)
	               AND (@Level = 2 OR Access_Key_ID_default = ISNULL(@CascadeType, Access_Key_ID_default))
	        
	        UPDATE S
	        SET    s.Access_Key_ID = @Value,
	               s.Access_Key_ID_Default = @IsDefault
	               OUTPUT INSERTED.site_ID,
	               'Access_Key_ID',
	               'SITE',
	               'Cascade.Site',
	               INSERTED.Access_Key_ID,
	               DELETED.Access_Key_ID
	               INTO @Audit
	        FROM   SITE S
	               INNER JOIN @Audit CO
	                    ON  s.Sub_Company_ID = co.id
	                    AND TYPE = 'SUBCOMPANY'
	        WHERE  S.Access_Key_ID_Default = ISNULL(@CascadeType, S.Access_Key_ID_default) OR (@Level = 2 AND @CascadeType = 0 AND S.Access_Key_ID_Default = 1)
	        
	        
	        UPDATE BP
	        SET    bp.Access_Key_ID = @Value,
	               bp.Access_Key_ID_Default = @IsDefault
	               OUTPUT INSERTED.Bar_Position_ID,
	               'Access_Key_ID',
	               'BARPOSITION',
	               'Cascade.Position',
	               INSERTED.Access_Key_ID,
	               DELETED.Access_Key_ID
	               INTO @Audit
	        FROM   BAR_POSITION BP
	               INNER JOIN @Audit S
	                    ON  bp.Site_ID = s.ID
	                    AND TYPE = 'SITE'
			Where @Level =1 OR (@Level = 2 AND @CascadeType != 0)
	    END
	END
	
	
	IF @bCompany_Jackpot = 1
	BEGIN
	    IF @CascadeType = 0
	       AND @Level = 1
	    BEGIN
	        UPDATE Sub_Company
	        SET    sub_Company_Jackpot_default = 0
	               OUTPUT INSERTED.Sub_Company_ID,
	               'sub_Company_Jackpot_default',
	               'SUBCOMPANY',
	               'Cascade.Sub Company',
	               INSERTED.sub_Company_Jackpot_default,
	               DELETED.sub_Company_Jackpot_default
	               INTO @Audit
	        WHERE  Company_ID = @Company_ID AND Sub_Company.Sub_Company_ID = ISNULL(@SubCompanyID, Sub_Company.Sub_Company_ID)
	               AND sub_Company_Jackpot_default = 1
	    END
	    ELSE
	    BEGIN
	        UPDATE Sub_Company
	        SET    sub_Company_Jackpot = @Value,
	               sub_Company_Jackpot_default = @IsDefault
	               OUTPUT INSERTED.Sub_Company_ID,
	               'sub_Company_Jackpot',
	               'SUBCOMPANY',
	               'Cascade.Sub Company',
	               INSERTED.sub_Company_Jackpot,
	               DELETED.sub_Company_Jackpot
	               INTO @Audit
	        WHERE  Company_ID = @Company_ID AND Sub_Company.Sub_Company_ID = ISNULL(@SubCompanyID, Sub_Company.Sub_Company_ID)
	               AND (@Level = 2 OR sub_Company_Jackpot_default = ISNULL(@CascadeType, sub_Company_Jackpot_default))
	        
	        --return on cascade none
	        IF @CascadeType = 0
	            RETURN
	        
	        UPDATE S
	        SET    s.Site_Jackpot = @Value,
	               s.Site_Jackpot_Default = @IsDefault
	               OUTPUT INSERTED.site_ID,
	               'Site_Jackpot',
	               'SITE',
	               'Cascade.Site',
	               INSERTED.Site_Jackpot,
	               DELETED.Site_Jackpot
	               INTO @Audit
	        FROM   SITE S
	               INNER JOIN @Audit CO
	                    ON  s.Sub_Company_ID = co.id
	                    AND TYPE = 'SUBCOMPANY'
	        WHERE  S.Site_Jackpot_Default = ISNULL(@CascadeType, S.Site_Jackpot_Default) OR (@Level = 2 AND @CascadeType = 0 AND S.Site_Jackpot_Default = 1)
	        
	        
	        
	        UPDATE BP
	        SET    bp.Bar_Position_Jackpot = @Value,
	               bp.Bar_Position_Jackpot_Default = @IsDefault
	               OUTPUT INSERTED.Bar_Position_ID,
	               'Bar_Position_Jackpot',
	               'BARPOSITION',
	               'Cascade.Position',
	               INSERTED.Bar_Position_Jackpot,
	               DELETED.Bar_Position_Jackpot
	               INTO @Audit
	        FROM   BAR_POSITION BP
	               INNER JOIN @Audit S
	                    ON  bp.Site_ID = s.ID
	                    AND TYPE = 'SITE'
			Where @Level =1 OR (@Level = 2 AND @CascadeType != 0)
	    END
	END
	
	
	IF @bCompany_Percentage_Payout = 1
	BEGIN
	    IF @CascadeType = 0
	       AND @Level = 1
	    BEGIN
	        UPDATE Sub_Company
	        SET    Sub_Company_Percentage_Payout_default = 0
	               OUTPUT INSERTED.Sub_Company_ID,
	               'Sub_Company_Percentage_Payout_default',
	               'SUBCOMPANY',
	               'Cascade.Sub Company',
	               INSERTED.Sub_Company_Percentage_Payout_default,
	               DELETED.Sub_Company_Percentage_Payout_default
	               INTO @Audit
	        WHERE  Company_ID = @Company_ID AND Sub_Company.Sub_Company_ID = ISNULL(@SubCompanyID, Sub_Company.Sub_Company_ID)
	               AND Sub_Company_Percentage_Payout_default = 1
	    END
	    ELSE
	    BEGIN
	        UPDATE Sub_Company
	        SET    Sub_Company_Percentage_Payout = @Value,
	               Sub_Company_Percentage_Payout_default = @IsDefault
	               OUTPUT INSERTED.Sub_Company_ID,
	               'Sub_Company_Percentage_Payout',
	               'SUBCOMPANY',
	               'Cascade.Sub Company',
	               INSERTED.Sub_Company_Percentage_Payout,
	               DELETED.Sub_Company_Percentage_Payout
	               INTO @Audit
	        WHERE  Company_ID = @Company_ID AND Sub_Company.Sub_Company_ID = ISNULL(@SubCompanyID, Sub_Company.Sub_Company_ID)
	               AND (@Level = 2 OR Sub_Company_Percentage_Payout_default = ISNULL(@CascadeType, Sub_Company_Percentage_Payout_default))
	        
	        
	        UPDATE S
	        SET    s.Site_Percentage_Payout = @Value,
	               s.Site_Percentage_Payout_Default = @IsDefault
	               OUTPUT INSERTED.site_ID,
	               'Site_Percentage_Payout',
	               'SITE',
	               'Cascade.Site',
	               INSERTED.Site_Percentage_Payout,
	               DELETED.Site_Percentage_Payout
	               INTO @Audit
	        FROM   SITE S
	               INNER JOIN @Audit CO
	                    ON  s.Sub_Company_ID = co.id
	                    AND TYPE = 'SUBCOMPANY'
	        WHERE  S.Site_Percentage_Payout_Default = ISNULL(@CascadeType, S.Site_Percentage_Payout_Default) OR (@Level = 2 AND @CascadeType = 0 AND S.Site_Percentage_Payout_Default = 1)
	        
	        UPDATE BP
	        SET    bp.Bar_Position_Percentage_Payout = @Value,
	               bp.Bar_Position_Percentage_Payout_Default = 1
	               OUTPUT INSERTED.Bar_Position_ID,
	               'Bar_Position_Percentage_Payout',
	               'BARPOSITION',
	               'Cascade.Position',
	               INSERTED.Bar_Position_Percentage_Payout,
	               DELETED.Bar_Position_Percentage_Payout
	               INTO @Audit
	        FROM   BAR_POSITION BP
	               INNER JOIN @Audit S
	                    ON  bp.Site_ID = s.ID
	                    AND TYPE = 'SITE'
			Where @Level =1 OR (@Level = 2 AND @CascadeType != 0)
	    END
	END
	
	IF @bCompany_Price_Per_Play = 1
	BEGIN
	    IF @CascadeType = 0
	       AND @Level = 1
	    BEGIN
	        UPDATE Sub_Company
	        SET    Sub_Company_Price_Per_Play_default = 0
	               OUTPUT INSERTED.Sub_Company_ID,
	               'Sub_Company_Price_Per_Play_default',
	               'SUBCOMPANY',
	               'Cascade.Sub Company',
	               INSERTED.Sub_Company_Price_Per_Play_default,
	               DELETED.Sub_Company_Price_Per_Play_default
	               INTO @Audit
	        WHERE  Company_ID = @Company_ID AND Sub_Company.Sub_Company_ID = ISNULL(@SubCompanyID, Sub_Company.Sub_Company_ID)
	               -----------------------------------------------------
	               --add condition  here
	               -----------------------------------------------------
	               -----------------------------------------------------
	               -----------------------------------------------------
	               -----------------------------------------------------
	               -----------------------------------------------------
	               -----------------------------------------------------
	               -----------------------------------------------------
	    END
	    ELSE
	    BEGIN
	        UPDATE Sub_Company
	        SET    Sub_Company_Price_Per_Play = @Value,
	               Sub_Company_Price_Per_Play_default = @IsDefault
	               OUTPUT INSERTED.Sub_Company_ID,
	               'Sub_Company_Price_Per_Play',
	               'SUBCOMPANY',
	               'Cascade.Sub Company',
	               INSERTED.Sub_Company_Price_Per_Play,
	               DELETED.Sub_Company_Price_Per_Play
	               INTO @Audit
	        WHERE  Company_ID = @Company_ID AND Sub_Company.Sub_Company_ID = ISNULL(@SubCompanyID, Sub_Company.Sub_Company_ID)
	               AND (@Level = 2 OR Sub_Company_Price_Per_Play_default = ISNULL(@CascadeType, Sub_Company_Price_Per_Play_default))
	        
	        
	        UPDATE S
	        SET    s.Site_Price_Per_Play = @Value,
	               S.Site_Price_Per_Play_Default = @IsDefault
	               OUTPUT INSERTED.site_ID,
	               'Site_Price_Per_Play',
	               'SITE',
	               'Cascade.Site',
	               INSERTED.Site_Price_Per_Play,
	               DELETED.Site_Price_Per_Play
	               INTO @Audit
	        FROM   SITE S
	               INNER JOIN @Audit CO
	                    ON  s.Sub_Company_ID = co.id
	                    AND TYPE = 'SUBCOMPANY'
	        WHERE  S.Site_Price_Per_Play_Default = ISNULL(@CascadeType, S.Site_Price_Per_Play_Default) OR (@Level = 2 AND @CascadeType = 0 AND S.Site_Price_Per_Play_Default  = 1)
	        
	        UPDATE BP
	        SET    bp.Bar_Position_Price_Per_Play = @Value,
	               bp.Bar_Position_Price_Per_Play_Default = 1
	               OUTPUT INSERTED.Bar_Position_ID,
	               'Bar_Position_Price_Per_Play',
	               'BARPOSITION',
	               'Cascade.Position',
	               INSERTED.Bar_Position_Price_Per_Play,
	               DELETED.Bar_Position_Price_Per_Play
	               INTO @Audit
	        FROM   BAR_POSITION BP
	               INNER JOIN @Audit S
	                    ON  bp.Site_ID = s.ID
	                    AND TYPE = 'SITE'
			Where @Level =1 OR (@Level = 2 AND @CascadeType != 0)
	    END
	END
	
	IF @bStaff_ID = 1
	BEGIN
	    IF @CascadeType = 0
	       AND @Level = 1
	    BEGIN
	        UPDATE Sub_Company
	        SET    Staff_ID_default = 0
	               OUTPUT INSERTED.Sub_Company_ID,
	               'Staff_ID_default',
	               'SUBCOMPANY',
	               'Cascade.Sub Company',
	               INSERTED.Staff_ID_default,
	               DELETED.Staff_ID_default
	               INTO @Audit
	        WHERE  Company_ID = @Company_ID AND Sub_Company.Sub_Company_ID = ISNULL(@SubCompanyID, Sub_Company.Sub_Company_ID)
	               AND Staff_ID_default = 1
	    END
	    ELSE
	    BEGIN
	        UPDATE Sub_Company
	        SET    Staff_ID = @Value,
	               Staff_ID_default = @IsDefault
	               OUTPUT INSERTED.Sub_Company_ID,
	               'Staff_ID',
	               'SUBCOMPANY',
	               'Cascade.Sub Company',
	               INSERTED.Staff_ID,
	               DELETED.Staff_ID
	               INTO @Audit
	        WHERE  Company_ID = @Company_ID AND Sub_Company.Sub_Company_ID = ISNULL(@SubCompanyID, Sub_Company.Sub_Company_ID)
	               AND (@Level = 2 OR Staff_ID_default = ISNULL(@CascadeType, Staff_ID_default))
	        
	        UPDATE S
	        SET    s.Staff_ID = @Value,
	               S.Staff_ID_Default = @IsDefault
	               OUTPUT INSERTED.site_ID,
	               'Staff_ID',
	               'SITE',
	               'Cascade.Site',
	               INSERTED.Staff_ID,
	               DELETED.Staff_ID
	               INTO @Audit
	        FROM   SITE S
	               INNER JOIN @Audit CO
	                    ON  s.Sub_Company_ID = co.id
	                    AND TYPE = 'SUBCOMPANY'
	        WHERE  S.Staff_ID_Default = ISNULL(@CascadeType, S.Staff_ID_Default) OR (@Level = 2 AND @CascadeType = 0 AND S.Staff_ID_default = 1)
	               
	               --        UPDATE BP
	               --        SET    bp.re= @Value,
	               --               bp.Bar_Position_Staff_ID_default = 1
	               --               OUTPUT INSERTED.Bar_Position_ID,'Bar_Position_Staff_ID','BARPOSITION','Cascade.Position' INTO @Audit
	               --        FROM   BAR_POSITION BP
	               --               INNER JOIN SITE S WITH (NOLOCK)
	               --                    ON  bp.Site_ID = s.Site_ID
	               --               INNER JOIN Sub_Company CO WITH (NOLOCK)
	               --                    ON  s.Sub_Company_ID = co.Sub_Company_ID
	               --        WHERE  CO.Company_ID = @Company_ID AND Sub_Company.Sub_Company_ID = ISNULL(@SubCompanyID, Sub_Company.Sub_Company_ID)
	    END
	END
	
	IF @bTerms_Group_ID = 1
	BEGIN
	    IF @CascadeType = 0
	       AND @Level = 1
	    BEGIN
	        UPDATE Sub_Company
	        SET    Terms_Group_ID_default = 0
	               OUTPUT INSERTED.Sub_Company_ID,
	               'Terms_Group_ID_default',
	               'SUBCOMPANY',
	               'Cascade.Sub Company',
	               INSERTED.Terms_Group_ID_default,
	               DELETED.Terms_Group_ID_default
	               INTO @Audit
	        WHERE  Company_ID = @Company_ID AND Sub_Company.Sub_Company_ID = ISNULL(@SubCompanyID, Sub_Company.Sub_Company_ID)
	               AND Terms_Group_ID_default = 1
	    END
	    ELSE
	    BEGIN
	        UPDATE Sub_Company
	        SET    Terms_Group_ID = @Value,
	               Terms_Group_ID_default = @IsDefault
	               OUTPUT INSERTED.Sub_Company_ID,
	               'Terms_Group_ID',
	               'SUBCOMPANY',
	               'Cascade.Sub Company',
	               INSERTED.Terms_Group_ID,
	               DELETED.Terms_Group_ID
	               INTO @Audit
	        WHERE  Company_ID = @Company_ID AND Sub_Company.Sub_Company_ID = ISNULL(@SubCompanyID, Sub_Company.Sub_Company_ID)
	               AND (@Level = 2 OR Terms_Group_ID_default = ISNULL(@CascadeType, Terms_Group_ID_default))
	        
	        UPDATE S
	        SET    s.Terms_Group_ID = @Value,
	               S.Terms_Group_ID_Default = @IsDefault
	               OUTPUT INSERTED.site_ID,
	               'Terms_Group_ID',
	               'SITE',
	               'Cascade.Site',
	               INSERTED.Terms_Group_ID,
	               DELETED.Terms_Group_ID
	               INTO @Audit
	        FROM   SITE S
	               LEFT JOIN @Audit CO
	                    ON  s.Sub_Company_ID = co.id
	                    AND TYPE = 'SUBCOMPANY'
	        WHERE @Level =1 OR (@Level = 2 AND @CascadeType != 0)
	        
	        UPDATE BP
	        SET    BP.Terms_Group_ID = @Value,
	               BP.Terms_Group_Future_ID = @Value,
	               BP.Terms_Group_Changeover_Date = NULL,
	               BP.Terms_Group_ID_Default = 1
	               OUTPUT INSERTED.Bar_Position_ID,
	               'Terms_Group_ID',
	               'BARPOSITION',
	               'Cascade.Position',
	               INSERTED.Terms_Group_ID,
	               DELETED.Terms_Group_ID
	               INTO @Audit
	        FROM   BAR_POSITION BP
	               LEFT JOIN @Audit S
	                    ON  bp.Site_ID = s.ID
	                    AND TYPE = 'SITE'
			WHERE @Level =1 OR (@Level = 2 AND @CascadeType != 0)
	    END
	END
	
	
	--AUDITING 
	IF (@CascadeType = 0)
	BEGIN
	    --'Cascade update of sub company information - removing default   ..[Sub Company ' + CAST(INSERTED.Sub_Company_ID AS VARCHAR(20)) +' Default]: "True" --> "FALSE"',
	    
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
	           @Audit_ModuleName,
	           Cascade_Level_AuditDesc,
	           'Cascade update of ' + TYPE + 
	           ' information - removing default   ..[' + TYPE + ' ' + CAST(id AS VARCHAR(20)) 
	           + ' Default]: "True"-->"FALSE"',
	           FieldName,
	           OldValue,
	           NewValue,
	           'MODIFY',
	           ''
	    FROM   @Audit
	END
	ELSE
	BEGIN
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
	           @Audit_ModuleName,
	           Cascade_Level_AuditDesc,
	           'Cascade update for ' + [TYPE] + ' ' + CAST(id AS VARCHAR(20)) + 
	           ' Type (' +
	           CASE 
	                WHEN @CascadeType = 0 THEN 'None'
	                WHEN @CascadeType = 1 THEN 'Default'
	                WHEN @CascadeType IS NULL THEN 'All'
	           END 
	           + ' )' AS DESC1,
	           FieldName,
	           OldValue,
	           NewValue,
	           'MODIFY',
	           ''
	    FROM   @Audit
	END 
	
	
	--FOR AUDITING 
	SELECT ID,
	       Fieldname,
	       TYPE
	FROM   @Audit
	WHERE  1 = 2
END

GO

