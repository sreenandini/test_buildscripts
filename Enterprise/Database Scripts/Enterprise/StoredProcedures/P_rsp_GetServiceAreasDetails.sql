USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetServiceAreasDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetServiceAreasDetails]
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
* <Name of the owner>         DateCreated       Type(Created/Modified)  Description    
   Kalaiyarasan.P             25-May-2012         Created               This SP is used to get ServiceAreas details     
                                                                        based on Depot_ID     
*/    
  
CREATE PROCEDURE rsp_GetServiceAreasDetails  @Depot_ID as int    
         
AS    
SELECT Service_Area_ID        
      ,Service_Area_Name       
  FROM Service_Areas WHERE Depot_ID =@Depot_ID


GO

