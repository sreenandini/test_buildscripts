USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAAMSAssetDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAAMSAssetDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dbo.rsp_GetAAMSAssetDetails

AS

SELECT Machine_ID, Machine_Stock_No, Machine_MAC_Address FROM Machine

GO

