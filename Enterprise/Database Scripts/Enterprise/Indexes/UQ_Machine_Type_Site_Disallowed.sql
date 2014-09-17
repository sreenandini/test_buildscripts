USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Type_Site_Disallowed]') AND name = N'Machine_Type_Site_Disallowed_ID')
DROP INDEX [Machine_Type_Site_Disallowed_ID] ON [dbo].[Machine_Type_Site_Disallowed] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Machine_Type_Site_Disallowed_ID] ON [dbo].[Machine_Type_Site_Disallowed] 
(
	[Machine_Type_Site_Disallowed_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Type_Site_Disallowed]') AND name = N'Machine_Type_Site_Disallowed_Machine_Type_ID')
DROP INDEX [Machine_Type_Site_Disallowed_Machine_Type_ID] ON [dbo].[Machine_Type_Site_Disallowed] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Type_Site_Disallowed_Machine_Type_ID] ON [dbo].[Machine_Type_Site_Disallowed] 
(
	[Machine_Type_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Type_Site_Disallowed]') AND name = N'Machine_Type_Site_Disallowed_Site_ID')
DROP INDEX [Machine_Type_Site_Disallowed_Site_ID] ON [dbo].[Machine_Type_Site_Disallowed] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Type_Site_Disallowed_Site_ID] ON [dbo].[Machine_Type_Site_Disallowed] 
(
	[Site_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

