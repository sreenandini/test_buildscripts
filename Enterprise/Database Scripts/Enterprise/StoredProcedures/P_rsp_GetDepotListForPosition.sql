USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDepotListForPosition]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDepotListForPosition]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetDepotListForPosition
(
	@SupplierID int 
)
AS
/*****************************************************************************************************
DESCRIPTION : To display Depot Name   
			-- If supplier ID is passed get related records 
            -- else get all recrods
CREATED DATE: 30.1.2013
MODULE      : BarPosition       
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR						MODIFIED DATE		DESCRIPTON                                                        
------------------------------------------------------------------------------------------------------
                                                              
*****************************************************************************************************/
IF  @SupplierID = 0
BEGIN
     SELECT	Depot_ID, 
			Depot_Name 
	FROM Depot 
	order by Depot_Name
END
ELSE
BEGIN
	SELECT	Depot_ID, 
			Depot_Name 
	FROM Depot 
	WHERE Supplier_ID = @SupplierID 
	order by Depot_Name
END

GO

