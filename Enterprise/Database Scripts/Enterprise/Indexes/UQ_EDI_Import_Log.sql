USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[EDI_Import_Log]') AND name = N'EDI_Import_Log_ID')
DROP INDEX [EDI_Import_Log_ID] ON [dbo].[EDI_Import_Log] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [EDI_Import_Log_ID] ON [dbo].[EDI_Import_Log] 
(
	[EDI_Import_Log_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

