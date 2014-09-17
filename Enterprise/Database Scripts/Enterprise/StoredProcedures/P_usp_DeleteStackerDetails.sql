USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_DeleteStackerDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_DeleteStackerDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Author:		Durga Devi M
-- Create date: 6th July 2012
-- Description:	Insert Stacker Details
-- =============================================
/*
UPDATE STACKER SET SYSDELETE=0
SELECT * FROM MACHINE
SELECT * FROM STACKER
DECLARE @Status INT
EXEC usp_DeleteStackerDetails 1, @Status OUTPUT
Print @Status
*/
CREATE PROCEDURE usp_DeleteStackerDetails
	@StackerId INT,
	@Status INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @UsedMachines INT	
	DECLARE @UseStatus INT
	EXEC rsp_IsStackerInUse @StackerId, @UseStatus OUTPUT
	SET @Status = 0
	
	IF (@UseStatus = 0)
	BEGIN
		UPDATE Stacker
	    SET    SysDelete = 1
	    WHERE  Stacker_Id = @StackerId
	    
	    IF @@ROWCOUNT > 0
	        SET @Status = 1
Insert into dbo.Export_History(EH_Date,EH_Reference1,EH_Type, EH_Site_Code) SELECT GETDATE(),@StackerId,'STACKER', Site_Code FROM SITE WHERE Site_Enabled=1 AND sitestatus='FULLYCONFIGURED'
	END
	
	SET NOCOUNT OFF
END

GO

