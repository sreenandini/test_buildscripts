USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetBarPositionDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetBarPositionDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetBarPositionDetails](
@EHTYPE AS VARCHAR(50),
@BarPosName AS VARCHAR(50) = NULL)
AS
--GET THE BAR POS IDS and Bar Position names  FROM 
--bar position and EXPORT HISTORY BASED ON eh type

SELECT bar_position_name as [Bar Pos Name],bar_position_id as [Bar Pos ID],
SITE.SITE_CODE
 FROM BAR_POSITION 
INNER JOIN [SITE]
ON BAR_POSITION.SITE_ID=SITE.SITE_ID
INNER JOIN EXPORT_HISTORY 
ON
BAR_POSITION_ID = EH_REFERENCE1  
AND 
EH_TYPE=''+ @EHTYPE + ''
AND(EH_Status is not null and EH_Status = '1')
AND ( ( @BarPosName IS NULL )     
         OR  
           ( @BarPosName IS NOT NULL   
             AND  
              BAR_POSITION.bar_position_name in(@BarPosName)
			
			)  
       )  

GO

