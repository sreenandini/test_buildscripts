USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Banking_Recovery]') AND name = N'Banking_Recovery_Bar_Position_ID')
DROP INDEX [Banking_Recovery_Bar_Position_ID] ON [dbo].[Banking_Recovery] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Banking_Recovery_Bar_Position_ID] ON [dbo].[Banking_Recovery] 
(
	[Bar_Position_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Banking_Recovery]') AND name = N'Banking_Recovery_Batch_ID')
DROP INDEX [Banking_Recovery_Batch_ID] ON [dbo].[Banking_Recovery] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Banking_Recovery_Batch_ID] ON [dbo].[Banking_Recovery] 
(
	[Batch_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Banking_Recovery]') AND name = N'Banking_Recovery_Collector_ID')
DROP INDEX [Banking_Recovery_Collector_ID] ON [dbo].[Banking_Recovery] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Banking_Recovery_Collector_ID] ON [dbo].[Banking_Recovery] 
(
	[Collector_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Banking_Recovery]') AND name = N'Banking_Recovery_ID')
DROP INDEX [Banking_Recovery_ID] ON [dbo].[Banking_Recovery] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Banking_Recovery_ID] ON [dbo].[Banking_Recovery] 
(
	[Banking_Recovery_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Banking_Recovery]') AND name = N'Banking_Recovery_Staff_ID')
DROP INDEX [Banking_Recovery_Staff_ID] ON [dbo].[Banking_Recovery] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Banking_Recovery_Staff_ID] ON [dbo].[Banking_Recovery] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

