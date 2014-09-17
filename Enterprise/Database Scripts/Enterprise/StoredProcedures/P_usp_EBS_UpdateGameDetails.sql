USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EBS_UpdateGameDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EBS_UpdateGameDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
/*
[usp_EBS_UpdateGameDetails] 'c'
Select * from EBS_Export_History
Select * from tblCMPGameTypes
*/
CREATE PROCEDURE [dbo].[usp_EBS_UpdateGameDetails](@GamePrefix CHAR)  
AS  
BEGIN  
SET NOCOUNT ON
 DECLARE @Game  TABLE (  
             GameID VARCHAR(1) ,  
             GameName VARCHAR(50),  
             IsActive BIT 
         )  
   
 DECLARE @Value    XML   
 
 INSERT INTO @Game  
 EXEC [dbo].[rsp_EBS_GetGameDetails] @SiteCode =NULL, @GamePrefix = @GamePrefix  
   
 SELECT @Value = (  
            SELECT 
            (CASE CAST(ISNULL(G.GameID, '') AS VARBINARY(1)) WHEN 0x00 THEN '' ELSE CAST(ISNULL(G.GameID, '') AS VARBINARY(1)) END) AS GameID,
              GameName,
              IsActive
            FROM  @Game G  
                   FOR XML PATH('Game'),  
                   TYPE,  
                   ELEMENTS,  
                   ROOT('Games')  
        )  

IF @Value IS NOT NULL
BEGIN 
	EXEC [dbo].[usp_EBS_InsertExportHistory] @EH_Type = 'Game', @EH_Value = @Value,  @EH_SiteCode = 0 
END
SET NOCOUNT OFF
END  
GO

