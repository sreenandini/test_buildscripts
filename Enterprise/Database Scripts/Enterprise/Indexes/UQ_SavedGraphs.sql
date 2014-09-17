USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SavedGraphs]') AND name = N'Saved_Graph_ID')
DROP INDEX [Saved_Graph_ID] ON [dbo].[SavedGraphs] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Saved_Graph_ID] ON [dbo].[SavedGraphs] 
(
	[Saved_Graph_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

