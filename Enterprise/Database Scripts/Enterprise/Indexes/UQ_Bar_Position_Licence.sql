USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position_Licence]') AND name = N'Bar_Position_Licence_Bar_Position_ID')
DROP INDEX [Bar_Position_Licence_Bar_Position_ID] ON [dbo].[Bar_Position_Licence] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Bar_Position_Licence_Bar_Position_ID] ON [dbo].[Bar_Position_Licence] 
(
	[Bar_Position_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position_Licence]') AND name = N'Bar_Position_Licence_ID')
DROP INDEX [Bar_Position_Licence_ID] ON [dbo].[Bar_Position_Licence] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Bar_Position_Licence_ID] ON [dbo].[Bar_Position_Licence] 
(
	[Bar_Position_Licence_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position_Licence]') AND name = N'Bar_Position_Licence_Licence_ID')
DROP INDEX [Bar_Position_Licence_Licence_ID] ON [dbo].[Bar_Position_Licence] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Bar_Position_Licence_Licence_ID] ON [dbo].[Bar_Position_Licence] 
(
	[Licence_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

