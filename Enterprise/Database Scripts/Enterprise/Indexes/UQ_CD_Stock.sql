USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CD_Stock]') AND name = N'CD_Stock_CD_ID')
DROP INDEX [CD_Stock_CD_ID] ON [dbo].[CD_Stock] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [CD_Stock_CD_ID] ON [dbo].[CD_Stock] 
(
	[CD_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CD_Stock]') AND name = N'CD_Stock_CD_Program_ID')
DROP INDEX [CD_Stock_CD_Program_ID] ON [dbo].[CD_Stock] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [CD_Stock_CD_Program_ID] ON [dbo].[CD_Stock] 
(
	[CD_Program_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CD_Stock]') AND name = N'CD_Stock_CD_Stock_Sheduled_Program_ID')
DROP INDEX [CD_Stock_CD_Stock_Sheduled_Program_ID] ON [dbo].[CD_Stock] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [CD_Stock_CD_Stock_Sheduled_Program_ID] ON [dbo].[CD_Stock] 
(
	[CD_Stock_Sheduled_Program_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CD_Stock]') AND name = N'CD_Stock_ID')
DROP INDEX [CD_Stock_ID] ON [dbo].[CD_Stock] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [CD_Stock_ID] ON [dbo].[CD_Stock] 
(
	[CD_Stock_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

