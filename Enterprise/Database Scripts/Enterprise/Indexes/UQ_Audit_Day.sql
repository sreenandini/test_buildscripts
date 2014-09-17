USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Audit_Day]') AND name = N'Audit_Day_ID')
DROP INDEX [Audit_Day_ID] ON [dbo].[Audit_Day] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Audit_Day_ID] ON [dbo].[Audit_Day] 
(
	[Audit_Day_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

