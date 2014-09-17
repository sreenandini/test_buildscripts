USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AAMS_Reply_Error_Codes]') AND type in (N'U'))

BEGIN

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[AAMS_Reply_Error_Codes](
	[AREC_Code] [varchar](4) NOT NULL,
	[AREC_Description] [varchar](250) NULL
) ON [PRIMARY]

ALTER TABLE [dbo].[AAMS_Reply_Error_Codes] ADD  CONSTRAINT [PK_AAMS_Reply_Error_Codes] PRIMARY KEY CLUSTERED 
(
	[AREC_Code] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
END

GO

