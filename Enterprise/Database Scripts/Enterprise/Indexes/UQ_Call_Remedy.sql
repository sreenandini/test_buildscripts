USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Call_Remedy]') AND name = N'Call_Remedy_ID')
DROP INDEX [Call_Remedy_ID] ON [dbo].[Call_Remedy] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Call_Remedy_ID] ON [dbo].[Call_Remedy] 
(
	[Call_Remedy_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

