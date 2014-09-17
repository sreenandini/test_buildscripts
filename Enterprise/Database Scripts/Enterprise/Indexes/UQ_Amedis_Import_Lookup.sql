USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Amedis_Import_Lookup]') AND name = N'Amedis_Import_Lookup_ID')
DROP INDEX [Amedis_Import_Lookup_ID] ON [dbo].[Amedis_Import_Lookup] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Amedis_Import_Lookup_ID] ON [dbo].[Amedis_Import_Lookup] 
(
	[Amedis_Import_Lookup_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Amedis_Import_Lookup]') AND name = N'Amedis_Import_Lookup_Machine_Type_ID')
DROP INDEX [Amedis_Import_Lookup_Machine_Type_ID] ON [dbo].[Amedis_Import_Lookup] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Amedis_Import_Lookup_Machine_Type_ID] ON [dbo].[Amedis_Import_Lookup] 
(
	[Machine_Type_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Amedis_Import_Lookup]') AND name = N'Amedis_Import_Lookup_Sub_Company_ID')
DROP INDEX [Amedis_Import_Lookup_Sub_Company_ID] ON [dbo].[Amedis_Import_Lookup] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Amedis_Import_Lookup_Sub_Company_ID] ON [dbo].[Amedis_Import_Lookup] 
(
	[Sub_Company_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

