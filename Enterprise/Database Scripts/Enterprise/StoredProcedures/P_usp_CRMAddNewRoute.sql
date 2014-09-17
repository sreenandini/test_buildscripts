USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_CRMAddNewRoute]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_CRMAddNewRoute]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
CREATE PROC dbo.usp_CRMAddNewRoute
 @SiteID INT ,
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
Description		: Add new Route
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
    
	INSERT INTO [dbo].[Route]
	  (
	    [Route_Name],
	    [CreatedAt],
	    [Active],
	    [ModifiedDate],
	    [Site_ID],
	    [User_id]
	  )
	VALUES
	  (
	    @RouteName,
	    GETDATE(),
	    @Acive,
	    GETDATE(),
	    @SiteID,
	    @UserID
	  )
	
	--INSERT INTO [dbo].[Audit_History]
	--	([Audit_Module_Name],
	--	[Audit_User_ID],
	--	[Audit_Screen_Name],
	--	[Audit_Module_ID],
	--	[Audit_Desc],
	--	[Audit_Operation_Type],
	--	[Audit_User_Name],
	--	[Audit_Date]	)
	--VALUES
	--(@Audit_ModuleName,
	--@UserID,
	--@Audit_ScreenName,
	--@Audit_ModuleId ,
	--'Route Added',
	--'ADD',
	--@UserName,
	--GETDATE()
	--)
	
     
	DECLARE @RouteID VARCHAR(200) 
	SET @RouteID ='Route ID: ['+ CAST( SCOPE_IDENTITY() AS VARCHAR(20)) + '] RouteName:' + @RouteName
	 
	EXEC usp_InsertAuditData 
	     @User_ID = @UserID,
	     @USER_NAME = @UserName,
	     @Module_ID = @Audit_ModuleId,
	     @Module_Name = @Audit_ModuleName,
	     @Screen_Name = @Audit_ScreenName,
	     @Aud_Desc = 'Route Added',
	     @Operation_Type = 'ADD',
	     @Slot='',@Aud_Field='',@Old_Value='',@New_Value= @RouteID 
	     
END


GO

