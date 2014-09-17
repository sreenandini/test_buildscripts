USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[esp_InsertGameTypes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[esp_InsertGameTypes]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* Revision History
* Insert the game types sent to CMP. 
*  Anuradha			Created		16 Jan 2012
*  Selva Kumar S	Modified	05 Jun 2012		Update the game type based on Machine_Id
*/
/*
Select * from tblCMPGameTypes
Select * from EBS_Export_History order by 1 desc
 
*/


CREATE PROCEDURE esp_InsertGameTypes
(
@GameType VARCHAR(50),
@GamePrefix CHAR(1)
)
AS

BEGIN
	
	IF NOT EXISTS (SELECT 1 FROM tblCMPGameTypes WITH(NOLOCK) WHERE GamePrefix=@GamePrefix)
	BEGIN
		INSERT INTO tblCMPGameTypes
		(
			GameTypeCode,
			GamePrefix
	
		)
		VALUES
		(
			@GameType,
			@GamePrefix
			
		)
		
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (START)  
 *****************************************************************************************************/  
  EXEC [dbo].[usp_EBS_UpdateGameDetails] @GamePrefix    
    
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (END)  
 *****************************************************************************************************/    		

		
	END
	ELSE
	BEGIN
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (START)  
 *****************************************************************************************************/  
  DECLARE @_Modified TABLE (  
            OldFlag VARCHAR(50),  
            NewFlag VARCHAR(50),  
            FlagChanged AS (CASE WHEN OldFlag = NewFlag THEN 0 ELSE 1 END)  
     )  
 /*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (END)  
 *****************************************************************************************************/ 
		UPDATE tblCMPGameTypes 
		SET	GameTypeCode = @GameType
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (START)  
 *****************************************************************************************************/  
		OUTPUT   
            DELETED.GameTypeCode,  
            INSERTED.GameTypeCode  
               INTO @_Modified  
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (END)  
 *****************************************************************************************************/  
		WHERE GamePrefix = @GamePrefix
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (START)  
 *****************************************************************************************************/  
IF EXISTS(  
       SELECT 1  
       FROM   @_Modified m  
       WHERE  m.FlagChanged = 1  
                  )  
     BEGIN  
		EXEC [dbo].[usp_EBS_UpdateGameDetails] @GamePrefix  
     END   
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (END)  
 *****************************************************************************************************/       
	END

END	
GO

