USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_CloseServiceCalls]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_CloseServiceCalls]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------   
--  
-- Description: TO auto close the service calls if the option is set to true
--      
--    Steps  
--  
-- Inputs:      Service_ID
--  
-- Outputs:     NONE  
--  
-- Return:       
--  
-- =======================================================================  
--   
-- Revision History  
--  Sudarsan S		04/09/2008		created
--  Sudarsan S		25/08/2009		added 3 nullable parameters
---------------------------------------------------------------------------   

CREATE PROCEDURE [dbo].[usp_CloseServiceCalls]
	@Service_ID INT,
	@Service_Allocated_Job_No VARCHAR(20) = NULL,
	@RemedyID INT = NULL,
	@UserID INT = NULL,
	@Notes VARCHAR(1000) = NULL
AS
BEGIN
	DECLARE @IsServiceCallFeatureFull  VARCHAR(10)
	DECLARE @Service_Downtime          VARCHAR(20)
	
	SET @IsServiceCallFeatureFull = 'false'
	
	SELECT @IsServiceCallFeatureFull = LOWER(Setting_Value)
	FROM   Setting
	WHERE  Setting_Name = 'IsServiceCallFeatureFull' 
	
	
	IF @Service_Allocated_Job_No IS NOT NULL
	BEGIN
	    SELECT @Service_ID = Service_ID
	    FROM   dbo.Service
	    WHERE  Service_Allocated_Job_No = CAST(
	               SUBSTRING(
	                   @Service_Allocated_Job_No,
	                   1,
	                   CHARINDEX('/', @Service_Allocated_Job_No) - 1
	               ) AS INT
	           )
	    
	    UPDATE dbo.Service
	    SET    Call_Remedy_ID = @RemedyID,
	           Call_Remedy_Additional_Description = @Notes,
	           Service_Cleared_Staff_ID = @UserID
	    WHERE  Service_ID = @Service_ID
	END
	
	SELECT @Service_Downtime = CASE 
	                                WHEN (
	                                         ISNULL(cg.Call_Group_Downtime, 0) = 
	                                         1
	                                         OR ISNULL(@IsServiceCallFeatureFull, 'false') 
	                                            = 'false'
	                                     ) THEN CASE 
	                                                 WHEN ISNULL(soh.Standard_Opening_Hours_ID, 0) 
	                                                      > 0 THEN dbo.fnServiceGetDownTime(
	                                                          CONVERT(DATETIME, s.Service_Received, 101),
	                                                          soh.Standard_Opening_Hours_Open_Monday,
	                                                          soh.Standard_Opening_Hours_Open_Tuesday,
	                                                          soh.Standard_Opening_Hours_Open_Wednesday,
	                                                          soh.Standard_Opening_Hours_Open_Thursday,
	                                                          soh.Standard_Opening_Hours_Open_Friday,
	                                                          soh.Standard_Opening_Hours_Open_Saturday,
	                                                          soh.Standard_Opening_Hours_Open_Sunday,
	                                                          GETDATE()
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
	                                                          GETDATE()
	                                                      )
	                                            END
	                                ELSE '00:00'
	                           END
	FROM   [Service] s WITH (NOLOCK)
	       INNER JOIN dbo.[Site] si
	            ON  si.Site_ID = s.Site_ID
	       LEFT JOIN dbo.Standard_Opening_Hours soh
	            ON  si.Standard_Opening_Hours_ID = soh.Standard_Opening_Hours_ID
	       LEFT JOIN Call_Group cg WITH (NOLOCK)
	            ON  s.Call_Group_ID = cg.Call_Group_ID
	WHERE  Service_ID = @Service_ID
	
	INSERT INTO dbo.Service_Closed
	  (
	    Service_ID,
	    Call_Source_ID,
	    Call_Fault_ID,
	    Call_Status_ID,
	    Call_Remedy_ID,
	    Call_Remedy_Additional_Description,
	    Site_ID,
	    Bar_Position_ID,
	    Zone_ID,
	    Machine_ID,
	    Machine_Type_ID,
	    Installation_ID,
	    SLA_Contract_ID,
	    Service_DownTime,
	    Service_Received,
	    Service_Received_Staff_ID,
	    Service_Issued,
	    Service_Issued_By_Staff_ID,
	    Service_Issued_To_Staff_ID,
	    Service_Arrived_At_Site,
	    Service_Cleared,
	    Service_Cleared_Staff_ID,
	    Call_Group_ID,
	    Call_Fault_Additional_Notes,
	    Service_Additional_Work_Req,
	    Service_Message_ID,
	    Service_Sequence_No,
	    Service_Alert_Priority_Site,
	    Service_Alert_Priority_Machine,
	    Service_Visit_No,
	    Service_Allocated_Job_No,
	    Service_Acknowledged,
	    Service_GMU_Source_ID,
	    Service_GMU_Type_ID
	  )
	SELECT Service_ID,
	       Call_Source_ID,
	       Call_Fault_ID,
	       Call_Status_ID,
	       CASE 
	            WHEN (@RemedyID IS NULL AND ISNULL(Call_Remedy_ID, 0) = 0) THEN 
	                 dbo.fnGetAutoCloseIDs('REMEDY')
	            ELSE Call_Remedy_ID
	       END,
	       Call_Remedy_Additional_Description,
	       s.Site_ID,
	       Bar_Position_ID,
	       Zone_ID,
	       Machine_ID,
	       Machine_Type_ID,
	       Installation_ID,
	       SLA_Contract_ID,
	       CAST(
	           LEFT(@Service_Downtime, CHARINDEX(':', @Service_Downtime, 0) -1) 
	           AS INT
	       ) * 60 +
	       CAST(
	           RIGHT(
	               @Service_Downtime,
	               LEN(@Service_Downtime) -CHARINDEX(':', @Service_Downtime, 0)
	           ) AS INT
	       ),
	       Service_Received,
	       Service_Received_Staff_ID,
	       Service_Issued,
	       Service_Issued_By_Staff_ID,
	       CASE 
	            WHEN ISNULL(Service_Issued_To_Staff_ID, 0) = 0 THEN dbo.fnGetAutoCloseIDs('ENGINEER')
	            ELSE Service_Issued_To_Staff_ID
	       END,
	       Service_Arrived_At_Site,
	       GETDATE(),
	       Service_Cleared_Staff_ID,
	       s.Call_Group_ID,
	       Call_Fault_Additional_Notes,
	       Service_Additional_Work_Req,
	       Service_Message_ID,
	       Service_Sequence_No,
	       Service_Alert_Priority_Site,
	       Service_Alert_Priority_Machine,
	       Service_Visit_No,
	       Service_Allocated_Job_No,
	       Service_Acknowledged,
	       Service_GMU_Source_ID,
	       Service_GMU_Type_ID
	FROM   dbo.[Service] s
	       INNER JOIN dbo.[Site] si
	            ON  si.Site_ID = s.Site_ID
	       LEFT JOIN dbo.Standard_Opening_Hours soh
	            ON  si.Standard_Opening_Hours_ID = soh.Standard_Opening_Hours_ID
	       LEFT JOIN Call_Group cg WITH (NOLOCK)
	            ON  s.Call_Group_ID = cg.Call_Group_ID
	WHERE  Service_ID = @Service_ID
	
	IF @@RowCount = 1
	    DELETE 
	    FROM   dbo.Service
	    WHERE  Service_ID = @Service_ID
	
	IF @@RowCount > 0
	BEGIN
	    IF @Service_Allocated_Job_No IS NOT NULL
	    BEGIN
	        RETURN 10
	    END
	END
END
GO
