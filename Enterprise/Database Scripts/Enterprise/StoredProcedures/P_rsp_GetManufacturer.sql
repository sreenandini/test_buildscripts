USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetManufacturer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetManufacturer]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetManufacturer]
	@Manufacturer_Name VARCHAR(50) = NULL
AS
/*****************************************************************************************************
DESCRIPTION : Gets the List of Manufacturers  
CREATED DATE: 25 May 2012
CREATED BY: Lekha
MODULE            : Manufacturers      
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR                              DESCRIPTON                                                        MODIFIED DATE
------------------------------------------------------------------------------------------------------
                                                              
*****************************************************************************************************/

BEGIN
		SET NOCOUNT ON
      
		SELECT
			Manufacturer_Name,
			Manufacturer_ID
		FROM Manufacturer
		WHERE
			@Manufacturer_Name IS NULL OR @Manufacturer_Name = 'All' OR UPPER(LTRIM(RTRIM(Manufacturer_Name))) = UPPER(LTRIM(RTRIM(@Manufacturer_Name)))
		ORDER BY Manufacturer_Name
END

GO
