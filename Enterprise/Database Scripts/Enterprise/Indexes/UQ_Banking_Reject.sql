USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Banking_Reject]') AND name = N'Banking_Reject_Banking_Reject_Batch_ID')
DROP INDEX [Banking_Reject_Banking_Reject_Batch_ID] ON [dbo].[Banking_Reject] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Banking_Reject_Banking_Reject_Batch_ID] ON [dbo].[Banking_Reject] 
(
	[Banking_Reject_Batch_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Banking_Reject]') AND name = N'Banking_Reject_ID')
DROP INDEX [Banking_Reject_ID] ON [dbo].[Banking_Reject] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Banking_Reject_ID] ON [dbo].[Banking_Reject] 
(
	[Banking_Reject_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

