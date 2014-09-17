USE [Enterprise]
GO
/************************************************************
 * Inserts Employee Mode Details
 * Time: 04/09/2013 16:09:42
 * Author: Aishwarrya V S
SELECT * FROM EMPGMUEVENTS
 ************************************************************/
IF EXISTS (
       SELECT TOP 1 1
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_InsertEmpGMUEvent]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_InsertEmpGMUEvent]
GO

CREATE PROCEDURE usp_InsertEmpGMUEvent(
    @EmpXML      VARCHAR(MAX),
    @ExportXML   VARCHAR(MAX),
    @UserID      INT,
    @UserName    VARCHAR(15),
    @ModuleID    INT,
    @ModuleName  VARCHAR(30),
    @Desc        INT,
    @CardLevel   INT
)
AS
BEGIN
	SET NOCOUNT ON  
	DECLARE @idoc         INT   
	DECLARE @EmpCardId    INT  
	DECLARE @RetVal       INT
	DECLARE @Description  VARCHAR(250)
	
	
	SET @EmpCardId = 0  
	
	DECLARE @RoleID    TABLE (RoleID INT)
	DECLARE @GMUEvent  TABLE (EventID INT, RoleID INT, isDelete BIT, isNew BIT)	
	
	
	EXEC sp_xml_preparedocument @idoc OUTPUT,
	     @EmpXML 
	
	BEGIN TRAN
	INSERT INTO @RoleID
	  (
	    RoleID
	  )
	SELECT RoleID
	FROM   OPENXML(@idoc, './/EmpCardIDs/Role') WITH 
	       (RoleID INT './@ID')   
	
	INSERT INTO @GMUEvent
	  (
	    EventID,
	    RoleID,
	    isDelete,
	    isNew
	  )
	SELECT EventID,
	       A.RoleID,
	       isDelete,
	       isNew
	FROM   OPENXML(@idoc, '//Role/GMUEvent') WITH 
	       (
	           EventID INT './@ID',
	           isDelete BIT './@isDelete',
	           isNew BIT './@isNew',
	           RoleID INT '../@ID'
	       ) AS A
	       INNER JOIN @RoleID E
	            ON  E.RoleID = A.RoleID  
	
	EXEC sp_xml_removedocument @idoc	
	
	INSERT INTO EmpGMUEvents
	  (
	    RoleID,
	    GMUEventId
	  )
	SELECT RoleID,
	       EventID
	FROM   @GMUEvent G
	WHERE  isNew = 1
	       AND isDelete = 0
	
	DELETE EG
	FROM   @GMUEvent G
	       INNER JOIN EmpGMUEvents EG
	            ON  EG.RoleID = G.RoleID
	WHERE  isDelete = 1
	       AND EG.RoleID = G.RoleID
	       AND EG.GMUEventId = G.EventID
	
	UPDATE [ROLE]
	SET    CardLevel = @CardLevel
	WHERE  SecurityRoleID = @Desc
	
	
	INSERT INTO EmployeeExportHistory
	  (
	    EH_Details
	  )
	VALUES
	  (
	    @ExportXML
	  )  	 
	
	SET @RetVal = SCOPE_IDENTITY()
	
	EXEC usp_Insert_ExportHistory @RetVal,
	     'EMPGMUEVENTS',
	     @UserID,
	     'ALL'	
	
	SET @Description = 'Event(s) association modified for Role: ' + CAST(@Desc AS VARCHAR(10))
	
	EXEC usp_InsertAuditData
	     @User_ID = @UserID,
	     @User_Name = @UserName,
	     @Module_ID = @ModuleID,
	     @Module_Name = @ModuleName,
	     @Screen_Name = 'Associate Events',
	     @Slot = NULL,
	     @Aud_Field = NULL,
	     @Old_Value = NULL,
	     @New_Value = 'TRUE',
	     @Aud_Desc = @Description,
	     @Operation_Type = 'ADD/DEL' 
	
	COMMIT TRAN
	RETURN 0
	Err: 
	ROLLBACK TRAN
	RETURN -99
END 




