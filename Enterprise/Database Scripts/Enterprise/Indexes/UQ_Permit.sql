USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Permit]') AND name = N'Permit_ID')
DROP INDEX [Permit_ID] ON [dbo].[Permit] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Permit_ID] ON [dbo].[Permit] 
(
	[Permit_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Permit]') AND name = N'Permit_Permit_Authority_ID')
DROP INDEX [Permit_Permit_Authority_ID] ON [dbo].[Permit] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Permit_Permit_Authority_ID] ON [dbo].[Permit] 
(
	[Permit_Authority_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Permit]') AND name = N'Permit_Permit_Sold_Staff_Id')
DROP INDEX [Permit_Permit_Sold_Staff_Id] ON [dbo].[Permit] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Permit_Permit_Sold_Staff_Id] ON [dbo].[Permit] 
(
	[Permit_Sold_Staff_Id] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Permit]') AND name = N'Permit_Site_ID')
DROP INDEX [Permit_Site_ID] ON [dbo].[Permit] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Permit_Site_ID] ON [dbo].[Permit] 
(
	[Site_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Permit]') AND name = N'Permit_Staff_ID')
DROP INDEX [Permit_Staff_ID] ON [dbo].[Permit] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Permit_Staff_ID] ON [dbo].[Permit] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

