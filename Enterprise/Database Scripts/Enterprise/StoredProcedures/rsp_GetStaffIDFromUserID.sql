USE [ENTERPRISE]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetStaffIDFromUserID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetStaffIDFromUserID]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetStaffIDFromUserID]
(
 @UserID INT,
 @StaffID INT OUTPUT
)
AS
BEGIN

SET NOCOUNT ON

	SELECT @StaffID = Staff_ID FROM Enterprise.dbo.Staff WHERE UserTableID = @UserID

END
GO