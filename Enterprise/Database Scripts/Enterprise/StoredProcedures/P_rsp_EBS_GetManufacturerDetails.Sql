USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_EBS_GetManufacturerDetails]    Script Date: 03/03/2014 18:56:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_EBS_GetManufacturerDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_EBS_GetManufacturerDetails]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_EBS_GetManufacturerDetails]    Script Date: 03/03/2014 18:56:21 ******/
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

CREATE PROCEDURE [dbo].[rsp_EBS_GetManufacturerDetails]
(	
	@SiteCode VARCHAR(50) = null,
	@Manufacturer_ID int = null
)
AS

BEGIN
      SET NOCOUNT ON 
      SELECT Manufacturer_ID AS ManufacturerID, 
      Manufacturer_Name AS ManufacturerName,
      Manufacturer_Name AS ManufacturerValue,
      CAST(1 AS BIT) AS IsActive  
      FROM Manufacturer WITH (NOLOCK) WHERE Manufacturer_ID = COALESCE(@Manufacturer_ID,Manufacturer_ID)
END

GO


