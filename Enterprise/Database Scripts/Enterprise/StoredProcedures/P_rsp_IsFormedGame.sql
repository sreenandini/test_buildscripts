USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_IsFormedGame]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_IsFormedGame]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dbo.rsp_IsFormedGame
	@Game_Ids VARCHAR(MAX)
AS
/*****************************************************************************************************
DESCRIPTION : PROC Description  
CREATED DATE: 30 - 11 - 2012
MODULE      : PROC used in Modules      
CHANGE HISTORY :
EXAMPLE		: rsp_IsFormedGame '127,123'
			  rsp_IsFormedGame '2,127,4'
			  rsp_IsFormedGame '19,27'
------------------------------------------------------------------------------------------------------
AUTHOR                              DESCRIPTON                                                        MODIFIED DATE
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/
BEGIN	

	DECLARE @Mac_Class_ID     VARCHAR(MAX)
	DECLARE @Mac_Type_ID     VARCHAR(MAX)	
		
	SET @Game_Ids = ',' + @Game_Ids + ',';
	
	WITH InstallationDetail AS (
	    SELECT 
	           Ig.Installation_No
	    FROM   Installation_Game_Info
	           Ig WITH(NOLOCK)
	           INNER JOIN Game_Library
	                      gl WITH(NOLOCK)
	                ON GL.MG_Game_ID = IG.IGI_Game_ID
	    WHERE  gl.MG_Game_ID IN (SELECT [str]
	                                        FROM   dbo.iter_charlist_to_tbl(@Game_Ids, ','))
	),
	AllFormedGame AS (
	    SELECT GL.MG_Game_ID,
	           M.Machine_Class_ID,
	           Mc.Machine_Type_ID
	    FROM   Installation_Game_Info IG WITH(NOLOCK)
			   INNER JOIN Game_Library GL WITH(NOLOCK)
			   ON GL.MG_Game_ID = IG.IGI_Game_ID
	           INNER JOIN InstallationDetail ID
	                ON  IG.Installation_No = ID.Installation_No
	           INNER JOIN Installation
	                I WITH(NOLOCK)
	                ON  I.Installation_ID = ID.Installation_No
	           INNER JOIN MACHINE
	                M WITH(NOLOCK)
	                ON  M.Machine_ID = I.Machine_ID
	           INNER JOIN Machine_Class
					MC  WITH(NOLOCK)
					ON MC.Machine_Class_ID = M.Machine_Class_ID
		WHERE M.IsMultiGame = 0
	)
	
	SELECT @Game_Ids = COALESCE(@Game_Ids, ',') +
	       CASE 
	            WHEN (
	                     @Game_Ids LIKE '%,' + CAST(MG_Game_ID AS VARCHAR(10)) +
	                     ',%'
	                 ) THEN ''
	            ELSE CAST(MG_Game_ID AS VARCHAR(10)) + ','
	       END,
	       @Mac_Class_ID = COALESCE(@Mac_Class_ID, ',') +
	       CASE 
	            WHEN (
	                     @Mac_Class_ID LIKE '%,' + CAST(Machine_Class_ID AS VARCHAR(10)) 
	                     + '-' + CAST(Machine_Type_ID AS VARCHAR(10))
	                     + ',%'
	                 ) THEN ''
	            ELSE CAST(Machine_Class_ID AS VARCHAR(10)) + '-' + CAST(Machine_Type_ID AS VARCHAR(10)) + ','
	       END
	FROM   AllFormedGame
	
	IF (ISNULL(@Mac_Class_ID, '') = '')
	BEGIN
	    SELECT 1 AS IsMultiGame
	END
	ELSE
	BEGIN
	    SELECT 
	           SUBSTRING(@Game_Ids, 2, LEN(@Game_Ids) -2) AS Game_ID,
	           SUBSTRING(@Mac_Class_ID, 2, LEN(@Mac_Class_ID) -2) AS 
	           Machine_Class_ID,
	           0 AS IsMultiGame
	END
END

GO
