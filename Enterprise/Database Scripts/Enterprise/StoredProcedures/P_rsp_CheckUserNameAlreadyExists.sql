USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckUserNameAlreadyExists]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[rsp_CheckUserNameAlreadyExists]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_CheckUserNameAlreadyExists]
      @StaffUser_Name VARCHAR(50) = '',
      @Staff_Id INT
AS

-------------------------------------------------------------------------------------------------------------------------
---    
--- Description: Check whether the user name already exists
---        
--- Inputs:         
--- Outputs:     
--- ======================================================================================================================    
---     
--- Revision History    
---     
--- Dinesh R		03/05/2013		Created     
--------------------------------------------------------------------------------------------------------------------------

BEGIN
		SET NOCOUNT ON

		SELECT Staff_Username FROM [Staff] WHERE Staff_Username = @StaffUser_Name and Staff_ID <> @Staff_Id  
	
END

GO