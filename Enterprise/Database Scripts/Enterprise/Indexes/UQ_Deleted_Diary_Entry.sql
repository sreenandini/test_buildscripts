USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Deleted_Diary_Entry]') AND name = N'Deleted_Diary_Entry_Bar_Position_ID')
DROP INDEX [Deleted_Diary_Entry_Bar_Position_ID] ON [dbo].[Deleted_Diary_Entry] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Deleted_Diary_Entry_Bar_Position_ID] ON [dbo].[Deleted_Diary_Entry] 
(
	[Bar_Position_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Deleted_Diary_Entry]') AND name = N'Deleted_Diary_Entry_Batch_ID')
DROP INDEX [Deleted_Diary_Entry_Batch_ID] ON [dbo].[Deleted_Diary_Entry] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Deleted_Diary_Entry_Batch_ID] ON [dbo].[Deleted_Diary_Entry] 
(
	[Batch_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Deleted_Diary_Entry]') AND name = N'Deleted_Diary_Entry_Delete_Code')
DROP INDEX [Deleted_Diary_Entry_Delete_Code] ON [dbo].[Deleted_Diary_Entry] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Deleted_Diary_Entry_Delete_Code] ON [dbo].[Deleted_Diary_Entry] 
(
	[Delete_Code] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Deleted_Diary_Entry]') AND name = N'Deleted_Diary_Entry_Diary_Entry_ID')
DROP INDEX [Deleted_Diary_Entry_Diary_Entry_ID] ON [dbo].[Deleted_Diary_Entry] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Deleted_Diary_Entry_Diary_Entry_ID] ON [dbo].[Deleted_Diary_Entry] 
(
	[Diary_Entry_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Deleted_Diary_Entry]') AND name = N'Deleted_Diary_Entry_ID')
DROP INDEX [Deleted_Diary_Entry_ID] ON [dbo].[Deleted_Diary_Entry] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Deleted_Diary_Entry_ID] ON [dbo].[Deleted_Diary_Entry] 
(
	[Deleted_Diary_Entry_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Deleted_Diary_Entry]') AND name = N'Deleted_Diary_Entry_Installation_ID')
DROP INDEX [Deleted_Diary_Entry_Installation_ID] ON [dbo].[Deleted_Diary_Entry] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Deleted_Diary_Entry_Installation_ID] ON [dbo].[Deleted_Diary_Entry] 
(
	[Installation_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Deleted_Diary_Entry]') AND name = N'Deleted_Diary_Entry_Staff_ID')
DROP INDEX [Deleted_Diary_Entry_Staff_ID] ON [dbo].[Deleted_Diary_Entry] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Deleted_Diary_Entry_Staff_ID] ON [dbo].[Deleted_Diary_Entry] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

