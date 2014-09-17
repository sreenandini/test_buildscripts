USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ResetAAMSDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ResetAAMSDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_ResetAAMSDetails
@ID int
As

UPDATE BMC_AAMS_Details
SET BAD_AAMS_Status = 0,
BAD_Verification_Status = 0,
BAD_AAMS_EnableDisable = 0,
BAD_Entity_Command = 'Disabled',
BAD_Comments = 'Installation Removed. Resetting Approvals.'
WHERE BAD_Reference_ID = @ID AND BAD_AAMS_Entity_Type = 3


GO

