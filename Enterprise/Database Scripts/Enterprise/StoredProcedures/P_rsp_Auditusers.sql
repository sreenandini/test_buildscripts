USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_Auditusers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_Auditusers]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
CREATE PROCEDURE rsp_Auditusers  
(
@audittime varchar(50),  
@Sitecode varchar(20),  
@CollectionBatch int,  
@username varchar(50),  
@logmessage varchar(5000)
)  
AS  
  SET DATEFORMAT dmy  
  
INSERT INTO USER_AUDIT  
(  
 TIME_OF_AUDIT,  
 Sitecode,    
 CollectionBatch,  
 USERNAME,  
 LOG_MESSAGE
)  
VALUES  
(  
  @AUDITTIME,  
  @Sitecode,  
  @CollectionBatch,  
  @USERNAME,  
  @LOGMESSAGE
)  
  
if (@@error =0)  
  RETURN -1  
  
RETURN 0  

GO

