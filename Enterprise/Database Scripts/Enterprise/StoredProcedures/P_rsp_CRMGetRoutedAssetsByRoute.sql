use Enterprise
GO 
IF EXISTS(SELECT * FROM SYS.OBJECTS  WHERE NAME = 'rsp_CRMGetRoutedAssetsByRoute')
BEGIN 
	DROP PROC rsp_CRMGetRoutedAssetsByRoute  
END 
GO 
CREATE PROC dbo.rsp_CRMGetRoutedAssetsByRoute 
@Route_No int
/************************************************************************************
Used In(Module) : Route Manager
Created Date	:
Description		: Returns all routed assets by route number
======================================================================================
Modification History
	Developer		      Date		Modification 					
======================================================================================
1) K.Karthicksundar			  		Created 	
2)	
**************************************************************************************/
As 
BEGIN 
	SELECT  BP.Bar_Position_ID,
			Bar_Position_Name,
			isnull(M.Machine_Manufacturers_Serial_No,'') Machine_Manufacturers_Serial_No,
			isnuLL(Machine_Stock_No,'') Machine_Stock_No ,
			RM.Route_ID
	FROM dbo.Route_Member  RM
	INNER JOIN  dbo.Bar_Position BP On RM.Bar_Position_ID = BP.Bar_Position_ID  and   RM.Route_ID=@Route_No
	LEFT OUTER JOIN  dbo.installation I on BP.Bar_Position_ID = I.Bar_Position_ID  AND I.Installation_End_Date IS NULL 
	LEFT OUTER  JOIN dbo.Machine M ON M.Machine_ID = I.Machine_ID  
	LEFT OUTER  JOIN Machine_Class MC ON M.Machine_Class_ID = MC.Machine_Class_ID  
	LEFT OUTER  JOIN MAchine_Type MT ON MT.Machine_Type_ID = MC.Machine_Type_ID  
	WHERE ISNULL(MT.IsNonGamingAssetType,0) <> 1 
	Order by  BP.Bar_Position_ID
	 

END

go 