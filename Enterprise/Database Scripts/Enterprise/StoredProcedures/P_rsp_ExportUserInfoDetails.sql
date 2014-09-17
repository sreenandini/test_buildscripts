USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportUserInfoDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportUserInfoDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.rsp_ExportUserInfoDetails  
@User_ID INT  
AS  
BEGIN  
 SELECT SecurityuserID,Password,PasswordChangeDate,isReset,isLocked       
 FROM dbo.[user] AS User_Info  
 WHERE SecurityuserID =@User_ID 
 FOR XML AUTO, ELEMENTS, ROOT('Users')  
END

GO

