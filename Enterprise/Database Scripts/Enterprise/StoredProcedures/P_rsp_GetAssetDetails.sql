USE [Enterprise]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAssetDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAssetDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetAssetDetails(@AssetNo AS VARCHAR(100), @TransitSiteCode AS VARCHAR(50))
AS
BEGIN
	DECLARE @SuccessCode        INT  
	DECLARE @Message            VARCHAR(200)  
	DECLARE @MachineStatusFlag  INT  
	
	SET @SuccessCode = 0  
	SET @Message = ''  
	
	SELECT @MachineStatusFlag = Machine_Status_Flag
	FROM   MACHINE
	WHERE  Machine_Stock_No = @AssetNo  
	
	IF (@MachineStatusFlag = 18) -- Transit Asset
	BEGIN
	    -- Check Asset In Transit  
	    IF NOT EXISTS(
	           SELECT *
	           FROM   MACHINE
	           WHERE  Machine_Stock_No = @AssetNo
	                  AND Machine_Status_Flag = 18
	                  AND Machine_Transit_Site_Code = @TransitSiteCode
	       )
	    BEGIN
	        SET @SuccessCode = -3    
	        SET @Message = 'Asset In Transit'
	    END 
	    
	    --Check asset already allocated                                  
	    IF EXISTS(
	           SELECT *
	           FROM   Installation
	                  JOIN MACHINE
	                       ON  Installation.Machine_Id = MACHINE.Machine_Id
	           WHERE  Installation_END_date IS NULL
	                  AND MACHINE.Machine_Stock_No = @AssetNo
	       )
	    BEGIN
	        SET @SuccessCode = -2      
	        SET @Message = 'Asset already in use'
	    END 
	    
	    --Check for Asset No & Also which is not terminated.                                 
	    IF NOT EXISTS (
	           SELECT *
	           FROM   MACHINE
	           WHERE  Machine_Stock_No = @AssetNo
	                  AND (Machine_end_date IS NULL OR Machine_end_date = '')
	       )
	    BEGIN
	        SET @SuccessCode = -1    
	        SET @Message = 'Asset Not exists'
	    END
	END
	ELSE
	BEGIN
	    --Check for Asset No & Also which is not terminated.                                 
	    IF NOT EXISTS (
	           SELECT *
	           FROM   MACHINE
	           WHERE  Machine_Stock_No = @AssetNo
	                  AND Machine_Status_Flag NOT IN (12, 13, 14)
	       )
	    BEGIN
	        SET @SuccessCode = -1    
	        SET @Message = 'Asset Not exists'
	    END 
	    
	    --Check if NGA.  
	    IF EXISTS (
	           SELECT *
	           FROM   MACHINE M
	                  INNER JOIN Machine_Class MC
	                       ON  M.Machine_Class_ID = MC.Machine_Class_ID
	                  INNER JOIN Machine_Type MT
	                       ON  MC.Machine_Type_ID = MT.Machine_Type_ID
	           WHERE  MT.IsNonGamingAssetType = 1
	                  AND M.Machine_Stock_No = @AssetNo
	       )
	    BEGIN
	        SET @SuccessCode = -1    
	        SET @Message = 'Asset Not exists'
	    END 
	    
	    --Check asset already allocated                                  
	    IF EXISTS(
	           SELECT *
	           FROM   Installation
	                  JOIN MACHINE
	                       ON  Installation.Machine_Id = MACHINE.Machine_Id
	           WHERE  Installation_END_date IS NULL
	                  AND MACHINE.Machine_Stock_No = @AssetNo
	       )
	    BEGIN
	        SET @SuccessCode = -2      
	        SET @Message = 'Asset already in use'
	    END
	END  
	
	IF @SuccessCode = 0
	BEGIN
	    SELECT @SuccessCode AS SuccessCode,
	           ISNULL(@Message, '') AS [Message],
	           M.Machine_Stock_No,
	           Machine_Manufacturers_Serial_No AS SerialNo,
	           ISNULL(Machine_Alternative_Serial_Numbers, '') AS AltSerialNo,
	           Machine_Type_Code AS MachineTypeCode,
	           Manufacturer_Name AS Manufacturer_Name,
	           machine_Type_Code AS GameCode,
	           ISNULL(LTRIM(RTRIM(Machine_Type_category)), '') AS GameCategory,
	           Machine_name AS Game,
	           ActAssetNo,
	           GMUNo,
	           ActSerialNo,
	           EnrolmentFlag,
	           M.CMPGameType  AS CMPGameType,
	           Optr.Operator_Name,
	           M.isMultiGame,
	           M.GetGameDetails,
	           M.IsDefaultAssetDetail,
	           M.Base_Denom,
	           M.Percentage_Payout,
	           M.Machine_Occupancy_Hour as OccupanyHour,
	           M.AssetDisplayName ,
	           CGT.GameTypeCode
	    FROM   MACHINE M
	           JOIN Machine_Class MC
	                ON  MC.Machine_Class_ID = M.Machine_Class_ID
			   JOIN tblcmpGameTypes CGT 
				 ON  M.CMPGameType = CGT.Gameprefix
	           JOIN Machine_Type MT
	                ON  MC.Machine_Type_ID = MT.Machine_Type_ID
	           JOIN Manufacturer MF
	                ON  MC.Manufacturer_ID = MF.Manufacturer_ID
	           JOIN tblCMPGameTypes tCMP
	                ON  M.CMPGameType = tCMP.GamePrefix
	           JOIN Operator Optr
	                ON  m.Operator_ID = Optr.Operator_ID
	    WHERE  M.Machine_Stock_No = @AssetNo
	           AND (M.Machine_end_date IS NULL OR M.Machine_end_date = '')
	END
	ELSE 
	IF (@SuccessCode = -2)
	BEGIN
	    SELECT @SuccessCode AS SuccessCode,
	           @Message AS [Message],
	           S.Site_Name,
	           B.Bar_Position_Name
	    FROM   Installation I
	           JOIN MACHINE M
	                ON  I.Machine_Id = M.Machine_Id
	           JOIN Bar_Position B
	                ON  I.Bar_Position_ID = B.Bar_Position_ID
	           JOIN SITE S
	                ON  S.site_id = B.site_ID
	    WHERE  Installation_END_date IS NULL
	           AND M.Machine_Stock_No = @AssetNo
	END
	ELSE 
	IF (@SuccessCode = -3)
	BEGIN
	    SELECT @SuccessCode AS SuccessCode,
	           @Message AS [Message],
	           S.Site_Name,
	           NULL AS [Bar_Position_Name]
	    FROM   MACHINE M
	           LEFT JOIN SITE S
	                ON  S.Site_Code = M.Machine_Transit_Site_Code
	    WHERE  M.Machine_Stock_No = @AssetNo
	END
	ELSE
	BEGIN
	    SELECT @SuccessCode,
	           @Message AS [Message]
	END
END


GO

