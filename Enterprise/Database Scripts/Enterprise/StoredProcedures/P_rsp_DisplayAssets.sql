USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_DisplayAssets]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_DisplayAssets]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*      
 *Purpose: this stored procedure is to fetch Asset details       
 * Change History: exec [rsp_DisplayAssets]   
 * Vineetha  16-02-2009  created       
*/       
    
CREATE PROCEDURE [dbo].rsp_DisplayAssets
(
	@AssetNo as Varchar(100)=''
)    
AS         
  
SET DATEFORMAT dmy  
  
BEGIN
     IF @AssetNo = '' SET @AssetNo = NULL  
	   SELECT 	
			 M.Machine_ID AS Machine_ID
			,M.Machine_Stock_No AS Stock
			,Machine_Manufacturers_Serial_No AS SerialNo
			,MC.Machine_name AS MachineName	
			,MT.Machine_Type_Code AS GameCode
			,ISNULL(MT.Machine_Type_category,0) AS GameCategory			
		FROM Machine M
			JOIN Machine_Class MC ON MC.Machine_Class_ID = M.Machine_Class_ID
			JOIN Machine_Type MT ON MC.Machine_Type_ID = MT.Machine_Type_ID 	
		WHERE		
		(( @AssetNo IS NULL )OR ( @AssetNo IS NOT NULL AND M.Machine_Stock_No = @AssetNo ))   
		AND (M.Machine_end_date is null or M.Machine_end_date = '')      
END   

GO

