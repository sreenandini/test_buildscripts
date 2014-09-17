USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[esp_Calculate_Read_Negative_Net]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[esp_Calculate_Read_Negative_Net]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- esp_Calculate_Read_Negative_Net
-- -----------------------------------------------------------------
-- 
-- To calcultate negative net for read based records
-- 
-- -----------------------------------------------------------------
-- Revision History
-- 
-- 18/10/2012 Dinesh Rathinavel Created
-- 
-- =================================================================

CREATE PROCEDURE [dbo].[esp_Calculate_Read_Negative_Net]
	@Site_Id INT = NULL,
	@Retailer_Percentage DECIMAL
AS
BEGIN

	SET DATEFORMAT dmy
	SET NOCOUNT ON

	DECLARE @newnegshare  AS FLOAT
    DECLARE @PrevNegShare FLOAT
    DECLARE @Min_Read_No INT
	DECLARE @Max_Read_No INT

    -- get the prevNegShare
	SELECT TOP 1 @PrevNegShare = ISNULL([Negative_Net], 0) FROM [LiquidationDetails] WITH(NOLOCK) ORDER BY LiquidationId DESC

	SELECT
		TOP 1 @Min_Read_No = ISNULL(R.[Read_ID], 0) + 1
	FROM [Read] R WITH(NOLOCK)
		INNER JOIN [Installation] I WITH (NOLOCK) ON I.[Installation_ID] = R.[Installation_ID]
		INNER JOIN [Bar_Position] BP WITH (NOLOCK) ON BP.[Bar_Position_ID] = I.[Bar_Position_ID]
	WHERE R.Read_ID >= (SELECT TOP 1 ISNULL(LD.[ReadId], 0) FROM [LiquidationDetails] LD WITH (NOLOCK) ORDER BY LD.[ReadId] DESC)
		AND BP.[Site_ID] = @Site_Id
		ORDER BY R.[Read_ID] ASC

	SELECT @Min_Read_No = ISNULL(@Min_Read_No, 0)
	SELECT TOP 1 @Max_Read_No = ISNULL(MAX(Read_ID), 0) FROM [Read] WITH(NOLOCK)

	;WITH Liq_Summary_ForRead_CTE AS
	(
		SELECT
		(CAST(ISNULL(CASH_IN_1P, 0) AS FLOAT) * 0.01) AS Cash_Collected_1p,
			(CAST(ISNULL(CASH_IN_2P, 0) AS FLOAT) * 0.02) AS Cash_Collected_2p, --+ dbo.fn_IncludeMissing1P('COLL',Installation_No, LinkReference) ,    
			(CAST(ISNULL(CASH_IN_5P, 0) AS FLOAT) * 0.05) AS Cash_Collected_5p,    
			(CAST(ISNULL(CASH_IN_10P, 0) AS FLOAT) * 0.10) AS Cash_Collected_10p,    
			(CAST(ISNULL(CASH_IN_20P, 0) AS FLOAT) * 0.20) AS Cash_Collected_20p,    
			(CAST(ISNULL(CASH_IN_50P, 0) AS FLOAT) * 0.50) AS Cash_Collected_50p,    
			CAST(ISNULL(CASH_IN_100P, 0) AS FLOAT) AS Cash_Collected_100p,    
			(CAST(ISNULL(CASH_IN_200P, 0) AS FLOAT) * 2) AS Cash_Collected_200p,    
			(CAST(ISNULL(CASH_IN_500P, 0) AS FLOAT) * 5) AS Cash_Collected_500p,    
			(CAST(ISNULL(CASH_IN_1000P, 0) AS FLOAT) * 10) AS Cash_Collected_1000p,    
			(CAST(ISNULL(CASH_IN_2000P, 0) AS FLOAT) * 20) AS Cash_Collected_2000p,    
			(CAST(ISNULL(CASH_IN_5000P, 0) AS FLOAT) * 50) AS Cash_Collected_5000p,    
			(CAST(ISNULL(CASH_IN_10000P, 0) AS FLOAT) * 100) AS Cash_Collected_10000p,    
			(CAST(ISNULL(CASH_IN_20000P, 0) AS FLOAT) * 200) AS Cash_Collected_20000p,    
			(CAST(ISNULL(CASH_IN_50000P, 0) AS FLOAT) * 500) AS Cash_Collected_50000p,    
			(CAST(ISNULL(CASH_IN_100000P, 0) AS FLOAT) * 1000) AS Cash_Collected_100000p, 

			((CAST(ISNULL(READ_HANDPAY, 0) AS FLOAT) * ISNULL(I.Installation_Price_Per_Play, 0)) / 100) AS READ_HANDPAY,
			((CAST(COALESCE(Read_Ticket_In_Suspense, 0) AS FLOAT )) / 100) AS DeclaredTicketValue,    
			(CAST(COALESCE(READ_TICKET, 0) AS FLOAT )) / 100 AS DeclaredTicketPrintedValue
		FROM [Read] R WITH (NOLOCK) 
			INNER JOIN [Installation] I WITH (NOLOCK) ON I.[Installation_ID] = R.[Installation_ID]
			INNER JOIN [Bar_Position] BP WITH (NOLOCK) ON BP.[Bar_Position_ID] = I.[Bar_Position_ID]
		WHERE R.[Read_ID] BETWEEN @Min_Read_No AND @Max_Read_No
			AND ((@Site_Id IS NULL) OR (BP.[Site_ID] = @Site_Id))
	)
	
	SELECT
		CASE WHEN S.Region = 'US' Then          
		(    
			(    
				CAST(Cash_Collected_1p AS FLOAT) + 
				CAST(Cash_Collected_2p AS FLOAT) +               
				CAST(Cash_Collected_5p AS FLOAT) +               
				CAST(Cash_Collected_10p AS FLOAT) +               
				CAST(Cash_Collected_20p AS FLOAT) +               
				CAST(Cash_Collected_50p AS FLOAT)    
			)            
			-- - (CashRefills               
			-- + Treasury_Refills              
			-- + Treasury_Repayments              
			-- + Collection_Treasury_Defloat)              
		+              
			--Notes              
			(            
				CAST(Cash_Collected_100p AS FLOAT) +               
				CAST(Cash_Collected_200p AS FLOAT) +               
				CAST(Cash_Collected_500p AS FLOAT) +               
				CAST(Cash_Collected_1000p AS FLOAT) +               
				CAST(Cash_Collected_2000p AS FLOAT) +               
				CAST(Cash_Collected_5000p AS FLOAT) +            
				CAST(Cash_Collected_10000p AS FLOAT) +        
				CAST(Cash_Collected_20000p AS FLOAT) +        
				CAST(Cash_Collected_50000p AS FLOAT) +        
				CAST(Cash_Collected_100000p AS FLOAT)         
			)    
		+    
			--Tickets (in-out)    
			(    
				CAST(DeclaredTicketValue AS FLOAT)    
				-    
				CAST(DeclaredTicketPrintedValue AS FLOAT)    
			)    

		-    

			--Handpay    
			(CASE WHEN READ_HANDPAY = 0 THEN 0 ELSE (READ_HANDPAY / 100) END)    

			-- -              

			--shortpay              
			-- +               
			-- voids              

		)    
		WHEN S.Region = 'AR' THEN           
		--New Cash Take              
		(              
			--Net_Cash              
			(    
				
				(CAST(Cash_Collected_1p AS FLOAT) +   
					CAST(Cash_Collected_2p AS FLOAT) +               
				CAST(Cash_Collected_5p AS FLOAT) +               
				CAST(Cash_Collected_10p AS FLOAT) +               
				CAST(Cash_Collected_20p AS FLOAT) +               
				CAST(Cash_Collected_50p AS FLOAT) +          
				CAST(Cash_Collected_100p AS FLOAT))

			)             
			-- - (CashRefills               
			-- + Treasury_Refills              
			-- + Treasury_Repayments              
			-- + Collection_Treasury_Defloat)              
		+              
			--Notes              
			(            
				CAST(Cash_Collected_200p AS FLOAT) +           
				CAST(Cash_Collected_500p AS FLOAT) +               
				CAST(Cash_Collected_1000p AS FLOAT) +               
				CAST(Cash_Collected_2000p AS FLOAT) +               
				CAST(Cash_Collected_5000p AS FLOAT) +            
				CAST(Cash_Collected_10000p AS FLOAT) +        
				CAST(Cash_Collected_20000p AS FLOAT) +        
				CAST(Cash_Collected_50000p AS FLOAT) +        
				CAST(Cash_Collected_100000p AS FLOAT)         
			)      

		+    

			--Tickets (in-out)              
			(              
				CAST(DeclaredTicketValue AS FLOAT)    
				-    
				CAST(DeclaredTicketPrintedValue AS FLOAT)    
			)    

		-    

		--Handpay              
		(CASE WHEN READ_HANDPAY = 0 THEN 0 ELSE (READ_HANDPAY / 100) END)          

		-- -              
		--shortpay              
		-- +               
		-- voids              
		)    

		ELSE          
		(    
			--New Cash Take              
			--Net_Cash              
			(    
				CAST(Cash_Collected_1p AS FLOAT) + 
				CAST(Cash_Collected_2p AS FLOAT) +    
				CAST(Cash_Collected_5p AS FLOAT) +    
				CAST(Cash_Collected_10p AS FLOAT) +    
				CAST(Cash_Collected_20p AS FLOAT) +    
				CAST(Cash_Collected_50p AS FLOAT) +    
				CAST(Cash_Collected_100p AS FLOAT) +    
				CAST(Cash_Collected_200p AS FLOAT)       
			)    
			-- - (Collection.CashRefills               
			-- + Collection.Treasury_Refills              
			-- + Collection.Treasury_Repayments              
			-- + Collection.Collection_Treasury_Defloat)              
		+              
			--Notes              
			(            
				CAST(Cash_Collected_500p AS FLOAT) +               
				CAST(Cash_Collected_1000p AS FLOAT) +               
				CAST(Cash_Collected_2000p AS FLOAT) +               
				CAST(Cash_Collected_5000p AS FLOAT) +            
				CAST(Cash_Collected_10000p AS FLOAT) +        
				CAST(Cash_Collected_20000p AS FLOAT) +        
				CAST(Cash_Collected_50000p AS FLOAT) +        
				CAST(Cash_Collected_100000p AS FLOAT)         
			)    

		+    

			--Tickets (in-out)    
			(    
				CAST(DeclaredTicketValue AS FLOAT)               
				-               
				CAST(DeclaredTicketPrintedValue AS FLOAT)              
			)    

		-              

		--Handpay    
		(CASE WHEN READ_HANDPAY = 0 THEN 0 ELSE (READ_HANDPAY / 100) END)    

		-- -              
		--shortpay              
		-- +               
		-- voids              

		)    
		END    
		AS CashTake

	INTO #Temp_LiqDetails    
	FROM Liq_Summary_ForRead_CTE L, [Site] S WITH(NOLOCK)
	WHERE S.[Site_ID] = @Site_Id
  
    -- Get the cash take for this batch, multiply it by .22 ( settingvalue ) + prevNegShare  
  
    SET @newnegshare = ((SELECT sum(CashTake) FROM #Temp_LiqDetails) * @Retailer_Percentage) + @PrevNegShare    
  
	-- If < 0, set the new negshare to the newnegshare, else set to zero  

	IF @newnegshare < 0   
		SELECT CAST(@newnegshare AS DECIMAL(18, 2)) AS Retailer_Negative_Net
	ELSE    
		SELECT CAST(0 AS DECIMAL(18, 2)) AS Retailer_Negative_Net
		
END

GO

