USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSetting]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSetting]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetSetting    
     
    @Setting_ID      int = 0,    
    @Setting_Name    varchar(100) = NULL,    
    @Setting_Default varchar(100) = NULL,    
    @Setting_Value   varchar(100) OUTPUT    
    
AS    
    
  SET NOCOUNT ON      -- <<< ADO likes this when using temp tables     
   
	IF @Setting_Name ='PRODUCTVERSION'
	BEGIN
		SELECT top 1 @Setting_Value=VersionName 
		FROM versionhistory	ORDER BY versionDate desc
		return 0 
	END
 
  -- test for invalid setting name/id     
  IF ( @Setting_ID = 0 AND @Setting_Name IS NULL )     
    RETURN -1    
    
  IF ( @Setting_ID <> 0 )    
    IF NOT EXISTS ( SELECT 1     
                      FROM Setting     
                     WHERE Setting_ID = @Setting_ID    
                  )    
      -- can't find setting based on id, requires user to create ..    
      RETURN -1    
    
  IF ( @Setting_Name <> '' )    
  BEGIN      
    IF NOT EXISTS ( SELECT 1     
                      FROM Setting     
                     WHERE Setting_Name = @Setting_Name     
                  )    
    
    -- create new record ..    
    --        
    INSERT INTO Setting    
          ( Setting_Name, Setting_Value )    
      VALUES    
          ( @Setting_Name, @Setting_Default )      
  END       
    
  -- get value ..    
  --    
  SELECT @Setting_Value = COALESCE ( Setting_Value, '' )     
    FROM Setting    
   WHERE (     
           @Setting_Name IS NOT NULL     
           AND     
           Setting_Name = @Setting_Name     
         )    
      OR (     
           @Setting_ID <> 0     
           AND     
           Setting_ID = @Setting_ID    
         )    
    
    
-- Return Error (if any)    
--    
RETURN @@ERROR    

GO

