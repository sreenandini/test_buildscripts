USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ConfirmPeriodEnd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ConfirmPeriodEnd]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-------------------------------------------------------------------------- 
---
--- Description: Assigns a statement against a period end id.
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
--- C.Taylor   25 Jul 2008     Created 
--- C.Taylor   6 Aug 2008      Issue when updating statement_no
---------------------------------------------------------------------------
CREATE PROCEDURE usp_ConfirmPeriodEnd
 
  @Period_End_ID     int,
  @Sub_Company_ID    int,
  @Period_End_Doc_No int

AS

  UPDATE Period_End
     SET Statement_No   = @Period_End_Doc_No
   WHERE Period_End_ID  = @Period_End_ID
     AND Sub_Company_ID = @Sub_Company_ID
     AND COALESCE(Statement_No,0)   IN ( -1, 0 )
     

GO

