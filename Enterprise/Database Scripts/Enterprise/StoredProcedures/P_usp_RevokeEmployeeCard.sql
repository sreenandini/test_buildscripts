USE [Enterprise]
GO

IF EXISTS (
       SELECT TOP 1 1
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_RevokeEmployeeCard]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_RevokeEmployeeCard]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*    
* ******************************************************************************************************    
* Revision History    
*     
* Anuradha   Created    11/06/2012    
*     
* Revoke the Employee card details from Employee     
* ******************************************************************************************************     
*/    
    
        
CREATE PROCEDURE [dbo].[usp_RevokeEmployeeCard](
    @UserID          INT,
    @EmpCardNumbers  VARCHAR(200),
    @AuditUserId     INT,
    @AuditUserName   VARCHAR(50)
)
AS
BEGIN
	SET NOCOUNT ON 
	
	BEGIN TRAN
	DECLARE @EmpName VARCHAR(50)
	
	SELECT @EmpName = EmployeeName
	FROM   tblEmployeeCardDetails
	WHERE  UserID = @UserID
	
	
	UPDATE tblEmployeeCardDetails
	SET    UserID = NULL,
	       EmployeeName = ''
	WHERE  EmployeeCardNumber IN (SELECT VALUE AS EmpCardNumbers
	                              FROM   UDF_GetStringTable(@EmpCardNumbers, ','))
	
	UPDATE [User]
	SET    EmpCardNumber = '',
	       IsSingleCardEmployee = NULL,
	       IsActiveCard = 0
	WHERE  SecurityUserID = @UserID
	
	
	
	IF EXISTS(
	       SELECT TOP 1 1
	       FROM   tblEmployeeCardDetails
	       WHERE  UserID = @UserID
	   )
	BEGIN
	    DECLARE @EmpAssignedCards VARCHAR(100)
	    
	    SELECT @EmpAssignedCards = ISNULL(@EmpAssignedCards, '') + ISNULL(Emp.EmployeeCardNumber, '') 
	           + ','
	    FROM   tblEmployeeCardDetails Emp
	    WHERE  Emp.UserID = @UserID
	    
	    UPDATE [User]
	    SET    EmpCardNumber = @EmpAssignedCards,
	           IsSingleCardEmployee = NULL,
	           IsActiveCard = 1
	    WHERE  SecurityUserID = @UserID
	END
	
	INSERT Export_History
	  (
	    EH_Date,
	    EH_Reference1,
	    EH_Type,
	    EH_Site_Code
	  )
	SELECT GETDATE(),
	       @UserID,
	       'UserDetails',
	       St.Site_Code
	FROM   UserSite_lnk USL
	       INNER JOIN [Site] St
	            ON  St.Site_ID = USL.SiteID
	            AND USL.SecurityUserID = @UserID
	
	DECLARE @Desc VARCHAR(250)
	
	SET @Desc = 'Employee Card:  ' + @EmpCardNumbers +
	    ' SignedOff Successfully for Employee: ' + @EmpName
	
	SELECT @Desc
	
	EXEC usp_InsertAuditData
	     @User_ID = @AuditUserId,
	     @User_Name = @AuditUserName,
	     @Module_ID = 557,
	     @Module_Name = 'EmployeeCard',
	     @Screen_Name = 'Employee Card Details - SignOff Employee',
	     @Slot = NULL,
	     @Aud_Field = EmployeeCard,
	     @Old_Value = NULL,
	     @New_Value = 'TRUE',
	     @Aud_Desc = @Desc,
	     @Operation_Type = 'Remove(SignOff)' 
	
	IF @@ERROR = 0
	    COMMIT TRAN
	ELSE
	    ROLLBACK TRAN
END
GO
