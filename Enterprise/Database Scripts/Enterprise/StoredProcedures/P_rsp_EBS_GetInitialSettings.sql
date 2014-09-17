USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_EBS_GetInitialSettings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_EBS_GetInitialSettings]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[rsp_EBS_GetInitialSettings]

AS
BEGIN
	SELECT 
        SendDataToEBS,
        EBSEndPointURL,
        IsEBSEnabled,
        EBSVersion
        
 FROM   (  
            SELECT Setting_Name,  
                   Setting_Value  
            FROM   Setting  
        ) AS Source   
        PIVOT(  
            MAX(Setting_Value)   
            FOR Setting_Name IN (
        SendDataToEBS,
        EBSEndPointURL,
        IsEBSEnabled,
         EBSVersion
           )  
        ) AS Pvt  

	
END


GO


