USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCabinetModelType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCabinetModelType]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[rsp_GetCabinetModelType]  
AS  
BEGIN 

SELECT MT_Model_Name  FROM Model_Type MT WITH (NOLOCK)
ORDER BY MT.MT_Model_Name

END