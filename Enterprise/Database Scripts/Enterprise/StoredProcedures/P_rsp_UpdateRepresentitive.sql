USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_UpdateRepresentitive]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_UpdateRepresentitive]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_UpdateRepresentitive    
(    
@Site_ID INT,@Staff_ID INT,@Staff_ID_Default BIT    
)    
AS        
BEGIN    
    
Update   [Site] SET Staff_ID=@Staff_ID,Staff_ID_Default=@Staff_ID_Default WHERE Site_ID=@Site_ID    
    
END


GO

