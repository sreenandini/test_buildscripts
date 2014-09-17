USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetModelType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetModelType]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
* Revision History  
*   
* <Name of the owner>         DateCreated       Type(Created/Modified)  Description  
  Kalaiyarasan.P              18-Sep-2012         Created               This SP is used to get Model Type details
  --Exec  rsp_GetModelType                                                                   
*/  
  
CREATE PROCEDURE rsp_GetModelType
	@IsNGA BIT = NULL,
	@MT_ID INT = NULL
AS
BEGIN
	SELECT MT_ID,
	       MT_Model_Name,
	       MT_Model_Desc,
	       MT_IsNGA
	FROM   Model_Type WITH(NOLOCK)
	WHERE  MT_IsNGA = COALESCE(@IsNGA, MT_IsNGA)
	       AND MT_ID = COALESCE(@MT_ID, MT_ID)
	ORDER BY
	       MT_Model_Name
END

GO

