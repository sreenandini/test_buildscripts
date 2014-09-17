USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Class_Rent_Band]') AND name = N'Machine_Class_Rent_Band')
DROP INDEX [Machine_Class_Rent_Band] ON [dbo].[Machine_Class_Rent_Band] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Machine_Class_Rent_Band] ON [dbo].[Machine_Class_Rent_Band] 
(
	[Machine_Class_Rent_Band] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Class_Rent_Band]') AND name = N'Machine_Class_Rent_Band_Machine_Class_ID')
DROP INDEX [Machine_Class_Rent_Band_Machine_Class_ID] ON [dbo].[Machine_Class_Rent_Band] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Class_Rent_Band_Machine_Class_ID] ON [dbo].[Machine_Class_Rent_Band] 
(
	[Machine_Class_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Class_Rent_Band]') AND name = N'Machine_Class_Rent_Band_Rent_Band_ID')
DROP INDEX [Machine_Class_Rent_Band_Rent_Band_ID] ON [dbo].[Machine_Class_Rent_Band] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Class_Rent_Band_Rent_Band_ID] ON [dbo].[Machine_Class_Rent_Band] 
(
	[Rent_Band_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Class_Rent_Band]') AND name = N'Machine_Class_Rent_Band_Rent_Band_ID_Future')
DROP INDEX [Machine_Class_Rent_Band_Rent_Band_ID_Future] ON [dbo].[Machine_Class_Rent_Band] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Class_Rent_Band_Rent_Band_ID_Future] ON [dbo].[Machine_Class_Rent_Band] 
(
	[Rent_Band_ID_Future] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Class_Rent_Band]') AND name = N'Machine_Class_Rent_Band_Rent_Band_ID_Past')
DROP INDEX [Machine_Class_Rent_Band_Rent_Band_ID_Past] ON [dbo].[Machine_Class_Rent_Band] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Class_Rent_Band_Rent_Band_ID_Past] ON [dbo].[Machine_Class_Rent_Band] 
(
	[Rent_Band_ID_Past] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

