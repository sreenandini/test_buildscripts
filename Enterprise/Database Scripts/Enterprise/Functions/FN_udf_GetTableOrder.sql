USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[udf_GetTableOrder]')
              AND TYPE IN (N'FN', N'IF', N'TF', N'FS', N'FT')
   )
    DROP FUNCTION [dbo].udf_GetTableOrder
GO

CREATE FUNCTION [dbo].udf_GetTableOrder
(
	@Mode_Id INT
)
RETURNS @TableList TABLE
        (
            TableLevel INT,
            FRL_DB VARCHAR(50),
            FRL_Table VARCHAR(200),
            FRL_UniqueColumn VARCHAR(200),
            FRL_RefTable VARCHAR(200),
            FRL_RefColumn VARCHAR(200)
        )
--/*****************************************************************************************************
--DESCRIPTION    :	Based on Mode_ID gets the table list from Factory_Reset_List table and re-orders
--					the list based on relationship. Returns the re-ordered list to calling SP
--CREATED DATE   : 
--MODULE		   : Called in usp_ResetTable SP for Factory Reset
--Example		   : =============================================
--					 SELECT * FROM 	udf_GetTableOrder(@Mode_Id)
--					 GO
--					 =============================================
--CHANGE HISTORY :
--------------------------------------------------------------------------------------------------------
--AUTHOR						MODIFIED DATE		DESCRIPTON
--------------------------------------------------------------------------------------------------------

--*****************************************************************************************************/
BEGIN
	DECLARE @level  INT -- Current depth
	        ,
	        @count  INT 
	
	DECLARE  @TmpTable TABLE (TableLevel INT, TableID INT, TableName VARCHAR(200))
	
	-- Step 1: Start with tables that have no FK dependencies
	--
	
	INSERT @TmpTable
	SELECT 0 AS TableLevel,
				St.object_id AS TableID,
				ST.name AS TableName	       
	FROM   Sys.Tables St
	       JOIN Sys.Schemas SSchm
	            ON  St.schema_id = SSchm.schema_id
	WHERE  NOT EXISTS
	       (
	           SELECT 1
	           FROM   Sys.Foreign_keys SFk
	           WHERE  SFk.parent_object_id = St.object_id
	       )
	
	SET @count = @@rowcount         
	SET @level = 0
	
	
	-- Step 2: For a given depth this finds tables joined to
	-- tables at this given depth.  A table can live at multiple
	-- depths if it has more than one join path into it, so we
	-- filter these out in step 3 at the end.
	--
	WHILE @count > 0
	BEGIN
	    INSERT @TmpTable
	      (
	      	TableLevel,
	      	TableID,
	        TableName	        
	      )
	    SELECT @level + 1 AS TableLevel,
	    St.object_id AS TableID,
	    St.name AS TableName
	           
	           
	    FROM   Sys.Tables St
	           JOIN Sys.Schemas SSchm
	                ON  SSchm.schema_id = St.schema_id
	    WHERE  EXISTS
	           (
	               SELECT 1
	               FROM   Sys.Foreign_keys SFk
	                      JOIN @TmpTable Tab
	                           ON  SFk.referenced_object_id = Tab.TableID
	                           AND Tab.TableLevel = @level
	                           AND SFk.parent_object_id = St.object_id
	                           AND SFk.parent_object_id != SFk.referenced_object_id
	           )
	    -- The last line ignores self-joins.  You'll
	    -- need to deal with these separately
	    
	    SET @count = @@rowcount
	    SET @level = @level + 1
	END
	
	-- Step 3: This filters out the maximum depth an object occurs at
	-- and inserts the deepest first in return temp table.
	--
	INSERT @TableList
	SELECT Tab.TableLevel TableLevel,		   	
	       FRL.FRL_DB,
	       Tab.TableName,
	       FRL.FRL_UniqueColumn,
	       FRL.FRL_RefTable,
	       FRL.FRL_RefColumn
	FROM   @TmpTable Tab
	       JOIN (
	                SELECT TableName AS TableName,
	                       MAX(TableLevel) AS TableLevel
	                FROM   @TmpTable
	                GROUP BY
	                       TableName
	            ) Tt
	            ON  Tab.TableName = Tt.TableName
	            AND Tab.TableLevel = Tt.TableLevel
	       INNER JOIN Factory_Reset_List FRL
	            ON  FRL.FRL_Table = Tab.TableName
	WHERE  FRL.FRL_Mode_ID = @Mode_Id
	ORDER BY
	       Tab.TableLevel DESC
	
	RETURN
END
GO