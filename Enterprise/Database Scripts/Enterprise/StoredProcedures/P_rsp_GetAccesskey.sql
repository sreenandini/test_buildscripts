USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAccesskey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAccesskey]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetAccesskey
AS  
BEGIN 
SELECT Access_Key_ID,Access_Key_Name,Access_Key_Ref,Access_Key_Manufacturer,Access_Key_Type FROM Access_Key ORDER BY Access_Key_Name ASC
END


GO

