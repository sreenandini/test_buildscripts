USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetMachineIDForTemplate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetMachineIDForTemplate]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
DECLARE 
EXEC [rsp_GetMachineIDForTemplate] 'Asset'
*/

CREATE PROCEDURE [dbo].[rsp_GetMachineIDForTemplate] @TemplateName varchar(50), @Machine_ID INT OUTPUT
AS
BEGIN
	
 SELECT @Machine_ID = Machine_ID   
 FROM dbo.AssetCreationTemplate ASCT(NOLOCK)   
 INNER JOIN dbo.Machine M(NOLOCK)   
 ON M.Machine_Stock_No = ASCT.Machine_Stock_No    
 WHERE TemplateName = @TemplateName

END


GO

