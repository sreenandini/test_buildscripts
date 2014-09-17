USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_VerifyManufacturerName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_VerifyManufacturerName]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_VerifyManufacturerName]
      @Manufacturer_Name VARCHAR(50) = '',
      @Manufacturer_ID INT
AS
/*****************************************************************************************************
DESCRIPTION : Checks whether Manufacturer exists or not  
CREATED DATE: 01 Jun 2012
CREATED BY: Lekha
MODULE            : Manufacturer      
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR                              DESCRIPTON                                                        MODIFIED DATE
------------------------------------------------------------------------------------------------------
                                                              
*****************************************************************************************************/

BEGIN
		SET NOCOUNT ON

		SELECT Manufacturer_Name FROM Manufacturer WHERE Manufacturer_Name=@Manufacturer_Name and Manufacturer_ID<>@Manufacturer_ID  
	
END

GO

