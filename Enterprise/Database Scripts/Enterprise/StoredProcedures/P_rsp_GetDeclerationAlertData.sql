USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDeclerationAlertData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDeclerationAlertData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetDeclerationAlertData]    
(@Site_ID INT = 0,
@UserID INT = 0,  
@Installation_ID INT = 0,  
@Collection_ID  INT = 0  
)      
AS      
      
BEGIN      
    
DECLARE @VersionName VARCHAR(20)    
DECLARE @temp NVARCHAR(20)    
DECLARE @temp1 NVARCHAR(2000)    
SET @temp = ''    
SET @temp1 = ''    
SELECT TOP 1 @VersionName=VersionName FROM VersionHistory ORDER BY 1 DESC    
DECLARE @MessageDateTime DATETIME    
SELECT @MessageDateTime = GETDATE()    
  
SELECT TOP 1    
'DECL' AS [Source],      
@VersionName AS [BMCVersion],    
'001' AS ExceptionCode,    
'000' AS OperatorId,    
@temp AS [SubCode],     
C.Company_Name AS Company,    
ISNULL(SCR.Sub_Company_Region_Name,'') AS [Region],    
ISNULL(SCA.Sub_Company_Area_Name,'') AS [Area],    
S.Site_Code AS [SiteId],      
S.Site_Name AS [SiteName],       
M.Machine_Stock_No AS [Asset],      
BP.Bar_Position_Name AS [Stand],  
@temp1 AS DropPositionsList,    
@temp AS DeclarationType,    
B.Batch_Ref AS BatchNumber,   
@MessageDateTime AS DeclartionDateTime,  
st.Staff_First_Name + ' ' + st.Staff_Last_Name AS DeclaredBy,       
@MessageDateTime AS [MessageDateTime]    
  
FROM dbo.[Site] AS S  
INNER JOIN dbo.Sub_Company SC  
ON S.Sub_Company_ID = SC.Sub_Company_ID  
INNER JOIN dbo.Company C  
ON C.Company_ID = SC.Company_ID  
LEFT JOIN Sub_Company_Region SCR ON SCR.SUB_COMPANY_REGION_ID=S.SUB_COMPANY_REGION_ID
LEFT JOIN Sub_Company_Area SCA ON SCA.SUB_COMPANY_AREA_ID=S.SUB_COMPANY_AREA_ID 
,dbo.Staff st, Installation AS I  
INNER JOIN dbo.Machine M  
ON I.Machine_ID = M.Machine_ID  
INNER JOIN dbo.Bar_Position BP  
ON I.Bar_Position_ID = BP.Bar_Position_ID,  
Collection CB
INNER JOIN dbo.Batch B
ON CB.Batch_ID = B.Batch_ID 
  
WHERE st.Staff_ID = @UserID AND I.Installation_ID = @Installation_ID AND CB.Collection_ID = @Collection_ID  AND S.Site_ID = @Site_ID
    
END 

GO

