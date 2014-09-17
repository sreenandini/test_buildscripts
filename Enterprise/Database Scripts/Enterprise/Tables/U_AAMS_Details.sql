USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AAMS_Details]') AND type in (N'U'))

BEGIN

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[AAMS_Details](
	[AAMS_Details_ID] [int] IDENTITY(1,1) NOT NULL,
	[Reference_ID] [int] NOT NULL,
	[Type] [varchar](10) NOT NULL,
	[Location_Code] [varchar](12) NULL,
	[Type_Code] [bigint] NULL,
	[AAMS_Status] [int] NOT NULL DEFAULT ((0))
) ON [PRIMARY]

END

GO

