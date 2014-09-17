USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CD_Request]') AND name = N'CD_Request_CD_ID')
DROP INDEX [CD_Request_CD_ID] ON [dbo].[CD_Request] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [CD_Request_CD_ID] ON [dbo].[CD_Request] 
(
	[CD_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CD_Request]') AND name = N'CD_Request_CD_Program_ID')
DROP INDEX [CD_Request_CD_Program_ID] ON [dbo].[CD_Request] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [CD_Request_CD_Program_ID] ON [dbo].[CD_Request] 
(
	[CD_Program_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CD_Request]') AND name = N'CD_Request_ID')
DROP INDEX [CD_Request_ID] ON [dbo].[CD_Request] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [CD_Request_ID] ON [dbo].[CD_Request] 
(
	[CD_Request_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

