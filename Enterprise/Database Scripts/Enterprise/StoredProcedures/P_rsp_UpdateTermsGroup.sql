USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_UpdateTermsGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_UpdateTermsGroup]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE rsp_UpdateTermsGroup    
(    
@Site_ID INT,@Terms_Group_ID INT,@Terms_Group_ID_Default BIT    
)    
AS        
BEGIN    
    
Update   [Site]SET Terms_Group_ID=@Terms_Group_ID,Terms_Group_ID_Default=@Terms_Group_ID_Default WHERE Site_ID=@Site_ID    

UPDATE BP
	SET Terms_Group_ID=@Terms_Group_ID,Terms_Group_ID_Default=@Terms_Group_ID_Default 
	FROM Bar_Position BP
	INNER JOIN [Site] S ON BP.Site_ID = S.Site_ID
	WHERE S.Site_ID = @Site_ID
    
END


GO

