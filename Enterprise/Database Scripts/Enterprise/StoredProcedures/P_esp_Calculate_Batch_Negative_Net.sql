/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 12/10/2013 2:47:11 PM
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[esp_Calculate_Batch_Negative_Net]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[esp_Calculate_Batch_Negative_Net]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


    
CREATE PROCEDURE dbo.esp_Calculate_Batch_Negative_Net
	@Site_Id INT = 0,
	@Batch_No INT = 0
AS
	SET DATEFORMAT dmy           
	SET NOCOUNT ON          
	
	DECLARE @newnegshare         FLOAT,
	        @SUBCOMPIDSETTING    INT,
	        @RETAILER_SHARE      FLOAT,
	        @SGVIENABLEDSETTING  VARCHAR(10),
	        @Batch_Negative_Net  FLOAT,
	        @PrevNegShare        FLOAT,
	        @cashtake            FLOAT,
	        @sub_company_id      INT,
	        --  @Site_ID            int,          
	        @Working_Batch_ID    INT,
	        @batch_date          DATETIME,
	        @Count               INT,
	        @Start               INT,
	        @IsRetailer          BIT 
	
	-- use this table instead of Cursor          
	DECLARE @WorkingTable        TABLE 
	        (Sno INT IDENTITY(1, 1), WorkingBatchID INT, Date DATETIME)          
	
	SET @RETAILER_SHARE = 0.0 
	
	-- don't run if not SGVI code.          
	EXEC rsp_GetSetting NULL,
	     'SGVI_Enabled',
	     'false',
	     @SGVIENABLEDSETTING OUTPUT
	
	IF @SGVIENABLEDSETTING = 'false'
	    RETURN (0) 
	
	-- get sub company and site id, based on batch no          
	SELECT @sub_company_id = Sub_Company_ID,
	       @Site_ID = s.Site_ID,
	       @batch_date = bat.batch_date,
	       @IsRetailer = Apply_Retailer_Share
	FROM   batch bat
	       INNER JOIN SITE s
	            ON  SUBSTRING(batch_ref, 1, CHARINDEX(',', batch_ref, 1) -1) = s.site_code
	WHERE  batch_id = @Batch_No 
	
	-- if company is an ent centre then set retailer share to zero.           
	IF @IsRetailer = 1
	BEGIN
	    EXEC rsp_GetSetting NULL,
	         'SGVI_Batch_Net_Value',
	         '0.22',
	         @RETAILER_SHARE OUTPUT
	END          
	
	SELECT @sub_company_id,
	       @site_id,
	       @RETAILER_SHARE 
	
	-- get a list of all batches and all subsequent from this one          
	DECLARE @working_batch_no  CHAR(11),
	        @tmpBD             DATETIME          
	
	SET @Start = 1          
	
	INSERT INTO @WorkingTable
	SELECT DISTINCT batch_id,
	       CAST(batch_date AS DATETIME) AS batchdate
	FROM   batch bat
	       INNER JOIN SITE s
	            ON  SUBSTRING(batch_ref, 1, CHARINDEX(',', batch_ref, 1) -1) = s.site_code
	WHERE  CAST(batch_date AS DATETIME) >= GETDATE()
	       AND s.Site_ID = @Site_ID
	ORDER BY
	       CAST(batch_date AS DATETIME)           
	
	SELECT @Count = COUNT(*)
	FROM   @WorkingTable          
	
	WHILE @Start <= @Count
	BEGIN
	    SELECT @Working_Batch_ID = WorkingBatchID,
	           @tmpBD = Date
	    FROM   @WorkingTable
	    WHERE  Sno = @Start          
	    
	    SELECT @PrevNegShare = ISNULL(BATCH_NEGATIVE_NET, 0)
	    FROM   dbo.Batch
	    WHERE  Batch_ID = (
	               SELECT MAX(bat.batch_id)
	               FROM   batch bat
	                      INNER JOIN SITE s
	                           ON  SUBSTRING(batch_ref, 1, CHARINDEX(',', batch_ref, 1) -1) = 
	                               s.site_code
	               WHERE  bat.batch_id < @Working_Batch_ID
	                      AND s.Site_ID = @Site_ID
	           ) 
	    
	    -- Get the cash take for this batch          
	    SELECT @cashtake = SUM(
	               (
	                   COALESCE(cs.Collection_Declared_Tickets, 0) + (cs.Collection_Declared_Notes) 
	                   + (cs.Collection_Declared_Coins)
	               ) -(
	                   dbo.GetAttendantPay(c.Collection_id) + (
	                       CAST(
	                           ISNULL(c.COLLECTION_RDC_TICKETS_PRINTED_VALUE, 0) 
	                           + ISNULL(c.RDC_TICKETS_PRINTED_NONCASHABLE_VALUE, 0) 
	                           AS FLOAT
	                       ) / 100
	                   )
	               )
	           )
	    FROM   dbo.Collection c
	           INNER JOIN dbo.Collection_Calcs cs
	                ON  cs.Collection_ID = c.Collection_ID
	    WHERE  c.batch_id = @Working_Batch_ID
	           AND cs.batch_id = @Working_Batch_ID 
	    
	    
	    -- multiply it by retailer share ( .22 - settingvalue ) + prevNegShare          
	    SET @newnegshare = (@cashtake * @RETAILER_SHARE) + @PrevNegShare 
	    
	    -- If < 0, set the new negshare to the newnegshare, else set to zero          
	    IF @newnegshare < 0
	        SET @Batch_Negative_Net = @newnegshare
	    ELSE
	        SET @Batch_Negative_Net = 0          
	    
	    SELECT @working_batch_id,
	           @cashtake,
	           @PrevNegShare,
	           @newnegshare 
	    
	    PRINT @Batch_Negative_Net 
	    --Update the collection batch table with the calculated value          
	    UPDATE BATCH
	    SET    Batch_Negative_Net = @Batch_Negative_Net
	    WHERE  BATCH_id = @Working_Batch_ID          
	    
	    SELECT @working_batch_id 
	    
	    --    FETCH NEXT FROM c1 INTO @Working_Batch_ID, @tmpBD           
	    
	    SET @Start = @Start + 1
	END 
	
	--CLOSE c1
	--DEALLOCATE c1          
	
	RETURN(0)
GO

