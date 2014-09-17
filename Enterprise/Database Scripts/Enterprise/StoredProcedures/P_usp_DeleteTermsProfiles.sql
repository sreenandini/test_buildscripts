USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_DeleteTermsProfiles]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_DeleteTermsProfiles] 
GO

CREATE PROCEDURE [dbo].[usp_DeleteTermsProfiles] 
(@Terms_Profile_ID INT)
AS
BEGIN
	IF EXISTS(
	       SELECT 1
	       FROM   dbo.Terms_Profile
	       WHERE  Terms_Profile_ID = @Terms_Profile_ID
	   )
	BEGIN
	    DELETE 
	    FROM   dbo.Terms_Profile
	    WHERE  Terms_Profile_ID = @Terms_Profile_ID
	END
END
GO
