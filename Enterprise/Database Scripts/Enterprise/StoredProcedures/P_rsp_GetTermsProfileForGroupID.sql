USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetTermsProfileForGroupID]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetTermsProfileForGroupID]
GO

CREATE PROCEDURE [dbo].[rsp_GetTermsProfileForGroupID] 
(@SelectedTermGroupID INT)
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT Terms_Profile.Machine_Type_ID,
	       Terms_Profile.Terms_Profile_ID,
	       Machine_Type.Machine_Type_Code
	FROM   Terms_Profile
	       LEFT JOIN Machine_Type
	            ON  Terms_Profile.Machine_Type_ID = Machine_Type.Machine_Type_ID
	WHERE  Terms_Profile.Terms_Group_ID = @SelectedTermGroupID
END
GO
