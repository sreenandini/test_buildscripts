USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetManufacturerDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetManufacturerDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*****************************************************************************************************
DESCRIPTION : Gets  the Manufacturer Details  
CREATED DATE: 25 May 2012
CREATED BY: Lekha
MODULE            : Manufacturer      
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR                              DESCRIPTON                                                        MODIFIED DATE
------------------------------------------------------------------------------------------------------
                                                              
*****************************************************************************************************/

CREATE PROCEDURE dbo.rsp_GetManufacturerDetails
      @Manufacturer_ID int = null
AS

BEGIN
      SET NOCOUNT ON 
      SELECT * FROM Manufacturer WITH (NOLOCK) WHERE Manufacturer_ID = COALESCE(@Manufacturer_ID,Manufacturer_ID)
END

GO

