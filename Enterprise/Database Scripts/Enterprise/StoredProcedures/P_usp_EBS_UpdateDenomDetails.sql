USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EBS_UpdateDenomDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EBS_UpdateDenomDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* Revision History
* Insert the Denom Types. 
* Venkatesan H Created to Store all possible Denomination in seperate table
*/
/*
Select * from BaseDenom
Select * from EBS_Export_History order by 1 desc
 
*/


CREATE PROCEDURE usp_EBS_UpdateDenomDetails
(
@Name VARCHAR(10) 
)
AS

BEGIN
DECLARE @Denom  TABLE (  
             DenominationId VARCHAR(50) ,  
             DenominationName VARCHAR(50),  
             DenominationValue VARCHAR(255),
             IsActive BIT 
         )  
DECLARE @Value    XML   

   INSERT INTO @Denom
	EXEC rsp_EBS_GetDenominationDetails @SiteCode = '', @Denomination_Id = @Name
	
	
	SELECT @Value = (  
            SELECT 
            DenominationId,
              DenominationName,
              DenominationValue,
              IsActive
            FROM  @Denom D  
                   FOR XML PATH('Denom'),  
                   TYPE,  
                   ELEMENTS,  
                   ROOT('Denoms')  
        )  

IF @Value IS NOT NULL
BEGIN 
	EXEC [dbo].[usp_EBS_InsertExportHistory] @EH_Type = 'Denom', @EH_Value = @Value,  @EH_SiteCode = 0 
END
SET NOCOUNT OFF
END  

GO

