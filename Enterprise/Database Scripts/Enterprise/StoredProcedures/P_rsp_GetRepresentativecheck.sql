USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetRepresentativecheck]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetRepresentativecheck]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetRepresentativecheck
AS  
BEGIN 

SELECT Staff_Last_Name,Staff_First_Name,Staff_ID FROM Staff WHERE Staff_IsaRepresentative = 'True' ORDER BY Staff_Last_Name ASC, Staff_First_Name ASC
END


GO

