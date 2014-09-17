USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Rent_Schedule]') AND name = N'Rent_Schedule_ID')
DROP INDEX [Rent_Schedule_ID] ON [dbo].[Rent_Schedule] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Rent_Schedule_ID] ON [dbo].[Rent_Schedule] 
(
	[Rent_Schedule_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

