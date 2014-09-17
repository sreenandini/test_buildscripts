USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Permit_Duty]') AND name = N'Permit_Duty_Duty_ID')
DROP INDEX [Permit_Duty_Duty_ID] ON [dbo].[Permit_Duty] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Permit_Duty_Duty_ID] ON [dbo].[Permit_Duty] 
(
	[Duty_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Permit_Duty]') AND name = N'Permit_Duty_ID')
DROP INDEX [Permit_Duty_ID] ON [dbo].[Permit_Duty] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Permit_Duty_ID] ON [dbo].[Permit_Duty] 
(
	[Permit_Duty_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Permit_Duty]') AND name = N'Permit_Duty_Permit_ID')
DROP INDEX [Permit_Duty_Permit_ID] ON [dbo].[Permit_Duty] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Permit_Duty_Permit_ID] ON [dbo].[Permit_Duty] 
(
	[Permit_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

