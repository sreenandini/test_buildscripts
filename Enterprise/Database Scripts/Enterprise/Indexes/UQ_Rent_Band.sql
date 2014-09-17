USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Rent_Band]') AND name = N'Rent_Band_ID')
DROP INDEX [Rent_Band_ID] ON [dbo].[Rent_Band] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Rent_Band_ID] ON [dbo].[Rent_Band] 
(
	[Rent_Band_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Rent_Band]') AND name = N'Rent_Band_Rent_Schedule_ID')
DROP INDEX [Rent_Band_Rent_Schedule_ID] ON [dbo].[Rent_Band] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Rent_Band_Rent_Schedule_ID] ON [dbo].[Rent_Band] 
(
	[Rent_Schedule_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

