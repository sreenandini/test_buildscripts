USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateSiteStats]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateSiteStats]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------                             
--                            
-- Description: Updates the site table with the site status XML
--              
-- Inputs:     @Site_Name
--				@SiteStatus
--                            
--                            
-- RETURN:      NONE                      
--                            
-- =======================================================================                            
--                             
-- Revision History                            
--                             
-- Madhu		19/06/2008     Created      
---------------------------------------------------------------------------  
CREATE procedure [dbo].[usp_UpdateSiteStats]  
(  
 @SiteName varchar(100),  
 @SiteStatus xml  
)  
  
as  
  Declare @SiteCode varchar(10)
Declare @SiteOldStatus xml
Declare @UpdateTimeStamp datetime
IF  EXISTS (SELECT * FROM Site where ltrim(rtrim(Site_Code)) = ltrim(rtrim(@SiteName)) )  
BEGIN  


Select @SiteCode = Site_Code,
	@SiteOldStatus = Site_status,
	@UpdateTimeStamp = Last_Updated_Time
 from Site where Site_Code = ltrim(rtrim(@SiteName))  


update Site  
 SET Site_Status  = @SiteStatus,  
     Last_Updated_Time = getdate()  
 WHERE ltrim(rtrim(Site_Code)) = ltrim(rtrim(@SiteName))  

IF @SiteStatus is not null
begin
	Insert into SiteStatusHistory (SiteCode ,SiteStatus ,UpdateTimeStamp) values (@SiteCode,@SiteStatus,getdate())
end

END  

--update site workstation from the XML recived  
declare @siteID int
select top 1 @SiteID = Site_ID from site where Site_Code = ltrim(rtrim(@SiteName)) 

DECLARE @docHandle int    
EXEC sp_xml_preparedocument @docHandle OUTPUT, @SiteStatus    

IF isnull(@SiteID,0) <> 0 
BEGIN
	delete from SiteWorkstations where Site_ID = @SiteID

	insert into SiteWorkstations
	select @SiteID,HostName  from openxml(@dochandle, 'Site/Status/HostNames/HostName',2)
	with 
	(
		HostName varchar(100) 'TIW_Name'
	)
END
  
EXEC sp_xml_removedocument @dochandle    

GO

