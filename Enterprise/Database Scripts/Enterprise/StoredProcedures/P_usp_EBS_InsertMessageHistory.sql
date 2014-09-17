/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 03/11/2014 3:18:03 PM
 ************************************************************/

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_EBS_InsertMessageHistory]    Script Date: 03/04/2014 20:25:54 ******/
IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_EBS_InsertMessageHistory]')
              AND TYPE IN (N'P', N'PC')
	   )
    DROP PROCEDURE [dbo].[usp_EBS_InsertMessageHistory]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_EBS_InsertMessageHistory]    Script Date: 03/04/2014 20:25:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_EBS_InsertMessageHistory](
    @EBS_FromSystem  TINYINT,
    @EBS_ToSystem    TINYINT,
    @EBS_SiteCode    VARCHAR(50),
    @EBS_DateTime    DATETIME,
    @EBS_RefID INT = NULL,
    @EBS_Request     XML = NULL,
    @EBS_Response    XML = NULL
)
AS
BEGIN
	INSERT INTO [dbo].[EBS_Message_History]
	  (
	    [EBS_FromSystem],
	    [EBS_ToSystem],
	    [EBS_SiteCode],
	    [EBS_DateTime],
	    [EBS_RefID],
	    [EBS_Request],
	    [EBS_Response]
	  )
	VALUES
	  (
	    @EBS_FromSystem,
	    @EBS_ToSystem,
	    @EBS_SiteCode,
	    @EBS_DateTime,
	    @EBS_RefID,
	    @EBS_Request,
	    @EBS_Response
	  )
END
GO


