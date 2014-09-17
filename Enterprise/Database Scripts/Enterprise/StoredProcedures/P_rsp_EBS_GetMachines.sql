/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 03/05/2014 8:40:30 PM
 ************************************************************/

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_EBS_GetMachines]    Script Date: 03/04/2014 12:02:11 ******/
IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].rsp_EBS_GetMachines')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_EBS_GetMachines]
GO

/****************************************************************
Select * from Site
[rsp_EBS_GetMachines] '1212', '001869'
****************************************************************/

USE [Enterprise]
GO
/****** Object:  StoredProcedure [dbo].[rsp_EBS_GetMachines]    Script Date: 03/04/2014 10:47:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
--[rsp_EBS_GetMachines] '1212', '000090'
-- Select * from Machine Where Machine_Stock_No like '%000621%'
CREATE PROCEDURE [dbo].[rsp_EBS_GetMachines]
(
	@SiteCode VARCHAR(50) = NULL,
	@MachineId VARCHAR(50) = NULL, 
	@MachineIDActual INT = NULL
)
AS
BEGIN
	IF (@MachineIDActual IS NOT NULL)
	BEGIN
	    SELECT @MachineId = dbo.udf_GetNumeric(Machine_Stock_No)
	    FROM   dbo.[Machine] m
	    WHERE  m.Machine_ID = @MachineIDActual
	    IF @MachineId IS NULL
	    Return
	END	
	
	SELECT 
		dbo.udf_GetNumeric(M.Machine_Stock_No) AS MachineID, 
		NULL AS Area,
		'MA' AS MachineType, --1 Means Slot/2 Means Table
		NULL AS BANK,
		ISNULL(CAST(M.CMPGameType AS VARCHAR(50)), '') AS GameName,
		ISNULL(CAST((CAST(I.Installation_Price_Per_Play AS FLOAT)/100.0) AS VARCHAR(50)),'') AS DenominationID,
		CAST (MF.Manufacturer_ID AS VARCHAR(50))  AS ManufacturerId,
		ISNULL(CAST(S.Site_Code AS VARCHAR(50)), '') AS CasinoID,
		ISNULL(CAST(Z.Zone_ID AS VARCHAR(50)), '') AS ZoneID,
		CAST ((CASE WHEN I.Installation_End_Date IS NULL THEN 1 ELSE 0 END) AS BIT) AS IsActive,
		ISNULL(BP.Bar_Position_Name,'') AS MachineLoc

	FROM 
		MACHINE M
	           JOIN Installation I
	                ON  I.machine_id = M.machine_id
	           JOIN (
	                    SELECT Installation.Machine_ID,
	                           MAX(Installation.Installation_ID) AS 
	                           Installation_ID
	                    FROM   Installation
	                    GROUP BY
	                           Installation.Machine_ID
	                ) ins
	                ON  I.Installation_ID = ins.Installation_ID
	           JOIN (
	                    SELECT MAX(M.Machine_Id) AS Machine_ID
	                    FROM   MACHINE M
	                    GROUP BY
	                           M.Machine_Stock_No
	                ) mac
	                ON  mac.Machine_ID = ins.Machine_ID
	           JOIN Machine_Class
	                ON  M.Machine_Class_Id = Machine_Class.Machine_Class_Id
	           JOIN Manufacturer MF
	                ON  MF.Manufacturer_ID = Machine_Class.Manufacturer_ID
	           JOIN Machine_Type
	                ON  Machine_Class.Machine_Type_Id = Machine_Type.Machine_Type_Id
	           JOIN Bar_Position BP
	                ON  BP.Bar_Position_Id = I.Bar_Position_Id
	           LEFT JOIN Zone Z
	                ON  BP.Zone_ID = Z.Zone_ID
	           JOIN SITE S
	                ON  BP.Site_Id = S.Site_Id
		WHERE  S.Site_Code = COALESCE(@SiteCode, S.Site_Code) AND Machine_Type.IsNonGamingAssetType = 0 
		AND dbo.udf_GetNumeric(Machine_Stock_No) =  COALESCE(@MachineId, dbo.udf_GetNumeric(Machine_Stock_No))
		AND ISNULL(dbo.udf_GetNumeric(Machine_Stock_No), '') <> ''
	
	ORDER BY 1
	
	
END
GO





