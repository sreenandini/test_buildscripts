USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_IsNoteCounterVisible]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_IsNoteCounterVisible]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dbo.[rsp_IsNoteCounterVisible]
(@Site_ID INT,
@IsVisible BIT = NULL OUTPUT)  
AS  
BEGIN  
 SET NOCOUNT ON  
 -- BEGIN  
   
 DECLARE @TDMValue VARCHAR(1000)  

 EXEC [dbo].[rsp_GetSiteSetting] @Site_ID, 'TicketDeclarationMethod', @TDMValue OUTPUT
 IF @TDMValue IS NULL OR @TDMValue = ''
	SET @TDMValue = 'AUTO'
   
 SELECT @TDMValue  
 SET @IsVisible = (CASE @TDMValue WHEN 'MANUAL' THEN 1 ELSE 0 END)  
   
 -- END  
 SET NOCOUNT OFF  
END  

GO

