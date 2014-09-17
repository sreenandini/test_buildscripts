USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetGuardianRights]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetGuardianRights]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/****** Object:  StoredProcedure [dbo].[rsp_GetGuardianRights]    
		Script Date: 08/10/2010 17:57:02
		Created By: Kirubakar S. ******/


CREATE PROCEDURE rsp_GetGuardianRights 
@USERID int
AS
BEGIN
	SELECT HQ_GUARDIAN
	FROM
		hq_user_access HQ
	WHERE
		HQ.HQ_User_Access_ID 
		in (SELECT HQ_User_Access_ID FROM user_group UG
			WHERE
				UG.User_Group_ID in (SELECT User_Group_ID FROM STAFF ST
									WHERE
										ST.UserTableID=@USERID))
END


GO

