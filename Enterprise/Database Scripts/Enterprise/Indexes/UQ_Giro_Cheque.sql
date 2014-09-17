USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Giro_Cheque]') AND name = N'Giro_Cheque_Giro_ID')
DROP INDEX [Giro_Cheque_Giro_ID] ON [dbo].[Giro_Cheque] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Giro_Cheque_Giro_ID] ON [dbo].[Giro_Cheque] 
(
	[Giro_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Giro_Cheque]') AND name = N'Giro_Cheque_ID')
DROP INDEX [Giro_Cheque_ID] ON [dbo].[Giro_Cheque] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Giro_Cheque_ID] ON [dbo].[Giro_Cheque] 
(
	[Giro_Cheque_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

