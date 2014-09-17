USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_UpdateAccesskey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_UpdateAccesskey]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE rsp_UpdateAccesskey  
(  
@Site_ID INT,@Access_Key_ID INT,@Access_Key_ID_Default BIT  
)  
AS      
BEGIN  
  
Update   [Site] SET Access_Key_ID=@Access_Key_ID,Access_Key_ID_Default=@Access_Key_ID_Default WHERE Site_ID=@Site_ID  
  
END


GO

