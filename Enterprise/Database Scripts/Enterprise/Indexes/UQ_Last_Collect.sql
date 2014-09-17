USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Last_Collect]') AND name = N'Last_Collect_ID')
DROP INDEX [Last_Collect_ID] ON [dbo].[Last_Collect] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Last_Collect_ID] ON [dbo].[Last_Collect] 
(
	[Last_Collect_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

