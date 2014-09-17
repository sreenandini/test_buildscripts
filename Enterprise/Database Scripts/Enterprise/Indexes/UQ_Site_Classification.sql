USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Site_Classification]') AND name = N'Site_Classification_ID')
DROP INDEX [Site_Classification_ID] ON [dbo].[Site_Classification] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Site_Classification_ID] ON [dbo].[Site_Classification] 
(
	[Site_Classification_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

