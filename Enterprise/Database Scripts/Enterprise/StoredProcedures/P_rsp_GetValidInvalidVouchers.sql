USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetValidInvalidVouchers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetValidInvalidVouchers]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetValidInvalidVouchers
	@Tickets VARCHAR(MAX),
	@Asset VARCHAR(50)
AS
	IF EXISTS (
	       SELECT *
	       FROM   sys.objects
	       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[#VouchersList]')
	              AND TYPE IN (N'U')
	   )
	    DROP TABLE [dbo].[#VouchersList]
	
	IF EXISTS (
	       SELECT *
	       FROM   sys.objects
	       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[#Valid_Vouchers]')
	              AND TYPE IN (N'U')
	   )
	    DROP TABLE [dbo].[#Valid_Vouchers]
	
	IF EXISTS (
	       SELECT *
	       FROM   sys.objects
	       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[#InValid_Vouchers]')
	              AND TYPE IN (N'U')
	   )
	    DROP TABLE [dbo].[#InValid_Vouchers]       
	
	
	
	CREATE TABLE #VouchersList
	(
		ID          INT IDENTITY,
		strBarCode  VARCHAR(32)
	)      
	CREATE TABLE #Valid_Vouchers
	(
		strBarCode  VARCHAR(32),
		iAmount     INT
	)      
	CREATE TABLE #InValid_Vouchers
	(
		strBarCode  VARCHAR(32),
		Reasonid    INT
	) 
	
	--declare @Tickets varchar(max)
	--set @Tickets='101000002000035307,101000000700011935,101000000700024065,101000000700035801,101000000700043332,101000000700059944,101000002000035307,101000002000042725,1234,101000000700069516'
	--declare @Asset varchar(50)
	--set @Asset='LC0003'      
	
	INSERT INTO #VouchersList
	  (
	    strBarCode
	  )
	SELECT STR
	FROM   dbo.iter_charlist_to_tbl (@Tickets, DEFAULT)      
	
	INSERT INTO #Valid_Vouchers
	  (
	    strBarCode,
	    iAmount
	  )
	SELECT Voucher.strBarcode,
	       Voucher.iAmount
	FROM   Voucher
	       INNER JOIN #VouchersList
	            ON  Voucher.strBarCode = #VouchersList.strBarCode
	WHERE  Voucher.strBarCode IN (SELECT strBarCode
	                              FROM   #VouchersList)
	       AND strVoucherStatus = 'PD'
	       AND iPayDeviceID = (
	               SELECT iDEVICEID
	               FROM   DEVICE
	               WHERE  strSerial = @Asset
	           )
	       AND ISNULL(iRedeemCollectionCompleted, 0) <> 1;      
	
	BEGIN
		/* Select Duplicate records */ 
		WITH CTE(strBarCode, iAmount, DuplicateCount) 
		AS 
		
		(
		    SELECT strBarCode,
		           iAmount,
		           ROW_NUMBER() OVER(PARTITION BY strBarCode, iAmount ORDER BY strBarCode) AS 
		           DuplicateCount
		    FROM   #Valid_Vouchers
		)      
		INSERT INTO #InValid_Vouchers
		  (
		    strBarCode,
		    Reasonid
		  )
		SELECT strBarCode,
		       1 --Duplicate
		FROM   CTE
		WHERE  DuplicateCount > 1
	END      
	
	BEGIN
		/* Delete Duplicate records */ 
		WITH CTE(strBarCode, iAmount, DuplicateCount) 
		AS 
		
		(
		    SELECT strBarCode,
		           iAmount,
		           ROW_NUMBER() OVER(PARTITION BY strBarCode, iAmount ORDER BY strBarCode) AS 
		           DuplicateCount
		    FROM   #Valid_Vouchers
		)      
		DELETE 
		FROM   CTE
		WHERE  DuplicateCount > 1
	END      
	
	BEGIN
		--Get All Invalid Vouchers      
		INSERT INTO #InValid_Vouchers
		  (
		    strBarCode,
		    Reasonid
		  )
		SELECT strBarCode,
		       0
		FROM   #VouchersList
		WHERE  strBarCode NOT IN (SELECT strBarCode
		                          FROM   #Valid_Vouchers) 
		                          
		--Expired Ticket + Exception(9)  
		UPDATE #InValid_Vouchers
		SET    Reasonid = 9
		WHERE  #InValid_Vouchers.strBarcode IN (SELECT strBarcode
		                                        FROM   voucher
		                                        WHERE  strVoucherStatus IS NULL
		                                               AND dtExpire < GETDATE()
													   AND ErrCode IS NOT NULL) 
		
		--Partially Paid(8)  
		UPDATE #InValid_Vouchers
		SET    Reasonid = 8
		WHERE  #InValid_Vouchers.strBarcode IN (SELECT strBarcode
		                                        FROM   voucher
		                                        WHERE  strVoucherStatus = 'PP') 
		         
		--Active Ticket + Exception (7)      
		UPDATE #InValid_Vouchers
		SET    Reasonid = 7
		WHERE  #InValid_Vouchers.strBarcode IN (SELECT strBarcode
		                                        FROM   voucher
		                                        WHERE  strVoucherStatus IS NULL
		                                               AND dtExpire > GETDATE()
													   AND ErrCode IS NOT NULL) 
		                                                                                     
		--Expired Ticket(6)  
		UPDATE #InValid_Vouchers
		SET    Reasonid = 6
		WHERE  #InValid_Vouchers.strBarcode IN (SELECT strBarcode
		                                        FROM   voucher
		                                        WHERE  strVoucherStatus IS NULL
		                                               AND dtExpire < GETDATE()
													   AND ErrCode IS NULL) 
		
		--Active Ticket(5)      
		UPDATE #InValid_Vouchers
		SET    Reasonid = 5
		WHERE  #InValid_Vouchers.strBarcode IN (SELECT strBarcode
		                                        FROM   voucher
		                                        WHERE  strVoucherStatus IS NULL
		                                               AND dtExpire > GETDATE()
													   AND ErrCode IS NULL) 
		
		--Already in old Collection(4)      
		UPDATE #InValid_Vouchers
		SET    Reasonid = 4
		WHERE  #InValid_Vouchers.strBarcode NOT IN (SELECT strBarcode
		                                            FROM   voucher
		                                            WHERE  ISNULL(iRedeemCollectionCompleted, 0)
		                                                   <> 1) 
		
		
		--Not in Position(3)      
		UPDATE #InValid_Vouchers
		SET    Reasonid = 3
		WHERE  #InValid_Vouchers.strBarcode NOT IN 
											(SELECT strBarcode
												FROM   voucher vc
													   INNER JOIN device D
															ON  (
																	(iPayDeviceID IS NULL AND vc.ErrDeviceID IS NULL )  
																    OR (VC.iPayDeviceID = d.iDEVICEID)
																	OR ( vc.ErrDeviceID = d.iDEVICEID)
																)
															AND D.strSerial = @Asset) 
		
		--Not in System(2)      
		UPDATE #InValid_Vouchers
		SET    Reasonid = 2
		WHERE  #InValid_Vouchers.strBarcode NOT IN (SELECT strBarcode
		                                            FROM   voucher)
	END 
	
	--select * from #VouchersList      
	SELECT strBarcode,
	       iAmount
	FROM   #Valid_Vouchers
	ORDER BY
	       strBarcode
	
	SELECT strBarcode,
	       Reasonid
	FROM   #InValid_Vouchers
	ORDER BY
	       strBarcode      
	
	SELECT SUM(iAmount) AS Total,
	       COUNT(iAmount) AS Quantity
	FROM   #Valid_Vouchers       
	
	SELECT COUNT(Reasonid) AS Quantity
	FROM   #InValid_Vouchers 
	       
	       -- Drop Table #VouchersList
	       -- Drop Table #Valid_Vouchers
	       -- Drop Table #InValid_Vouchers

GO

