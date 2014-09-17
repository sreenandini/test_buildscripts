USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetServiceCurrentCallDetailsByServiceID]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetServiceCurrentCallDetailsByServiceID]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetServiceCurrentCallDetailsByServiceID(@Service_ID INT, @IsCallClosed BIT = 0)
AS
BEGIN
	IF (@IsCallClosed = 0)
	BEGIN
	    SELECT Call_Fault_Additional_Notes,
	           Call_Fault_ID,
	           Call_Group_ID,
	           Call_Remedy_Additional_Description,
	           Call_Remedy_ID,
	           Call_Source_ID,
	           Call_Status_ID,
	           Installation_ID,
	           Machine_Type_ID,
	           Service_Acknowledged,
	           Service_Additional_Work_Req,
	           Service_Allocated_Job_No,
	           Service_Arrived_At_Site,
	           Service_Cleared,
	           Service_Issued,
	           Service_Issued_To_Staff_ID,
	           Service_Received,
	           Service_Visit_No,
	           Site_ID,
	           SLA_Contract_ID,
	           Service_Received_Staff_ID,
	           Service_Issued_By_Staff_ID,
			   Service_Cleared_Staff_ID
	    FROM   [Service] s WITH (NOLOCK)
	    WHERE  s.Service_ID = @Service_ID
	END
	ELSE
	BEGIN
	    SELECT Call_Fault_Additional_Notes,
	           Call_Fault_ID,
	           Call_Group_ID,
	           Call_Remedy_Additional_Description,
	           Call_Remedy_ID,
	           Call_Source_ID,
	           Call_Status_ID,
	           Installation_ID,
	           Machine_Type_ID,
	           Service_Acknowledged,
	           Service_Additional_Work_Req,
	           Service_Allocated_Job_No,
	           Service_Arrived_At_Site,
	           Service_Cleared,
	           Service_Issued,
	           Service_Issued_To_Staff_ID,
	           Service_Received,
	           Service_Visit_No,
	           Site_ID,
	           SLA_Contract_ID,
	           Service_Received_Staff_ID,
	           Service_Issued_By_Staff_ID,
			   Service_Cleared_Staff_ID
	    FROM   [Service_Closed] sc WITH (NOLOCK)
	    WHERE  sc.Service_ID = @Service_ID
	END
END
GO
