USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Export_History]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Export_History]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================    
-- Author:  <Sudarsan Srinivasan>    
-- Create date: <28/05/2008>    
-- Renjish - 04/07/08 - Modified @Reference1 size to 50 characters.
-- Sudarsan S 21/11/09	- inserted into LGE_Export_History table
-- Sudarsan S	26/01/10	- reverted the code
-- =============================================    
CREATE PROCEDURE [dbo].[usp_Export_History]    
 -- Add the parameters for the stored procedure here    
 @Reference1 Varchar(50),    
 @Type Varchar(30) ,  
 @Site_id int
 
AS    
BEGIN    
---- -- SET NOCOUNT ON added to prevent extra result sets from    
---- -- interfering with SELECT statements.    
-- SET NOCOUNT ON;    
-- DECLARE @EHReference  varchar(50)  
----get the site code from site table.  
DECLARE @Site_Code Varchar (50)
    SELECT @Site_Code = Site_Code FROM dbo.Site WHERE Site_ID = @Site_id  
--       
-- IF ISNULL(@Site_Code, '') = '1'  
--  BEGIN  
--  SET @EHReference = @Site_Code  
--     END  
--     ELSE  
-- -- the eh reference does not contain site code.  
--  BEGIN  
--  SET @EHReference =@Reference1  
--     END  

--	IF @Type <> 'GAME' and isnull(@Site_Code,'') <> ''
	Insert into dbo.Export_History(EH_Date,EH_Reference1,EH_Type, EH_Site_Code) Values (GETDATE(),@Reference1,@Type, @Site_Code)


--	IF @Type IN ('AUTOINSTALLATION', 'GAME')
--	BEGIN
--		INSERT INTO dbo.LGE_Export_History(LGE_EH_Date, LGE_EH_Reference, LGE_EH_Type)
--		VALUES(GETDATE(), @Reference1,@Type)
--	END
    
END


GO

