USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetLstMachineType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetLstMachineType]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].rsp_GetLstMachineType

AS
BEGIN

SELECT Machine_Type_ID, Machine_Type_Code+' '+ Machine_Type_Description+ ' '+ Machine_Type_AMEDIS_ID AS Machine_Type_Code, Machine_Type_Description,Machine_Type_AMEDIS_ID  FROM Machine_Type ORDER BY Machine_Type_Code
END


GO

