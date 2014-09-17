USE Enterprise 
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetTreasuryDetails]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetTreasuryDetails]
GO

USE Enterprise
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetTreasuryDetails(@collection_ID INT)
AS
BEGIN
	DECLARE @tTreasuryTab TABLE(
	            Treasury_Type VARCHAR(50),
	            Treasury_Date VARCHAR(30),
	            Treasury_Time VARCHAR(50),
	            Treasury_Amount FLOAT,
	            Treasury_User VARCHAR(50),
	            Treasury_Issued_User VARCHAR(50),
	            Treasury_Reason VARCHAR(200)
	        )
	
	INSERT INTO @tTreasuryTab
	SELECT T.Treasury_Type,
	       T.Treasury_Date,
	       ISNULL(T.Treasury_Time, '0:00'),
	       T.Treasury_Amount,
	       ISNULL(T.Treasury_User, 'Unknown') Treasury_User,
	       CASE 
	            WHEN ISNULL(T.AuthorizedUser_No, 0) <> 0 THEN s.Staff_Last_Name 
	                 + ',' + s.Staff_First_Name
	            ELSE ISNULL(T.Treasury_User, 'Unknown')
	       END AS Treasury_Issued_User,
	       T.Treasury_Reason
	FROM   Treasury_Entry T
	       LEFT JOIN [USER] u
	            ON  u.SecurityUserID = T.AuthorizedUser_No
	       LEFT JOIN Staff S
	            ON  S.UserTableID = u.SecurityUserID
	WHERE  Collection_ID = @collection_ID
	ORDER BY
	       T.Treasury_Date DESC,
	       T.Treasury_Time DESC
	
	--UNION ALL 
	INSERT INTO @tTreasuryTab
	SELECT 'Vouchers Out' Treasury_Type,
	       Collection_Date_Of_Collection Treasury_Date,
	       ISNULL(Collection_Time_Of_Collection,'0:00') Treasury_Time,
	       DeclaredTicketPrintedValue Treasury_Amount,
	       'n/a' Treasury_User,
	       'n/a' AS Treasury_Issued_User,
	       '' Treasury_Reason
	FROM   COLLECTION C WITH(NOLOCK)
	WHERE  Collection_ID = @collection_ID
	       AND DeclaredTicketPrintedValue > 0       
	
	SELECT Treasury_Type,
	       Treasury_Date,
	       Treasury_Time,
	       Treasury_Amount,
	       Treasury_User,
	       Treasury_Issued_User,
	       Treasury_Reason
	FROM   @tTreasuryTab
END
GO


