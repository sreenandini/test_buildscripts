USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Batch]') AND name = N'Batch_Batch_Audit_Day_ID')
DROP INDEX [Batch_Batch_Audit_Day_ID] ON [dbo].[Batch] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Batch_Batch_Audit_Day_ID] ON [dbo].[Batch] 
(
	[Batch_Audit_Day_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Batch]') AND name = N'Batch_Batch_Audit_Period_ID')
DROP INDEX [Batch_Batch_Audit_Period_ID] ON [dbo].[Batch] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Batch_Batch_Audit_Period_ID] ON [dbo].[Batch] 
(
	[Batch_Audit_Period_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Batch]') AND name = N'Batch_Batch_Audit_Week_ID')
DROP INDEX [Batch_Batch_Audit_Week_ID] ON [dbo].[Batch] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Batch_Batch_Audit_Week_ID] ON [dbo].[Batch] 
(
	[Batch_Audit_Week_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Batch]') AND name = N'Batch_Batch_Audit_Year_ID')
DROP INDEX [Batch_Batch_Audit_Year_ID] ON [dbo].[Batch] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Batch_Batch_Audit_Year_ID] ON [dbo].[Batch] 
(
	[Batch_Audit_Year_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Batch]') AND name = N'Batch_Batch_Collector_ID')
DROP INDEX [Batch_Batch_Collector_ID] ON [dbo].[Batch] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Batch_Batch_Collector_ID] ON [dbo].[Batch] 
(
	[Batch_Collector_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Batch]') AND name = N'Batch_ID')
DROP INDEX [Batch_ID] ON [dbo].[Batch] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Batch_ID] ON [dbo].[Batch] 
(
	[Batch_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Batch]') AND name = N'Batch_Schedule_ID')
DROP INDEX [Batch_Schedule_ID] ON [dbo].[Batch] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Batch_Schedule_ID] ON [dbo].[Batch] 
(
	[Schedule_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Batch]') AND name = N'Batch_Staff_ID')
DROP INDEX [Batch_Staff_ID] ON [dbo].[Batch] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Batch_Staff_ID] ON [dbo].[Batch] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

