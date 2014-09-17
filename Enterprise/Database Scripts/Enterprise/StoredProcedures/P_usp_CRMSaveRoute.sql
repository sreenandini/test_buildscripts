USE ENTERPRISE 
GO
 
IF EXISTS (
       SELECT 'D'
       FROM   SYS.OBJECTS
       WHERE  NAME = 'usp_CRMSaveRoute'
   )
BEGIN
    DROP PROC dbo.usp_CRMSaveRoute
END
GO
  
GO 

CREATE PROC dbo.usp_CRMSaveRoute
 @SiteID INT ,
 @RouteID INT,
 @RouteName VARCHAR(200),
 @Acive INT,
 @UserID INT,
 @Audit_ModuleName VARCHAR(100),
 @Audit_ScreenName VARCHAR(100),
 @Audit_ModuleId INT,
 @UserName VARCHAR(100)

AS
/************************************************************************************
Used In(Module) : Route Manager
Created Date	:
Description		: Save route modifications
======================================================================================
Modification History
	Developer		      Date		Modification 					
======================================================================================
1) K.Karthicksundar			  		Created 	
2)	
**************************************************************************************/
BEGIN
	DECLARE @staffid INT
	SELECT @staffid=Staff_ID,  @UserName=ISNULL(staff_last_name,'') + ',' + isnull(staff_first_name,'')
    FROM   staff WITH(NOLOCK)
    WHERE  UserTableID = @UserID
    
	DECLARE @Audit TABLE(
	            RouteID INT,
	            name_Old_Value VARCHAR(200),
	            name_New_Value VARCHAR(200),
	            Old_Value VARCHAR(200),
	            New_Value VARCHAR(200)
	        )
	
	
	UPDATE dbo.[Route]
	SET    Route_Name = @RouteName,
	       [User_id] = @UserID,
	       ACTIVE = @Acive,
	       ModifiedDate = GETDATE()
	       OUTPUT INSERTED.Route_ID,
	       DELETED.Route_Name,
	       INSERTED.Route_Name,
	       DELETED.ACTIVE,
	       INSERTED.ACTIVE
	       INTO @Audit
	WHERE  Route_ID = @RouteID
	--	if(@Acive=0)
	--	BEGIN
	--		DELETE FROM dbo.[Route_Member] Where Route_ID=@RouteID
	--	END
	
	DECLARE @name_Old_Value  VARCHAR(200)
	DECLARE @name_New_Value  VARCHAR(200)
	DECLARE @act_Old_Value   VARCHAR(200)
	DECLARE @act_New_Value   VARCHAR(200)
	
	--AUDIT NAME CHANGE 
	SELECT @name_Old_Value = name_Old_Value,
	       @name_New_Value = name_New_Value,
	       @act_Old_Value = Old_Value,
	       @act_New_Value = New_Value
	FROM   @Audit
	
	
	
	IF @name_Old_Value <> @name_new_Value
	BEGIN
	    EXEC usp_InsertAuditData 
	         @User_ID = @staffid,
	         @USER_NAME = @UserName,
	         @Module_ID = @Audit_ModuleId,
	         @Module_Name = @Audit_ModuleName,
	         @Screen_Name = @Audit_ScreenName,
	         @Aud_Desc = 'Updated Name',
	         @Operation_Type = 'MODIFY',
	         @Slot = '',
	         @Aud_Field = 'Route_Name',
	         @Old_Value = @name_Old_Value,
	         @New_Value = @name_new_Value
	END     
	
	IF @act_old_Value <> @act_New_Value
	BEGIN
	    EXEC usp_InsertAuditData 
	         @User_ID = @UserID,
	         @USER_NAME = @UserName,
	         @Module_ID = @Audit_ModuleId,
	         @Module_Name = @Audit_ModuleName,
	         @Screen_Name = @Audit_ScreenName,
	         @Aud_Desc = 'Updated Status',
	         @Operation_Type = 'MODIFY',
	         @Slot = '',
	         @Aud_Field = 'Active',
	         @Old_Value = @act_old_Value,
	         @New_Value = @act_New_Value
	END
END
GO