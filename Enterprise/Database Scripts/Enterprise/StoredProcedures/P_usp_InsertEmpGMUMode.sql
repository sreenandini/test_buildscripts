USE [Enterprise]
GO
/************************************************************
 * Inserts Employee Mode Details
 * Time: 04/09/2013 16:09:42
 * Author: Aishwarrya V S 
 ************************************************************/
IF EXISTS (
       SELECT TOP 1 1
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_InsertEmpGMUMode]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_InsertEmpGMUMode]
GO
 
CREATE PROCEDURE usp_InsertEmpGMUMode(
    @EmpCardNumber  VARCHAR(MAX),
    @ExportXML      VARCHAR(MAX),
    @UserID         INT,
    @UserName       VARCHAR(15),
    @ModuleID       INT,
    @ModuleName     VARCHAR(30),
    @Desc           VARCHAR(250),
    @EmpGroupID     INT,
    @CardLevel		INT
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @idoc         INT	
	DECLARE @RetVal       INT
	DECLARE @Description  VARCHAR(250)	
	
	DECLARE @EmpCard      TABLE (RoleID INT)
	DECLARE @GMUMode      TABLE (ModeID INT, RoleID INT, isDelete BIT, isNew BIT)	
	
	EXEC sp_xml_preparedocument @idoc OUTPUT,
	     @EmpCardNumber 
	
	BEGIN TRAN
	INSERT INTO @EmpCard
	  (
	    RoleID
	  )
	SELECT RoleID
	FROM   OPENXML(@idoc, './/EmpCardIDs/Role') WITH 
	       (RoleID INT './@ID')	
	
	INSERT INTO @GMUMode
	  (
	    ModeID,
	    RoleID,
	    isDelete,
	    isNew
	  )
	SELECT ModeID,
	       A.RoleID,
	       isDelete,
	       isNew
	FROM   OPENXML(@idoc, '//Role/GMUMode') WITH 
	       (
	           ModeID INT './@ID',
	           isDelete BIT'./@isDelete',
	           isNew BIT'./@isNew',
	           RoleID INT '../@ID'
	       ) AS A
	       INNER JOIN @EmpCard E
	            ON  E.RoleID = A.RoleID
	
	EXEC sp_xml_removedocument @idoc	
	
	INSERT INTO EmpGMUMode
	  (
	    RoleID,
	    GMUModeId
	  )
	SELECT RoleID,
	       ModeID
	FROM   @GMUMode
	WHERE  isNew = 1
	       AND isDelete = 0
	
	DELETE EG
	FROM   @GMUMode G
	       INNER JOIN EmpGMUMode eg
	            ON  EG.RoleID = G.RoleID
	WHERE  G.RoleID = EG.RoleID
	       AND G.ModeID = EG.GMUModeId
	       AND G.isDelete = 1
	
	UPDATE [ROLE]
	SET    EmpGMUModeGroup = @EmpGroupID,
		   CardLevel=@CardLevel
	WHERE  RoleName = @Desc
	
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
	     'EMPGMUMODES',
	     @UserID,
	     'ALL'
	
	
	SET @Description = 'Mode(s) modified for User Group: ' + @Desc 
	
	EXEC usp_InsertAuditData
	     @User_ID = @UserID,
	     @User_Name = @UserName,
	     @Module_ID = @ModuleID,
	     @Module_Name = @ModuleName,
	     @Screen_Name = 'Associate Modes',
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
  
  
