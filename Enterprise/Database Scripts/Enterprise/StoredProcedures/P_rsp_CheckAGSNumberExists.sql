
USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckAGSNumberExists]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckAGSNumberExists]
GO
USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rsp_CheckAGSNumberExists]
AS  
BEGIN  
IF NOT EXISTS (select 1 from Setting where Setting_Name ='ValidateAGSForGMU' and LTRIM(RTRIM(Setting_Value))  = 'true')
BEGIN
SELECT ActAssetNo,
ActSerialNo,
GMUNo, Machine_Stock_No FROM machine 
where 1 = 2
END
ELSE
BEGIN
DECLARE @Temp INT = 28
SELECT @Temp = CAST(Setting_Value AS INT) from Setting where Setting_Name = 'AGSValue'

SELECT 
(CASE WHEN ((@Temp & 8) = 8)
THEN ActAssetNo ELSE '0' END) AS ActAssetNo,
(CASE WHEN ((@Temp & 4) = 4)
THEN ActSerialNo ELSE '0' END) AS ActSerialNo,
(CASE WHEN ((@Temp & 16) = 16)
THEN GMUNo ELSE '0' END) AS GMUNo,
m.Machine_Stock_No FROM machine m
INNER JOIN machine_class mc on mc.machine_class_id = m.machine_class_id  
LEFT OUTER JOIN Machine_Type mt on mt.Machine_Type_ID=mc.Machine_Type_ID
where m.Machine_Stock_No IS NOT NULL 
AND m.Machine_Status_Flag in (1,0,13)
AND LTRIM(RTRIM(isnull(m.Machine_Stock_No ,''))) <> ''
AND mt.IsNonGamingAssetType=0
END
END
GO 

