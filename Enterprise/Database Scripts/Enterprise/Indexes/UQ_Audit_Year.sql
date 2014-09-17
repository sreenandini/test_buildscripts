USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Audit_Year]') AND name = N'Audit_Year_ID')
DROP INDEX [Audit_Year_ID] ON [dbo].[Audit_Year] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Audit_Year_ID] ON [dbo].[Audit_Year] 
(
	[Audit_Year_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

