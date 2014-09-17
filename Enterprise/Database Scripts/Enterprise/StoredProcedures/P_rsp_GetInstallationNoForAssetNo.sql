USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetInstallationNoForAssetNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetInstallationNoForAssetNo]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dbo.rsp_GetInstallationNoForAssetNo  
(  
 @Machine_Stock_No varchar(50),  
 @Installation_No INT = 0 OUTPUT  
)    
AS    
BEGIN    
 SET NOCOUNT ON  
   
 SELECT @Installation_No = Installation_ID     
  FROM installation inst WITH(NOLOCK)  
  inner Join machine mac WITH(NOLOCK) on inst.Machine_ID = mac.Machine_ID    
   WHERE mac.Machine_Stock_No =@Machine_Stock_No  
     
 SET NOCOUNT OFF  
END    

GO

