USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSTMExport_History]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSTMExport_History]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*  
Exec rsp_GetSTMExport_History
*/  
CREATE PROCEDURE rsp_GetSTMExport_History 
       
AS 
BEGIN
	SELECT TOP 100 ID
		  ,[Type]
		  ,ClientID
		  ,Site_Code
		  ,[Message]
		  ,Status
		  ,Received_Date
		  ,Processed_Date
		  ,Result
           FROM  
	       STM_Export_History WITH (NOLOCK) 
	WHERE ISNULL(Status,0) = 0 
	ORDER BY ID
END

GO

