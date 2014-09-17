USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Class_Component]') AND name = N'Machine_Class_Component_Component_ID')
DROP INDEX [Machine_Class_Component_Component_ID] ON [dbo].[Machine_Class_Component] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Class_Component_Component_ID] ON [dbo].[Machine_Class_Component] 
(
	[Component_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Class_Component]') AND name = N'Machine_Class_Component_ID')
DROP INDEX [Machine_Class_Component_ID] ON [dbo].[Machine_Class_Component] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Machine_Class_Component_ID] ON [dbo].[Machine_Class_Component] 
(
	[Machine_Class_Component_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Class_Component]') AND name = N'Machine_Class_Component_Machine_Class_ID')
DROP INDEX [Machine_Class_Component_Machine_Class_ID] ON [dbo].[Machine_Class_Component] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Class_Component_Machine_Class_ID] ON [dbo].[Machine_Class_Component] 
(
	[Machine_Class_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

