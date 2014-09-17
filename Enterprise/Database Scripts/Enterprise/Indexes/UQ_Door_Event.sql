USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Door_Event]') AND name = N'Door_Event_Collection_ID')
DROP INDEX [Door_Event_Collection_ID] ON [dbo].[Door_Event] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Door_Event_Collection_ID] ON [dbo].[Door_Event] 
(
	[Collection_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Door_Event]') AND name = N'Door_Event_Installation_ID')
DROP INDEX [Door_Event_Installation_ID] ON [dbo].[Door_Event] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Door_Event_Installation_ID] ON [dbo].[Door_Event] 
(
	[Installation_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Door_Event]') AND name = N'IDX_Door_Event_Cols')
DROP INDEX [IDX_Door_Event_Cols] ON [dbo].[Door_Event] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [IDX_Door_Event_Cols] ON [dbo].[Door_Event] 
(
	[Installation_ID] ASC,
	[Door_Event_Date] ASC,
	[Door_Event_Time] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

