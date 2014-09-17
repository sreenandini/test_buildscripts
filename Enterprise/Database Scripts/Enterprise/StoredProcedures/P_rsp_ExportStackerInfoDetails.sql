USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportStackerInfoDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportStackerInfoDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_ExportStackerInfoDetails @Stacker_Id int
       
AS 
BEGIN
	SELECT Stacker_Id
      ,StackerName
      ,StackerSize
      ,StackerStatus
      ,StackerDescription
      ,DateCreated
      ,DateModified
      ,SysDelete
  FROM Stacker AS Stacker_Info 
 Where  Stacker_Id =@Stacker_Id
 FOR XML AUTO, ELEMENTS, ROOT('Stackers')
END

GO

