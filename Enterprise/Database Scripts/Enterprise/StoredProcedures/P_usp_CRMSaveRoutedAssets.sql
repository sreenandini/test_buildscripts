USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_CRMSaveRoutedAssets]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_CRMSaveRoutedAssets]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
CREATE PROC dbo.usp_CRMSaveRoutedAssets 
@Route_No INT, 
@Bar_PositionsList XML,
@Audit_ModuleName VARCHAR(100),
@Audit_ScreenName VARCHAR(100),
@Audit_ModuleId INT,
@UserName VARCHAR(100),
@UserID INT

AS 
/************************************************************************************
Used In(Module) : Route Manager
Created Date	:
Description		: Asign assets to route
======================================================================================
Modification History
	Developer		      Date		Modification 					
======================================================================================
1) K.Karthicksundar			  		Created 	
2)	
**************************************************************************************/
BEGIN
	
	
	DECLARE @temp   TABLE
	        (Route_ID INT, Bar_Position_ID INT)
	
	DECLARE @Audit  TABLE
	        (RouteID INT, barposition_ID INT, Operation VARCHAR(20))
	
	 DECLARE @staff_id INT 
	 
     SELECT @staff_id=Staff_ID,  @UserName=ISNULL(staff_last_name,'') + ',' + isnull(staff_first_name,'')
     FROM   staff WITH(NOLOCK)
     WHERE  UserTableID = @UserID
        
        
	INSERT INTO @temp
	  (
	    Route_ID,
	    Bar_Position_ID
	  )
	SELECT DISTINCT @Route_No,
	       nref.value('.', 'int') AS POS
	FROM   @Bar_PositionsList.nodes('/Bar_PositionsList/Pos') r(nref)
	
	
	--SELECT usertableid FROM Staff s
	
	--DELETE POSITIONS FROM OTHER ROUTES
	
	--DELETE MEMBERS OF CURRENT ROUTE
	DELETE 
	FROM   [Route_Member]
	       OUTPUT DELETED.Route_ID,
	       DELETED.Bar_Position_ID,
	       'DELETE'
	       INTO @Audit
	WHERE  Route_ID = @Route_No 
	AND Bar_Position_ID NOT IN 
	(
		SELECT 
	       Bar_Position_ID
		FROM   @temp 
	)

	INSERT INTO 
	       Route_Member
	  (
	    --Route_Member_ID,	-- this column value is auto-generated,
	    Route_ID,
	    Bar_Position_ID
	  )OUTPUT INSERTED.Route_ID,INSERTED.Bar_Position_ID,'ADD'
	   INTO @Audit
	SELECT Route_ID,
	       Bar_Position_ID
	FROM   @temp
	WHERE Bar_Position_ID NOT IN 
	(
		SELECT 
	       Bar_Position_ID
		FROM   Route_Member RM WHERE Rm.Route_ID=@Route_No 
	)
	
	
	DELETE 
	FROM   @Audit
	WHERE  barposition_ID IN (SELECT B.barposition_ID
	                          FROM   @Audit B
	                          GROUP BY
	                                 B.barposition_ID
	                          HAVING COUNT('D') > 1)
	
	
	
	INSERT INTO Audit_History
	  (
	    --Audit_ID -- this column value is auto-generated,
	    Audit_Date,
	    Audit_User_ID,
	    Audit_User_Name,
	    Audit_Module_ID,
	    Audit_Module_Name,
	    Audit_Screen_Name,
	    Audit_Desc,
	    Audit_Field,
	    Audit_Old_Vl,
	    Audit_New_Vl,
	    Audit_Operation_Type
	  )
	SELECT GETDATE(),
	       @UserID,
	       @UserName,
	       @Audit_ModuleId,
	       @Audit_ModuleName,
	       @Audit_ScreenName,
	       CASE 
	            WHEN Operation = 'ADD' THEN 'Added BarPosition : ' + CAST(bp.Bar_Position_Name AS VARCHAR(20)) 
	                 + ' To Route Name : ' + CAST(r.Route_Name AS VARCHAR(20))
	            ELSE 'Deleted BarPosition : ' + CAST(bp.Bar_Position_Name AS VARCHAR(20)) +
	                 ' From Route Name : ' + CAST(r.Route_Name AS VARCHAR(20))
	       END,
	       '',
	       CASE 
	            WHEN Operation = 'DELETE' THEN CAST(barposition_ID AS VARCHAR(20))
	            ELSE ''
	       END,
	       CASE 
	            WHEN Operation = 'ADD' THEN CAST(barposition_ID AS VARCHAR(20))
	            ELSE ''
	       END,
	       Operation
	FROM   @Audit a
	       INNER JOIN [Route] r
	            ON  r.Route_ID = a.RouteID
	       INNER JOIN Bar_Position bp
	            ON  bp.Bar_Position_ID = a.barposition_ID
	
END


GO

