USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[EDI_Export_Log]') AND name = N'EDI_Export_Log_ID')
DROP INDEX [EDI_Export_Log_ID] ON [dbo].[EDI_Export_Log] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [EDI_Export_Log_ID] ON [dbo].[EDI_Export_Log] 
(
	[EDI_Export_Log_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

