USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Permit_Authority]') AND name = N'Permit_Authority_ID')
DROP INDEX [Permit_Authority_ID] ON [dbo].[Permit_Authority] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Permit_Authority_ID] ON [dbo].[Permit_Authority] 
(
	[Permit_Authority_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

