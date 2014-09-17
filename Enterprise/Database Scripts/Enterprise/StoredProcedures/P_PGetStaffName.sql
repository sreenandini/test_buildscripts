USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PGetStaffName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PGetStaffName]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE dbo.[PGetStaffName](@staffid int)  
as  
BEGIN  
Select Staff_first_name from Staff  
Where Staff_id = @staffid  
END
GO

