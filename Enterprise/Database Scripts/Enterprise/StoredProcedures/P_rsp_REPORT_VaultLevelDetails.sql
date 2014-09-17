USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_Report_VaultLevelDetails'
   )
    DROP PROCEDURE dbo.rsp_Report_VaultLevelDetails
GO   
    
CREATE PROCEDURE [dbo].[rsp_Report_VaultLevelDetails]    
(    
    @Company         INT = 0,    
    @SubCompany      INT = 0,    
    @Region          INT = 0,    
    @Area            INT = 0,    
    @District        INT = 0,    
    @Site            INT = 0,    
    @InventoryLevel  INT = 0,    
    @InventoryType   VARCHAR(20) = NULL,    
    @UserID          INT,
    @SiteIDList		 VARCHAR(MAX)
)    
AS    
BEGIN    
 IF @Company = 0    
     SET @Company = NULL    
     
 IF @Subcompany = 0    
     SET @Subcompany = NULL    
     
 IF @Area = 0    
     SET @Area = NULL    
     
 IF @District = 0    
     SET @District = NULL    
     
 IF @Region = 0    
     SET @Region = NULL    
     
 IF @Site = 0    
     SET @Site = NULL    
     
 IF @InventoryType = '--All--' OR @InventoryType = 'All'
     SET @InventoryType = NULL    
     
 IF @InventoryLevel = 0    
     SET @InventoryLevel = NULL    
     
 IF @InventoryType = 'BMC'    
     SET @InventoryType = '0'    
 ELSE     
 IF @InventoryType = 'Vault'    
     SET @InventoryType = '1'    
 ELSE    
     SET @InventoryType = NULL    
     
 SELECT DISTINCT   S.Site_Name,  
        tVT.Device_ID AS Vault_ID,    
        tVD.Site_ID,    
        S.Site_Code,    
        CASE     
             WHEN ISNULL(SCR.Sub_Company_Region_Name,'')='' THEN 'Unassigned'            
             ELSE SCR.Sub_Company_Region_Name    
        END AS RegionName,    
        CASE     
             WHEN ISNULL(SCA.Sub_Company_Area_Name,'') = '' THEN 'UnAssigned'                 
             ELSE SCA.Sub_Company_Area_Name    
        END AS AreaName,    
        tVD.Name,    
        tVD.Capacity AS InventoryLimit,    
		CASE       
		WHEN tVD.IsWebServiceEnabled = 0 THEN CurrentBalance      
		ELSE Vault_Balance      
		END AS CurrentBalance,
        CAST(    
            ROUND((tVT.CurrentBalance * 100 / tVD.Capacity), 2) AS NUMERIC(38, 2)    
        ) AS InventoryLevel,    
        CASE     
             WHEN tVD.IsWebServiceEnabled = 0 THEN 'BMC'    
             ELSE 'Vault'    
        END AS InventoryMethod,    
        CreatedDate AS LastUpdated    
 FROM   tVault_Devices tVD WITH(NOLOCK)    
        INNER JOIN tVault_Transactions tVT WITH(NOLOCK)    
             ON  tVD.Vault_ID = tVT.Device_ID    
        INNER JOIN SITE S WITH(NOLOCK)    
             ON  S.Site_ID = tVD.Site_ID    
             --AND S.Site_ID = ISNULL(@Site, S.Site_ID)    
             AND (
	               @SiteIDList IS NOT NULL AND S.site_id IN (SELECT DATA    
                                              FROM   dbo.fnSplit (@SiteIDList,','))
	           )
        INNER JOIN Sub_Company SC WITH(NOLOCK)    
             ON  SC.Sub_Company_ID = S.Sub_Company_ID    
             AND SC.Sub_Company_ID = ISNULL(@Subcompany, SC.Sub_Company_ID)    
        INNER JOIN Company C WITH(NOLOCK)    
             ON  C.Company_ID = SC.Company_ID    
             AND C.Company_ID = ISNULL(@Company, C.Company_ID)    
        LEFT JOIN Sub_Company_Region SCR WITH(NOLOCK)    
             ON  SCR.Sub_Company_ID = S.Sub_Company_ID    
             AND S.Sub_Company_Region_ID= SCR.Sub_Company_Region_ID             
        LEFT JOIN Sub_Company_Area SCA WITH(NOLOCK)    
             ON  SCA.Sub_Company_Region_ID = SCR.Sub_Company_Region_ID    
             AND S.Sub_Company_Area_ID= SCA.Sub_Company_Area_ID             
        LEFT JOIN Sub_Company_District SCD WITH(NOLOCK)    
             ON  SCD.Sub_Company_Area_ID = SCA.Sub_Company_Area_ID   
             AND S.Sub_Company_District_ID= SCD.Sub_Company_District_ID            
		INNER JOIN TNGAInstallations TI 
		     ON  tVD.NGADevice_ID = Ti.NGADevice_ID AND  TI.End_Date IS NULL   
 WHERE  (    
            @InventoryLevel IS NULL    
            OR @InventoryLevel IS NOT NULL    
            AND (tVT.CurrentBalance * 100 / tVD.Capacity <= @InventoryLevel)    
        )    
        AND tVD.IsWebServiceEnabled = ISNULL(@InventoryType, tVD.IsWebServiceEnabled) 
        AND S.Sub_Company_Region_ID = ISNULL(@Region, S.Sub_Company_Region_ID)    
        AND S.Sub_Company_Area_ID = ISNULL(@Area, S.Sub_Company_Area_ID)   
        AND S.Sub_Company_District_ID = ISNULL(@District, S.Sub_Company_District_ID)
        AND tVD.Active = 1		
        AND TI.Assigned_To_Site IS NOT NULL AND TI.[Start_Date] IS NOT NULL
 GROUP BY    
        tVT.Device_ID,    
        tVT.Transactions_ID,    
        tVD.Site_ID,    
        S.Site_Code,    
        S.Site_Name,    
        tVD.Name,    
        tVD.Capacity,    
        tVT.CurrentBalance, 
        tVT.Vault_Balance,   
        tVD.IsWebServiceEnabled,    
        CreatedDate,SCR.Sub_Company_Region_Name,SCA.Sub_Company_Area_Name    
 HAVING (    
            tVT.Transactions_ID IN (SELECT MAX(Transactions_ID)    
                                    FROM   tVault_Transactions    
                                    GROUP BY    
                     DEVICE_ID)    
 )
 ORDER BY CurrentBalance 
    
END 


-- exec [rsp_Report_VaultLevelDetails]  0,0,0,0,0,0,0,'--All--',1  