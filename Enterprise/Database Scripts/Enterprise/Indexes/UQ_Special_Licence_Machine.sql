USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Special_Licence_Machine]') AND name = N'Special_Licence_Machine_ID')
DROP INDEX [Special_Licence_Machine_ID] ON [dbo].[Special_Licence_Machine] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Special_Licence_Machine_ID] ON [dbo].[Special_Licence_Machine] 
(
	[Special_Licence_Machine_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Special_Licence_Machine]') AND name = N'Special_Licence_Machine_Licence_ID')
DROP INDEX [Special_Licence_Machine_Licence_ID] ON [dbo].[Special_Licence_Machine] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Special_Licence_Machine_Licence_ID] ON [dbo].[Special_Licence_Machine] 
(
	[Licence_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Special_Licence_Machine]') AND name = N'Special_Licence_Machine_Machine_ID')
DROP INDEX [Special_Licence_Machine_Machine_ID] ON [dbo].[Special_Licence_Machine] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Special_Licence_Machine_Machine_ID] ON [dbo].[Special_Licence_Machine] 
(
	[Machine_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

