USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertOrUpdateMachineClassShareBand]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertOrUpdateMachineClassShareBand]
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
        Kishore S             19-June-2014             Created          This SP is used to insert/update  Machine_Class_Share_Band table                                                                       based on Staff_ID.    
*/    
CREATE PROCEDURE usp_InsertOrUpdateMachineClassShareBand
	@Machine_Class_Share_Band int,
	@Machine_Class_ID int,
	@Share_Band_ID int,
	@Share_Band_ID_Future int,
	@Machine_Class_Share_Future_Date varchar(30),
	@Share_Band_ID_Past  int,
	@Machine_Class_Share_Past_Date varchar(30)
AS
SET NOCOUNT OFF

	IF NOT EXISTS ( SELECT 1 FROM dbo.Machine_Class_Share_Band mc WHERE (mc.Machine_Class_Share_Band=@Machine_Class_Share_Band))
	BEGIN
	       INSERT INTO dbo.Machine_Class_Share_Band
	        (Machine_Class_ID,Share_Band_ID,Share_Band_ID_Future,Machine_Class_Share_Future_Date,Share_Band_ID_Past,Machine_Class_Share_Past_Date)
	        VALUES (@Machine_Class_ID,@Share_Band_ID,@Share_Band_ID_Future,CONVERT(VARCHAR(14), CAST(@Machine_Class_Share_Future_Date AS DATETIME), 106),
	        @Share_Band_ID_Past,CONVERT(VARCHAR(14), CAST(@Machine_Class_Share_Past_Date AS DATETIME), 106))
	 END   
	 ELSE
	 BEGIN
	        UPDATE dbo.Machine_Class_Share_Band
	        SET  
	        Machine_Class_ID=@Machine_Class_ID,
	        Share_Band_ID=@Share_Band_ID,
	        Share_Band_ID_Future=@Share_Band_ID_Future,
	        Machine_Class_Share_Future_Date=CONVERT(VARCHAR(14), CAST(@Machine_Class_Share_Future_Date AS DATETIME), 106),
	        Share_Band_ID_Past=@Share_Band_ID_Past,
	        Machine_Class_Share_Past_Date=CONVERT(VARCHAR(14), CAST(@Machine_Class_Share_Past_Date AS DATETIME), 106)
	        WHERE  Machine_Class_Share_Band=@Machine_Class_Share_Band
	 END
GO
