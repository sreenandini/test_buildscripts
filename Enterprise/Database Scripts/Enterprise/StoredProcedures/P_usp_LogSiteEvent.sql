USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_LogSiteEvent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_LogSiteEvent]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------------------------------------------   
--  
-- Description: Checks to see if the incoming event is worthy of a service call entry  
--  
--              if so auto generates the call, applying group etc.  
--      
--    Steps  
--  
-- Inputs:      See inputs   
--  
-- Outputs:     NONE  
--  
-- Return:          0 - All Ok  
--                  1 - installation_No does not exist in installation table.  
--              OTHER - SQL Error  
--  
-- =============================================================================================================  
--   
-- Revision History  
--   
--	C.Taylor		01/06/2007		Created  
--	C.Taylor		11/06/2007		Updated dates to dd mmm yyyy  
--	Siva							@Installation_date and @datetime set to varchar(20) fields from datetime  
--	C.Taylor						Machine_type_Code used instead of machine_type_id  
--	Siva							Datapak_Fault is just a look up table and should not duplication of records  
--	Siva			28/05/2008		Changes for using HQ_installation_ID from exchange
--	Rakesh Marwaha	16/06/2008		Added Pooled events into Event table
--	Rakesh Marwaha	10/07/2008		Changed location of code to add Pooled events into Event table	 
--	Sudarsan S		05/08/2008		Called the procedure to generate the mail for events
--  Sudarsan S		04/09/2008		for Auto Close Service Calls
--  Siva			18/09/2008      bug fix - SP failing in SGVI if site_name has special chars (& for ex)
--									ignorning Site_Name for now as it is not needed
--  Sudarsan S		28/01/2009		to check if the service fault already exists and return, and uncommented machine_type_code
--  Yoganandh P		20/08/2010		For Auto Closed Events
---------------------------------------------------------------------------------------------------------------   
CREATE PROCEDURE usp_LogSiteEvent  
  
  @InstallationID     int,  
  @Site_Name          varchar(50),  
  @Fault_Source_ID    int,  
  @Fault_Type_ID      int,  
  @Fault_Details      varchar(50),  
  @Datetime           DATETIME,
  @Event_Auto_Closed  INT,  
  @CardNumber VARCHAR(15) = NULL,
  @isCardInserted bit = NULL,
  @ErrorCodeNumber	INT,
  @IsCreateServiceCall int = 0,
  @ServiceReceivedStaffID int = 1
    
AS  
  
  SET DATEFORMAT dmy  
  SET NOCOUNT ON  
  
  DECLARE @Installation_ID    int,  
          @Bar_Position_ID    int,  
          @Machine_ID         int,  
          @Machine_Type_ID    int,  
          @Machine_Type_Code  varchar(50),  
          @UserID             int,  
          @SiteID             int,  
          @SourceID           int,  
          @FaultID            int,  
          @GroupID            int,  
          @RemedyID           int,  
       
          @SLA_Contract_ID    int,  
          @EngineerID         int,  
          @NextServiceID      int,  
          @AllocatedJobNo     int,  
          @Autolog            bit,  
  
          @CALL_STATUS_LOGGED INT  

  DECLARE @Close_Service	BIT
  DECLARE @Source_ID	INT
  DECLARE @Type_ID		INT
  DECLARE @Service_ID	INT  
  DECLARE @Event_Description VARCHAR(100)
  
  SELECT @Event_Description = Datapak_Fault_Text 
	FROM dbo.Datapak_Fault df (NOLOCK)
	WHERE df.Datapak_Fault_Code = @Fault_Source_ID AND df.Datapak_Fault_Supplementary_Code = @Fault_Type_ID 
  
  
  -- constant  
  SET @CALL_STATUS_LOGGED = 1  
  
  -- use machine name, installation_date etc to get following, match to internals based on names  
/**  
  SET @SiteID = 0  
  SET @Bar_Position_ID = 0  
  SET @Machine_ID = 0  
  SET @Machine_Type_ID = 0  
  SET @Installation_ID = 0  
**/  
  
  --  fixed items    
  SET @UserID   = 99    -- fixed ( auto import )   
  SET @SourceID = 1 -- from site  
  
  print '1'  
  
  -- find installation ..  
  SELECT @Installation_ID   = Installation.Installation_Id,  
         @Bar_Position_ID   = bar_position.Bar_Position_ID,   
         @Machine_ID        = Machine.Machine_ID,  
         @Machine_Type_Code = machine_type.machine_type_code,  
         @SiteID            = bar_position.Site_ID  
  
    FROM dbo.Installation (nolock)  
    JOIN dbo.bar_position (nolock)    
      ON Installation.Bar_Position_ID = bar_position.Bar_Position_ID   
    JOIN dbo.Machine  (nolock)   
      ON Installation.Machine_ID = Machine.Machine_ID   
    JOIN dbo.Machine_Class  (nolock)   
      ON Machine.Machine_Class_Id = Machine_Class.Machine_Class_Id  
    JOIN dbo.Machine_Type  (nolock)  
      ON Machine_Class.Machine_Type_Id = Machine_Type.Machine_type_Id  
    JOIN dbo.Site  (nolock)  
      on site.site_id = bar_position.site_id  
  
  
     and Installation.Installation_ID = @InstallationID  
     --and Site_Name                    = @Site_Name  
     
    
  IF COALESCE(@Installation_ID,0) = 0  
  BEGIN  
    print 'unable to match installation ...'  
    return (1)  
  END  
  	Insert INTO dbo.[Event]
	(
		Evt_Site_Event_ID,
		Evt_Installation_ID,
		Evt_Datetime,
		Evt_Fault_Source,
		Evt_Fault_Type,
		Evt_Fault_Details,
		Evt_Auto_Closed,
		Evt_CardNumber,
		Evt_IsCardInserted,
		Evt_Description,
		Evt_ErrorCodeNumber		
	)
	Values
	(
		cast(ltrim(rtrim(@Fault_Details)) as INT),
		@Installation_ID,	--This is used get @Installation_ID in Event table
		@Datetime,
		@Fault_Source_ID,
		@Fault_Type_ID,
		@Fault_Details,
		@Event_Auto_Closed,		
		@CardNumber,
		@isCardInserted,
		@Event_Description,
		@ErrorCodeNumber
	)
  
  -- This call is to generate the mail if the event inserted has been configured to send mails.
  EXEC dbo.esp_SendEventsMail @Fault_Source_ID, @Fault_Type_ID, @Installation_ID, @Datetime
  
  If(ISNULL(@IsCreateServiceCall,0) = 0)
  return 0
  
  -- get defaults from configuration based on event  
  SELECT @FaultID = datapak_fault.Call_Fault_ID,  
         @GroupID = Call_Fault.Call_Group_ID,  
         @Autolog = Datapak_fault_auto_log_service_call_critical,
		 @Close_Service = ISNULL(Auto_Close_Service_Call, 0),
		 @Source_ID = ISNULL(Auto_Close_Source_ID, 0),
		 @Type_ID = ISNULL(Auto_Close_Type_ID, 0)
    FROM Datapak_Fault (nolock) 
    JOIN Call_Fault (nolock) 
      ON Datapak_Fault.Call_Fault_ID = Call_Fault.Call_Fault_ID  
   WHERE Datapak_Fault_Code = @Fault_Source_ID  
     AND Datapak_Fault_Supplementary_Code = @Fault_Type_ID  
  
  IF @@ROWCOUNT = 0  
  BEGIN  
    print 'auto creating fault entry'  
  
    -- unable to find a matching datapak fault entry, auto create one.  
	 IF EXISTS (SELECT 1 FROM DBO.DATAPAK_FAULT (NOLOCK) WHERE Datapak_Fault_Code = @Fault_Source_ID AND Datapak_Fault_Supplementary_Code = @Fault_Type_ID)  
	 RETURN (0)  
		--INSERT INTO datapak_fault VALUES ( 1,  @Fault_Source_ID, @Fault_Type_ID, '** UNKNOWN **', 0, 0, 3,1 )  
		INSERT INTO datapak_fault(Call_Fault_ID,Datapak_Fault_Code,Datapak_Fault_Supplementary_Code,Datapak_Fault_Text,Datapak_Fault_Auto_Log_Service_Call_Non_Critical,Datapak_Fault_Auto_Log_Service_Call_Critical,Datapak_Fault_Source_Protocol, SendMail, Auto_Close_Service_Call)  
		VALUES ( 1,  @Fault_Source_ID, @Fault_Type_ID, '** UNKNOWN **', 0, 0, 3, 0, 0)


		RETURN (0)  
  END  
  
  SET @RemedyID = 0          -- default  
  SET @SLA_Contract_ID = -1  -- default  
  SET @EngineerID = -1      -- default  


	IF ISNULL(@Close_Service, 0) = 0 AND ( @Autolog = 0 )
	BEGIN
		RETURN (0)
	END

	ELSE IF ISNULL(@Close_Service, 0) = 0 AND ( @Autolog = 1 ) 
	BEGIN
		GOTO INSERTSERVICE
	END

	ELSE IF ISNULL(@Close_Service, 0) = 1 AND ( @Autolog = 0 )
	BEGIN
		GOTO CLOSESERVICE
	END
	
	ELSE
	BEGIN
		SELECT @Service_ID = Service_ID FROM dbo.Service (nolock)
			WHERE Service_GMU_Source_ID = @Source_ID AND Service_GMU_Type_ID = @Type_ID AND Installation_ID = @Installation_ID

		IF ISNULL(@Service_ID, 0) <> 0
		BEGIN
			EXEC dbo.usp_CloseServiceCalls @Service_ID
			RETURN 0
		END
		ELSE
		BEGIN
			GOTO INSERTSERVICE
		END
	END



  INSERTSERVICE:  
--== get next allocated job no  
  SELECT @AllocatedJobNo = System_Parameter_Curr_Service_Job_ID   
    FROM System_Parameters  
  
  IF @AllocatedJobNo IS NULL  
    SET @AllocatedJobNo = 1  
  
  -- update next number  
  UPDATE System_Parameters  
     SET System_Parameter_Curr_Service_Job_ID = @AllocatedJobNo + 1  
  
--== get next service number  
  SELECT @NextServiceID  = System_Parameter_Curr_Service_ID   
    FROM System_Parameters  
  
  IF @NextServiceID  IS NULL  
    SET @NextServiceID  = 1  
  
  -- update next number  
  UPDATE System_Parameters  
     SET System_Parameter_Curr_Service_ID = @NextServiceID + 1  
  
/**  
  If SCSLACallsInSLADaysSite(GetItemData(CmbSiteName), GetItemData(CmbPriority)) Then  
    Rs.Fields("Service_Alert_Priority_Site") = True  
  End If  
**/  
/**  
  If SCSLACallsInSLADaysMachine(GetItemData(CmbMachineName), GetItemData(CmbPriority)) Then  
    Rs.Fields("Service_Alert_Priority_Machine") = True  
  End If  
**/  
  
   declare @dt varchar(20)  
   print @datetime   
   set @datetime = getdate()  
  
   set @dt = convert ( varchar(12), @datetime, 106 ) + ' ' + convert ( varchar(8), @datetime, 114 )    

	IF EXISTS(SELECT 1 FROM dbo.[Service] (nolock) 
				WHERE Installation_ID = @InstallationID AND Service_GMU_Source_ID = @Fault_Source_ID AND Service_GMU_Type_ID = @Fault_Type_ID AND Service_GMU_Source_ID = 300)
	BEGIN
		RETURN -99
	END

	   -- collated all information, create the record  
	   --  
   INSERT INTO Service  
   (    
	 Service_ID,  
	 Site_ID,  
	 Service_Received_Staff_ID,  
  
	 Call_Source_ID,  
	 Call_Fault_ID,  
	 Call_Group_ID,  
	 Call_Remedy_ID,  
  
	 Bar_Position_ID,  
	 Machine_ID,  
	 Machine_Type_ID,  
	 Installation_ID,  
  
	 SLA_Contract_ID,  
	 Service_Visit_No,  
	 Service_Issued_To_Staff_ID,  
	 Call_Status_ID,  
	 Service_Issued,  
	 Service_Received,  
  
	 Service_Allocated_Job_No,  
  
	 Call_Fault_Additional_Notes,
	 Service_GMU_Source_ID,
	 Service_GMU_Type_ID
   )  
   VALUES  
   (  
	 @NextServiceID,  
	 @SiteID,  	 
	 @ServiceReceivedStaffID, --@UserID,  
  
	 @SourceID,  
	 @FaultID,     
	 @GroupID,  
	 @RemedyID,  
  
	 @Bar_Position_ID,  
	 @Machine_ID,  
	 @Machine_Type_code,  
	 @Installation_ID,  
       
	 @SLA_Contract_ID,  
	 1,  
	 @EngineerID,  
	 @CALL_STATUS_LOGGED,  
	 NULL,  
	 @dt,  
  
	 @AllocatedJobNo,  
  
	 @Fault_Details,
	 @Fault_Source_ID,
	 @Fault_Type_ID	 
   )

   RETURN 0

	CLOSESERVICE:

	SELECT @Service_ID = Service_ID FROM dbo.[Service] (nolock)
		WHERE Service_GMU_Source_ID = @Source_ID AND Service_GMU_Type_ID = @Type_ID AND Installation_ID = @Installation_ID

	IF ISNULL(@Service_ID, 0) <> 0
	BEGIN
		EXEC dbo.usp_CloseServiceCalls @Service_ID
	END

	RETURN 0

-- return error   
--  
RETURN @@ERROR 


GO

