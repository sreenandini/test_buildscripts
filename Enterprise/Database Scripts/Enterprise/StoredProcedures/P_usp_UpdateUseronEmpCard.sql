USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateUseronEmpCard]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateUseronEmpCard]
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
* Anuradha   Created    07/06/2012        
*         
* Associate the Employee card details to Employee         
* ******************************************************************************************************         
*/        

CREATE  PROCEDURE usp_UpdateUseronEmpCard(      
    @UserID          INT,            
    @CardTrack       BIT,            
    @EmpCardNumbers  VARCHAR(50),      
    @IsUpdated       INT = 0 OUTPUT      
)      
AS      
BEGIN      
 SET NOCOUNT ON
 BEGIN TRY
	 
	 IF EXISTS (SELECT 1 FROM Sys.Objects WHERE Name =N'#tmpEmpCards')
		DROP TABLE #tmpEmpCards

	 CREATE TABLE #tmpEmpCards(EmpCardNumber  VARCHAR(50),UserID INT)
	 
	 INSERT #tmpEmpCards(EmpCardNumber,UserID)
	 SELECT value AS EmpCardNumbers,@UserID FROM UDF_GetStringTable(@EmpCardNumbers,',')
	 	  
	 UPDATE [User] SET EmpCardNumber = ISNULL(EmpCardNumber,'') + @EmpCardNumbers,
	 IsSingleCardEmployee = @CardTrack,
	 IsActiveCard = 1
	 WHERE SecurityUserID = @UserID
	 
	 UPDATE tblEmployeeCardDetails SET UserID = @UserID
	 WHERE EmployeeCardNumber IN (SELECT EmpCardNumber FROM #tmpEmpCards)
	 
	 INSERT Export_History(EH_Date,EH_Reference1,EH_Type,EH_Site_Code)
	 SELECT GETDATE(),@UserID,'UserDetails',St.Site_Code
	 FROM UserSite_lnk USL
	 INNER JOIN 
	 [Site] St
	 ON St.Site_ID = USL.SiteID AND USL.SecurityUserID = @UserID 
	 
	 SET @IsUpdated = 1 
 END TRY
 BEGIN CATCH
	SET @IsUpdated = 0
 END CATCH
END
GO
