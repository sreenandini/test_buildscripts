USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertStackerDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertStackerDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_InsertStackerDetails
@StackerName VARCHAR(255),
@StackerSize INT,
@StackerDescription VARCHAR(255),
@StackerStatus BIT
AS
DECLARE @Stacker_Id INT

BEGIN
INSERT INTO Stacker(StackerName,StackerSize,StackerDescription,StackerStatus)
VALUES (@StackerName,@StackerSize,@StackerDescription,@StackerStatus)

SET @Stacker_Id = CAST(SCOPE_IDENTITY() AS INT)
Insert into dbo.Export_History(EH_Date,EH_Reference1,EH_Type, EH_Site_Code) SELECT GETDATE(),@Stacker_Id,'STACKER', Site_Code FROM SITE WHERE Site_Enabled=1 AND sitestatus='FULLYCONFIGURED' 

END

GO

