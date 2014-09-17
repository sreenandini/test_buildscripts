USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_Accessory_ID')
DROP INDEX [Event_Master_Accessory_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Event_Master_Accessory_ID] ON [dbo].[Event_Master] 
(
	[Accessory_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_Accessory_Installation_ID')
DROP INDEX [Event_Master_Accessory_Installation_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Event_Master_Accessory_Installation_ID] ON [dbo].[Event_Master] 
(
	[Accessory_Installation_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_Accessory_Model_ID')
DROP INDEX [Event_Master_Accessory_Model_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Event_Master_Accessory_Model_ID] ON [dbo].[Event_Master] 
(
	[Accessory_Model_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_Bar_Position_ID')
DROP INDEX [Event_Master_Bar_Position_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Event_Master_Bar_Position_ID] ON [dbo].[Event_Master] 
(
	[Bar_Position_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_Company_ID')
DROP INDEX [Event_Master_Company_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Event_Master_Company_ID] ON [dbo].[Event_Master] 
(
	[Company_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_Depot_ID')
DROP INDEX [Event_Master_Depot_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Event_Master_Depot_ID] ON [dbo].[Event_Master] 
(
	[Depot_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_Event_Master_Created_By_Staff_ID')
DROP INDEX [Event_Master_Event_Master_Created_By_Staff_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Event_Master_Event_Master_Created_By_Staff_ID] ON [dbo].[Event_Master] 
(
	[Event_Master_Created_By_Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_Event_Master_Type_ID')
DROP INDEX [Event_Master_Event_Master_Type_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Event_Master_Event_Master_Type_ID] ON [dbo].[Event_Master] 
(
	[Event_Master_Type_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_ID')
DROP INDEX [Event_Master_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Event_Master_ID] ON [dbo].[Event_Master] 
(
	[Event_Master_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_Installation_ID')
DROP INDEX [Event_Master_Installation_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Event_Master_Installation_ID] ON [dbo].[Event_Master] 
(
	[Installation_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_Machine_Class_ID')
DROP INDEX [Event_Master_Machine_Class_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Event_Master_Machine_Class_ID] ON [dbo].[Event_Master] 
(
	[Machine_Class_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_Machine_ID')
DROP INDEX [Event_Master_Machine_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Event_Master_Machine_ID] ON [dbo].[Event_Master] 
(
	[Machine_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_Machine_Type_ID')
DROP INDEX [Event_Master_Machine_Type_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Event_Master_Machine_Type_ID] ON [dbo].[Event_Master] 
(
	[Machine_Type_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_Operator_ID')
DROP INDEX [Event_Master_Operator_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Event_Master_Operator_ID] ON [dbo].[Event_Master] 
(
	[Operator_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_Route_ID')
DROP INDEX [Event_Master_Route_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Event_Master_Route_ID] ON [dbo].[Event_Master] 
(
	[Route_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_Secondary_Sub_Company_ID')
DROP INDEX [Event_Master_Secondary_Sub_Company_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Event_Master_Secondary_Sub_Company_ID] ON [dbo].[Event_Master] 
(
	[Secondary_Sub_Company_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_Site_ID')
DROP INDEX [Event_Master_Site_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Event_Master_Site_ID] ON [dbo].[Event_Master] 
(
	[Site_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_Staff_ID')
DROP INDEX [Event_Master_Staff_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Event_Master_Staff_ID] ON [dbo].[Event_Master] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_Sub_Company_Area_ID')
DROP INDEX [Event_Master_Sub_Company_Area_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Event_Master_Sub_Company_Area_ID] ON [dbo].[Event_Master] 
(
	[Sub_Company_Area_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_Sub_Company_District_ID')
DROP INDEX [Event_Master_Sub_Company_District_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Event_Master_Sub_Company_District_ID] ON [dbo].[Event_Master] 
(
	[Sub_Company_District_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_Sub_Company_ID')
DROP INDEX [Event_Master_Sub_Company_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Event_Master_Sub_Company_ID] ON [dbo].[Event_Master] 
(
	[Sub_Company_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_Sub_Company_Region_ID')
DROP INDEX [Event_Master_Sub_Company_Region_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Event_Master_Sub_Company_Region_ID] ON [dbo].[Event_Master] 
(
	[Sub_Company_Region_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master]') AND name = N'Event_Master_Terms_Group_ID')
DROP INDEX [Event_Master_Terms_Group_ID] ON [dbo].[Event_Master] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Event_Master_Terms_Group_ID] ON [dbo].[Event_Master] 
(
	[Terms_Group_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

