USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Notes]') AND name = N'Service_Notes_Engineer_ID')
DROP INDEX [Service_Notes_Engineer_ID] ON [dbo].[Service_Notes] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Service_Notes_Engineer_ID] ON [dbo].[Service_Notes] 
(
	[Engineer_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Notes]') AND name = N'Service_Notes_ID')
DROP INDEX [Service_Notes_ID] ON [dbo].[Service_Notes] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Service_Notes_ID] ON [dbo].[Service_Notes] 
(
	[Service_Notes_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Notes]') AND name = N'Service_Notes_Service_Closed_ID')
DROP INDEX [Service_Notes_Service_Closed_ID] ON [dbo].[Service_Notes] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Service_Notes_Service_Closed_ID] ON [dbo].[Service_Notes] 
(
	[Service_Closed_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Notes]') AND name = N'Service_Notes_Service_ID')
DROP INDEX [Service_Notes_Service_ID] ON [dbo].[Service_Notes] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Service_Notes_Service_ID] ON [dbo].[Service_Notes] 
(
	[Service_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Notes]') AND name = N'Service_Notes_Staff_ID')
DROP INDEX [Service_Notes_Staff_ID] ON [dbo].[Service_Notes] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Service_Notes_Staff_ID] ON [dbo].[Service_Notes] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

