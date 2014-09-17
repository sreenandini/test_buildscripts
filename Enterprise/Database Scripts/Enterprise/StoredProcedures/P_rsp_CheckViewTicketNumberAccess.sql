USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckViewTicketNumberAccess]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckViewTicketNumberAccess]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================    
-- =======================================================================    

---------------------------------------------------------------------------    
CREATE PROCEDURE [dbo].[rsp_CheckViewTicketNumberAccess]  
(@LoginUserId INT)
AS        
BEGIN    
	SELECT HQ_User_Access.HQ_CashierTransactions_ViewNumberTickets 
	FROM Staff 
		INNER JOIN HQ_User_Access INNER JOIN User_Group 
				ON HQ_User_Access.HQ_User_Access_ID = User_Group.HQ_User_Access_ID 
				ON Staff.User_Group_ID = User_Group.User_Group_ID 
	INNER JOIN [user] ON  Staff.UserTableID=[user].SecurityUserID 
	WHERE [user].SecurityUserID = @LoginUserId
  
END     

  
  
   
  
GO

