USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_UpdateSiteDownAlert]    Script Date: 01/30/2013 05:11:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateSiteDownAlert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateSiteDownAlert]
GO


/****** Object:  StoredProcedure [dbo].[usp_UpdateSiteDownAlert]    Script Date: 01/30/2013 05:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Exec [usp_UpdateSiteDownAlert] 1
Create PROCEDURE [dbo].[usp_UpdateSiteDownAlert]
(@Site_ID INT = 0)
AS
BEGIN
	DECLARE @VersionName  VARCHAR(20)    
	DECLARE @temp         NVARCHAR(20)    
	DECLARE @temp1        NVARCHAR(MAX)   
	DECLARE @XML XML   
	DECLARE @MessageDateTime DATETIME  
	DECLARE @siteCode VARCHAR(50)
	
	SELECT @siteCode = site_Code FROM SITE WHERE Site_ID = @Site_ID  
		
	SELECT @temp1 =  STUFF((SELECT ',' + BP.Bar_Position_Name
            FROM Installation I INNER JOIN Bar_Position BP 
            ON I.Bar_Position_ID = BP.Bar_Position_ID
	                        WHERE  I.Installation_End_Date IS NULL AND BP.SITE_ID = @Site_ID
	                        ORDER BY BP.Bar_Position_Name
            FOR XML PATH('')) ,1,1,'') 
               
	SELECT TOP 1 @VersionName = VersionName
	FROM   VersionHistory
	ORDER BY
	       1 DESC
	
	SELECT @MessageDateTime = GETDATE()    
	
	SET @temp = ''    
	
	SET @XML = (
	SELECT TOP 1 
	       'SITEDOWN' AS [Source],
	       @VersionName AS [BMCVersion],
	       '100' AS ExceptionCode,
	       '000' AS OperatorId,
	       @temp AS [SubCode],
	       C.Company_Name AS Company,
	       ISNULL(SCR.Sub_Company_Region_Name, '') AS [Region],
	       ISNULL(SCA.Sub_Company_Area_Name, '') AS [Area],
	       S.Site_Code AS [SiteId],
	       S.Site_Name AS [SiteName],
	       M.Machine_Stock_No AS [Asset],
	       BP.Bar_Position_Name AS [Stand],
	       @temp1 AS SiteDownPositionsList,
	       convert(VARCHAR, @MessageDateTime , 120) AS SiteDownTime,
	       convert(VARCHAR, @MessageDateTime , 120) AS [MessageDateTime]
	FROM   Installation AS I
	       INNER JOIN dbo.Machine M
	            ON  I.Machine_ID = M.Machine_ID
	       INNER JOIN dbo.Bar_Position BP
	            ON  I.Bar_Position_ID = BP.Bar_Position_ID
	       INNER JOIN dbo.[Site] AS S
	            ON  S.SITE_ID = BP.SITE_ID
	       INNER JOIN dbo.Sub_Company SC
	            ON  S.Sub_Company_ID = SC.Sub_Company_ID
	       INNER JOIN dbo.Company C
	            ON  C.Company_ID = SC.Company_ID
	       LEFT JOIN Sub_Company_Region SCR
	            ON  SCR.SUB_COMPANY_REGION_ID = S.SUB_COMPANY_REGION_ID
	       LEFT JOIN Sub_Company_Area SCA
	            ON  SCA.SUB_COMPANY_AREA_ID = S.SUB_COMPANY_AREA_ID
	WHERE  S.Site_ID = @Site_ID
	       AND I.Installation_End_Date IS NULL
	ORDER BY
	       INSTALLATION_ID
	       FOR XML PATH('') ,TYPE, ROOT('BMCRequest'))
	 
	 INSERT INTO STM_Export_History ([Type],ClientID,Site_Code,[Message],Received_Date) VALUES
	 ('SITEDOWN',1,@siteCode,@XML,GETDATE())
	 
	RETURN @@ERROR 
END 
GO

