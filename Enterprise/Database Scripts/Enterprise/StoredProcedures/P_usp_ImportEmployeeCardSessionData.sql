USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ImportEmployeeCardSessionData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ImportEmployeeCardSessionData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/************************************************************    
 * Code formatted by SoftTree SQL Assistant � v4.8.29    
 * Time: 09/11/12 18:17:09    
 ************************************************************/    
    
-- =======================================================================        
-- OUTPUT    To get all users based on site id      
-- =======================================================================        
-- Revision History  --  Exec  usp_ImportEmployeeCardSessionData     
--  Anuradha   Created  14 Sep 2012    
---------------------------------------------------------------------------        
CREATE PROCEDURE [dbo].usp_ImportEmployeeCardSessionData    
(@doc VARCHAR(MAX))    
AS    
BEGIN    
 SET NOCOUNT ON    
     
 DECLARE @docHandle INT              
 EXEC sp_xml_preparedocument @docHandle OUTPUT,    
      @doc    
     
 IF NOT EXISTS     
    (    
        SELECT *    
        FROM   OPENXML(@docHandle, '/EmpSessionDetails/Session', 2)     
               WITH     
               (    
                   EmpID INT 'EmpID',    
                   SessionStart VARCHAR(30) 'SessionStart',    
                   SessionEnd VARCHAR(30) 'SessionEnd',    
                   InstallationID INT 'InstallationNo',   
                   UserID INT 'UserID', 
                   SiteCode VARCHAR(20) 'Site_ID'    
               ) x    
               INNER JOIN tblEmployeeCardSessions tecs    
                    ON  tecs.EmpID = x.EmpID    
                    AND tecs.SessionStart = x.SessionStart    
                    AND tecs.SessionEnd = x.SessionEnd    
                    AND tecs.InstallationID = x.InstallationId   
                    AND tecs.UserId = x.UserID 
                    AND tecs.SiteCode = x.SiteCode    
    )    
 BEGIN    
     INSERT INTO tblEmployeeCardSessions    
     SELECT EmpID,    
            EmpCardId,    
            EmpName,    
            SessionStart,    
            SessionEnd,    
            SessionDuration,    
            InstallationId,
            UserID,    
            SiteCode    
     FROM   OPENXML(@docHandle, '/EmpSessionDetails/Session', 2)     
            WITH     
            (    
                EmpID INT 'EmpID',    
                EmpCardID VARCHAR(30) 'EmpCardID',    
                EmpName VARCHAR(30) 'EmpName',    
                SessionStart VARCHAR(30) 'SessionStart',    
                SessionEnd VARCHAR(30) 'SessionEnd',    
                SessionDuration INT 'SessionDuration',    
                InstallationID INT 'InstallationNo',  
                UserID  INT 'UserID', 
                SiteCode VARCHAR(20) 'Site_ID'    
            ) x    
 END    
END

GO

