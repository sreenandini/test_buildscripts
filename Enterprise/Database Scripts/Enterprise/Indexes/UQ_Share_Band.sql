USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Share_Band]') AND name = N'Share_Band_ID')
DROP INDEX [Share_Band_ID] ON [dbo].[Share_Band] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Share_Band_ID] ON [dbo].[Share_Band] 
(
	[Share_Band_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Share_Band]') AND name = N'Share_Band_Share_Schedule_ID')
DROP INDEX [Share_Band_Share_Schedule_ID] ON [dbo].[Share_Band] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Share_Band_Share_Schedule_ID] ON [dbo].[Share_Band] 
(
	[Share_Schedule_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

