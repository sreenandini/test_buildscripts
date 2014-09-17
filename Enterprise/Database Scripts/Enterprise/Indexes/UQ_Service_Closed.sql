USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Closed]') AND name = N'Service_Closed_Bar_Position_ID')
DROP INDEX [Service_Closed_Bar_Position_ID] ON [dbo].[Service_Closed] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Service_Closed_Bar_Position_ID] ON [dbo].[Service_Closed] 
(
	[Bar_Position_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Closed]') AND name = N'Service_Closed_Call_Fault_ID')
DROP INDEX [Service_Closed_Call_Fault_ID] ON [dbo].[Service_Closed] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Service_Closed_Call_Fault_ID] ON [dbo].[Service_Closed] 
(
	[Call_Fault_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Closed]') AND name = N'Service_Closed_Call_Group_ID')
DROP INDEX [Service_Closed_Call_Group_ID] ON [dbo].[Service_Closed] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Service_Closed_Call_Group_ID] ON [dbo].[Service_Closed] 
(
	[Call_Group_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Closed]') AND name = N'Service_Closed_Call_Remedy_ID')
DROP INDEX [Service_Closed_Call_Remedy_ID] ON [dbo].[Service_Closed] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Service_Closed_Call_Remedy_ID] ON [dbo].[Service_Closed] 
(
	[Call_Remedy_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Closed]') AND name = N'Service_Closed_Call_Source_ID')
DROP INDEX [Service_Closed_Call_Source_ID] ON [dbo].[Service_Closed] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Service_Closed_Call_Source_ID] ON [dbo].[Service_Closed] 
(
	[Call_Source_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Closed]') AND name = N'Service_Closed_Call_Status_ID')
DROP INDEX [Service_Closed_Call_Status_ID] ON [dbo].[Service_Closed] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Service_Closed_Call_Status_ID] ON [dbo].[Service_Closed] 
(
	[Call_Status_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Closed]') AND name = N'Service_Closed_Installation_ID')
DROP INDEX [Service_Closed_Installation_ID] ON [dbo].[Service_Closed] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Service_Closed_Installation_ID] ON [dbo].[Service_Closed] 
(
	[Installation_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Closed]') AND name = N'Service_Closed_Machine_ID')
DROP INDEX [Service_Closed_Machine_ID] ON [dbo].[Service_Closed] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Service_Closed_Machine_ID] ON [dbo].[Service_Closed] 
(
	[Machine_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Closed]') AND name = N'Service_Closed_Machine_Type_ID')
DROP INDEX [Service_Closed_Machine_Type_ID] ON [dbo].[Service_Closed] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Service_Closed_Machine_Type_ID] ON [dbo].[Service_Closed] 
(
	[Machine_Type_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Closed]') AND name = N'Service_Closed_Previous_Service_ID')
DROP INDEX [Service_Closed_Previous_Service_ID] ON [dbo].[Service_Closed] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Service_Closed_Previous_Service_ID] ON [dbo].[Service_Closed] 
(
	[Previous_Service_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Closed]') AND name = N'Service_Closed_Service_Issued_By_Staff_ID')
DROP INDEX [Service_Closed_Service_Issued_By_Staff_ID] ON [dbo].[Service_Closed] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Service_Closed_Service_Issued_By_Staff_ID] ON [dbo].[Service_Closed] 
(
	[Service_Issued_By_Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Closed]') AND name = N'Service_Closed_Service_Issued_To_Staff_ID')
DROP INDEX [Service_Closed_Service_Issued_To_Staff_ID] ON [dbo].[Service_Closed] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Service_Closed_Service_Issued_To_Staff_ID] ON [dbo].[Service_Closed] 
(
	[Service_Issued_To_Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Closed]') AND name = N'Service_Closed_Service_Message_ID')
DROP INDEX [Service_Closed_Service_Message_ID] ON [dbo].[Service_Closed] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Service_Closed_Service_Message_ID] ON [dbo].[Service_Closed] 
(
	[Service_Message_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Closed]') AND name = N'Service_Closed_Service_Received_Staff_ID')
DROP INDEX [Service_Closed_Service_Received_Staff_ID] ON [dbo].[Service_Closed] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Service_Closed_Service_Received_Staff_ID] ON [dbo].[Service_Closed] 
(
	[Service_Received_Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Closed]') AND name = N'Service_Closed_Site_ID')
DROP INDEX [Service_Closed_Site_ID] ON [dbo].[Service_Closed] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Service_Closed_Site_ID] ON [dbo].[Service_Closed] 
(
	[Site_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Closed]') AND name = N'Service_Closed_SLA_Contract_ID')
DROP INDEX [Service_Closed_SLA_Contract_ID] ON [dbo].[Service_Closed] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Service_Closed_SLA_Contract_ID] ON [dbo].[Service_Closed] 
(
	[SLA_Contract_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Closed]') AND name = N'Service_Closed_Zone_ID')
DROP INDEX [Service_Closed_Zone_ID] ON [dbo].[Service_Closed] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Service_Closed_Zone_ID] ON [dbo].[Service_Closed] 
(
	[Zone_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Service_Closed]') AND name = N'Service_ID')
DROP INDEX [Service_ID] ON [dbo].[Service_Closed] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Service_ID] ON [dbo].[Service_Closed] 
(
	[Service_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

