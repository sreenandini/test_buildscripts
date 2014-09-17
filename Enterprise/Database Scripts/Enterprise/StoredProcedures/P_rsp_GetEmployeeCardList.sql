USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetEmployeeCardList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetEmployeeCardList]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetEmployeeCardList]
(
	@CardNumber INT =0,
	@EmployeeName VARCHAR(50) = NULL,
	@CardStatus VARCHAR(20) = NULL,
	@CardType VARCHAR(20) = NULL
)
AS
BEGIN
	
	IF @CardNumber=0 SET @CardNumber = NULL
	IF @EmployeeName='' OR @EmployeeName='--None--' OR @EmployeeName='All' OR @EmployeeName='--All--' SET @EmployeeName = NULL
	IF @CardStatus='' OR @CardStatus='All' OR @CardStatus='--All--' SET @CardStatus = NULL
	IF @CardType='' OR @CardType= 'All' OR  @CardType = '--All--' SET @CardType = NULL

SELECT 
	ED.EmployeeCardNumber,
	ET.EmpCardType,
	CardStatus = CASE ED.IsActive
		when 1 THEN 'Active'
		when 0 THEN 'InActive'
	END,
	ISNULL(St.Staff_First_Name + ' ' + St.Staff_Last_Name,'') As EmployeeName,
	ED.CreatedBy as UserName,
	ED.CreatedOn,
	ED.LastModifedDateTime,
	IsMasterCard= CASE ED.IsMasterCard
		WHEN 1 THEN 'Yes'
		WHEN 0 THEN 'No'
	END 
FROM
	tblEmployeeCardDetails ED
	INNER JOIN tblEmployeeCardType ET ON ET.EmpCardTypeID = CAST(ED.CardType AS Integer)
	LEFT JOIN dbo.[USER] U ON ED.UserID = U.SecurityUserID
	LEFT JOIN Staff St on St.UserTableID = U.SecurityUserID
WHERE
	(@CardNumber IS NULL
	 OR		
		(@CardNumber IS NOT NULL
		 AND
		 ED.EmployeeCardNumber = @CardNumber)
	)
	AND (@EmployeeName IS NULL
		 OR
			(@EmployeeName IS NOT NULL
			 AND 
			 LTRIM(RTRIM(UPPER(St.Staff_First_Name + ' ' + St.Staff_Last_Name))) = LTRIM(RTRIM(UPPER(@EmployeeName))))
		)
	AND (@CardStatus IS NULL
		 OR
			(@CardStatus IS NOT NULL
			 AND
				ED.IsActive = CASE WHEN LTRIM(RTRIM(UPPER(@CardStatus))) ='ACTIVE' THEN 1 ELSE 0 END )
		)
	AND (@CardType IS NULL
		 OR
			(@CardType IS NOT NULL
			 AND
		 	 ED.CardType = (SELECT EmpCardTypeID FROM tblEmployeeCardType tect WHERE tect.EmpCardType = @CardType ) )
		)
END

GO

