USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_DeleteTermsProfilesAndGroup]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_DeleteTermsProfilesAndGroup] 
GO

CREATE PROCEDURE [dbo].[usp_DeleteTermsProfilesAndGroup] 
(@TermsGroupID INT)
AS
BEGIN
	IF EXISTS(
	       SELECT 1
	       FROM   dbo.Terms_Profile
	       WHERE  Terms_Group_ID = @TermsGroupID
	   )
	BEGIN
	    DELETE 
	    FROM   dbo.Terms_Profile
	    WHERE  Terms_Group_ID = @TermsGroupID
	END
	
	IF EXISTS(
	       SELECT 1
	       FROM   dbo.Terms_Group
	       WHERE  Terms_Group_ID = @TermsGroupID
	   )
	BEGIN
	    DELETE 
	    FROM   dbo.Terms_Group
	    WHERE  Terms_Group_ID = @TermsGroupID
	END
END
GO
