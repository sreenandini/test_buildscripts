USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Type_Duty]') AND name = N'Machine_Type_Duty_Duty_ID')
DROP INDEX [Machine_Type_Duty_Duty_ID] ON [dbo].[Machine_Type_Duty] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Type_Duty_Duty_ID] ON [dbo].[Machine_Type_Duty] 
(
	[Duty_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Type_Duty]') AND name = N'Machine_Type_Duty_ID')
DROP INDEX [Machine_Type_Duty_ID] ON [dbo].[Machine_Type_Duty] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Machine_Type_Duty_ID] ON [dbo].[Machine_Type_Duty] 
(
	[Machine_Type_Duty_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Type_Duty]') AND name = N'Machine_Type_Duty_Machine_Type_ID')
DROP INDEX [Machine_Type_Duty_Machine_Type_ID] ON [dbo].[Machine_Type_Duty] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Type_Duty_Machine_Type_ID] ON [dbo].[Machine_Type_Duty] 
(
	[Machine_Type_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

