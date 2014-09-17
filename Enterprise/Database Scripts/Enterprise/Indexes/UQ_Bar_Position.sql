USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position]') AND name = N'Bar_Position_Access_Key_ID')
DROP INDEX [Bar_Position_Access_Key_ID] ON [dbo].[Bar_Position] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Bar_Position_Access_Key_ID] ON [dbo].[Bar_Position] 
(
	[Access_Key_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position]') AND name = N'Bar_Position_Bar_Position_Machine_Type_AMEDIS_Code')
DROP INDEX [Bar_Position_Bar_Position_Machine_Type_AMEDIS_Code] ON [dbo].[Bar_Position] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Bar_Position_Bar_Position_Machine_Type_AMEDIS_Code] ON [dbo].[Bar_Position] 
(
	[Bar_Position_Machine_Type_AMEDIS_Code] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position]') AND name = N'Bar_Position_Bar_Position_Rent_Schedule_ID')
DROP INDEX [Bar_Position_Bar_Position_Rent_Schedule_ID] ON [dbo].[Bar_Position] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Bar_Position_Bar_Position_Rent_Schedule_ID] ON [dbo].[Bar_Position] 
(
	[Bar_Position_Rent_Schedule_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position]') AND name = N'Bar_Position_Bar_Position_Share_Schedule_ID')
DROP INDEX [Bar_Position_Bar_Position_Share_Schedule_ID] ON [dbo].[Bar_Position] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Bar_Position_Bar_Position_Share_Schedule_ID] ON [dbo].[Bar_Position] 
(
	[Bar_Position_Share_Schedule_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position]') AND name = N'Bar_Position_Bar_Position_Supplier_AMEDIS_Code')
DROP INDEX [Bar_Position_Bar_Position_Supplier_AMEDIS_Code] ON [dbo].[Bar_Position] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Bar_Position_Bar_Position_Supplier_AMEDIS_Code] ON [dbo].[Bar_Position] 
(
	[Bar_Position_Supplier_AMEDIS_Code] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position]') AND name = N'Bar_Position_Bar_Position_Supplier_Depot_AMEDIS_Code')
DROP INDEX [Bar_Position_Bar_Position_Supplier_Depot_AMEDIS_Code] ON [dbo].[Bar_Position] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Bar_Position_Bar_Position_Supplier_Depot_AMEDIS_Code] ON [dbo].[Bar_Position] 
(
	[Bar_Position_Supplier_Depot_AMEDIS_Code] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position]') AND name = N'Bar_Position_Depot_ID')
DROP INDEX [Bar_Position_Depot_ID] ON [dbo].[Bar_Position] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Bar_Position_Depot_ID] ON [dbo].[Bar_Position] 
(
	[Depot_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position]') AND name = N'Bar_Position_Duty_ID')
DROP INDEX [Bar_Position_Duty_ID] ON [dbo].[Bar_Position] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Bar_Position_Duty_ID] ON [dbo].[Bar_Position] 
(
	[Duty_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position]') AND name = N'Bar_Position_ID')
DROP INDEX [Bar_Position_ID] ON [dbo].[Bar_Position] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Bar_Position_ID] ON [dbo].[Bar_Position] 
(
	[Bar_Position_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position]') AND name = N'Bar_Position_Machine_Type_ID')
DROP INDEX [Bar_Position_Machine_Type_ID] ON [dbo].[Bar_Position] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Bar_Position_Machine_Type_ID] ON [dbo].[Bar_Position] 
(
	[Machine_Type_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position]') AND name = N'Bar_Position_Site_ID')
DROP INDEX [Bar_Position_Site_ID] ON [dbo].[Bar_Position] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Bar_Position_Site_ID] ON [dbo].[Bar_Position] 
(
	[Site_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position]') AND name = N'Bar_Position_Terms_Group_Future_ID')
DROP INDEX [Bar_Position_Terms_Group_Future_ID] ON [dbo].[Bar_Position] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Bar_Position_Terms_Group_Future_ID] ON [dbo].[Bar_Position] 
(
	[Terms_Group_Future_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position]') AND name = N'Bar_Position_Terms_Group_ID')
DROP INDEX [Bar_Position_Terms_Group_ID] ON [dbo].[Bar_Position] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Bar_Position_Terms_Group_ID] ON [dbo].[Bar_Position] 
(
	[Terms_Group_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position]') AND name = N'Bar_Position_Terms_Group_Past_ID')
DROP INDEX [Bar_Position_Terms_Group_Past_ID] ON [dbo].[Bar_Position] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Bar_Position_Terms_Group_Past_ID] ON [dbo].[Bar_Position] 
(
	[Terms_Group_Past_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Bar_Position]') AND name = N'Bar_Position_Zone_ID')
DROP INDEX [Bar_Position_Zone_ID] ON [dbo].[Bar_Position] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Bar_Position_Zone_ID] ON [dbo].[Bar_Position] 
(
	[Zone_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

