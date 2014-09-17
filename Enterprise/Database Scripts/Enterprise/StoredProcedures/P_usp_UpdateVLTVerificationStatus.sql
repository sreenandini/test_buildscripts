USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateVLTVerificationStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateVLTVerificationStatus]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- SP to Update VLT Verification Status
-- Created by Madhu 20/1/2009
CREATE PROCEDURE dbo.usp_UpdateVLTVerificationStatus
(
	@doc xml
)
as
BEGIN

declare @Serial Varchar(50)
declare @Status int
declare @AAMSID varchar(20)
declare @Verified	DATETIME

DECLARE @docHandle int  
EXEC sp_xml_preparedocument @docHandle OUTPUT, @doc  

select	@Serial = Serial,
		@Status = VLTStatus,
		@AAMSID = AAMSID,
		@Verified = VLT_Verified
from OPENXML(@docHandle , './VLTVerification/VLT', 2 )  
with      
(      
	Serial varchar(50) './VLT_Serial',
	VLTStatus int './VLT_Status',
	AAMSID varchar(20) './AAMSID',
	VLT_Verified	DATETIME	'./VLT_Verified'
)  
 
EXEC sp_xml_removedocument @dochandle  

	IF EXISTS(SELECT 1 FROM dbo.VLT_Verification_Status WHERE VLT_Serial = @Serial) 
	BEGIN
		UPDATE dbo.VLT_Verification_Status 
		SET VLT_Status = @Status,
			VLT_Verified = @Verified
		WHERE VLT_Serial = @Serial
	END
	ELSE
	BEGIN
		IF @Serial IS NOT NULL
			INSERT INTO dbo.VLT_Verification_Status VALUES(@Serial, @Verified, @Status)
	END

	IF ISNULL(@AAMSID, '') <> ''
	BEGIN
		UPDATE dbo.LGE_Export_History
		   SET LGE_EH_Status = 100
		 WHERE LGE_EH_AAMS_Message_ID = @AAMSID
	END

END

GO

