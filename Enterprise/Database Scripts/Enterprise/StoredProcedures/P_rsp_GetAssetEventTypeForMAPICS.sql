USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAssetEventTypeForMAPICS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAssetEventTypeForMAPICS]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetAssetEventTypeForMAPICS]
(
@MessageId as VARCHAR(64),
@Result bit OUTPUT
)

AS

BEGIN

if exists(select 1 from BMC_BAS_Export_History where BBEH_BAS_Message_ID = @MessageID and BBEH_Process_Type = 4)
set @Result = 0 
else
set @Result = 1

END





GO

