USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Type_Site_Do_Not_Transmit]') AND name = N'Machine_Type_Site_Do_Not_Transmit_ID')
DROP INDEX [Machine_Type_Site_Do_Not_Transmit_ID] ON [dbo].[Machine_Type_Site_Do_Not_Transmit] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Machine_Type_Site_Do_Not_Transmit_ID] ON [dbo].[Machine_Type_Site_Do_Not_Transmit] 
(
	[Machine_Type_Site_Do_Not_Transmit_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Type_Site_Do_Not_Transmit]') AND name = N'Machine_Type_Site_Do_Not_Transmit_Machine_Type_ID')
DROP INDEX [Machine_Type_Site_Do_Not_Transmit_Machine_Type_ID] ON [dbo].[Machine_Type_Site_Do_Not_Transmit] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Type_Site_Do_Not_Transmit_Machine_Type_ID] ON [dbo].[Machine_Type_Site_Do_Not_Transmit] 
(
	[Machine_Type_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine_Type_Site_Do_Not_Transmit]') AND name = N'Machine_Type_Site_Do_Not_Transmit_Site_ID')
DROP INDEX [Machine_Type_Site_Do_Not_Transmit_Site_ID] ON [dbo].[Machine_Type_Site_Do_Not_Transmit] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Type_Site_Do_Not_Transmit_Site_ID] ON [dbo].[Machine_Type_Site_Do_Not_Transmit] 
(
	[Site_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

