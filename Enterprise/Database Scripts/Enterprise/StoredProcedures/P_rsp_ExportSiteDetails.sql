USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportSiteDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportSiteDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*      
 * this stored procedure is to export the site details to the corresponding Exchange      
 * Change History:  --EXEC dbo.rsp_ExportSiteDetails 39      
 * Sudarsan S  20-05-2008  created      
 *  Sudarsan S  11-07-2008  modified since the usp_Export_History was modified.       
 * Anuradha J  09 Feb 2009  Modified the code for Standard Opening Hours to retrive from standard opening hours table.            
 * Vineetha M  19 Sep 2009  Modified to export transaction flag name along with Site.
 * Lekha 29 Mar 2012  Modified to export IsCrossTicketingEnabled.            
 * Durga 07 Aug 2012 Export Stacker Details 
*/      
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
      
    
CREATE PROCEDURE [dbo].[rsp_ExportSiteDetails]
	@Site_ID VARCHAR(50)
AS
BEGIN
	DECLARE @Operator                 XML      
	DECLARE @Depot                    XML      
	DECLARE @Site                     XML      
	DECLARE @Position                 XML    
	DECLARE @Stacker                  XML  
	DECLARE @Result                   XML      
	DECLARE @Site_Code                VARCHAR(50)      
	DECLARE @EH_ID                    INT 
	DECLARE @SiteLicensingEnabled     XML

	--      
	SELECT @Site_Code = Site_Code
	FROM   dbo.Site
	WHERE  Site_ID = @Site_ID      
	
	SET @Operator = (
	        SELECT *
	        FROM   Operator FOR XML PATH('OPERATOR'),
	               TYPE,
	               ELEMENTS,
	               ROOT('OPERATORS')
	    )      
	
	SET @Depot = (
	        SELECT *,
	               dbo.fnGetNames(Supplier_ID, 'OPERATOR') AS Operator_Name
	        FROM   Depot FOR XML PATH('DEPOT'),
	               TYPE,
	               ELEMENTS,
	               ROOT('DEPOTS')
	    )      
	
	SET @SiteLicensingEnabled = (
	        SELECT [Setting_Value] AS IsSiteLicensingEnabled
	        FROM   [dbo].[Setting]
	        WHERE  UPPER(LTRIM(RTRIM([Setting_Name]))) = UPPER('IsSiteLicensingEnabled') 
	               FOR XML PATH('ISSITELICENSINGENABLED'), TYPE, ELEMENTS, ROOT('ISSITELICENSINGENABLED')
	    )
	
	SET @Position = (
	        SELECT DISTINCT POSITION.Bar_Position_ID,
	               POSITION.Bar_Position_Name,
	               POSITION.Bar_Position_Location,
	               CONVERT(DATETIME, POSITION.Bar_Position_Start_Date, 101) AS 
	               Bar_Position_Start_Date,
	               CONVERT(DATETIME, POSITION.Bar_Position_End_Date, 101) AS 
	               Bar_Position_End_Date,
	               POSITION.Bar_Position_Collection_Day,
	               dbo.fnGetNames(POSITION.Depot_ID, 'DEPOT') AS Depot_Name,
	               ZONE.Zone_Name,
	               ZONE.Zone_Description,
	               CONVERT(DATETIME, ZONE.Zone_Start_Date, 101) AS 
	               Zone_Start_Date,
	               CONVERT(DATETIME, ZONE.Zone_End_Date, 101) AS Zone_End_Date,
	               ISNULL(ZONE.PromotionEnabled,0) AS PromotionEnabled,
	               POSITION.Bar_Position_IsEnable
	        FROM   dbo.Bar_Position POSITION
	               LEFT JOIN dbo.Zone ZONE ON  POSITION.Zone_ID = ZONE.Zone_ID AND ZONE.Site_ID=@Site_ID
	        WHERE  POSITION.Site_ID = @Site_ID FOR XML AUTO, TYPE, ELEMENTS
	    )      
	
	SET @Stacker = (
	        SELECT *
	        FROM   Stacker FOR XML PATH('STACKER'),
	               TYPE,
	               ELEMENTS,
	               ROOT('STACKERS')
	    )
	
	SET @Site = (
	        SELECT S.Site_Code,
	               S.Site_Name,
	               S.Site_Phone_No,
	               S.Site_Fax_No,
	               S.Site_Email_Address,
	               S.Site_Manager,
	               (CASE WHEN S.STANDARD_OPENING_HOURS_ID = 0 THEN S.Site_Open_Monday ELSE SH.Standard_Opening_Hours_Open_Monday END) AS Standard_Opening_Hours_Open_Monday,
	                (CASE WHEN S.STANDARD_OPENING_HOURS_ID = 0 THEN S.Site_Open_Tuesday ELSE SH.Standard_Opening_Hours_Open_Tuesday END) AS Standard_Opening_Hours_Open_Tuesday,
	                (CASE WHEN S.STANDARD_OPENING_HOURS_ID = 0 THEN S.Site_Open_Wednesday ELSE SH.Standard_Opening_Hours_Open_Wednesday END) AS Standard_Opening_Hours_Open_Wednesday,
	                (CASE WHEN S.STANDARD_OPENING_HOURS_ID = 0 THEN S.Site_Open_Thursday ELSE SH.Standard_Opening_Hours_Open_Thursday END) AS Standard_Opening_Hours_Open_Thursday,
	                (CASE WHEN S.STANDARD_OPENING_HOURS_ID = 0 THEN S.Site_Open_Friday ELSE SH.Standard_Opening_Hours_Open_Friday END) AS Standard_Opening_Hours_Open_Friday,
	                (CASE WHEN S.STANDARD_OPENING_HOURS_ID = 0 THEN S.Site_Open_Saturday ELSE SH.Standard_Opening_Hours_Open_Saturday END) AS Standard_Opening_Hours_Open_Saturday,
	                 (CASE WHEN S.STANDARD_OPENING_HOURS_ID = 0 THEN S.Site_Open_Sunday ELSE SH.Standard_Opening_Hours_Open_Sunday END) AS Standard_Opening_Hours_Open_Sunday,
	                 --SH.Standard_Opening_Hours_Open_Monday,
	               --SH.Standard_Opening_Hours_Open_Tuesday,
	               --SH.Standard_Opening_Hours_Open_Wednesday,
	               --SH.Standard_Opening_Hours_Open_Thursday,
	               --SH.Standard_Opening_Hours_Open_Friday,
	               --SH.Standard_Opening_Hours_Open_Saturday,
	               --SH.Standard_Opening_Hours_Open_Sunday,
	               S.Site_Address_1,
	               S.Site_Address_2,
	               S.Site_Address_3,
	               S.Site_Address_4,
	               S.Site_Address_5,
	               S.Site_Postcode,
	               S.Site_Supplier_Code,
	               S.Region,
	               S.SiteStatus AS TransactionFlagName,
	               S.IsTITOEnabled,
	               S.IsNonCashVoucherEnabled,
	               S.IsCrossTicketingEnabled,
	               S.StackerLimitPercentage,
	               s.Ext_Site_Code,
	               /*      
	               D.Depot_Name, @Operator, @Depot, @Position FROM dbo.Site S LEFT JOIN dbo.Depot D ON S.Depot_ID = D.Depot_ID WHERE Site_ID = @Site_ID FOR XML PATH('SITE'), TYPE, ELEMENTS, ROOT('SITESETUP'))      
	               */ 
	               
	               D.Depot_Name,
	               @Operator,
	               @Depot,
	               @Position,
	               @Stacker,
	               ISNULL(C.Company_name, '') Company_name,
	               ISNULL(SC.sub_company_Name, '') sub_company_Name,
	               ISNULL(CR.Sub_Company_Region_Name, '')Sub_Company_Region_Name,
	               ISNULL(CA.sub_company_area_name, '') sub_company_area_name,
	               ISNULL(sub_company_District_Name, '') 
	               sub_company_District_Name,
	               @SiteLicensingEnabled
	        FROM   dbo.Site S
	               LEFT JOIN dbo.Depot D
	                    ON  S.Depot_ID = D.Depot_ID
	               LEFT JOIN STANDARD_OPENING_HOURS SH
	                    ON  S.STANDARD_OPENING_HOURS_ID = SH.STANDARD_OPENING_HOURS_ID
	               LEFT OUTER JOIN sub_company SC(NOLOCK)
	                    ON  S.Sub_Company_ID = SC.Sub_Company_ID
	               LEFT OUTER JOIN Company C(NOLOCK)
	                    ON  SC.Company_ID = C.Company_ID
	               LEFT OUTER JOIN sub_company_region CR(NOLOCK)
	                    ON  S.Sub_Company_Region_ID = CR.Sub_Company_Region_ID
	               LEFT OUTER JOIN sub_company_area CA(NOLOCK)
	                    ON  S.sub_company_area_ID = CA.Sub_Company_Area_ID
	               LEFT OUTER JOIN sub_company_District CD(NOLOCK)
	                    ON  s.Sub_Company_District_ID = cd.Sub_Company_District_ID
	        WHERE  Site_ID = @Site_ID FOR XML PATH('SITE'), TYPE, ELEMENTS, ROOT('SITESETUP')
	    )      
	
	
	
	SELECT @Site
END

GO

