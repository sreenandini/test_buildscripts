USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_CanRemoveCallSource]    Script Date: 07/31/2014 16:09:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CanRemoveCallSource]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CanRemoveCallSource]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_CanRemoveCallSource]    Script Date: 07/31/2014 16:09:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


  
CREATE PROCEDURE [dbo].[rsp_CanRemoveCallSource]  
(  
@Call_Source_ID int,  
@Result BIT output  
)  
AS  
BEGIN  
   
 SET @Result = 1
 IF EXISTS(SELECT 1 FROM [Service] WHERE  Call_Source_ID = isnull(@Call_Source_ID,0))
 BEGIN  
  SET @Result = 0  
 END  
  
  
END  
GO


