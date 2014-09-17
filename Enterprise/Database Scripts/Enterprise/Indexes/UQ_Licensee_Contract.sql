USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Licensee_Contract]') AND name = N'Licensee_Contract_ID')
DROP INDEX [Licensee_Contract_ID] ON [dbo].[Licensee_Contract] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Licensee_Contract_ID] ON [dbo].[Licensee_Contract] 
(
	[Licensee_Contract_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Licensee_Contract]') AND name = N'Licensee_Contract_Site_ID')
DROP INDEX [Licensee_Contract_Site_ID] ON [dbo].[Licensee_Contract] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Licensee_Contract_Site_ID] ON [dbo].[Licensee_Contract] 
(
	[Site_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Licensee_Contract]') AND name = N'Licensee_Contract_Staff_ID')
DROP INDEX [Licensee_Contract_Staff_ID] ON [dbo].[Licensee_Contract] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Licensee_Contract_Staff_ID] ON [dbo].[Licensee_Contract] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

