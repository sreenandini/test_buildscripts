USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Site_Period_Budget]') AND name = N'Site_Period_Budget_Calendar_Period_ID')
DROP INDEX [Site_Period_Budget_Calendar_Period_ID] ON [dbo].[Site_Period_Budget] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Site_Period_Budget_Calendar_Period_ID] ON [dbo].[Site_Period_Budget] 
(
	[Calendar_Period_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Site_Period_Budget]') AND name = N'Site_Period_Budget_ID')
DROP INDEX [Site_Period_Budget_ID] ON [dbo].[Site_Period_Budget] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Site_Period_Budget_ID] ON [dbo].[Site_Period_Budget] 
(
	[Site_Period_Budget_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Site_Period_Budget]') AND name = N'Site_Period_Budget_Site_ID')
DROP INDEX [Site_Period_Budget_Site_ID] ON [dbo].[Site_Period_Budget] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Site_Period_Budget_Site_ID] ON [dbo].[Site_Period_Budget] 
(
	[Site_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

