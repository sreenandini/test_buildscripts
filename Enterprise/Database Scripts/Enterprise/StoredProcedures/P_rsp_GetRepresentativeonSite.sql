/************************************************************
* Code formatted by SoftTree SQL Assistant © v4.8.29
* Time: 30/07/2013 8:26:39 PM
************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetRepresentativeonSite]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetRepresentativeonSite]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetRepresentativeonSite(@Site_ID INT)
AS
BEGIN
      SELECT DISTINCT Staff_Last_Name,
             Staff_First_Name,
             Staff_ID
      FROM   Staff st WITH(NOLOCK)
      INNER JOIN Usersite_lnk lnk  WITH(NOLOCK) ON
      st.UserTableID = lnk.SecurityUserID
      WHERE  Staff_IsaRepresentative = 'True'
      AND lnk.SiteID = @Site_ID
      ORDER BY
             Staff_Last_Name ASC,
             Staff_First_Name ASC
END
GO