USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Power_Event]') AND name = N'Power_Event_Collection_ID')
DROP INDEX [Power_Event_Collection_ID] ON [dbo].[Power_Event] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Power_Event_Collection_ID] ON [dbo].[Power_Event] 
(
	[Collection_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Power_Event]') AND name = N'Power_Event_Installation_ID')
DROP INDEX [Power_Event_Installation_ID] ON [dbo].[Power_Event] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Power_Event_Installation_ID] ON [dbo].[Power_Event] 
(
	[Installation_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

