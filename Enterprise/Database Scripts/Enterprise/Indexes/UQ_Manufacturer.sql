USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Manufacturer]') AND name = N'Manufacturer_ID')
DROP INDEX [Manufacturer_ID] ON [dbo].[Manufacturer] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Manufacturer_ID] ON [dbo].[Manufacturer] 
(
	[Manufacturer_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Manufacturer]') AND name = N'Manufacturer_Manufacturer_Code')
DROP INDEX [Manufacturer_Manufacturer_Code] ON [dbo].[Manufacturer] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Manufacturer_Manufacturer_Code] ON [dbo].[Manufacturer] 
(
	[Manufacturer_Code] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

