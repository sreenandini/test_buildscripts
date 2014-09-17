USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_CreatePeriodEndDocNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_CreatePeriodEndDocNo]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-------------------------------------------------------------------------- 
--
-- Description: update and return next period end document number
--
-- Inputs:      See inputs
--
-- Outputs:     
--
-- =======================================================================
-- 
-- Revision History
-- 
-- C.Taylor  17/07/08   Created
--------------------------------------------------------------------------- 
-- to use
-- DECLARE @docno varchar(10)
-- EXEC usp_CreatePeriodEndDocNo @docno  OUTPUT
-- select @docno
--------------------------------------------------------------------------- 
CREATE PROCEDURE usp_CreatePeriodEndDocNo

  @Period_End_DocNo varchar(10) OUTPUT

AS

  declare @retSetting_Value varchar(50),
          @iDocNo           int,
          @strDocNo         varchar(50)

-- get next number and return
  EXEC rsp_GetSetting 0, 'NEXT_PERIOD_END_DOC_NO','1', @retSetting_Value OUTPUT
 
  SET @Period_End_DocNo = @retSetting_Value 

-- update the number and save away for next time.
  SET @iDocNo = CAST ( @Period_End_DocNo AS INT ) 
  SET @iDocNo = @iDocNo + 1 

  SET @strDocNo = cast ( @iDocNo as varchar(50) )  

  EXEC usp_EditSetting 0, 'NEXT_PERIOD_END_DOC_NO', @strDocNo
   

GO

