USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Period_End_Site_Adjustment]') AND name = N'Period_End_Site_Adjustment_ID')
DROP INDEX [Period_End_Site_Adjustment_ID] ON [dbo].[Period_End_Site_Adjustment] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Period_End_Site_Adjustment_ID] ON [dbo].[Period_End_Site_Adjustment] 
(
	[Period_End_Site_Adjustment_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Period_End_Site_Adjustment]') AND name = N'Period_End_Site_Adjustment_Operator_ID')
DROP INDEX [Period_End_Site_Adjustment_Operator_ID] ON [dbo].[Period_End_Site_Adjustment] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Period_End_Site_Adjustment_Operator_ID] ON [dbo].[Period_End_Site_Adjustment] 
(
	[Operator_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Period_End_Site_Adjustment]') AND name = N'Period_End_Site_Adjustment_Operator_Period_End_ID')
DROP INDEX [Period_End_Site_Adjustment_Operator_Period_End_ID] ON [dbo].[Period_End_Site_Adjustment] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Period_End_Site_Adjustment_Operator_Period_End_ID] ON [dbo].[Period_End_Site_Adjustment] 
(
	[Operator_Period_End_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Period_End_Site_Adjustment]') AND name = N'Period_End_Site_Adjustment_Period_End_ID')
DROP INDEX [Period_End_Site_Adjustment_Period_End_ID] ON [dbo].[Period_End_Site_Adjustment] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Period_End_Site_Adjustment_Period_End_ID] ON [dbo].[Period_End_Site_Adjustment] 
(
	[Period_End_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Period_End_Site_Adjustment]') AND name = N'Period_End_Site_Adjustment_Secondary_Sub_Company_Period_End_ID')
DROP INDEX [Period_End_Site_Adjustment_Secondary_Sub_Company_Period_End_ID] ON [dbo].[Period_End_Site_Adjustment] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Period_End_Site_Adjustment_Secondary_Sub_Company_Period_End_ID] ON [dbo].[Period_End_Site_Adjustment] 
(
	[Secondary_Sub_Company_Period_End_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Period_End_Site_Adjustment]') AND name = N'Period_End_Site_Adjustment_Site_ID')
DROP INDEX [Period_End_Site_Adjustment_Site_ID] ON [dbo].[Period_End_Site_Adjustment] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Period_End_Site_Adjustment_Site_ID] ON [dbo].[Period_End_Site_Adjustment] 
(
	[Site_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Period_End_Site_Adjustment]') AND name = N'Period_End_Site_Adjustment_Staff_ID')
DROP INDEX [Period_End_Site_Adjustment_Staff_ID] ON [dbo].[Period_End_Site_Adjustment] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Period_End_Site_Adjustment_Staff_ID] ON [dbo].[Period_End_Site_Adjustment] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

