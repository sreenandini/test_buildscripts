USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[Usp_UpdateNegativeTreasuryEntries_114]    Script Date: 04/10/2014 16:51:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usp_UpdateNegativeTreasuryEntries_114]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Usp_UpdateNegativeTreasuryEntries_114]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[Usp_UpdateNegativeTreasuryEntries_114]    Script Date: 04/10/2014 16:51:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


GO
CREATE PROCEDURE [dbo].[Usp_UpdateNegativeTreasuryEntries_114]       
     
AS      
BEGIN   
SET NOCOUNT ON
CREATE TABLE #tmp_Treasury_Entry
(
ID INT IDENTITY(1, 1) ,
Treasury_ID INT, 
Collection_ID INT, 
Installation_ID INT, 
Treasury_Amount FLOAT,
Treasury_Type varchar(50),
Treasury_VoidedDate DATETIME, 
Treasury_TicketNumber varchar(50)
);

INSERT INTO #tmp_Treasury_Entry
SELECT Treasury_ID, Collection_ID, Installation_ID, Treasury_Amount,
Treasury_Type, Treasury_VoidedDate, Treasury_TicketNumber

  FROM Treasury_Entry WHERE Treasury_Reason  = 'NEGATIVE TREASURY ENTRY'
AND Treasury_Reason_Code = 0
ORDER BY Treasury_ID DESC


DECLARE @TotalRows INT
DECLARE @Count INT


SELECT @TotalRows = COUNT(*), @Count = 1 FROM #tmp_Treasury_Entry
DECLARE @Treasury_VoidedDate DATETIME
DECLARE @Treasury_ID INT
DECLARE @Original INT

WHILE(@TotalRows > 0 AND @Count <= @TotalRows)
BEGIN
SET @Treasury_ID = 0
SET @Original = 0
SELECT TOP 1 @Original = T.Treasury_ID, @Treasury_ID = T1.Treasury_ID, @Treasury_VoidedDate = ISNULL(T1.Treasury_VoidedDate, GETDATE()) 
FROM Treasury_Entry T, #tmp_Treasury_Entry T1  WHERE T.Treasury_ID < T1.Treasury_ID AND T.Treasury_Reason_Code = 0
AND T.Collection_ID = T1.Collection_ID
AND T.Installation_ID = T1.Installation_ID AND T.Treasury_Amount = -(T1.Treasury_Amount) AND T.Treasury_Type = T1.Treasury_Type AND
T1.ID = @Count
 ORDER BY T.Treasury_ID DESC
 
 IF ISNULL(@Original, 0) = 0
 BEGIN
 
	 SELECT TOP 1 
		 @Original = T.Treasury_ID, 
		 @Treasury_ID = T1.Treasury_ID,
		 @Treasury_VoidedDate = ISNULL(T1.Treasury_VoidedDate, GETDATE()) 
	FROM Treasury_Entry T, 
	#tmp_Treasury_Entry T1  
	WHERE T.Treasury_ID > T1.Treasury_ID AND 
	T.Treasury_Reason_Code = 0
	AND T.Collection_ID = T1.Collection_ID
	AND T.Installation_ID = T1.Installation_ID 
	AND T.Treasury_Amount = -(T1.Treasury_Amount) AND
	 T.Treasury_Type = T1.Treasury_Type AND
	T1.ID = @Count
	 ORDER BY T.Treasury_ID ASC
END

Update Treasury_Entry
SET Treasury_Reason_Code = @Original,
Treasury_VoidedDate = @Treasury_VoidedDate,
Treasury_Reason  = 'NEGATIVE TREASURY ENTRY'
WHERE Treasury_ID IN (@Original, @Treasury_ID)

PRINT @Count
PRINT @Treasury_ID
PRINT @Original
PRINT '------'
SET @Count = @Count + 1


END

DROP TABLE #tmp_Treasury_Entry
SET NOCOUNT OFF
END 

GO

