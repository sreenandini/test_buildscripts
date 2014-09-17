USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_InsertOrUpdateServiceCall]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_InsertOrUpdateServiceCall]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usp_InsertOrUpdateServiceCall]
(
    @ServiceId                        INT = 0,
    @SiteID                           INT,
    @CallSourceID                     INT,
    @CallFaultID                      INT,
    @CallGroupID                      INT,
    @CallRemedyID                     INT,
    @MachineTypeID                    VARCHAR(50),
    @InstallationID                   INT,
    @ServiceVisitNo                   INT,
    @ServiceReceivedStaffID           INT,
    @ServiceIssuedToStaffID           INT,
    @ServiceIssuedByStaffID           INT,
    @CallStatusID                     INT, 
    @CallFaultAdditionalNotes         NVARCHAR(MAX),
    @CallRemedyAdditionalDescription  NVARCHAR(MAX),
    @ServiceAllocatedJobNo            INT,
    @IsCallClosed                     BIT,
    @ServiceReceived                  DATETIME=NULL,
    @ServiceIssued                    DATETIME = NULL,    
    @ServiceAcknowledged			  DATETIME=NULL,
    @ServiceArrivedAtSite			  DATETIME=NULL,
    @ServiceCleared					  DATETIME=NULL,
    @ServiceClearedStaffId			  INT = 0
)
AS
BEGIN
	IF (@InstallationID > 0)
	BEGIN
	    DECLARE @BarPositionID  INT
	    DECLARE @MachineID      INT 
	    SELECT @BarPositionID = bp.Bar_Position_ID,
	           @MachineID = m.Machine_ID
	    FROM   Installation i
	           LEFT JOIN MACHINE m
	                ON  m.Machine_ID = i.Machine_ID
	           LEFT JOIN Bar_Position bp
	                ON  bp.Bar_Position_ID = i.Bar_Position_ID
	           LEFT JOIN [Site] s
	                ON  s.Site_ID = bp.Site_ID
	    WHERE  i.Installation_ID = @InstallationID
	END
	
	----------- Create New Call
	IF (@ServiceId = 0)
	BEGIN
	    --== get next allocated job no  
	    SELECT @ServiceAllocatedJobNo = System_Parameter_Curr_Service_Job_ID
	    FROM   System_Parameters  
	    
	    IF @ServiceAllocatedJobNo IS NULL OR @ServiceAllocatedJobNo <= 0
	        SET @ServiceAllocatedJobNo = 1 
	    
	    -- update next number  
	    UPDATE System_Parameters
	    SET    System_Parameter_Curr_Service_Job_ID = @ServiceAllocatedJobNo + 1 
	    
	    --== get next service number  
	    SELECT @ServiceId = System_Parameter_Curr_Service_ID
	    FROM   System_Parameters  
	    
	    IF @ServiceId IS NULL OR @ServiceId <= 0
	        SET @ServiceId = 1 
	    
	    -- update next number  
	    UPDATE System_Parameters
	    SET    System_Parameter_Curr_Service_ID = @ServiceId + 1 
	    
	    INSERT INTO dbo.[Service]
	      (
			Service_ID,
	        Site_ID,
	        Call_Source_ID,
	        Call_Fault_ID,
	        Call_Group_ID,
	        Call_Remedy_ID,
	        Machine_Type_ID,
	        Installation_ID,
	        Service_Visit_No,
	        Service_Received_Staff_ID,
	        Service_Issued_To_Staff_ID,
	        Service_Issued_By_Staff_ID,
	        Call_Status_ID,
	        Service_Received,
	        Service_Issued,	        
	        Call_Fault_Additional_Notes,
	        Call_Remedy_Additional_Description,
	        Bar_Position_ID,
	        Machine_ID,
	        Service_Allocated_Job_No
	      )
	    VALUES
	      (
			@ServiceId,
	        @SiteID,
	        @CallSourceID,
	        @CallFaultID,
	        @CallGroupID,
	        @CallRemedyID,
	        @MachineTypeID,
	        @InstallationID,
	        @ServiceVisitNo,
	        @ServiceReceivedStaffID,
	        @ServiceIssuedToStaffID,
	        @ServiceIssuedByStaffID,
	        @CallStatusID,
	        CONVERT(VARCHAR(20),@ServiceReceived,113),	        
	        CONVERT(VARCHAR(20),@ServiceIssued,113),
	        @CallFaultAdditionalNotes,
	        @CallRemedyAdditionalDescription,
	        @BarPositionID,
	        @MachineID,
	        @ServiceAllocatedJobNo
	      )
	END
	ELSE 
	IF (@ServiceId > 0)
	BEGIN
	    UPDATE dbo.[Service]
	    SET    Site_ID = @SiteID,
	           Call_Source_ID = @CallSourceID,
	           Call_Fault_ID = @CallFaultID,
	           Call_Group_ID = @CallGroupID,
	           Call_Remedy_ID = @CallRemedyID,
	           Machine_Type_ID = @MachineTypeID,
	           Installation_ID = @InstallationID,
	           Service_Visit_No = @ServiceVisitNo,
	           Service_Issued_To_Staff_ID = @ServiceIssuedToStaffID,
	           Service_Issued_By_Staff_ID = @ServiceIssuedByStaffID,
	           Call_Status_ID = @CallStatusID,
	           Service_Received = ISNULL(CONVERT(VARCHAR(20),@ServiceReceived,113), [Service_Received]),
	           Service_Issued = ISNULL(CONVERT(VARCHAR(20),@ServiceIssued,113), [Service_Issued]),
	           Service_Arrived_At_Site = ISNULL(CONVERT(VARCHAR(20),@ServiceArrivedAtSite,113),[Service_Arrived_At_Site]),
	           Service_Acknowledged = ISNULL(CONVERT(VARCHAR(20),@ServiceAcknowledged,113),[Service_Acknowledged]),
	           Service_Cleared = ISNULL( CONVERT(VARCHAR(20),@ServiceCleared,113),[Service_Cleared]),
	           Call_Fault_Additional_Notes = @CallFaultAdditionalNotes,
	           Call_Remedy_Additional_Description = @CallRemedyAdditionalDescription,
	           Bar_Position_ID = @BarPositionID,
	           Machine_ID = @MachineID,
	           Service_Allocated_Job_No = @ServiceAllocatedJobNo,
	           Service_Cleared_Staff_ID = @ServiceClearedStaffId
	    WHERE  Service_ID = @ServiceId
	END

	IF (@ServiceId > 0 AND @IsCallClosed = 1)
	BEGIN
	    EXEC dbo.usp_CloseServiceCalls @ServiceId
	END
	
	RETURN @ServiceAllocatedJobNo
END
GO
