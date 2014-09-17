USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Depreciation_Policy_Details]') AND name = N'Depreciation_Policy_Details_Depreciation_Policy_ID')
DROP INDEX [Depreciation_Policy_Details_Depreciation_Policy_ID] ON [dbo].[Depreciation_Policy_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Depreciation_Policy_Details_Depreciation_Policy_ID] ON [dbo].[Depreciation_Policy_Details] 
(
	[Depreciation_Policy_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Depreciation_Policy_Details]') AND name = N'Depreciation_Policy_Details_ID')
DROP INDEX [Depreciation_Policy_Details_ID] ON [dbo].[Depreciation_Policy_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Depreciation_Policy_Details_ID] ON [dbo].[Depreciation_Policy_Details] 
(
	[Depreciation_Policy_Details_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

