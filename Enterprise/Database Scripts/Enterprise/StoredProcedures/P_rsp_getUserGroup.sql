USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_getUserGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_getUserGroup]
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
* Name : Selva Kumar S
* Date : 31st May 2012
*
* Name					DateCreated       Type(Created/Modified)  Description
*
*/

CREATE PROCEDURE rsp_getUserGroup
AS
      SELECT User_Group_ID, User_Group_Name FROM User_Group
      ORDER BY User_Group_Name

GO

