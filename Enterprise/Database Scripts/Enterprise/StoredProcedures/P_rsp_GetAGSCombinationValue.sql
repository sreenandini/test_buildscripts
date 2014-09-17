
USE [ENTERPRISE]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAGSCombinationValue]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].rsp_GetAGSCombinationValue
GO
CREATE PROCEDURE dbo.rsp_GetAGSCombinationValue    
(@OK_A BIT OUTPUT, @OK_G BIT OUTPUT, @OK_S BIT OUTPUT)    
AS    
BEGIN    
 /*    
 * AGS Combination checking (START)     
 */    
 DECLARE @G_ActAssetNo   VARCHAR(50)    
 DECLARE @G_GMUNo        VARCHAR(50)    
 DECLARE @G_ActSerialNo  VARCHAR(50)    
 DECLARE @AGSVALUESTR    VARCHAR(50)    
 DECLARE @AGSVALUE       INT    
     
 SET @OK_A = 0    
 SET @OK_G = 0    
 SET @OK_S = 0    
 SET @G_ActAssetNo = NULL    
 SET @G_GMUNo = NULL    
 SET @G_ActSerialNo = NULL    
     
 EXEC dbo.rsp_GetSetting @Setting_Name = 'AGSValue',    
      @Setting_Value = @AGSVALUESTR OUTPUT    
     
 SET @AGSVALUE = CAST(ISNULL(@AGSVALUESTR, 0) AS INT)    
 SELECT @AGSVALUE    
     
 -- Serial Number    
 IF (@AGSVALUE = 0)    
 BEGIN    
     SET @OK_A = 1    
     SET @OK_G = 1    
     SET @OK_S = 1    
 END    
 ELSE    
 BEGIN    
     IF (    
            @AGSVALUE = 8    
            OR @AGSVALUE = 12    
            OR @AGSVALUE = 24    
            OR @AGSVALUE = 28    
        )    
         -- Asset No    
         SET @OK_A = 1    
         
     IF (    
            @AGSVALUE = 16    
            OR @AGSVALUE = 20    
            OR @AGSVALUE = 24    
            OR @AGSVALUE = 28    
        )    
         -- GMU No    
         SET @OK_G = 1    
         
     IF (    
            @AGSVALUE = 4    
            OR @AGSVALUE = 12    
            OR @AGSVALUE = 20    
            OR @AGSVALUE = 28    
        )    
         -- Serial No    
         SET @OK_S = 1    
 END    
END    
GO

