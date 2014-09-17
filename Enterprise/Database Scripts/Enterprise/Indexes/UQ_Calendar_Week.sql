USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Calendar_Week]') AND name = N'Calendar_Week_Calendar_ID')
DROP INDEX [Calendar_Week_Calendar_ID] ON [dbo].[Calendar_Week] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Calendar_Week_Calendar_ID] ON [dbo].[Calendar_Week] 
(
	[Calendar_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Calendar_Week]') AND name = N'Calendar_Week_ID')
DROP INDEX [Calendar_Week_ID] ON [dbo].[Calendar_Week] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Calendar_Week_ID] ON [dbo].[Calendar_Week] 
(
	[Calendar_Week_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

