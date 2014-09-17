USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetEmployeeCardInfo]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].rsp_GetEmployeeCardInfo
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
* **********************************************************************************************************
* Revision History
* 
* Anuradha		Created		07/06/2012		
* 
* Retrieve the Employee Card Details		
* 
* **********************************************************************************************************
*/


CREATE PROCEDURE [dbo].[rsp_GetEmployeeCardInfo] 
(@CardNumber VARCHAR(20) = NULL)
AS
BEGIN
	SET NOCOUNT ON                  
	
	SELECT EmpID,
	       EmployeeCardNumber,
	       EmployeeName,
	       IsActive,
	       CardType,
	       GMUModeGroupDescription,
	       ISNULL(tecd.UserID, 0) AS UserID,
	       ISNULL(S.Site_Code, '') AS SiteCode,
	       CASE 
	            WHEN userid IS NULL THEN 'UnAssigned'
	            ELSE 'Assigned'
	       END AS Mapped,
	       CASE 
	            WHEN userid IS NULL THEN CAST(0 AS BIT)
	            ELSE CAST(1 AS BIT)
	       END AS IsChecked,
	       tecd.EmployeeFlags,
	       tecd.IsMasterCard,
	       tecd.CardLevel
	FROM   tblEmployeeCardDetails tecd WITH(NOLOCK)
	       LEFT OUTER JOIN [USER] u WITH(NOLOCK)
	            ON  u.SecurityUserID = tecd.UserID
	       LEFT  JOIN VW_Enterprise_usersite_lnk usl WITH(NOLOCK)
	            ON  usl.SecurityUserID = u.SecurityUserID
	       LEFT  JOIN SITE s WITH(NOLOCK)
	            ON  s.Site_ID = usl.SiteID
	       LEFT JOIN [GMUModeGroup] G WITH(NOLOCK)
	            ON  tecd.CardType = G.GMUModeGroupID
	WHERE  (
	           (@CardNumber IS NULL)
	           OR (
	                  @CardNumber IS NOT NULL
	                  AND tecd.Employeecardnumber = @CardNumber
	              )
	       )
	ORDER BY
	       EmployeeCardNumber
END
GO
