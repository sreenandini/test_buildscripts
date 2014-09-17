USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCollectionsForPeriodEnd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCollectionsForPeriodEnd]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-------------------------------------------------------------------------- 
---
--- Description: 
---
--- Inputs:      see inputs
---
--- Outputs:     (0)   - no error .. 
---              OTHER - SQL error 
--- 
--- =======================================================================
--- 
--- Revision History
--- 
--- C.Taylor   02 Jul 2008     Created 
--- C.Taylor   25 Jul 2008     Debug code removed, changed to use cd.period_end_id
---------------------------------------------------------------------------
CREATE PROCEDURE rsp_GetCollectionsForPeriodEnd

  @Period_End_ID   INT

AS 

set nocount on

  select c.collection_id 
    from collection c
    join collection_details cd
      on c.collection_id = cd.collection_id
   where cd.period_end_id = @Period_End_ID

  RETURN @@ERROR


GO

