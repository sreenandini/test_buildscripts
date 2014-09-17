USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetMenuStructure_CrystalVersion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetMenuStructure_CrystalVersion]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



Create Procedure [dbo].[usp_GetMenuStructure_CrystalVersion] 

@MS_Order int

AS

BEGIN
	Select * from Menu_Structure where MS_Order=@MS_Order and ltrim(rtrim(MS_Status))='Active' Order By MS_ID
END



GO

