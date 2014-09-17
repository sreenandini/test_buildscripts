USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AAMS_Location_Types]') AND type in (N'U'))

BEGIN

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[AAMS_Location_Types](
	[ALT_Code] [int] NOT NULL,
	[ALT_Description] [varchar](50) NULL
) ON [PRIMARY]

ALTER TABLE [dbo].[AAMS_Location_Types] ADD  CONSTRAINT [PK_AAMS_Location_Types] PRIMARY KEY CLUSTERED 
(
	[ALT_Code] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
END

GO

