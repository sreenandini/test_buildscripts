USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertUpdateBaseDenom]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertUpdateBaseDenom]
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


CREATE PROCEDURE usp_InsertUpdateBaseDenom
(
@Name VARCHAR(10),
@Description VARCHAR(255) = NULL,
@SysDelete BIT = 0 
)
AS

BEGIN
	
	IF NOT EXISTS (SELECT 1 FROM BaseDenom WITH(NOLOCK) WHERE Name=@Name AND [SysDelete] = 0)
	BEGIN
		INSERT INTO BaseDenom
		(
			Name,
			[Description],
			[SysDelete]
	
		)
		VALUES
		(
			@Name,
			@Description,
			@SysDelete
		)
		
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (START)  
 *****************************************************************************************************/  
  EXEC [dbo].[usp_EBS_UpdateDenomDetails] @Name    
    
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
            OldDescription VARCHAR(255),  
            NewDescription VARCHAR(255),
            OldSysDelete BIT,
            NewSysDelete BIT, 
            DescriptionChanged AS (CASE WHEN OldDescription = NewDescription THEN 0 ELSE 1 END),  
            SysDeleteChanged AS (CASE WHEN OldSysDelete = NewSysDelete THEN 0 ELSE 1 END)
     )  
 /*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (END)  
 *****************************************************************************************************/ 
		UPDATE BaseDenom 
		SET	[Description] = @Description,
		[SysDelete] = @SysDelete
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (START)  
 *****************************************************************************************************/  
		OUTPUT   
            DELETED.[Description],  
            INSERTED.[Description],
            DELETED.[SysDelete],
            INSERTED.[SysDelete]
               INTO @_Modified  
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (END)  
 *****************************************************************************************************/  
		WHERE Name = @Name
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (START)  
 *****************************************************************************************************/  
IF EXISTS(  
       SELECT 1  
       FROM   @_Modified m  
       WHERE  m.DescriptionChanged = 1 OR m.SysDeleteChanged = 1  
                  )  
     BEGIN  
		EXEC [dbo].[usp_EBS_UpdateDenomDetails] @Name  
     END   
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (END)  
 *****************************************************************************************************/       
	END

END	
GO

