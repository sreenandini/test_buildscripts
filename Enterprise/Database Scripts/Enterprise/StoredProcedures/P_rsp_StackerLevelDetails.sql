USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_StackerLevelDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_StackerLevelDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_StackerLevelDetails]         
(
 @Company INT =0,
 @SubCompany INT =0,  
 @Region INT = 0,
 @Area INT = 0,  
 @District INT = 0,       
 @Site int =0,  
 @StackerLevel INT = 0,
 @SiteIDList VARCHAR(MAX)      
)        
AS        
BEGIN        
        
IF @COMPANY=0 SET @COMPANY=NULL       
IF @SUBCOMPANY=0 SET @SUBCOMPANY=NULL             
IF @Area=0 SET @Area=NULL      
IF @Region=0 SET @Region=NULL      
IF @SITE=0 SET @SITE=NULL      
IF @District=0 SET @District=NULL        

      
SELECT             
 S.REGION,
 CASE     
 WHEN ISNULL(SCR.Sub_Company_Region_Name,'')='' THEN 'Unassigned'            
 ELSE SCR.Sub_Company_Region_Name    
 END AS RegionName,       
 S.Site_Name,         
 S.Site_Code,         
 ISNULL(S.Site_Address_1,'') AS SiteAddress1,
 ISNULL(S.Site_Address_2,'') AS SiteAddress2,
 ISNULL(S.Site_Address_3,'') AS SiteAddress3,
 ISNULL(S.Site_Address_4,' ') AS Site_Address4,  
 S.Site_Postcode AS SitePostcode, 
 CASE     
 WHEN ISNULL(SCA.Sub_Company_Area_Name,'') = '' THEN 'Unassigned'                 
 ELSE SCA.Sub_Company_Area_Name    
 END AS AreaName, 
 CASE     
 WHEN ISNULL(SCD.Sub_Company_District_Name,'') = '' THEN 'Unassigned'                 
 ELSE SCD.Sub_Company_District_Name    
 END AS DistrictName,  
 BP.Bar_Position_Name,         
 M.MACHINE_STOCK_NO AS AssetNumber ,   
 CAST((SL.BillQty + SL.TicketQty)AS BIGINT) AS TOTALQTY,      
 ((CAST((SL.BILLQTY+SL.TICKETQTY)AS BIGINT)*100)/ST.STACKERSIZE) AS StackerLevel,                 
 I.INSTALLATION_ID,         
 M.MACHINE_ID        
FROM             
 INSTALLATION I      
 JOIN MACHINE M ON M.MACHINE_ID=I.MACHINE_ID      
 JOIN BAR_POSITION BP ON BP.BAR_POSITION_ID=I.BAR_POSITION_ID      
 JOIN [SITE] S ON S.SITE_ID=BP.SITE_ID      
 JOIN STACKER ST ON ST.STACKER_ID=M.STACKER_ID      
 JOIN STACKERLEVEL SL ON SL.INSTALLATION_NO = I.INSTALLATION_ID      
 JOIN SUB_COMPANY SC ON SC.SUB_COMPANY_ID=S.SUB_COMPANY_ID       
 LEFT JOIN COMPANY C ON C.COMPANY_ID=SC.COMPANY_ID      
 LEFT JOIN Sub_Company_Area SCA ON SCA.Sub_Company_Area_ID=S.Sub_Company_Area_ID      
 LEFT JOIN Sub_Company_District SCD ON SCD.Sub_Company_District_ID=S.Sub_Company_District_ID       
 LEFT JOIN SUB_COMPANY_REGION SCR ON SCR.Sub_Company_Region_ID=S.Sub_Company_Region_ID      
WHERE
 ((@SUBCOMPANY IS NULL) OR (@SUBCOMPANY IS NOT NULL AND S.SUB_COMPANY_ID = @SUBCOMPANY))        
 AND ((@COMPANY IS NULL) OR (@COMPANY IS NOT NULL AND C.COMPANY_ID = @COMPANY))      
 AND ((@District IS NULL) OR (@District IS NOT NULL AND S.Sub_company_district_id = @District))      
 AND ((@Site IS NULL) OR (@Site IS NOT NULL AND S.SITE_ID = @Site))      
 AND ((@Region IS NULL) OR (@Region IS NOT NULL AND SCR.SUB_COMPANY_REGION_ID=@Region))      
 AND (( @Area IS NULL) OR (@Area IS NOT NULL AND S.Sub_Company_Area_ID=@Area))      
 AND (CAST((SL.BillQty + SL.TicketQty)AS BIGINT) * 100)/ST.STACKERSIZE>= @StackerLevel 
 AND I.INSTALLATION_End_Date is NULL
 AND (@SiteIDList IS NOT NULL AND S.Site_ID IN(SELECT DATA FROM fnSplit(@SiteIDList,',')))
ORDER BY S.SITE_ID         
END

GO

