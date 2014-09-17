USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_verifySiteAAMSStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_verifySiteAAMSStatus]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--procedure to verify the site aams code is verified or not
-- created by Madhu on 10 dec 2009
create proc rsp_verifySiteAAMSStatus
(
	@SiteCode varchar(10),
	@IsVerified bit output
)
as
declare @AAMSStatus int
declare @AAMSCode varchar(100)
set	@IsVerified = 0
select @aamsstatus = isnull(bad_aams_status ,0),
		@AAMSCode = isnull(bad_aams_code,'')
 from site
join bmc_aams_details bad on bad.bad_reference_id = site.nga_machine_id
where site_code = @SiteCode
and BAD_AAMS_Entity_Type = 3

if	@AAMSCode <> '' and @aamsStatus >0
begin
	set	@IsVerified = 1
END

GO

