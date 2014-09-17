USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetStdOpeningHrsDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetStdOpeningHrsDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
CREATE  PROCEDURE [dbo].[rsp_GetStdOpeningHrsDetails]   
(  
 @STDHRID INT  
)  
AS  
  
BEGIN  
  
SELECT   
 Standard_Opening_Hours_Description,   
 Standard_Opening_Hours_Open_Monday,  
 Standard_Opening_Hours_Open_Tuesday,  
 Standard_Opening_Hours_Open_Wednesday,  
 Standard_Opening_Hours_Open_Thursday,  
 Standard_Opening_Hours_Open_Friday,  
 Standard_Opening_Hours_Open_Saturday,  
 Standard_Opening_Hours_Open_Sunday   
FROM Standard_Opening_Hours   
WHERE  
Standard_Opening_Hours_ID = @STDHRID  
  
END


GO

