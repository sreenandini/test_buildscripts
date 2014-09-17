USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetEmployeeCardSessions]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetEmployeeCardSessions]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetEmployeeCardSessions]
(
    @Company     INT = 0,
    @SubCompany  INT = 0,
    @Region      INT = 0,
    @Area        INT = 0,
    @District    INT = 0,
    @Site        INT = 0,
    @UserID      INT = 0,
    @EmpCardID   VARCHAR(20) = NULL,
    @StartDate   DATETIME,
    @EndDate     DATETIME,
    @SiteIDList  VARCHAR(MAX)
)
AS
BEGIN
	IF @Company = 0
	    SET @Company = NULL
	
	IF @Subcompany = 0
	    SET @Subcompany = NULL
	
	IF @Region = 0
	    SET @Region = NULL
	
	IF @Area = 0
	    SET @Area = NULL
	
	IF @District = 0
	    SET @District = NULL
	
	IF @Site = 0
	    SET @Site = NULL
	
	IF @UserID = 0
	    SET @UserID = NULL 
	
	IF @EmpCardID = '--ALL--' OR @EmpCardID='All'
	    SET @EmpCardID = NULL    
	
	SELECT U.UserName,
	       E.EmpCardID,
	       E.EmpName,
	       CONVERT(VARCHAR(20), EV.Evt_Datetime, 113) AS SessionStart,
	       EV.Evt_Description,
	       M.Machine_Stock_No,
	       CASE 
	            WHEN (EV.Evt_ErrorCodeNumber = 0) THEN 
	                 'LEGAL'
	            ELSE 'ILLEGAL'
	       END AS Event_Type
	FROM   tblEmployeeCardSessions E WITH(NOLOCK)
	       Left JOIN EVENT EV WITH(NOLOCK)
	            ON  EV.Evt_CardNumber = E.EmpCardID	     
	       INNER JOIN installation i WITH(NOLOCK)
	            ON  EV.evt_installation_id = i.Installation_ID
	       INNER JOIN MACHINE m WITH(NOLOCK)
	            ON  i.machine_id = m.machine_id
	       Left JOIN SITE WITH(NOLOCK)
	            ON  E.SiteCode = SITE.Site_Code
	       Left JOIN Sub_Company SC WITH(NOLOCK)
	            ON  SC.Sub_Company_ID = SITE.Sub_Company_ID
	       LEFT JOIN Sub_Company_Region SR WITH(NOLOCK)
	            ON  SR.Sub_Company_Region_ID = SITE.Sub_Company_Region_ID
	       LEFT JOIN Sub_Company_Area SA WITH(NOLOCK)
	            ON  SA.Sub_Company_Area_ID = SITE.Sub_Company_Area_ID
	       LEFT JOIN Sub_Company_District SD WITH(NOLOCK)
	            ON  SD.Sub_Company_District_ID = SITE.Sub_Company_District_ID
	       LEFT JOIN Company C WITH(NOLOCK)
	            ON  C.Company_ID = SC.Company_ID
	       LEFT JOIN dbo.[USER] U WITH(NOLOCK)
	            ON  (E.UserID = U.SecurityUserID)
	       LEFT JOIN Staff St WITH(NOLOCK)
	            ON  St.UserTableID = U.SecurityUserID
	WHERE  (
	           @Company IS NULL
	           OR (@Company IS NOT NULL AND C.Company_ID = @Company)
	       )
	       AND (
	               (@Subcompany IS NULL)
	               OR (
	                      @Subcompany IS NOT NULL
	                      AND SITE.Sub_Company_ID = @Subcompany
	                  )
	           )
	       AND (
	               (@Region IS NULL)
	               OR (
	                      @Region IS NOT NULL
	                      AND SITE.Sub_Company_Region_ID = @Region
	                  )
	           )
	       AND (
	               (@Area IS NULL)
	               OR (@Area IS NOT NULL AND SITE.Sub_Company_Area_ID = @Area)
	           )
	       AND (
	               (@District IS NULL)
	               OR (
	                      @District IS NOT NULL
	                      AND SITE.Sub_Company_District_ID = @District
	                  )
	           )
	       AND (
	               (@Site IS NULL)
	               OR (@Site IS NOT NULL AND SITE.Site_ID = @Site)
	           )
	       AND (
	               @SiteIDList IS NOT NULL
	               AND SITE.Site_ID IN (SELECT DATA
	                                    FROM   fnSplit(@SiteIDList, ','))
	           )
	       AND (
	               (@UserID IS NULL)
	               OR (@UserID IS NOT NULL AND E.UserID = @UserID)
	           )
	       AND (
	               (@EmpCardID IS NULL)
	               OR (@EmpCardID IS NOT NULL AND E.EmpCardID = @EmpCardID)
	           )
	       AND (E.SessionStart BETWEEN @StartDate AND @EndDate)
	       AND (E.SessionEnd BETWEEN @StartDate AND @EndDate)
	       AND EV.Evt_Datetime BETWEEN E.SessionStart AND E.SessionEnd
	ORDER BY
	       E.SessionStart,
	       E.EmpID
END
GO

