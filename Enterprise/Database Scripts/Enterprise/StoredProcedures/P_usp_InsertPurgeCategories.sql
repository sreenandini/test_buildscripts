/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 8/13/2014 11:57:29 AM
 ************************************************************/

USE Enterprise
GO

IF EXISTS (
       SELECT 1
       FROM   sys.objects o
       WHERE  o.[name] = 'usp_InsertPurgeCategories'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE usp_InsertPurgeCategories
END     
 
 GO
 /*
 * revision history
 * 
 * Anuradha			Created			11 Aug 2014
 */
 
CREATE PROCEDURE usp_InsertPurgeCategories
	@doc VARCHAR(MAX)
	 --@PurgeCategoryname VARCHAR(200),
	 --@purgeType INT,
	 --@IsActive BIT
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @idoc INT 
	
	EXEC sp_xml_preparedocument @idoc OUTPUT,
	     @doc
	
	SELECT * INTO #tempPurge
	FROM   OPENXML(@idoc, 'PurgeRoot/PurgeCat', 2) 
	       WITH
	       (
	           CategoryName VARCHAR(50) './CategoryName',
	           PurgeType INT './purgeType',
	           IsActive BIT './IsActive'
	       )o
	
	IF NOT EXISTS (
	       SELECT 1
	       FROM   PurgeCategory pc
	              INNER JOIN #tempPurge tp
	                   ON  pc.PC_CategoryName = tp.CategoryName
	                   AND pc.PC_PurgeTypeID = tp.purgeType
	   )
	BEGIN
	    INSERT INTO PurgeCategory
	      (
	        -- PC_ID -- this column value is auto-generated,
	        PC_CategoryName,
	        PC_PurgeTypeID,
	        PC_IsActive
	      )
	    SELECT CategoryName,
	           PurgeType,
	           IsActive
	    FROM   #tempPurge
	END
END
GO
