USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetTermsConfigForPeriodEnd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetTermsConfigForPeriodEnd]
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
--- C.Taylor   02 Jul 08     Created 
--------------------------------------------------------------------------- 
CREATE PROCEDURE rsp_GetTermsConfigForPeriodEnd

AS

  declare @date datetime

  set @date = getdate()
  
  select Terms_Group_Name,
         tg.Terms_Group_ID,
	 Site_Share = CASE WHEN @date < cast ( Share_Band_Past_End_Date as datetime ) THEN share_band_Past_site_share 
                           WHEN @date > cast ( Share_Band_Future_Start_Date as datetime ) THEN share_band_future_site_share
  	 		   ELSE share_band_site_share
                      END,

 	 Operator_share = CASE WHEN @date < cast ( Share_Band_Past_End_Date as datetime ) THEN share_band_Past_supplier_share 
                               WHEN @date > cast ( Share_Band_Future_Start_Date as datetime ) THEN share_band_future_supplier_share
	 		      ELSE share_band_supplier_share
                          END,

	 Company_Share = CASE WHEN @date < cast ( Share_Band_Past_End_Date as datetime ) THEN share_band_Past_company_share 
                              WHEN @date > cast ( Share_Band_Future_Start_Date as datetime ) THEN share_band_future_company_share
			      ELSE share_band_company_share
                         END

    FROM terms_profile tp

    JOIN terms_Group tg
      ON tp.Terms_Group_ID = tg.Terms_Group_ID

    JOIN share_schedule ss
      ON Terms_Profile_Partners_Supplier_Share_Schedule = ss.share_schedule_id

    JOIN share_band sb
      ON ss.share_schedule_id = sb.share_schedule_id


GO

