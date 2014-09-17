USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateMenuStructure]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateMenuStructure]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].usp_UpdateMenuStructure 
(@type as int)
AS

BEGIN
	--Check the type if it is 1 then SGVI Report is there and make it as Active else if type is 2 make it DeActive
    if (@type = 1)

     BEGIN 
	   UPDATE Server_Reports_Menu_Structure SET MS_Status='Active'
       WHERE 
	   MS_Application='SGVIReports.EXE' 
     END
   else if (@type = 2)

     BEGIN 
	   UPDATE Server_Reports_Menu_Structure SET MS_Status='DeActive'
       WHERE 
	   MS_Application = 'SGVIReports.EXE' 
     END
END




GO

