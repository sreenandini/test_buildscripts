USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Operator_Calendar]') AND name = N'Operator_Calendar_Calendar_ID')
DROP INDEX [Operator_Calendar_Calendar_ID] ON [dbo].[Operator_Calendar] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Operator_Calendar_Calendar_ID] ON [dbo].[Operator_Calendar] 
(
	[Calendar_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Operator_Calendar]') AND name = N'Operator_Calendar_ID')
DROP INDEX [Operator_Calendar_ID] ON [dbo].[Operator_Calendar] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Operator_Calendar_ID] ON [dbo].[Operator_Calendar] 
(
	[Operator_Calendar_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Operator_Calendar]') AND name = N'Operator_Calendar_Operator_ID')
DROP INDEX [Operator_Calendar_Operator_ID] ON [dbo].[Operator_Calendar] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Operator_Calendar_Operator_ID] ON [dbo].[Operator_Calendar] 
(
	[Operator_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

