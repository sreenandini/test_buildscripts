USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Calendar_Period]') AND name = N'Calendar_Period_Calendar_ID')
DROP INDEX [Calendar_Period_Calendar_ID] ON [dbo].[Calendar_Period] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Calendar_Period_Calendar_ID] ON [dbo].[Calendar_Period] 
(
	[Calendar_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Calendar_Period]') AND name = N'Calendar_Period_ID')
DROP INDEX [Calendar_Period_ID] ON [dbo].[Calendar_Period] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Calendar_Period_ID] ON [dbo].[Calendar_Period] 
(
	[Calendar_Period_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

