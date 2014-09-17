USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetServiceCurrentCallDetails]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetServiceCurrentCallDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetServiceCurrentCallDetails(
    @CallStatusID  INT = 0,
    @CallGroupID   INT = 0,
    @DepotIDList   VARCHAR(MAX) = NULL,
    @StaffIDList   VARCHAR(MAX) = NULL,
    @SiteIDList    VARCHAR(MAX) = NULL,
    @SubCompanyID  INT = 0,
    @JobID         INT = 0
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
	
	           SELECT s.Service_Allocated_Job_No,
	                  s.Service_ID,
	                  s.Call_Status_ID,
	                  CONVERT(VARCHAR(10), CAST(s.Service_Received AS DATETIME), 103) 
	       + ' ' + CONVERT(VARCHAR(5), CAST(s.Service_Received AS DATETIME), 108) AS 
	       Service_Received,
	                  DATEDIFF(d, CAST(s.Service_Received AS DATETIME), GETDATE()) AS 
	                  CallOpenDays,
	                  CASE 
	                       WHEN (
	                                ISNULL(cg.Call_Group_Downtime, 0) = 1
	                                OR ISNULL(@IsServiceCallFeatureFull, 'false') 
	                                   = 'false'
	                            ) THEN CASE 
	                                        WHEN ISNULL(Site_Standard_Opening_Hours.Standard_Opening_Hours_ID, 0) 
	                                             > 0 THEN dbo.fnServiceGetDownTime(
	                                                 CONVERT(DATETIME, s.Service_Received, 101),
	                                                 Site_Standard_Opening_Hours.Standard_Opening_Hours_Open_Monday,
	                                                 Site_Standard_Opening_Hours.Standard_Opening_Hours_Open_Tuesday,
	                                                 Site_Standard_Opening_Hours.Standard_Opening_Hours_Open_Wednesday,
	                                                 Site_Standard_Opening_Hours.Standard_Opening_Hours_Open_Thursday,
	                                                 Site_Standard_Opening_Hours.Standard_Opening_Hours_Open_Friday,
	                                                 Site_Standard_Opening_Hours.Standard_Opening_Hours_Open_Saturday,
	                                                 Site_Standard_Opening_Hours.Standard_Opening_Hours_Open_Sunday,
	                                                 NULL
	                                             )
	                                        ELSE dbo.fnServiceGetDownTime(
	                                                 CONVERT(DATETIME, s.Service_Received, 101),
	                                                 si.Site_Open_Monday,
	                                                 si.Site_Open_Tuesday,
	                                                 si.Site_Open_Wednesday,
	                                                 si.Site_Open_Thursday,
	                                                 si.Site_Open_Friday,
	                                                 si.Site_Open_Saturday,
	                                                 si.Site_Open_Sunday,
	                                                 NULL
	                                             )
	                                   END
	                       ELSE '00:00'
	                  END AS TimeOpened,
	                  stf.Staff_Last_Name + ',' + stf.Staff_First_Name AS 
	                  Staff_Name,
	                  si.Site_Name + ',' + si.Site_Address_2 AS Site_Address,
	                  si.Site_Code,
	                  ISNULL(mc.Machine_Name, 'UNKNOWN') AS Machine_Name,
	                  ISNULL(s.Machine_Type_ID, 'UNKNOWN') AS Machine_Type_ID,
	                  CASE 
	                       WHEN eng.Staff_First_Name IS NOT NULL THEN eng.Staff_First_Name 
	                            +
	                            ' ' + eng.Staff_Last_Name
	                       ELSE 'UNALLOCATED'
	                  END AS Engineer_Name,
	                  sla.SLA_Contract_Description,
	                  CASE 
	                       WHEN cg.Call_Group_Description IS NOT NULL THEN cg.Call_Group_Reference
	                            + ' - ' + cf.Call_Fault_Description
	                       ELSE 'UNKNOWN'
	                  END AS Call_Description,
	                  CASE 
	                       WHEN s.Call_Status_ID = 1 THEN 'Logged'
	                       WHEN s.Call_Status_ID = 2 THEN 'Viewed'
	                       WHEN s.Call_Status_ID = 3 THEN 'Despatched'
	                       WHEN s.Call_Status_ID = 4 THEN 'Accepted'
	                       WHEN s.Call_Status_ID = 5 THEN 'Enroute'
	                       WHEN s.Call_Status_ID = 6 THEN 'At Site'
	                       WHEN s.Call_Status_ID = 7 THEN 'Received'
	                       WHEN s.Call_Status_ID = 8 THEN 'Completed'
	                       WHEN s.Call_Status_ID = 9 THEN 'Rejected'
	                       ELSE ''
	                  END AS Call_Status_Description,
	                  CASE 
	                       WHEN ISNULL(s.Service_Visit_No, 0) > 0 THEN CAST(s.Service_Allocated_Job_No AS VARCHAR)
	                            + '\' + CAST(s.Service_Visit_No AS VARCHAR)
	                       ELSE CAST(s.Service_Allocated_Job_No AS VARCHAR)
	                  END AS Service_Job_Visit_No,
	                  sc.Sub_Company_Name,
	                  (
	                      CASE 
	                           WHEN ISNULL(s.Service_Alert_Priority_Site, 0) = 1 THEN 
	                                'S'
	                           ELSE ''
	                      END + CASE 
	                                 WHEN ISNULL(s.Service_Alert_Priority_Machine, 0) 
	                                      = 1 THEN 'M'
	                                 ELSE ''
	                            END + CASE 
	                                       WHEN ISNULL(s.Service_Additional_Work_Req, 0) 
	                                            =
	                                            1 THEN 'A'
	                                       ELSE ''
	                                  END
	                  ) AS Service_SMA,
	                  si.Site_Postcode,
	                  CASE 
	                       WHEN ISNULL(s.Service_Alert_Priority_Site, 0) = 1 THEN 
	                            'S'
	                  END AS Service_Alert_Priority_Site,
	                  CASE 
	                       WHEN ISNULL(s.Service_Alert_Priority_Machine, 0) = 1 THEN 
	                            'M'
	                  END AS Service_Alert_Priority_Machine,
	                  CASE 
	                       WHEN ISNULL(s.Service_Additional_Work_Req, 0) = 1 THEN 
	                            'A'
	                  END AS Service_Additional_Work_Req,
	                  bp.Bar_Position_Name,
	                  si.Site_ID
	           FROM   [SERVICE] s WITH (NOLOCK)
	                  LEFT	JOIN Call_Fault cf WITH (NOLOCK)
	                       ON  s.Call_Fault_ID = cf.Call_Fault_ID
	                  LEFT JOIN Call_Group cg WITH (NOLOCK)
	                       ON  s.Call_Group_ID = cg.Call_Group_ID
	                  LEFT JOIN Call_Remedy cr WITH (NOLOCK)
	                       ON  s.Call_Remedy_ID = cr.Call_Remedy_ID
	                  LEFT JOIN Call_Source cs WITH (NOLOCK)
	                       ON  s.Call_Source_ID = cs.Call_Source_ID
	                  LEFT JOIN Call_Status cls WITH (NOLOCK)
	                       ON  s.Call_Status_ID = cls.Call_Status_ID
	                  LEFT JOIN [Site] si WITH (NOLOCK)
	                       ON  s.Site_ID = si.Site_ID
	                  LEFT JOIN Bar_Position bp WITH (NOLOCK)
	                       ON  s.Bar_Position_ID = bp.Bar_Position_ID
	                  LEFT JOIN [Zone] z WITH (NOLOCK)
	                       ON  s.Zone_ID = z.Zone_ID
	                  LEFT JOIN MACHINE m WITH (NOLOCK)
	                       ON  s.Machine_ID = m.Machine_ID
	                  LEFT JOIN SLA_Contract sla WITH (NOLOCK)
	                       ON  s.SLA_Contract_ID = sla.SLA_Contract_ID
	                  LEFT JOIN Staff eng WITH (NOLOCK)
	                       ON  s.Service_Issued_To_Staff_ID = eng.Staff_ID
	                  LEFT JOIN Staff stf WITH (NOLOCK)
	                       ON  s.Service_Received_Staff_ID = stf.Staff_ID
	                  LEFT JOIN Machine_Class mc WITH (NOLOCK)
	                       ON  m.Machine_Class_ID = mc.Machine_Class_ID
	                  LEFT JOIN Sub_Company sc WITH (NOLOCK)
	                       ON  si.Sub_Company_ID = sc.Sub_Company_ID
	                  LEFT JOIN Standard_Opening_Hours AS 
	                       Site_Standard_Opening_Hours WITH (NOLOCK)
	                       ON  si.Standard_Opening_Hours_ID = 
	                           Site_Standard_Opening_Hours.Standard_Opening_Hours_ID
	                  LEFT JOIN Standard_Opening_Hours AS 
	                       Zone_Standard_Opening_Hours WITH (NOLOCK)
	                       ON  z.Standard_Opening_Hours_ID = 
	                           Zone_Standard_Opening_Hours.Standard_Opening_Hours_ID
	                  LEFT JOIN Depot d WITH (NOLOCK)
	                       ON  si.Service_Depot_ID = d.Depot_ID
	                  LEFT JOIN Operator op WITH (NOLOCK)
	                       ON  d.Supplier_ID = op.Operator_ID
	           WHERE  s.Service_Received IS NOT NULL
	                  AND (
	                          @CallStatusID = 0
	                          OR (@CallStatusID <> 0 AND s.Call_Status_ID = @CallStatusID)
	                      )
	                  AND (
	                          @CallGroupID = 0
	                          OR (@CallGroupID <> 0 AND s.Call_Group_ID = @CallGroupID)
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
	ORDER BY
	       CAST(s.Service_Received AS DATETIME) DESC
END
GO
