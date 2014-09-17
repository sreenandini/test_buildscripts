USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Licence]') AND name = N'Licence_Duty_ID')
DROP INDEX [Licence_Duty_ID] ON [dbo].[Licence] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Licence_Duty_ID] ON [dbo].[Licence] 
(
	[Duty_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Licence]') AND name = N'Licence_ID')
DROP INDEX [Licence_ID] ON [dbo].[Licence] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Licence_ID] ON [dbo].[Licence] 
(
	[Licence_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Licence]') AND name = N'Licence_Licence_Last_Updated_Staff_ID')
DROP INDEX [Licence_Licence_Last_Updated_Staff_ID] ON [dbo].[Licence] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Licence_Licence_Last_Updated_Staff_ID] ON [dbo].[Licence] 
(
	[Licence_Last_Updated_Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Licence]') AND name = N'Licence_Licence_Number')
DROP INDEX [Licence_Licence_Number] ON [dbo].[Licence] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Licence_Licence_Number] ON [dbo].[Licence] 
(
	[Licence_Number] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Licence]') AND name = N'Licence_Licence_Sold_Staff_ID')
DROP INDEX [Licence_Licence_Sold_Staff_ID] ON [dbo].[Licence] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Licence_Licence_Sold_Staff_ID] ON [dbo].[Licence] 
(
	[Licence_Sold_Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Licence]') AND name = N'Licence_Machine_Type_Duty_ID')
DROP INDEX [Licence_Machine_Type_Duty_ID] ON [dbo].[Licence] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Licence_Machine_Type_Duty_ID] ON [dbo].[Licence] 
(
	[Machine_Type_Duty_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Licence]') AND name = N'Licence_Site_ID')
DROP INDEX [Licence_Site_ID] ON [dbo].[Licence] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Licence_Site_ID] ON [dbo].[Licence] 
(
	[Site_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Licence]') AND name = N'Licence_Staff_ID')
DROP INDEX [Licence_Staff_ID] ON [dbo].[Licence] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Licence_Staff_ID] ON [dbo].[Licence] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

