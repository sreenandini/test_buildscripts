USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Variance]') AND name = N'Collection_Variance_From_Collection_ID')
DROP INDEX [Collection_Variance_From_Collection_ID] ON [dbo].[Collection_Variance] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Variance_From_Collection_ID] ON [dbo].[Collection_Variance] 
(
	[From_Collection_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Variance]') AND name = N'Collection_Variance_ID')
DROP INDEX [Collection_Variance_ID] ON [dbo].[Collection_Variance] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Collection_Variance_ID] ON [dbo].[Collection_Variance] 
(
	[Collection_Variance_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Variance]') AND name = N'Collection_Variance_Recovered_Collection_ID')
DROP INDEX [Collection_Variance_Recovered_Collection_ID] ON [dbo].[Collection_Variance] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Variance_Recovered_Collection_ID] ON [dbo].[Collection_Variance] 
(
	[Recovered_Collection_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Variance]') AND name = N'Collection_Variance_Recovered_Diary_Entry_ID')
DROP INDEX [Collection_Variance_Recovered_Diary_Entry_ID] ON [dbo].[Collection_Variance] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Variance_Recovered_Diary_Entry_ID] ON [dbo].[Collection_Variance] 
(
	[Recovered_Diary_Entry_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

