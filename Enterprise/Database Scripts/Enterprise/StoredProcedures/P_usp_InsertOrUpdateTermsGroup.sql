USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_InsertOrUpdateTermsGroup]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_InsertOrUpdateTermsGroup]
GO

CREATE PROCEDURE [dbo].[usp_InsertOrUpdateTermsGroup]
(
    @TermsGroupName   VARCHAR(50),
    @TermsGroupID     INT,
    @TermsGroupIDOut  INT OUTPUT
)
AS
BEGIN
	IF @TermsGroupID IS NULL
	BEGIN
	    IF NOT EXISTS(
	           SELECT 1
	           FROM   dbo.Terms_Group
	           WHERE  Terms_Group_Name = @TermsGroupName
	       )
	    BEGIN
	        INSERT INTO dbo.Terms_Group
	          (
	            Terms_Group_Name
	          )
	        VALUES
	          (
	            @TermsGroupName
	          )		
	        SET @TermsGroupIDOut = SCOPE_IDENTITY()
	    END
	END
	ELSE
	BEGIN
	    UPDATE dbo.Terms_Group
	    SET    Terms_Group_Name = @TermsGroupName
	    WHERE  Terms_Group_ID = @TermsGroupID
	    
	    SET @TermsGroupIDOut = @TermsGroupID
	END
END
GO
