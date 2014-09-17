USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Route_Member]') AND name = N'Route_Member_Bar_Position_ID')
DROP INDEX [Route_Member_Bar_Position_ID] ON [dbo].[Route_Member] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Route_Member_Bar_Position_ID] ON [dbo].[Route_Member] 
(
	[Bar_Position_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Route_Member]') AND name = N'Route_Member_ID')
DROP INDEX [Route_Member_ID] ON [dbo].[Route_Member] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Route_Member_ID] ON [dbo].[Route_Member] 
(
	[Route_Member_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Route_Member]') AND name = N'Route_Member_Route_ID')
DROP INDEX [Route_Member_Route_ID] ON [dbo].[Route_Member] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Route_Member_Route_ID] ON [dbo].[Route_Member] 
(
	[Route_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

