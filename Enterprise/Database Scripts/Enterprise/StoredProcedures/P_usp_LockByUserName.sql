USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_LockByUserName]    Script Date: 12/30/2013 10:40:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_LockByUserName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_LockByUserName]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_LockByUserName]    Script Date: 12/30/2013 10:40:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



Create Procedure [dbo].[usp_LockByUserName]
(@UserName VarChar(200))
AS
BEGIN
Update [USER]
Set isLocked = 1
Where UserName =@UserName
END



GO

