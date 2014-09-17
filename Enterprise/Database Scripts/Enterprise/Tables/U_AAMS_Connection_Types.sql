USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AAMS_Connection_Types]') AND type in (N'U'))

BEGIN

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[AAMS_Connection_Types](
	[ACT_Code] [int] NOT NULL,
	[ACT_Description] [varchar](50) NULL
) ON [PRIMARY]

ALTER TABLE [dbo].[AAMS_Connection_Types] ADD  CONSTRAINT [PK_AAMS_Connection_Types] PRIMARY KEY CLUSTERED 
(
	[ACT_Code] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
END

GO

