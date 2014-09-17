USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Depreciation_Policy]') AND name = N'Depreciation_Policy_ID')
DROP INDEX [Depreciation_Policy_ID] ON [dbo].[Depreciation_Policy] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Depreciation_Policy_ID] ON [dbo].[Depreciation_Policy] 
(
	[Depreciation_Policy_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

