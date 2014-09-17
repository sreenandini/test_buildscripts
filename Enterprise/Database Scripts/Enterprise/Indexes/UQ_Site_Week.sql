USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Site_Week]') AND name = N'Site_Week_Calendar_Period_ID')
DROP INDEX [Site_Week_Calendar_Period_ID] ON [dbo].[Site_Week] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Site_Week_Calendar_Period_ID] ON [dbo].[Site_Week] 
(
	[Calendar_Period_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Site_Week]') AND name = N'Site_Week_Calendar_Week_ID')
DROP INDEX [Site_Week_Calendar_Week_ID] ON [dbo].[Site_Week] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Site_Week_Calendar_Week_ID] ON [dbo].[Site_Week] 
(
	[Calendar_Week_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Site_Week]') AND name = N'Site_Week_ID')
DROP INDEX [Site_Week_ID] ON [dbo].[Site_Week] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Site_Week_ID] ON [dbo].[Site_Week] 
(
	[Site_Week_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Site_Week]') AND name = N'Site_Week_Site_ID')
DROP INDEX [Site_Week_Site_ID] ON [dbo].[Site_Week] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Site_Week_Site_ID] ON [dbo].[Site_Week] 
(
	[Site_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

