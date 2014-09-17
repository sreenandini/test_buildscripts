USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateStaffDepot]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateStaffDepot]
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
  Kalaiyarasan.P              05-Sep-2012         Created               This SP is used to Update Staff_Depot details     
                                                                        based on Staff_ID.    
*/    
CREATE PROCEDURE usp_UpdateStaffDepot
	@Staff_ID INT,
	@Depot_ID INT
AS
BEGIN
	IF @Depot_ID = 0
	BEGIN
	    DELETE 
	    FROM   Staff_Depot
	    WHERE  Staff_ID = @Staff_ID
	END
	
	IF @Depot_ID <> 0
	    INSERT INTO 
	           Staff_Depot ( Staff_ID, Depot_ID ) VALUES ( @Staff_ID, @Depot_ID  ) 
	    
	    --Exec usp_UpdateStaffDepot 2,4
END

GO

