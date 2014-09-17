USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CD_Track]') AND name = N'CD_Track_CD_ID')
DROP INDEX [CD_Track_CD_ID] ON [dbo].[CD_Track] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [CD_Track_CD_ID] ON [dbo].[CD_Track] 
(
	[CD_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CD_Track]') AND name = N'CD_Track_ID')
DROP INDEX [CD_Track_ID] ON [dbo].[CD_Track] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [CD_Track_ID] ON [dbo].[CD_Track] 
(
	[CD_Track_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

