USE [Enterprise]
GO


/****** Object:  StoredProcedure [dbo].[ASPNET_SP_AddWindowsUserName]    Script Date: 12/09/2013 15:20:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ReadYearonYear]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ReadYearonYear]
GO

GO

USE [Enterprise]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
 
CREATE Procedure [dbo].[rsp_ReadYearonYear]
(
	@StartDate DATETIME,
	@EndDate DATETIME
)  
AS  
BEGIN 

	SELECT             
		            
		Max([Read].Read_ID) AS Read_No,            
		            
		Max(Installation.Installation_ID) AS Installation_No,            
		            
		Max([Read].ReadDate) AS Read_Date,      
		[Read].Read_Games_Bet ,        
		[Read].READ_COIN_DROP ,              
		ISNULL(SUM(dbo.[Read].READ_HANDPAY),0) AS HandPay,             
		            
		ISNULL(SUM(dbo.[Read].READ_TICKET), 0) AS TicketsIN,             
		            
		SUM((CASE Machine_Class.Machine_Class_Sp_Features            
		            
		 WHEN 1 THEN            
		            
		  (CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100            
		            
		 WHEN 10 THEN            
		             
		  ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		  +            
		  ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)              
		             
		 WHEN 12 THEN            
		              
		  ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		  +            
		  ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)              
		            
		 WHEN 20 THEN            
		              
		  ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		  +            
		  ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)              
		            
		 ELSE            
		              
		  (            
		    (CAST(ISNULL(CASH_IN_1p,0) AS FLOAT) / 100) +          
		  (CAST(ISNULL(CASH_IN_2p,0) AS FLOAT) / 50) +            
		  (CAST(ISNULL(CASH_IN_5p,0) AS FLOAT) / 20) +            
		  (CAST(ISNULL(CASH_IN_10p,0) AS FLOAT) / 10) +            
		  (CAST(ISNULL(CASH_IN_20p,0) AS FLOAT) / 5) +            
		  (CAST(ISNULL(CASH_IN_50p,0) AS FLOAT) / 2) +            
		  (CAST(ISNULL(CASH_IN_100p,0) AS FLOAT))  +            
		  (CAST(ISNULL(CASH_IN_200p,0) AS FLOAT) * 2) +            
		  (CAST(ISNULL(CASH_IN_500p,0) AS FLOAT) * 5) +            
		  (CAST(ISNULL(CASH_IN_1000p,0) AS FLOAT) * 10) +            
		  (CAST(ISNULL(CASH_IN_2000p,0) AS FLOAT) * 20) +            
		  (CAST(ISNULL(CASH_IN_5000p,0) AS FLOAT) * 50) +
		  (CAST(ISNULL(CASH_IN_10000p,0) AS FLOAT) * 100) +
		  (CAST(ISNULL(CASH_IN_20000p,0) AS FLOAT) * 200) +
		  (CAST(ISNULL(CASH_IN_50000p,0) AS FLOAT) * 500) +
		  (CAST(ISNULL(CASH_IN_100000p,0) AS FLOAT) * 1000)
		              
		  )            
		            
		            
		END)) AS RDCCashIn,            
		            
		            
		SUM((CASE Machine_Class.Machine_Class_Sp_Features            
		            
		 WHEN 1 THEN            
		            
		  (CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100            
		            
		 WHEN 10 THEN            
		             
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		  -            
		            
		  (((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		             
		  -            
		            
		  ((CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100))            
		             
		 WHEN 12 THEN            
		              
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		  -            
		            
		  (((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		             
		  -            
		            
		  ((CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100))            
		            
		 WHEN 20 THEN            
		              
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		  -            
		            
		  (((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		             
		  -            
		            
		  ((CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100))            
		            
		 ELSE            
		              
		  (            
		   (CAST(ISNULL(CASH_OUT_1p,0) AS FLOAT) / 100) +            
		  (CAST(ISNULL(CASH_OUT_2p,0) AS FLOAT) / 50) +            
		  (CAST(ISNULL(CASH_OUT_5p,0) AS FLOAT) / 20) +            
		  (CAST(ISNULL(CASH_OUT_10p,0) AS FLOAT) / 10) +            
		  (CAST(ISNULL(CASH_OUT_20p,0) AS FLOAT) / 5) +            
		  (CAST(ISNULL(CASH_OUT_50p,0) AS FLOAT) / 2) +            
		  (CAST(ISNULL(CASH_OUT_100p,0) AS FLOAT))  +            
		  (CAST(ISNULL(CASH_OUT_200p,0) AS FLOAT) * 2) +            
		  (CAST(ISNULL(CASH_OUT_500p,0) AS FLOAT) * 5) +           
		  (CAST(ISNULL(CASH_OUT_1000p,0) AS FLOAT) * 10) +            
		  (CAST(ISNULL(CASH_OUT_2000p,0) AS FLOAT) * 20) +            
		  (CAST(ISNULL(CASH_OUT_5000p,0) AS FLOAT) * 50) +
		  (CAST(ISNULL(CASH_OUT_10000p,0) AS FLOAT) * 100) +
		  (CAST(ISNULL(CASH_OUT_20000p,0) AS FLOAT) * 200) +
		  (CAST(ISNULL(CASH_OUT_50000p,0) AS FLOAT) * 500) +
		  (CAST(ISNULL(CASH_OUT_100000p,0) AS FLOAT) * 1000)
		              
		  )            
		            
		            
		END)) AS RDCCashOut,            
		            
		SUM((            
		            
		(CASE Machine_Class.Machine_Class_Sp_Features            
		            
		 WHEN 1 THEN            
		            
		  (CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100            
		            
		 WHEN 10 THEN            
		             
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		             
		 WHEN 12 THEN            
		              
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		 WHEN 20 THEN            
		              
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		 ELSE            
		              
		  (            
		   (CAST(ISNULL(CASH_IN_1p,0) AS FLOAT) / 100) +             
		  (CAST(ISNULL(CASH_IN_2p,0) AS FLOAT) / 50) +            
		  (CAST(ISNULL(CASH_IN_5p,0) AS FLOAT) / 20) +            
		  (CAST(ISNULL(CASH_IN_10p,0) AS FLOAT) / 10) +            
		  (CAST(ISNULL(CASH_IN_20p,0) AS FLOAT) / 5) +            
		  (CAST(ISNULL(CASH_IN_50p,0) AS FLOAT) / 2) +            
		  (CAST(ISNULL(CASH_IN_100p,0) AS FLOAT))  +            
		  (CAST(ISNULL(CASH_IN_200p,0) AS FLOAT) * 2) +            
		  (CAST(ISNULL(CASH_IN_500p,0) AS FLOAT) * 5) +            
		  (CAST(ISNULL(CASH_IN_1000p,0) AS FLOAT) * 10) +            
		  (CAST(ISNULL(CASH_IN_2000p,0) AS FLOAT) * 20) +            
		  (CAST(ISNULL(CASH_IN_5000p,0) AS FLOAT) * 50) +
		  (CAST(ISNULL(CASH_IN_10000p,0) AS FLOAT) * 100) +
		  (CAST(ISNULL(CASH_IN_20000p,0) AS FLOAT) * 200) +
		  (CAST(ISNULL(CASH_IN_50000p,0) AS FLOAT) * 500) +
		  (CAST(ISNULL(CASH_IN_100000p,0) AS FLOAT) * 1000)
		              
		  )            
		            
		            
		END)            
		            
		-            
		            
		            
		(CASE Machine_Class.Machine_Class_Sp_Features            
		            
		 WHEN 1 THEN            
		            
		  (CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100            
		            
		 WHEN 10 THEN            
		             
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		  -            
		            
		  (((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		             
		  -            
		            
		  ((CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100))            
		             
		 WHEN 12 THEN            
		              
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		  -            
		            
		  (((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		             
		  -            
		            
		  ((CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100))            
		            
		 WHEN 20 THEN            
		              
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		  -            
		            
		  (((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		             
		  -            
		            
		  ((CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100))            
		            
		 ELSE            
		              
		  (            
		  (CAST(ISNULL(CASH_OUT_1p,0) AS FLOAT) / 100) +              
		  (CAST(ISNULL(CASH_OUT_2p,0) AS FLOAT) / 50) +            
		  (CAST(ISNULL(CASH_OUT_5p,0) AS FLOAT) / 20) +            
		  (CAST(ISNULL(CASH_OUT_10p,0) AS FLOAT) / 10) +            
		  (CAST(ISNULL(CASH_OUT_20p,0) AS FLOAT) / 5) +            
		  (CAST(ISNULL(CASH_OUT_50p,0) AS FLOAT) / 2) +            
		  (CAST(ISNULL(CASH_OUT_100p,0) AS FLOAT))  +            
		  (CAST(ISNULL(CASH_OUT_200p,0) AS FLOAT) * 2) +            
		  (CAST(ISNULL(CASH_OUT_500p,0) AS FLOAT) * 5) +            
		  (CAST(ISNULL(CASH_OUT_1000p,0) AS FLOAT) * 10) +            
		  (CAST(ISNULL(CASH_OUT_2000p,0) AS FLOAT) * 20) +            
		  (CAST(ISNULL(CASH_OUT_5000p,0) AS FLOAT) * 50) +
		  (CAST(ISNULL(CASH_OUT_10000p,0) AS FLOAT) * 100) +
		  (CAST(ISNULL(CASH_OUT_20000p,0) AS FLOAT) * 200) +
		  (CAST(ISNULL(CASH_OUT_50000p,0) AS FLOAT) * 500) +
		  (CAST(ISNULL(CASH_OUT_100000p,0) AS FLOAT) * 1000)
		              
		  )                
		            
		END)            
		            
		)) AS RDCCash,           
		            
		SUM((CASE Machine_Class.Machine_Class_Sp_Features            
		            
		 WHEN 1 THEN            
		            
		  ((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100) * 10            
		            
		 WHEN 10 THEN            
		             
		  ((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100) * 10            
		             
		 WHEN 12 THEN            
		              
		  ((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100) * 10            
		            
		 WHEN 20 THEN            
		              
		  ((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100) * 10            
		            
		 ELSE            
		              
		  (            
		              
		  (CAST(ISNULL(VTP,0) AS FLOAT))            
		            
		              
		  )            
		            
		END)) AS VTP,            
		            
		MAX(CASE Read_Forced            
		            
		 WHEN 0 THEN 0            
		            
		 ELSE 1            
		            
		END) AS Read_Forced,          
		            
		SUM(CASE ISNULL(READ_COINS_IN,0)            
		             
		 WHEN 0 THEN 0            
		            
		 ELSE             
		              
		 100 - ((CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) / CAST(ISNULL(READ_COINS_IN,0) AS FLOAT))  * 100)            
		            
		END) AS Hold,             
		            
		SUM(ISNULL([Read].READ_TICKET_IN_SUSPENSE_VALUE,0)) AS Value,            
		            
		            
		SUM(ISNULL([Read].Read_Ticket_In_Suspense,0)) AS SuspendedTicketCount,
		
		ISNULL([Read].Read_Days,0) as Read_Days	            
            
FROM ((([Read] WITH (NOLOCK)             
INNER JOIN Installation WITH (NOLOCK) ON ReadDate between @StartDate and @EndDate AND [Read].Installation_ID = Installation.Installation_ID)            
LEFT JOIN Machine WITH (NOLOCK) ON Installation.Machine_ID = Machine.Machine_ID)            
LEFT JOIN Machine_Class WITH (NOLOCK) ON Machine.Machine_Class_ID = Machine_Class.Machine_Class_ID)            
--LEFT JOIN Machine_Type ON Machine.Machine_Category_ID = Machine_Type.Machine_Type_ID)                 
WHERE
(
	               CAST(Installation.installation_Start_Date AS DATETIME) <= @EndDate
	               AND (
	                       CAST(Installation.installation_End_Date AS DATETIME) >= @EndDate
	                       OR Installation.installation_End_Date IS NULL
	                   )
	           )
GROUP BY 
[Read].Installation_ID, 
[Read].ReadDate, 
[Read].Previous_Read_Date,      
--dbo.[Read].Week_ID,      
--dbo.[Read].Period_ID,      
[Read].Read_Games_Bet ,        
--Installation.Bar_Position_ID,       
[Read].READ_COIN_DROP  ,      
--Machine_Type.Machine_Type_Code ,        
--Machine_Type.Machine_Type_ID,      
--Machine_Class.Machine_Name,    
--Installation.Installation_Start_Date,      
--Installation.Installation_End_Date,
[Read].Read_Days


	
END

GO


