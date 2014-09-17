USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Special_Licence]') AND name = N'Special_Licence_Bar_Position_ID')
DROP INDEX [Special_Licence_Bar_Position_ID] ON [dbo].[Special_Licence] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Special_Licence_Bar_Position_ID] ON [dbo].[Special_Licence] 
(
	[Bar_Position_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Special_Licence]') AND name = N'Special_Licence_Duty_ID')
DROP INDEX [Special_Licence_Duty_ID] ON [dbo].[Special_Licence] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Special_Licence_Duty_ID] ON [dbo].[Special_Licence] 
(
	[Duty_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Special_Licence]') AND name = N'Special_Licence_ID')
DROP INDEX [Special_Licence_ID] ON [dbo].[Special_Licence] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Special_Licence_ID] ON [dbo].[Special_Licence] 
(
	[Special_Licence_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Special_Licence]') AND name = N'Special_Licence_Machine_ID')
DROP INDEX [Special_Licence_Machine_ID] ON [dbo].[Special_Licence] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Special_Licence_Machine_ID] ON [dbo].[Special_Licence] 
(
	[Machine_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Special_Licence]') AND name = N'Special_Licence_Special_Licence_Sold_Staff_ID')
DROP INDEX [Special_Licence_Special_Licence_Sold_Staff_ID] ON [dbo].[Special_Licence] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Special_Licence_Special_Licence_Sold_Staff_ID] ON [dbo].[Special_Licence] 
(
	[Special_Licence_Sold_Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Special_Licence]') AND name = N'Special_Licence_Staff_ID')
DROP INDEX [Special_Licence_Staff_ID] ON [dbo].[Special_Licence] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Special_Licence_Staff_ID] ON [dbo].[Special_Licence] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

