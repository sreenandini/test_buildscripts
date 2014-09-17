USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_InsertTermsProfiles]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_InsertTermsProfiles] 
GO

CREATE PROCEDURE [dbo].[usp_InsertTermsProfiles]
(@TermGroupID INT, @MachineID INT)
AS
BEGIN
	IF NOT EXISTS(
	       SELECT 1
	       FROM   dbo.Terms_Profile
	       WHERE  Terms_Group_ID = @TermGroupID
	       AND Machine_Type_ID = @MachineID
	   )
	BEGIN
	    INSERT INTO dbo.Terms_Profile
	      (
	        Terms_Group_ID,
	        Machine_Type_ID
	      )
	    VALUES
	      (
	        @TermGroupID,
	        @MachineID
	      )
	END
END
GO
