USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetInstallationGameInfoinXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetInstallationGameInfoinXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--SP to get Installation_Game_info in XML
create proc rsp_GetInstallationGameInfoinXML 
(
	@Installation_ID int
)
as
Select * from Installation_Game_Info 
where HQ_IGI_ID = @Installation_ID
for xml path('InstallGameInfo'), root('Game')

GO

