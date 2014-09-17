USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_DeleteMachineClassShareBand]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_DeleteMachineClassShareBand]
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
        Kishore S             20-June-2014             Created          This SP is used to delete Machine_Class_Share_Band table                                                                       based on Staff_ID.    
*/    
CREATE PROCEDURE usp_DeleteMachineClassShareBand
	@Machine_Class_Share_Band int
AS
SET NOCOUNT OFF
DELETE FROM machine_class_share_band WHERE machine_class_share_band = @Machine_Class_Share_Band; 
	
GO
