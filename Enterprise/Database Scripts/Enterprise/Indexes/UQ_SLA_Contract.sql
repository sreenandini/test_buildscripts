USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SLA_Contract]') AND name = N'SLA_Contract_ID')
DROP INDEX [SLA_Contract_ID] ON [dbo].[SLA_Contract] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [SLA_Contract_ID] ON [dbo].[SLA_Contract] 
(
	[SLA_Contract_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SLA_Contract]') AND name = N'SLA_Contract_SLA_ID')
DROP INDEX [SLA_Contract_SLA_ID] ON [dbo].[SLA_Contract] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [SLA_Contract_SLA_ID] ON [dbo].[SLA_Contract] 
(
	[SLA_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

