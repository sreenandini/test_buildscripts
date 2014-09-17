USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetServiceClosedCallDetails]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetServiceClosedCallDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetServiceClosedCallDetails(
    @StartDate       DATETIME = NULL,
    @EndDate         DATETIME = NULL,
    @CallRemedyID    INT = 0,
    @DepotIDList     VARCHAR(MAX) = NULL,
    @StaffIDList     VARCHAR(MAX) = NULL,
    @SiteIDList      VARCHAR(MAX) = NULL,
    @SubCompanyID    INT = 0,
    @JobID           INT = 0,
    @MachineStockNo  VARCHAR(50) = NULL
)
AS
BEGIN
	DECLARE @IsServiceCallFeatureFull VARCHAR(10)
	
	SET @IsServiceCallFeatureFull = 'false'
	
	SELECT @IsServiceCallFeatureFull = LOWER(Setting_Value)
	FROM   Setting
	WHERE  Setting_Name = 'IsServiceCallFeatureFull'  
	
	IF (@DepotIDList = '0')
	    SET @DepotIDList = NULL
	
	IF (@StaffIDList = '0')
	    SET @StaffIDList = NULL
	
	IF (@SiteIDList = '0')
	    SET @SiteIDList = NULL
	
	SELECT CONVERT(VARCHAR(10), CAST(s.Service_Received AS DATETIME), 103) 
	       + ' ' + CONVERT(VARCHAR(5), CAST(s.Service_Received AS DATETIME), 108) AS 
	       Service_Received,
	       (stfr.Staff_Last_Name + ', ' + stfr.Staff_Last_Name) AS 
	       Staff_Name_Received,
	       (stfc.Staff_Last_Name + ', ' + stfc.Staff_Last_Name) AS 
	       Staff_Name_Cleared,
	       CASE 
	            WHEN LEN(CAST(s.Service_DownTime / 60 AS VARCHAR)) = 1 THEN '0' 
	                 +
	                 CAST(s.Service_DownTime / 60 AS VARCHAR)
	            ELSE CAST(s.Service_DownTime / 60 AS VARCHAR)
	       END + ':' +
	       CASE 
	            WHEN LEN(CAST(s.Service_DownTime%60 AS VARCHAR)) = 1 THEN '0' + 
	                 CAST(s.Service_DownTime%60 AS VARCHAR)
	            ELSE CAST(s.Service_DownTime%60 AS VARCHAR)
	       END AS Service_DownTime_HH_mm,
	       CONVERT(VARCHAR(10), CAST(s.Service_Cleared AS DATETIME), 103) 
	       + ' ' + CONVERT(VARCHAR(5), CAST(s.Service_Cleared AS DATETIME), 108) AS 
	       Service_Cleared,
	       CASE 
	            WHEN ISNULL(s.Service_Visit_No, 0) > 0 THEN CAST(s.Service_Allocated_Job_No AS VARCHAR)
	                 + '\' + CAST(s.Service_Visit_No AS VARCHAR)
	            ELSE CAST(s.Service_Allocated_Job_No AS VARCHAR)
	       END AS Service_Job_Visit_No,
	       si.Site_Name + ',' + si.Site_Address_2 AS Site_Name_Address,
	       si.Site_Code,
	       (mc.Machine_Name + ' [' + m.Machine_Stock_No + ']') AS 
	       Machine_Name_Stock_No,
	       s.Machine_Type_ID,
	       CASE 
	            WHEN eng.Staff_First_Name IS NOT NULL THEN eng.Staff_First_Name 
	                 +
	                 ' ' + eng.Staff_Last_Name
	            ELSE 'UNALLOCATED'
	       END AS Engineer_Name,
	       sla.SLA_Contract_Description,
	       (
	           cg.Call_Group_Reference + ' - ' + cf.Call_Fault_Description
	       ) AS Call_Description,
	       (
	           cr.Call_Remedy_Description + ' - ' + CAST(s.Call_Remedy_Additional_Description AS VARCHAR(MAX))
	       ) AS Call_Remedy_Description,
	       bp.Bar_Position_Name,
	       ISNULL(s.Service_DownTime, 0) / 60 AS TimeOpened,
	       s.Service_DownTime,
	       s.Call_Remedy_ID,
	       s.Service_Allocated_Job_No,
	       s.Service_ID,
	       s.Call_Status_ID,
	       z.Zone_ID,
	       si.Site_ID,
	       si.Staff_ID,
	       cg.Call_Group_ID,
	       d.Depot_ID
	FROM   Service_Closed s WITH (NOLOCK)
	       LEFT JOIN Call_Fault cf
	            ON  s.Call_Fault_ID = cf.Call_Fault_ID
	       LEFT JOIN Call_Group cg WITH (NOLOCK)
	            ON  s.Call_Group_ID = cg.Call_Group_ID
	       LEFT JOIN Call_Remedy cr WITH (NOLOCK)
	            ON  s.Call_Remedy_ID = cr.Call_Remedy_ID
	       LEFT JOIN Call_Source WITH (NOLOCK)
	            ON  s.Call_Source_ID = Call_Source.Call_Source_ID
	       LEFT JOIN Call_Status WITH (NOLOCK)
	            ON  s.Call_Status_ID = Call_Status.Call_Status_ID
	       LEFT JOIN [Site] si WITH (NOLOCK)
	            ON  s.Site_ID = si.Site_ID
	       LEFT JOIN Bar_Position bp WITH (NOLOCK)
	            ON  s.Bar_Position_ID = bp.Bar_Position_ID
	       LEFT JOIN [Zone] z WITH (NOLOCK)
	            ON  s.Zone_ID = z.Zone_ID
	       LEFT JOIN [Machine] m WITH (NOLOCK)
	            ON  s.Machine_ID = m.Machine_ID
	       LEFT JOIN SLA_Contract sla WITH (NOLOCK)
	            ON  s.SLA_Contract_ID = sla.SLA_Contract_ID
	       LEFT JOIN Staff eng WITH (NOLOCK)
	            ON  s.Service_Issued_To_Staff_ID = eng.Staff_ID
	       LEFT JOIN Staff stfc WITH (NOLOCK)
	            ON  s.Service_Cleared_Staff_ID = stfc.Staff_ID
	       LEFT JOIN Staff stfr WITH (NOLOCK)
	            ON  s.Service_Received_Staff_ID = stfr.Staff_ID
	       LEFT JOIN Machine_Class mc WITH (NOLOCK)
	            ON  m.Machine_Class_ID = mc.Machine_Class_ID
	       LEFT JOIN Sub_Company sc WITH (NOLOCK)
	            ON  si.Sub_Company_ID = sc.Sub_Company_ID
	       LEFT JOIN Depot d WITH (NOLOCK)
	            ON  si.Service_Depot_ID = d.Depot_ID
	       LEFT JOIN Operator op WITH (NOLOCK)
	            ON  d.Supplier_ID = op.Operator_ID
	WHERE  (
	           (
	               (@StartDate IS NULL OR @EndDate IS NULL)
	               OR (
	                      @StartDate IS NOT NULL
	                      AND @EndDate IS NOT NULL
	                      AND CAST(s.Service_Cleared AS DATETIME) 
	                          BETWEEN CAST(
	                              CONVERT(VARCHAR(10), @StartDate, 112) 
	                              + ' ' +
	                              '00:00:00.000' 
	                              AS DATETIME
	                          ) AND CAST(
	                              CONVERT(VARCHAR(10), @EndDate, 112) 
	                              + ' ' +
	                              '23:59:59.997' AS 
	                              DATETIME
	                          )
	                  )
	           )
	       )
	       AND (
	               @CallRemedyID = 0
	               OR (@CallRemedyID <> 0 AND s.Call_Remedy_ID = @CallRemedyID)
	           )
	       AND (
	               ISNULL(@DepotIDList, '') = ''
	               OR (
	                      ISNULL(@DepotIDList, '') <> ''
	                      AND d.Depot_ID IN (SELECT DATA
	                                         FROM   fnSplit(@DepotIDList, ','))
	                  )
	           )
	       AND (
	               ISNULL(@SiteIDList, '') = ''
	               OR (
	                      ISNULL(@SiteIDList, '') <> ''
	                      AND s.Site_ID IN (SELECT DATA
	                                        FROM   fnSplit(@SiteIDList, ','))
	                  )
	           )
	       AND (
	               ISNULL(@StaffIDList, '') = ''
	               OR (
	                      ISNULL(@StaffIDList, '') <> ''
	                      AND eng.Staff_ID IN (SELECT DATA
	                                           FROM   fnSplit(@StaffIDList, ','))
	                  )
	           )
	       AND (
	               @SubCompanyID = 0
	               OR (@SubCompanyID <> 0 AND sc.Sub_Company_ID = @SubCompanyID)
	           )
	       AND (
	               ISNULL(@JobID, 0) = 0
	               OR (
	                      ISNULL(@JobID, 0) <> 0
	                      AND CAST(s.Service_Allocated_Job_No AS VARCHAR) 
	                          LIKE '%' + CAST(@JobID AS VARCHAR) +
	                          '%'
	                  )
	           )
	       AND (
	               ISNULL(@MachineStockNo, '') = ''
	               OR (
	                      ISNULL(@MachineStockNo, '') <> ''
	                      AND m.Machine_Stock_No LIKE '%' + @MachineStockNo 
	                          +
	                          '%'
	                  )
	           )
	ORDER BY
	       CAST(s.Service_Cleared AS DATETIME) DESC
END
GO
