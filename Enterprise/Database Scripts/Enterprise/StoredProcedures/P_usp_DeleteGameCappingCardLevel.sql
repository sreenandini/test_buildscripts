USE ENTERPRISE
GO
 
IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_DeleteGameCappingCardLevel]')
              AND TYPE IN (N'P', N'PC')
   )
BEGIN
    DROP PROCEDURE usp_DeleteGameCappingCardLevel
END
GO

/*****************************************************************************************************  
DESCRIPTION : Delete CardLevel from GameCappingCardLevelSettings    
CREATED DATE: 08/10/2013 
Sp Used by Module : ENTERPRISE   
  
CHANGE HISTORY :  
------------------------------------------------------------------------------------------------------  
AUTHOR    DESCRIPTON           DATE  
------------------------------------------------------------------------------------------------------  
Aishwarrya V S	  Prodecure created        08/10/2013   
              
*****************************************************************************************************/  
CREATE PROCEDURE usp_DeleteGameCappingCardLevel(@CardLevel INT, @Site VARCHAR(50),@Staff_ID INT,@Module_ID INT,@Module_Name VARCHAR(150), @Screen_Name VARCHAR(150))
AS																				  
BEGIN																			  
	DECLARE @Site_ID VARCHAR(10)		
	DECLARE @Audit_DESC VARCHAR(MAX)
	DECLARE @Staff_Name  VARCHAR(100)
												 
	DELETE GameCappingCardLevelSettings
	WHERE  CardLevel = @CardLevel
	       AND SITE = @Site	
	       
	 SET @Audit_DESC = 'SiteCode = ' + @Site
			+ ' ,CardLevel = ' + CAST(@CardLevel AS VARCHAR(10)) +' Deleted'
	       
	SELECT @Staff_Name = ISNULL(Staff_First_Name, '')
	FROM   Staff
	WHERE  Staff_ID = @Staff_ID
	
	EXEC [usp_InsertAuditData] 
	     @Staff_ID,
	     @Staff_Name,
	     @Module_ID,
	     @Module_Name,
	     @Screen_Name,
	     '',
	     '',
	     '',
	     'TRUE',
	     @Audit_DESC,
	     'DELETED'
END


--SELECT * FROM AUDIT_HISTORY ORDER BY 1 DESC

