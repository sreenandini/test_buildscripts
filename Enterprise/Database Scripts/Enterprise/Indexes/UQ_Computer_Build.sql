USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Computer_Build]') AND name = N'Computer_Build_ID')
DROP INDEX [Computer_Build_ID] ON [dbo].[Computer_Build] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Computer_Build_ID] ON [dbo].[Computer_Build] 
(
	[Computer_Build_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

