USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position_Recovery]') AND name = N'Bar_Position_Recovery_Bar_Position_ID')
DROP INDEX [Bar_Position_Recovery_Bar_Position_ID] ON [dbo].[Bar_Position_Recovery] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Bar_Position_Recovery_Bar_Position_ID] ON [dbo].[Bar_Position_Recovery] 
(
	[Bar_Position_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position_Recovery]') AND name = N'Bar_Position_Recovery_Collection_ID')
DROP INDEX [Bar_Position_Recovery_Collection_ID] ON [dbo].[Bar_Position_Recovery] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Bar_Position_Recovery_Collection_ID] ON [dbo].[Bar_Position_Recovery] 
(
	[Collection_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position_Recovery]') AND name = N'Bar_Position_Recovery_ID')
DROP INDEX [Bar_Position_Recovery_ID] ON [dbo].[Bar_Position_Recovery] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Bar_Position_Recovery_ID] ON [dbo].[Bar_Position_Recovery] 
(
	[Bar_Position_Recovery_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position_Recovery]') AND name = N'Bar_Position_Recovery_Staff_ID')
DROP INDEX [Bar_Position_Recovery_Staff_ID] ON [dbo].[Bar_Position_Recovery] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Bar_Position_Recovery_Staff_ID] ON [dbo].[Bar_Position_Recovery] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

