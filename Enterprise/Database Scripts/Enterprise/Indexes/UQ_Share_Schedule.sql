USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Share_Schedule]') AND name = N'Share_Schedule_ID')
DROP INDEX [Share_Schedule_ID] ON [dbo].[Share_Schedule] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Share_Schedule_ID] ON [dbo].[Share_Schedule] 
(
	[Share_Schedule_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

