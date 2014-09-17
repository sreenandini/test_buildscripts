USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Component]') AND name = N'Machine_Component')
DROP INDEX [Machine_Component] ON [dbo].[Machine_Component] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Machine_Component] ON [dbo].[Machine_Component] 
(
	[Machine_Component] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Component]') AND name = N'Machine_Component_Component_ID')
DROP INDEX [Machine_Component_Component_ID] ON [dbo].[Machine_Component] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Component_Component_ID] ON [dbo].[Machine_Component] 
(
	[Component_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Component]') AND name = N'Machine_Component_Macine_ID')
DROP INDEX [Machine_Component_Macine_ID] ON [dbo].[Machine_Component] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Component_Macine_ID] ON [dbo].[Machine_Component] 
(
	[Macine_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

