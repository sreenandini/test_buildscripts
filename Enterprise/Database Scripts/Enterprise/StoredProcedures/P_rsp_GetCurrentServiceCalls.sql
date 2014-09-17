USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetCurrentServiceCalls]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetCurrentServiceCalls]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Revision History   
--   
-- 15/01/2009 Renjish N Created  
-- 02 Nov 2009 Anuradha	Modified  Changed code to get the downtime based on site/Zone opening hours
-- -----------------------------------------------------------------------------------------------------------------------------------  
  
CREATE PROCEDURE rsp_GetCurrentServiceCalls
	@SITECODE VARCHAR(10),
	@STARTBARPOSNO VARCHAR(10) = '',
	@LASTBARPOSNO VARCHAR(10) = ''
AS
	DECLARE @SiteID INT
	SELECT @SiteID = Site_ID
	FROM   SITE (NOLOCK)
	WHERE  Site_code = @SITECODE
	
	DECLARE @IsServiceCallFeatureFull VARCHAR(10),@ServiceCallRecordCount Int
	
	SET @IsServiceCallFeatureFull = 'false'
	
	SELECT @IsServiceCallFeatureFull = Setting_Value
	FROM   Setting (NOLOCK)
	WHERE  Setting_Name = 'IsServiceCallFeatureFull' 

	EXEC rsp_GetSetting 0,'ServiceCallRecordCount',100,@ServiceCallRecordCount out
	
	

BEGIN	       
	
	WITH XMLNAMESPACES('uri1' AS ns1) 	 
	
	SELECT TOP(@ServiceCallRecordCount) SR.Service_ID,
	                  ISNULL(SR.Service_Received, GETDATE()) AS Service_Received,
	                  ISNULL(BP.Bar_Position_Name, '') AS Bar_Position_Name,
	                  ISNULL(M.Machine_Stock_No, '') AS Machine_Stock_No,
	                  ISNULL(MC.Machine_Name, '') AS Machine_Name,
	                  --  ISNULL(CG.Call_Group_Downtime,'') AS Call_Group_Downtime,  
	                  CASE 
	                       WHEN (
	                                ISNULL(CG.Call_Group_Downtime, 0) = 1
	                                OR LOWER(ISNULL(@IsServiceCallFeatureFull, 'false')) 
	                                   = 'false'
	                            ) THEN CASE 
	                                        WHEN ISNULL(SSOH.Standard_Opening_Hours_ID, 0) 
	                                             > 0 THEN dbo.fnServiceGetDownTime(
	                                                 CONVERT(DATETIME, SR.Service_Received, 101),
	                                                 SSOH.Standard_Opening_Hours_Open_Monday,
	                                                 SSOH.Standard_Opening_Hours_Open_Tuesday,
	                                                 SSOH.Standard_Opening_Hours_Open_Wednesday,
	                                                 SSOH.Standard_Opening_Hours_Open_Thursday,
	                                                 SSOH.Standard_Opening_Hours_Open_Friday,
	                                                 SSOH.Standard_Opening_Hours_Open_Saturday,
	                                                 SSOH.Standard_Opening_Hours_Open_Sunday,
	                                                 NULL
	                                             )
	                                        ELSE dbo.fnServiceGetDownTime(
	                                                 CONVERT(DATETIME, SR.Service_Received, 101),
	                                                 S.Site_Open_Monday,
	                                                 S.Site_Open_Tuesday,
	                                                 S.Site_Open_Wednesday,
	                                                 S.Site_Open_Thursday,
	                                                 S.Site_Open_Friday,
	                                                 S.Site_Open_Saturday,
	                                                 S.Site_Open_Sunday,
	                                                 NULL
	                                             )
	                                   END
	                       ELSE '00:00'
	                  END AS DownTime,
	                  ISNULL(SR.Call_Status_ID, '') AS Call_Status_ID,
	                  ISNULL(SR.Service_Alert_Priority_Site, '') AS 
	                  Service_Alert_Priority_Site,
	                  ISNULL(SR.Service_Alert_Priority_Machine, '') AS 
	                  Service_Alert_Priority_Machine,
	                  ISNULL(SR.Service_Additional_Work_Req, '') AS 
	                  Service_Additional_Work_Req,
	                  ISNULL(CG.Call_Group_Description, '') AS 
	                  Call_Group_Description,
	                  ISNULL(CG.Call_Group_Reference, '') AS 
	                  Call_Group_Reference,
	                  ISNULL(CF.Call_Fault_Description, '') AS 
	                  Call_Fault_Description,
	                  Z.Zone_ID,
	                  ZSOH.Standard_Opening_Hours_ID AS Zone_Standard_Open_ID,
	                  CAST(SR.Service_Allocated_Job_No AS VARCHAR) + '/' + CAST(SR.Service_Visit_No AS VARCHAR) AS Service_Allocated_Job_No,
	                  SR.Service_Visit_No,
	                  SR.Machine_Type_ID
	           FROM   [Service] SR (NOLOCK)
	                  LEFT JOIN Call_Fault CF (NOLOCK)
	                       ON  SR.Call_Fault_ID = CF.Call_Fault_ID
	                  LEFT JOIN Call_Group CG (NOLOCK)
	                       ON  SR.Call_Group_ID = CG.Call_Group_ID
	                  LEFT JOIN Call_Remedy CR (NOLOCK)
	                       ON  SR.Call_Remedy_ID = CR.Call_Remedy_ID
	                  LEFT JOIN Call_Source CS (NOLOCK)
	                       ON  SR.Call_Source_ID = CS.Call_Source_ID
	                  LEFT JOIN Call_Status CST (NOLOCK)
	                       ON  SR.Call_Status_ID = CST.Call_Status_ID
	                  LEFT JOIN [Site] S (NOLOCK)
	                       ON  SR.Site_ID = S.Site_ID
	                  LEFT JOIN Bar_Position BP (NOLOCK)
	                       ON  SR.Bar_Position_ID = BP.Bar_Position_ID
	                  LEFT JOIN [Zone] Z (NOLOCK)
	                       ON  SR.Zone_ID = Z.Zone_ID
	                  LEFT JOIN MACHINE M (NOLOCK)
	                       ON  SR.Machine_ID = M.Machine_ID
	                  LEFT JOIN SLA_Contract SLA (NOLOCK)
	                       ON  SR.SLA_Contract_ID = SLA.SLA_Contract_ID
	                  LEFT JOIN Staff E (NOLOCK)
	                       ON  SR.Service_Issued_To_Staff_ID = E.Staff_ID
	                  LEFT JOIN Staff ST (NOLOCK)
	                       ON  SR.Service_Received_Staff_ID = ST.Staff_ID
	                  LEFT JOIN Machine_Class MC (NOLOCK)
	                       ON  M.Machine_Class_ID = MC.Machine_Class_ID
	                  LEFT JOIN Sub_Company SC (NOLOCK)
	                       ON  S.Sub_Company_ID = SC.Sub_Company_ID
	                  LEFT JOIN Standard_Opening_Hours SSOH (NOLOCK)
	                       ON  S.Standard_Opening_Hours_ID = SSOH.Standard_Opening_Hours_ID
	                  LEFT JOIN Standard_Opening_Hours ZSOH (NOLOCK)
	                       ON  Z.Standard_Opening_Hours_ID = ZSOH.Standard_Opening_Hours_ID
	                  LEFT JOIN Depot D (NOLOCK)
	                       ON  S.Service_Depot_ID = D.Depot_ID
	                  LEFT JOIN Operator O (NOLOCK)
	                       ON  D.Supplier_ID = O.Operator_ID
	                  LEFT JOIN Service_Areas SA (NOLOCK)
	                       ON  S.Service_Area_ID = SA.Service_Area_ID
	           WHERE  S.Site_Id = @SiteID
	                  AND SR.Site_ID = @SiteID
	                  AND (
	                          ISNULL(@STARTBARPOSNO, '') = ''
	                          OR (
	                                 ISNULL(@STARTBARPOSNO, '') <> ''
	                                 AND BP.Bar_Position_Name IN (SELECT 
	                                                                     DISTINCT 
	                                                                     Bar_Position_Name
	                                                              FROM   
	                                                                     Bar_Position (NOLOCK)
	                                                              WHERE  
	                                                                     Bar_Position_Name 
	                                                                     BETWEEN 
	                                                                     @STARTBARPOSNO 
	                                                                     AND @LASTBARPOSNO)
	                             )
	                      )
	ORDER BY
	       CAST(SR.Service_Received AS DATETIME) DESC 
	       
	       FOR XML PATH('ServiceCall'),
	       ROOT('root')
END 

RETURN @@ERROR


GO

