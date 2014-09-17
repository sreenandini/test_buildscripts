USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Operator_Period_End]') AND name = N'Operator_Period_End_ID')
DROP INDEX [Operator_Period_End_ID] ON [dbo].[Operator_Period_End] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Operator_Period_End_ID] ON [dbo].[Operator_Period_End] 
(
	[Operator_Period_End_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

