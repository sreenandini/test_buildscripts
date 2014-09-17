USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Terms_Group]') AND name = N'Terms_Group_ID')
DROP INDEX [Terms_Group_ID] ON [dbo].[Terms_Group] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Terms_Group_ID] ON [dbo].[Terms_Group] 
(
	[Terms_Group_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

