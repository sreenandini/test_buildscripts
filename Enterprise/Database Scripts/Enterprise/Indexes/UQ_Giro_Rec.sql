USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Giro_Rec]') AND name = N'Giro_Rec_ID')
DROP INDEX [Giro_Rec_ID] ON [dbo].[Giro_Rec] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Giro_Rec_ID] ON [dbo].[Giro_Rec] 
(
	[Giro_Rec_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

