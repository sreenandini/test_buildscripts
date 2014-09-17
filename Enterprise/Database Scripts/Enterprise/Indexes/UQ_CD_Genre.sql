USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CD_Genre]') AND name = N'CD_Genre_ID')
DROP INDEX [CD_Genre_ID] ON [dbo].[CD_Genre] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [CD_Genre_ID] ON [dbo].[CD_Genre] 
(
	[CD_Genre_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

