USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Depot_Sub_Company_Code]') AND name = N'Depot_Sub_Company_Code_Depot_ID')
DROP INDEX [Depot_Sub_Company_Code_Depot_ID] ON [dbo].[Depot_Sub_Company_Code] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Depot_Sub_Company_Code_Depot_ID] ON [dbo].[Depot_Sub_Company_Code] 
(
	[Depot_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Depot_Sub_Company_Code]') AND name = N'Depot_Sub_Company_Code_ID')
DROP INDEX [Depot_Sub_Company_Code_ID] ON [dbo].[Depot_Sub_Company_Code] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Depot_Sub_Company_Code_ID] ON [dbo].[Depot_Sub_Company_Code] 
(
	[Depot_Sub_Company_Code_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Depot_Sub_Company_Code]') AND name = N'Depot_Sub_Company_Code_Sub_Company_ID')
DROP INDEX [Depot_Sub_Company_Code_Sub_Company_ID] ON [dbo].[Depot_Sub_Company_Code] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Depot_Sub_Company_Code_Sub_Company_ID] ON [dbo].[Depot_Sub_Company_Code] 
(
	[Sub_Company_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

