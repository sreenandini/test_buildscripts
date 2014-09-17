USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Class]') AND name = N'Machine_Class_Depreciation_Policy_ID')
DROP INDEX [Machine_Class_Depreciation_Policy_ID] ON [dbo].[Machine_Class] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Class_Depreciation_Policy_ID] ON [dbo].[Machine_Class] 
(
	[Depreciation_Policy_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Class]') AND name = N'Machine_Class_ID')
DROP INDEX [Machine_Class_ID] ON [dbo].[Machine_Class] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Machine_Class_ID] ON [dbo].[Machine_Class] 
(
	[Machine_Class_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Class]') AND name = N'Machine_Class_Machine_Class_Category_ID')
DROP INDEX [Machine_Class_Machine_Class_Category_ID] ON [dbo].[Machine_Class] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Class_Machine_Class_Category_ID] ON [dbo].[Machine_Class] 
(
	[Machine_Class_Category_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Class]') AND name = N'Machine_Class_Machine_Type_ID')
DROP INDEX [Machine_Class_Machine_Type_ID] ON [dbo].[Machine_Class] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Class_Machine_Type_ID] ON [dbo].[Machine_Class] 
(
	[Machine_Type_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Class]') AND name = N'Machine_Class_Manufacturer_ID')
DROP INDEX [Machine_Class_Manufacturer_ID] ON [dbo].[Machine_Class] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Class_Manufacturer_ID] ON [dbo].[Machine_Class] 
(
	[Manufacturer_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

