/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 03/05/2014 6:44:07 PM
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_EBS_GetSettingValue]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_EBS_GetSettingValue]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [rsp_EBS_GetSettingValue]
	@SettingName VARCHAR(100)
AS
BEGIN
	SELECT Setting_Value
	FROM   setting WITH(NOLOCK)
	WHERE  setting_name = @SettingName
END
GO

