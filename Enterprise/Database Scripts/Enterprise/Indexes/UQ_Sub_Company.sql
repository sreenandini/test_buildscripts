USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Sub_Company]') AND name = N'Sub_Company_Access_Key_ID')
DROP INDEX [Sub_Company_Access_Key_ID] ON [dbo].[Sub_Company] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Sub_Company_Access_Key_ID] ON [dbo].[Sub_Company] 
(
	[Access_Key_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Sub_Company]') AND name = N'Sub_Company_Calendar_ID')
DROP INDEX [Sub_Company_Calendar_ID] ON [dbo].[Sub_Company] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Sub_Company_Calendar_ID] ON [dbo].[Sub_Company] 
(
	[Calendar_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Sub_Company]') AND name = N'Sub_Company_Company_ID')
DROP INDEX [Sub_Company_Company_ID] ON [dbo].[Sub_Company] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Sub_Company_Company_ID] ON [dbo].[Sub_Company] 
(
	[Company_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Sub_Company]') AND name = N'Sub_Company_Company_Model_Set_ID')
DROP INDEX [Sub_Company_Company_Model_Set_ID] ON [dbo].[Sub_Company] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Sub_Company_Company_Model_Set_ID] ON [dbo].[Sub_Company] 
(
	[Company_Model_Set_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Sub_Company]') AND name = N'Sub_Company_ID')
DROP INDEX [Sub_Company_ID] ON [dbo].[Sub_Company] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Sub_Company_ID] ON [dbo].[Sub_Company] 
(
	[Sub_Company_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Sub_Company]') AND name = N'Sub_Company_Staff_ID')
DROP INDEX [Sub_Company_Staff_ID] ON [dbo].[Sub_Company] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Sub_Company_Staff_ID] ON [dbo].[Sub_Company] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Sub_Company]') AND name = N'Sub_Company_Sub_Company_Default_Opening_Hours_ID')
DROP INDEX [Sub_Company_Sub_Company_Default_Opening_Hours_ID] ON [dbo].[Sub_Company] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Sub_Company_Sub_Company_Default_Opening_Hours_ID] ON [dbo].[Sub_Company] 
(
	[Sub_Company_Default_Opening_Hours_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Sub_Company]') AND name = N'Sub_Company_Sub_Company_Invoice_Postcode')
DROP INDEX [Sub_Company_Sub_Company_Invoice_Postcode] ON [dbo].[Sub_Company] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Sub_Company_Sub_Company_Invoice_Postcode] ON [dbo].[Sub_Company] 
(
	[Sub_Company_Invoice_Postcode] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Sub_Company]') AND name = N'Sub_Company_Sub_Company_Postcode')
DROP INDEX [Sub_Company_Sub_Company_Postcode] ON [dbo].[Sub_Company] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Sub_Company_Sub_Company_Postcode] ON [dbo].[Sub_Company] 
(
	[Sub_Company_Postcode] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Sub_Company]') AND name = N'Sub_Company_Terms_Group_ID')
DROP INDEX [Sub_Company_Terms_Group_ID] ON [dbo].[Sub_Company] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Sub_Company_Terms_Group_ID] ON [dbo].[Sub_Company] 
(
	[Terms_Group_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

