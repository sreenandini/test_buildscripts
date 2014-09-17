USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OpCosts]') AND name = N'OpCosts_Depot_ID')
DROP INDEX [OpCosts_Depot_ID] ON [dbo].[OpCosts] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [OpCosts_Depot_ID] ON [dbo].[OpCosts] 
(
	[Depot_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OpCosts]') AND name = N'OpCosts_ID')
DROP INDEX [OpCosts_ID] ON [dbo].[OpCosts] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [OpCosts_ID] ON [dbo].[OpCosts] 
(
	[OpCosts_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OpCosts]') AND name = N'OpCosts_Machine_Type_ID')
DROP INDEX [OpCosts_Machine_Type_ID] ON [dbo].[OpCosts] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [OpCosts_Machine_Type_ID] ON [dbo].[OpCosts] 
(
	[Machine_Type_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OpCosts]') AND name = N'OpCosts_Sub_Company_ID')
DROP INDEX [OpCosts_Sub_Company_ID] ON [dbo].[OpCosts] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [OpCosts_Sub_Company_ID] ON [dbo].[OpCosts] 
(
	[Sub_Company_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

