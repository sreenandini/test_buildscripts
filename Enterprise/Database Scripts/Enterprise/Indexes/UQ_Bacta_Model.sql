USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bacta_Model]') AND name = N'AMEDIS_Code')
DROP INDEX [AMEDIS_Code] ON [dbo].[Bacta_Model] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [AMEDIS_Code] ON [dbo].[Bacta_Model] 
(
	[AMEDIS_Code] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bacta_Model]') AND name = N'Bacta_Model_Type_Code')
DROP INDEX [Bacta_Model_Type_Code] ON [dbo].[Bacta_Model] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Bacta_Model_Type_Code] ON [dbo].[Bacta_Model] 
(
	[Type_Code] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

