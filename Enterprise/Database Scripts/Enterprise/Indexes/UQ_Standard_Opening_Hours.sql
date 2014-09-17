USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Standard_Opening_Hours]') AND name = N'Standard_Opening_Hours_ID')
DROP INDEX [Standard_Opening_Hours_ID] ON [dbo].[Standard_Opening_Hours] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Standard_Opening_Hours_ID] ON [dbo].[Standard_Opening_Hours] 
(
	[Standard_Opening_Hours_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

