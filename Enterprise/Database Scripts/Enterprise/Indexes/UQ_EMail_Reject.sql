USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[EMail_Reject]') AND name = N'EMail_Reject_ID')
DROP INDEX [EMail_Reject_ID] ON [dbo].[EMail_Reject] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [EMail_Reject_ID] ON [dbo].[EMail_Reject] 
(
	[EMail_Reject_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

