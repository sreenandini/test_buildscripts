USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetMachineDetailsBasedonGameName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetMachineDetailsBasedonGameName]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
* Revision History  
*   
* <Name>						DateModified		Type(Created/Modified)		Description  
* Venkatesh Kumar.J				27thDec-2012		Modified from base Proc		This SP is used to get Stock details                                                              
*
*
*/  

CREATE PROCEDURE rsp_GetMachineDetailsBasedonGameName  
@Machine_Type_ID INT,     
@Operator_ID INT,     
@Depot_ID INT,    
@Staff_ID INT,    
@Manufacturer_ID INT,     
@Game_Category_ID INT,     
@ModelTypeID INT,       
@Machine_Status  NVARCHAR(50),    
@OrderBy VARCHAR(30),    
@SearchCriteria  NVARCHAR(100),    
@MG_Game_ID INT=0,  
@MG_Game_Name nvarchar(50) = 'MULTI GAME'  
AS      
BEGIN    
 IF ISNULL(@Machine_Type_ID,0) = 0    
  SET @Machine_Type_ID = NULL    
  
 IF ISNULL(@Operator_ID,0) = 0    
  SET @Operator_ID = NULL    
  
 IF ISNULL(@Depot_ID,0) = 0    
  SET @Depot_ID = NULL    
  
 IF ISNULL(@Staff_ID,0) = 0    
  SET @Staff_ID = NULL    
  
 IF ISNULL(@Manufacturer_ID,0) = 0    
  SET @Manufacturer_ID = NULL    
  
 IF ISNULL(@Game_Category_ID,0) = 0    
  SET @Game_Category_ID = NULL    
  
 IF ISNULL(@ModelTypeID,0) = 0    
  SET @ModelTypeID = NULL    
  
 IF ISNULL(@SearchCriteria,'') = ''    
  SET @SearchCriteria = NULL    
  
 IF ISNULL(@MG_Game_ID,0) = 0          
  SET @MG_Game_ID = NULL      
  
 /*    
 * To Retreive In Use and Terminated Assets    
 */    
  
 ;WITH CTEdisplayInstallation(Machine_ID ,Site_Name, Site_Code, Bar_Position_Name,Site_ZonaRice) AS    
 (    
  SELECT Machine_ID ,Site_Name, Site_Code, Bar_Position_Name,Site_ZonaRice FROM Installation WITH(NOLOCK)      
  INNER JOIN Bar_Position WITH(NOLOCK) ON Installation.Bar_Position_ID = Bar_Position.Bar_Position_ID       
  INNER JOIN Site WITH(NOLOCK) ON Bar_Position.Site_ID = Site.Site_ID      
  WHERE COALESCE(Installation_End_Date,'')='' AND Installation_ID IS NOT NULL      
 UNION    
  SELECT Machine.Machine_ID,Site_Name, Site_Code,null,null FROM     
  (Site WITH(NOLOCK) LEFT JOIN Machine WITH(NOLOCK) ON Machine.Machine_ID = Site.NGA_Machine_ID)     
  WHERE Site.NGA_Machine_ID is not null     
 )     
 SELECT  DISTINCT    
  CTE.Bar_Position_Name,    
  CTE.Site_ZonaRice,    
  M.Staff_ID,M.Machine_Transit_Site_Code, M.Machine_End_Date,     
  Op.Operator_ID,Op.Operator_Name,     
  D.Depot_ID, D.Depot_Name,     
  MT.Machine_Type_ID, MT.Machine_Type_Code,MT.IsNonGamingAssetType,     
  CASE WHEN (M.Machine_Status_Flag = 13) THEN '' ELSE MC.Machine_Name END Machine_Name,     
  CASE WHEN (M.Machine_Status_Flag = 13) THEN '' ELSE MC.Machine_Class_ID END Machine_Class_ID,     
  M.Machine_Stock_No,M.Machine_Manufacturers_Serial_No, M.Machine_Alternative_Serial_Numbers,    
  CASE WHEN (M.Machine_Status_Flag = 13) THEN 0 ELSE M.Machine_ID END Machine_ID, M.Machine_Status_Flag,    
  MC.Machine_BACTA_Code, CASE WHEN (M.Machine_Status_Flag = 13) THEN '' ELSE MC.Machine_Class_Model_Code END Machine_Class_Model_Code,     
  Staff.Staff_First_Name, Staff.Staff_Last_Name,    
  MT.Machine_Type_Code AS Machine_Category_Code,     
  Manufacturer.Manufacturer_Name,     
  CTE.Site_Code, CTE.Site_Name ,    
  CASE WHEN (ISNULL(Installation_Game_Info.Installation_No,0) > 0 AND M.Machine_Status_Flag =1) THEN 1 ELSE 0 END As PaytableFlag,    
  CASE WHEN @OrderBy = 'Game Category' THEN GAME_CATEGORY.Game_Category_ID ELSE '' END AS Game_Category_ID  ,    
  CASE WHEN @OrderBy = 'Game Category' THEN GAME_CATEGORY.Game_Category_Name  ELSE '' END AS Game_Category_Name,    
  CASE WHEN @OrderBy = 'Depot' THEN Op.Operator_Name ELSE '' END AS Orderby_Operator_Name ,    
  CASE WHEN @OrderBy = 'Depot' THEN Op.Operator_ID ELSE 0 END AS Orderby_Operator_ID ,    
  CASE WHEN @OrderBy = 'Depot' THEN D.Depot_Name ELSE '' END AS Orderby_Depot_Name ,    
  CASE WHEN @OrderBy = 'Depot' THEN D.Depot_ID ELSE 0 END AS Orderby_Depot_ID ,    
  CASE WHEN @OrderBy = 'Availability' THEN  M.Machine_Status_Flag ELSE 0 END AS Orderby_Machine_Status_Flag ,    
  CASE WHEN @OrderBy = 'Representative' THEN Staff.Staff_Last_Name ELSE '' END AS Orderby_Staff_Last_Name,      
  CASE WHEN @OrderBy = 'Representative' THEN Staff_First_Name ELSE  '' END AS Orderby_Staff_First_Name ,    
  CASE WHEN @OrderBy = 'Game Category' THEN GAME_CATEGORY.Game_Category_ID ELSE 0 END  AS Orderby_Game_Category_ID ,    
  CASE WHEN @OrderBy = 'GMG_Game_ID' THEN Game_Library.MG_Game_ID ELSE 0 END  AS Orderby_MG_Game_ID    
 INTO #TempInUse  
 FROM ((((((Machine M WITH(NOLOCK) RIGHT JOIN Machine_Class MC WITH(NOLOCK)  ON MC.Machine_Class_ID = M.Machine_Class_ID)     
 RIGHT JOIN Machine_Type MT WITH(NOLOCK) ON MT.Machine_Type_ID = MC.Machine_Type_ID )     
 LEFT JOIN Depot D WITH(NOLOCK) ON M.Depot_ID = D.Depot_ID)     
 LEFT JOIN Operator Op WITH(NOLOCK) ON D.Supplier_ID = Op.Operator_ID)     
 LEFT JOIN Staff WITH(NOLOCK) ON M.Staff_ID = Staff.Staff_ID))     
 LEFT JOIN CTEdisplayInstallation CTE ON M.Machine_ID=CTE.Machine_ID    
 LEFT JOIN Manufacturer WITH(NOLOCK)  ON MC.Manufacturer_ID = Manufacturer.Manufacturer_ID     
 LEFT JOIN Site WITH(NOLOCK) ON Site.Site_Code = M.Machine_Transit_Site_Code      
 LEFT JOIN INSTALLATION WITH(NOLOCK)  ON INSTALLATION.Machine_ID=M.Machine_ID AND INSTALLATION.Installation_End_Date is NULL     
 LEFT JOIN Installation_Game_Info WITH(NOLOCK)  ON Installation_Game_Info.Installation_No=INSTALLATION.Installation_ID     
 LEFT JOIN GAME_LIBRARY  WITH(NOLOCK) ON GAME_LIBRARY.MG_Game_ID=Installation_Game_Info.IGI_Game_ID 
 AND MC.Manufacturer_ID = GAME_LIBRARY.MG_Game_Manufacturer_ID   
 LEFT JOIN GAME_Title WITH(NOLOCK)  ON GAME_LIBRARY.MG_Group_id = Game_Title.Game_Title_id    
 LEFT JOIN GAME_CATEGORY  WITH(NOLOCK) ON GAME_CATEGORY.Game_Category_ID = GAME_Title.Game_Category_ID     
 WHERE (@Machine_Type_ID IS NULL OR MC.Machine_Type_ID = @Machine_Type_ID  )    
 AND ((ISNULL(M.Machine_Status_Flag,2) = 1 AND ISNULL(M.Machine_End_Date,'') = '' ) OR M.Machine_Status_Flag IS NULL)
 --AND ((ISNULL(M.Machine_Status_Flag,0) IN (6) and M.Machine_End_Date is not null) OR ISNULL(M.Machine_End_Date,'') = '')
 AND (@Operator_ID IS NULL OR M.Operator_ID = @Operator_ID)    
 AND (ISNULL(MT.Machine_Type_Code,'') <> '')    
 AND (@Depot_ID IS NULL OR M.Depot_ID = @Depot_ID)    
 AND (@Staff_ID IS NULL OR M.Staff_ID = @Staff_ID)    
 AND (@ModelTypeID IS NULL OR M.Machine_ModelTypeID = @ModelTypeID)      
 AND (@Manufacturer_ID IS NULL OR MC.Manufacturer_ID = @Manufacturer_ID)    
 AND (@MG_Game_ID IS NULL OR  GAME_Title.Game_Title_ID = @MG_Game_ID)
AND ((@Game_Category_ID IS NULL OR GAME_CATEGORY.Game_Category_ID = @Game_Category_ID))    
 AND ( '--ANY--' = @MG_Game_Name OR  Game_Title.Game_Title = @MG_Game_Name)
AND (@SearchCriteria IS NULL OR (MC.Machine_Name LIKE @SearchCriteria    
 OR M.Machine_Stock_No LIKE @SearchCriteria    
 OR M.Machine_Manufacturers_Serial_No LIKE @SearchCriteria    
 OR M.Machine_Alternative_Serial_Numbers LIKE @SearchCriteria    
 OR MC.Machine_Class_Model_Code LIKE @SearchCriteria    
 OR MC.Machine_BACTA_Code LIKE @SearchCriteria  
 OR GAME_LIBRARY.MG_Game_Name LIKE @SearchCriteria))    
 ORDER BY       
  Orderby_Operator_Name ASC,    
  Orderby_Operator_ID ASC,    
  Orderby_Depot_Name ASC,    
  Orderby_Depot_ID ASC,    
  Orderby_Machine_Status_Flag ASC,    
  Orderby_Staff_Last_Name ASC,     
  Orderby_Staff_First_Name ASC,    
  Orderby_Game_Category_ID ASC ,    
  Orderby_MG_Game_ID ASC,     
  MT.Machine_Type_Code ASC,    
  MT.Machine_Type_ID ASC,    
  Machine_Name ASC,    
  Machine_Class_ID ASC,    
  M.Machine_Stock_No ASC,    
  Machine_ID ASC    
  
/*    
* To Retreive In Stock Assets    
*/    
  
 SELECT   
  NULL as Bar_Position_Name,            
  NULL as Site_ZonaRice,    
  M.Staff_ID,M.Machine_Transit_Site_Code, M.Machine_End_Date,           
  Op.Operator_ID,Op.Operator_Name,           
  D.Depot_ID, D.Depot_Name,           
  MT.Machine_Type_ID, MT.Machine_Type_Code,MT.IsNonGamingAssetType,           
  CASE WHEN (M.Machine_Status_Flag = 13) THEN '' ELSE MC.Machine_Name END Machine_Name,           
  CASE WHEN (M.Machine_Status_Flag = 13) THEN '' ELSE MC.Machine_Class_ID END Machine_Class_ID,           
  M.Machine_Stock_No,M.Machine_Manufacturers_Serial_No, M.Machine_Alternative_Serial_Numbers,          
  CASE WHEN (M.Machine_Status_Flag = 13) THEN 0 ELSE M.Machine_ID END Machine_ID, M.Machine_Status_Flag,          
  MC.Machine_BACTA_Code, CASE WHEN (M.Machine_Status_Flag = 13) THEN '' ELSE MC.Machine_Class_Model_Code END Machine_Class_Model_Code,           
  Staff.Staff_First_Name, Staff.Staff_Last_Name,          
  MT.Machine_Type_Code AS Machine_Category_Code,           
  Manufacturer.Manufacturer_Name,           
  NULL as Site_Code, NULL as Site_Name ,          
  0 As PaytableFlag,          
  CASE WHEN @OrderBy = 'Game Category' THEN GAME_CATEGORY.Game_Category_ID ELSE '' END AS Game_Category_ID  ,          
  CASE WHEN @OrderBy = 'Game Category' THEN GAME_CATEGORY.Game_Category_Name  ELSE '' END AS Game_Category_Name,          
  CASE WHEN @OrderBy = 'Depot' THEN Op.Operator_Name ELSE '' END AS Orderby_Operator_Name ,          
  CASE WHEN @OrderBy = 'Depot' THEN Op.Operator_ID ELSE 0 END AS Orderby_Operator_ID ,          
  CASE WHEN @OrderBy = 'Depot' THEN D.Depot_Name ELSE '' END AS Orderby_Depot_Name ,          
  CASE WHEN @OrderBy = 'Depot' THEN D.Depot_ID ELSE 0 END AS Orderby_Depot_ID ,          
  CASE WHEN @OrderBy = 'Availability' THEN  M.Machine_Status_Flag ELSE 0 END AS Orderby_Machine_Status_Flag ,          
  CASE WHEN @OrderBy = 'Representative' THEN Staff.Staff_Last_Name ELSE '' END AS Orderby_Staff_Last_Name,            
  CASE WHEN @OrderBy = 'Representative' THEN Staff_First_Name ELSE  '' END AS Orderby_Staff_First_Name ,          
  CASE WHEN @OrderBy = 'Game Category' THEN GAME_CATEGORY.Game_Category_ID ELSE 0 END  AS Orderby_Game_Category_ID ,          
  CASE WHEN @OrderBy = 'GMG_Game_ID' THEN Game_Title.Game_Title_ID ELSE 0 END  AS Orderby_MG_Game_ID    
 INTO #TempInStock    
 FROM ((((((Machine M WITH(NOLOCK) RIGHT JOIN Machine_Class MC WITH(NOLOCK)  ON MC.Machine_Class_ID = M.Machine_Class_ID    
 AND ISNULL(M.Machine_Status_Flag,2) IN (0,13))    
 RIGHT JOIN Machine_Type MT WITH(NOLOCK) ON MT.Machine_Type_ID = MC.Machine_Type_ID )           
 LEFT JOIN Depot D WITH(NOLOCK) ON M.Depot_ID = D.Depot_ID)           
 LEFT JOIN Operator Op WITH(NOLOCK) ON D.Supplier_ID = Op.Operator_ID)           
 LEFT JOIN Staff WITH(NOLOCK) ON M.Staff_ID = Staff.Staff_ID))    
 LEFT JOIN Manufacturer WITH(NOLOCK)  ON MC.Manufacturer_ID = Manufacturer.Manufacturer_ID    
 LEFT JOIN GAME_Title WITH(NOLOCK)  ON MC.Machine_Name   = Game_Title.Game_Title  
 LEFT JOIN GAME_CATEGORY  WITH(NOLOCK) ON GAME_CATEGORY.Game_Category_ID = GAME_Title.Game_Category_ID    
 WHERE (@Machine_Type_ID IS NULL OR MC.Machine_Type_ID = @Machine_Type_ID  )          
 AND (ISNULL(M.Machine_Status_Flag,2) IN (0,13))
 AND ((ISNULL(M.Machine_Status_Flag,0) = 13 and M.Machine_End_Date IS NOT NULL) OR ISNULL(M.Machine_End_Date,'') = '')    
 AND (@Operator_ID IS NULL OR M.Operator_ID = @Operator_ID)          
 AND (ISNULL(MT.Machine_Type_Code,'') <> '')          
 AND (@Depot_ID IS NULL OR M.Depot_ID = @Depot_ID)          
 AND (@Staff_ID IS NULL OR M.Staff_ID = @Staff_ID)          
 AND (@ModelTypeID IS NULL OR M.Machine_ModelTypeID = @ModelTypeID)            
 AND (@Manufacturer_ID IS NULL OR MC.Manufacturer_ID = @Manufacturer_ID)          
 AND (@MG_Game_ID IS NULL OR GAME_Title.Game_Title_ID = @MG_Game_ID)           
 AND ((@Game_Category_ID IS NULL OR GAME_CATEGORY.Game_Category_ID = @Game_Category_ID))  
 AND ( '--ANY--' = @MG_Game_Name OR MC.Machine_Name = @MG_Game_Name)  
 AND (@SearchCriteria IS NULL OR (MC.Machine_Name LIKE @SearchCriteria          
 OR M.Machine_Stock_No LIKE @SearchCriteria          
 OR M.Machine_Manufacturers_Serial_No LIKE @SearchCriteria          
 OR M.Machine_Alternative_Serial_Numbers LIKE @SearchCriteria          
 OR MC.Machine_Class_Model_Code LIKE @SearchCriteria          
 OR MC.Machine_BACTA_Code LIKE @SearchCriteria        
 OR Game_Title.Game_Title LIKE @SearchCriteria))       
 ORDER BY           
  Orderby_Operator_Name ASC,        
  Orderby_Operator_ID ASC,        
  Orderby_Depot_Name ASC,        
  Orderby_Depot_ID ASC,        
  Orderby_Machine_Status_Flag ASC,        
  Orderby_Staff_Last_Name ASC,         
  Orderby_Staff_First_Name ASC,        
  Orderby_Game_Category_ID ASC ,        
  Orderby_MG_Game_ID ASC,         
  MT.Machine_Type_Code ASC,        
  MT.Machine_Type_ID ASC,        
  Machine_Name ASC,        
  Machine_Class_ID ASC,        
  M.Machine_Stock_No ASC,        
  M.Machine_ID ASC        
  
/*    
* Final Results based on Machine Status filter    
*/    
  
 SELECT DISTINCT * FROM (  
  SELECT   
  Bar_Position_Name,  
  Site_ZonaRice,            
  Staff_ID,Machine_Transit_Site_Code,Machine_End_Date,             
  Operator_ID,Operator_Name,             
  Depot_ID,Depot_Name,             
  Machine_Type_ID, Machine_Type_Code,IsNonGamingAssetType,             
  Machine_Name,             
  Machine_Class_ID,             
  Machine_Stock_No,Machine_Manufacturers_Serial_No, Machine_Alternative_Serial_Numbers,            
  Machine_ID,Machine_Status_Flag,            
  Machine_BACTA_Code, Machine_Class_Model_Code,             
  Staff_First_Name,Staff_Last_Name,            
  Machine_Category_Code,             
  Manufacturer_Name,             
  Site_Code,Site_Name ,            
  PaytableFlag,            
  Game_Category_ID  ,            
  Game_Category_Name,            
  Orderby_Operator_Name ,            
  Orderby_Operator_ID ,            
  Orderby_Depot_Name ,            
  Orderby_Depot_ID ,            
  Orderby_Machine_Status_Flag ,            
  Orderby_Staff_Last_Name,              
  Orderby_Staff_First_Name ,            
  Orderby_Game_Category_ID ,            
  Orderby_MG_Game_ID    
  FROM #TempInUse WHERE ((#TempInUse.Machine_Status_Flag in       
  (SELECT IntItem FROM dbo.Fn_GetIntTableFromStringList(@Machine_Status))) OR #TempInUse.Machine_Status_Flag  is null)  
  UNION
  SELECT   
  RIGHT('000' + CONVERT(VARCHAR(3),Bar_Position_Name),3) Bar_Position_Name,   
  CONVERT(VARCHAR(30),Site_ZonaRice) Site_ZonaRice,  
  Staff_ID,Machine_Transit_Site_Code,Machine_End_Date,             
  Operator_ID,Operator_Name,             
  Depot_ID,Depot_Name,             
  Machine_Type_ID, Machine_Type_Code,IsNonGamingAssetType,             
  Machine_Name,             
  Machine_Class_ID,             
  Machine_Stock_No,Machine_Manufacturers_Serial_No, Machine_Alternative_Serial_Numbers,            
  Machine_ID,Machine_Status_Flag,            
  Machine_BACTA_Code, Machine_Class_Model_Code,             
  Staff_First_Name,Staff_Last_Name,            
  Machine_Category_Code,             
  Manufacturer_Name,             
  CONVERT(VARCHAR(30),Site_Code) Site_Code,  
  CONVERT(VARCHAR(30),Site_Name) Site_Name,
  PaytableFlag,            
  Game_Category_ID  ,            
  Game_Category_Name,            
  Orderby_Operator_Name ,            
  Orderby_Operator_ID ,            
  Orderby_Depot_Name ,            
  Orderby_Depot_ID ,            
  Orderby_Machine_Status_Flag ,            
  Orderby_Staff_Last_Name,              
  Orderby_Staff_First_Name ,            
  Orderby_Game_Category_ID ,            
  Orderby_MG_Game_ID    
  FROM #TempInStock WHERE ISNULL(#TempInStock.Machine_Status_Flag,0)  in (SELECT IntItem FROM dbo.Fn_GetIntTableFromStringList(@Machine_Status))  
  )  x  
 ORDER BY             
 Orderby_Operator_Name ASC,          
 Orderby_Operator_ID ASC,          
 Orderby_Depot_Name ASC,          
 Orderby_Depot_ID ASC,          
 Orderby_Machine_Status_Flag ASC,          
 Orderby_Staff_Last_Name ASC,           
 Orderby_Staff_First_Name ASC,          
 Orderby_Game_Category_ID ASC ,     
 Orderby_MG_Game_ID ASC,           
 Machine_Type_Code ASC,          
 Machine_Type_ID ASC,          
 Machine_Name ASC,          
 Machine_Class_ID ASC,          
 Machine_Stock_No ASC,          
 Machine_ID ASC          
  
END
GO