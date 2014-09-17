USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertIntoCurrentCredits]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertIntoCurrentCredits]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_InsertIntoCurrentCredits(@installationID int, @CurrentCredits int)
AS 
BEGIN

INSERT INTO CurrentCreditHistory (InstallationID, CurrentCredit, Date)
VALUES (@installationID, @CurrentCredits, GetDate())

END

GO

