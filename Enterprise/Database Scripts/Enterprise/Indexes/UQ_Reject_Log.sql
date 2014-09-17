USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Reject_Log]') AND name = N'Reject_Log_ID')
DROP INDEX [Reject_Log_ID] ON [dbo].[Reject_Log] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Reject_Log_ID] ON [dbo].[Reject_Log] 
(
	[Reject_Log_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

