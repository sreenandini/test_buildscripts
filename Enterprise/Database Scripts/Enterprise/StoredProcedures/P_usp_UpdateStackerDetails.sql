USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateStackerDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateStackerDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_UpdateStackerDetails
	@StackerID INT,
	@StackerName VARCHAR(255),
	@StackerSize INT,
	@StackerDescription VARCHAR(255),
	@StackerStatus BIT
AS
--DECLARE @Stacker_Id INT

BEGIN
UPDATE Stacker SET 
StackerName=@StackerName,
StackerSize=@StackerSize,
StackerDescription=@StackerDescription,
StackerStatus=@StackerStatus,
DateModified=getdate()
WHERE Stacker_Id=@StackerID

--SET @Stacker_Id = CAST(SCOPE_IDENTITY() AS INT)
IF @@ROWCOUNT>0
Insert into dbo.Export_History(EH_Date,EH_Reference1,EH_Type, EH_Site_Code) SELECT GETDATE(),@StackerId,'STACKER', Site_Code FROM SITE WHERE Site_Enabled=1 AND sitestatus='FULLYCONFIGURED' 
END

GO

