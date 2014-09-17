USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateLastRuninSetting]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateLastRuninSetting]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================    
-- SP to Update setting table with last verification run   

-- Revision History    
-- M Senthil 07/06/2010  Created    
-- ======================================================================= 

create Proc usp_UpdateLastRuninSetting  
(  
 @date varchar(50)  
)  
as  
if exists(select * from setting where setting_name = 'LastVerificationRun')  
BEGIN  
 Update Setting set Setting_Value =@date where setting_name = 'LastVerificationRun'  
END  
Else  
BEGIN  
 Insert into Setting Values('LastVerificationRun',@date)  
END  
GO

