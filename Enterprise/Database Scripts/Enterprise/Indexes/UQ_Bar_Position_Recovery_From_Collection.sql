USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position_Recovery_From_Collection]') AND name = N'Bar_Position_Recovery_From_Collection_Bar_Position_Recovery_ID')
DROP INDEX [Bar_Position_Recovery_From_Collection_Bar_Position_Recovery_ID] ON [dbo].[Bar_Position_Recovery_From_Collection] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Bar_Position_Recovery_From_Collection_Bar_Position_Recovery_ID] ON [dbo].[Bar_Position_Recovery_From_Collection] 
(
	[Bar_Position_Recovery_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position_Recovery_From_Collection]') AND name = N'Bar_Position_Recovery_From_Collection_Collection_ID')
DROP INDEX [Bar_Position_Recovery_From_Collection_Collection_ID] ON [dbo].[Bar_Position_Recovery_From_Collection] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Bar_Position_Recovery_From_Collection_Collection_ID] ON [dbo].[Bar_Position_Recovery_From_Collection] 
(
	[Collection_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position_Recovery_From_Collection]') AND name = N'Bar_Position_Recovery_From_Collection_ID')
DROP INDEX [Bar_Position_Recovery_From_Collection_ID] ON [dbo].[Bar_Position_Recovery_From_Collection] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Bar_Position_Recovery_From_Collection_ID] ON [dbo].[Bar_Position_Recovery_From_Collection] 
(
	[Bar_Position_Recovery_From_Collection_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

