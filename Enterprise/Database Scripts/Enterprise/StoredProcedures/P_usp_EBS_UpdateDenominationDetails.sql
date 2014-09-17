USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EBS_UpdateDenominationDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EBS_UpdateDenominationDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
/*
[usp_EBS_UpdateDenominationDetails] 136
Select Installation_ID,Installation_Price_Per_Play, * from Installation
Select * from EBS_Export_History
*/
CREATE PROCEDURE [dbo].[usp_EBS_UpdateDenominationDetails](@Installation_No INT)  
AS  
BEGIN  
SET NOCOUNT ON
 DECLARE @Denomination  TABLE (  
             DenominationId VARCHAR(50) ,  
             DenominationName VARCHAR(50), 
             DenominationValue VARCHAR(50), 
             IsActive BIT 
         )  
   
 DECLARE @Site_Code VARCHAR(50)  
 DECLARE @Value    XML   
 DECLARE @Denomination_Id Float
 
 SELECT @Site_Code = Site_Code, @Denomination_Id = CAST(ISNULL(Installation_Price_Per_Play,0) AS Float)/100 From Site S
 INNER JOIN Bar_Position BP ON BP.Site_ID = S.Site_ID
 INNER JOIN Installation I ON I.Bar_Position_ID = BP.Bar_Position_ID
WHERE I.Installation_ID =  @Installation_No

 INSERT INTO @Denomination  
 EXEC [dbo].[rsp_EBS_GetDenominationDetails]  @SiteCode = @Site_Code,  @Denomination_Id = @Denomination_Id  
   
 SELECT @Value = (  
            SELECT 
            DenominationId,
            DenominationName,
            DenominationValue,
              IsActive
            FROM  @Denomination   
                   FOR XML PATH('Denomination'),  
                   TYPE,  
                   ELEMENTS,  
                   ROOT('Denominations')  
        )  

IF @Value IS NOT NULL
BEGIN  
	 EXEC [dbo].[usp_EBS_InsertExportHistory] @EH_Type = 'Denom',  
		@EH_Value = @Value, @EH_SiteCode = @Site_Code  
END
SET NOCOUNT OFF
END  
GO

