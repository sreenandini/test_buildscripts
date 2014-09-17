USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateSTMExportHistoryDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateSTMExportHistoryDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*  
--Exec usp_UpdateSTMExportHistoryDetails 1,0,'Failed'
--Select * from STM_Export_History
*/  
CREATE PROCEDURE usp_UpdateSTMExportHistoryDetails 
    @SID int,    
    @Status int,
    @Result Varchar(8000)=null  
AS 
BEGIN
   UPDATE STM_Export_History
   SET
		   Processed_Date=GETDATE(),
           Status=@Status,
		   Result=@Result           
	WHERE ID = @SID 
END

GO

