USE Enterprise
GO

-- =============================================
-- Create basic stored procedure template
-- =============================================

-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_GetCollectionData'
   )
    DROP PROCEDURE dbo.rsp_GetCollectionData
GO

CREATE PROCEDURE dbo.rsp_GetCollectionData
(@SiteID INT = NULL, @Bar_Position_ID INT = NULL)
AS
BEGIN
	SET NOCOUNT ON
	
	CREATE TABLE #tmpTreasury
	(
		Treasury_Type  VARCHAR(50),
		Collection_ID  INT,
		Amount         REAL,
		Amount2        FLOAT,
	)
	CREATE INDEX idx__tmpTreasury ON #tmpTreasury(Treasury_Type, Collection_ID) 
	INCLUDE(Amount, Amount2)
	
	INSERT INTO #tmpTreasury
	SELECT Treasury_Type,
	       Collection_ID,
	       SUM(Amount),
	       CAST(SUM(AMOUNT) AS FLOAT)
	FROM   (
	           SELECT Treasury_Type = (
	                      CASE TE.Treasury_Type
	                           WHEN 'Offline Voucher-Shortpay' THEN 'Shortpay'
	                           ELSE TE.Treasury_Type
	                      END
	                  ),
	                  TE.Collection_ID,
	                  Amount = Treasury_Amount
	           FROM   dbo.Treasury_Entry (NOLOCK) TE
	           WHERE  TE.Treasury_Type IN ('Shortpay', 
	                                      'Offline Voucher-Shortpay', 'Void', 
	                                      'Refund', 'Expired')
	       ) A
	GROUP BY
	       A.Treasury_Type,
	       A.Collection_ID
	ORDER BY
	       A.Treasury_Type,
	       A.Collection_ID
	
	SELECT Ins.Installation_ID,
		   co.Collection_ID,	       
	       (ISNULL(CD.Collection_Days,1)) AS Collection_Days,
	       (
	           (CC.Collection_Cash_Take) 
	           +
	           CC.Collection_Refills 
	           +
	           COALESCE(
	               (
	                   SELECT SUM(TE.Amount)
	                   FROM   #tmpTreasury TE
	                   WHERE  Treasury_Type IN ('VOID')
	                          AND TE.Collection_ID = CO.Collection_ID
	               ),
	               0
	           ) 
	           - 
	           COALESCE(
	               (
	                   SELECT SUM(TE.Amount)
	                   FROM   #tmpTreasury TE
	                   WHERE  Treasury_Type IN ('Shortpay')
	                          AND TE.Collection_ID = CO.Collection_ID
	               ),
	               0
	           ) 
	           - 
	           CAST(
	               COALESCE(
	                   (
	                       SELECT SUM(TE.Amount)
	                       FROM   #tmpTreasury TE
	                       WHERE  Treasury_Type IN ('Refund')
	                              AND TE.Collection_ID = CO.Collection_ID
	                   ),
	                   0
	               ) AS REAL
	           )
	       ) AS Cash_Take,
	       CD.Collection_Gross AS Collection_Gross,	       
	       CD.Collection_Company_Share AS Collection_Company_Share,
	       CD.Collection_Supplier_Share AS Collection_Supplier_Share,
	       CD.Collection_Location_Share AS Collection_Location_Share,
	       CD.Collection_Other_Share AS Collection_Other_Share
	FROM   dbo.[Collection] CO(NOLOCK)
	       INNER JOIN dbo.Collection_Calcs CC(NOLOCK)
	            ON  CO.Collection_ID = CC.Collection_ID
	       INNER JOIN dbo.Collection_Details CD(NOLOCK)
	            ON  CO.Collection_ID = CD.Collection_ID	       
	       INNER JOIN dbo.Installation INS(NOLOCK)
	            ON  co.Installation_ID = ins.Installation_ID
	       INNER JOIN dbo.Bar_Position BP(NOLOCK)
	            ON  BP.Bar_Position_ID = INS.Bar_Position_ID
	       INNER JOIN dbo.Site ST(NOLOCK)
	            ON  BP.Site_ID = ST.Site_ID	       
	WHERE  ST.Site_ID = ISNULL(@SiteID, ST.Site_ID)
	       AND BP.Bar_Position_ID = ISNULL(@Bar_Position_ID, BP.Bar_Position_ID)
	       AND ins.Installation_End_Date IS NULL
END
GO