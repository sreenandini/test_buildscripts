USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetManufacturerbyMCType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetManufacturerbyMCType]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
* Revision History  
*   
* <Name of the owner>         DateCreated       Type(Created/Modified)  Description  
  Kalaiyarasan.P              26-Nov-2012         Created               This SP is used to get  Manufacturer details by Machine Type 
  --Exec  rsp_GetManufacturerbyMCType  3                                                                 
*/  
  
CREATE PROCEDURE rsp_GetManufacturerbyMCType
	@Machine_TypeID INT
AS
BEGIN
	SELECT DISTINCT M.Manufacturer_ID,
	       M.Manufacturer_Name
	FROM   Machine_Class MC WITH(NOLOCK)
	       INNER JOIN Manufacturer M WITH(NOLOCK)
	            ON  M.Manufacturer_ID = MC.Manufacturer_ID
	WHERE  MC.Machine_Type_ID = @Machine_TypeID
	ORDER BY
	       M.Manufacturer_Name
END

GO

