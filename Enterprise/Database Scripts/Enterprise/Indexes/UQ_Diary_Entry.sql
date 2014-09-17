USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Diary_Entry]') AND name = N'Diary_Entry_Bar_Position_ID')
DROP INDEX [Diary_Entry_Bar_Position_ID] ON [dbo].[Diary_Entry] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Diary_Entry_Bar_Position_ID] ON [dbo].[Diary_Entry] 
(
	[Bar_Position_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Diary_Entry]') AND name = N'Diary_Entry_Batch_ID')
DROP INDEX [Diary_Entry_Batch_ID] ON [dbo].[Diary_Entry] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Diary_Entry_Batch_ID] ON [dbo].[Diary_Entry] 
(
	[Batch_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Diary_Entry]') AND name = N'Diary_Entry_Company_ID')
DROP INDEX [Diary_Entry_Company_ID] ON [dbo].[Diary_Entry] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Diary_Entry_Company_ID] ON [dbo].[Diary_Entry] 
(
	[Company_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Diary_Entry]') AND name = N'Diary_Entry_ID')
DROP INDEX [Diary_Entry_ID] ON [dbo].[Diary_Entry] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Diary_Entry_ID] ON [dbo].[Diary_Entry] 
(
	[Diary_Entry_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Diary_Entry]') AND name = N'Diary_Entry_Schedule_ID')
DROP INDEX [Diary_Entry_Schedule_ID] ON [dbo].[Diary_Entry] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Diary_Entry_Schedule_ID] ON [dbo].[Diary_Entry] 
(
	[Schedule_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Diary_Entry]') AND name = N'Diary_Entry_Staff_ID')
DROP INDEX [Diary_Entry_Staff_ID] ON [dbo].[Diary_Entry] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Diary_Entry_Staff_ID] ON [dbo].[Diary_Entry] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

