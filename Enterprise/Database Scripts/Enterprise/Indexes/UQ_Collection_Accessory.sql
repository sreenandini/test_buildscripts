USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Accessory]') AND name = N'Collection_Accessory_Accessory_Installation_ID')
DROP INDEX [Collection_Accessory_Accessory_Installation_ID] ON [dbo].[Collection_Accessory] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Accessory_Accessory_Installation_ID] ON [dbo].[Collection_Accessory] 
(
	[Accessory_Installation_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Accessory]') AND name = N'Collection_Accessory_Collection_ID')
DROP INDEX [Collection_Accessory_Collection_ID] ON [dbo].[Collection_Accessory] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Accessory_Collection_ID] ON [dbo].[Collection_Accessory] 
(
	[Collection_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Accessory]') AND name = N'Collection_Accessory_ID')
DROP INDEX [Collection_Accessory_ID] ON [dbo].[Collection_Accessory] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Collection_Accessory_ID] ON [dbo].[Collection_Accessory] 
(
	[Collection_Accessory_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

