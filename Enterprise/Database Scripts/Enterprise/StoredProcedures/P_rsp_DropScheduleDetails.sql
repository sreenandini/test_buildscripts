USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_DropScheduleDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_DropScheduleDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_DropScheduleDetails]     
AS    
BEGIN    
SELECT         
 S.REGION,  
 SCR.Sub_Company_Region_Name,     
 S.Site_Name,     
 S.Site_Code,     
 BP.Bar_Position_Id,     
 M.MACHINE_STOCK_NO AS AssetNumber,    
 BP.BAR_POSITION_NAME,     
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
 LEFT JOIN SUB_COMPANY_REGION SCR ON SCR.SUB_COMPANY_ID=SC.SUB_COMPANY_ID  
ORDER BY S.SITE_ID     
END     

GO
