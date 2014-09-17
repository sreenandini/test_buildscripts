USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Audit_Day_ID')
DROP INDEX [Collection_Details_Audit_Day_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Audit_Day_ID] ON [dbo].[Collection_Details] 
(
	[Audit_Day_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Audit_Period_ID')
DROP INDEX [Collection_Details_Audit_Period_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Audit_Period_ID] ON [dbo].[Collection_Details] 
(
	[Audit_Period_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Audit_Week_ID')
DROP INDEX [Collection_Details_Audit_Week_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Audit_Week_ID] ON [dbo].[Collection_Details] 
(
	[Audit_Week_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Audit_Year_ID')
DROP INDEX [Collection_Details_Audit_Year_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Audit_Year_ID] ON [dbo].[Collection_Details] 
(
	[Audit_Year_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Collection_Amedis_Col_Export_Status')
DROP INDEX [Collection_Details_Collection_Amedis_Col_Export_Status] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Collection_Amedis_Col_Export_Status] ON [dbo].[Collection_Details] 
(
	[Collection_Amedis_Col_Export_Status] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Collection_Amedis_EDC_Export_Status')
DROP INDEX [Collection_Details_Collection_Amedis_EDC_Export_Status] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Collection_Amedis_EDC_Export_Status] ON [dbo].[Collection_Details] 
(
	[Collection_Amedis_EDC_Export_Status] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Collection_Company_ID')
DROP INDEX [Collection_Details_Collection_Company_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Collection_Company_ID] ON [dbo].[Collection_Details] 
(
	[Collection_Company_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Collection_Honeyframe_Col_Export_Status')
DROP INDEX [Collection_Details_Collection_Honeyframe_Col_Export_Status] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Collection_Honeyframe_Col_Export_Status] ON [dbo].[Collection_Details] 
(
	[Collection_Honeyframe_Col_Export_Status] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Collection_Honeyframe_EDC_Export_Status')
DROP INDEX [Collection_Details_Collection_Honeyframe_EDC_Export_Status] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Collection_Honeyframe_EDC_Export_Status] ON [dbo].[Collection_Details] 
(
	[Collection_Honeyframe_EDC_Export_Status] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Collection_LeisureData_Col_Export_Status')
DROP INDEX [Collection_Details_Collection_LeisureData_Col_Export_Status] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Collection_LeisureData_Col_Export_Status] ON [dbo].[Collection_Details] 
(
	[Collection_LeisureData_Col_Export_Status] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Collection_LeisureData_EDC_Export_Status')
DROP INDEX [Collection_Details_Collection_LeisureData_EDC_Export_Status] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Collection_LeisureData_EDC_Export_Status] ON [dbo].[Collection_Details] 
(
	[Collection_LeisureData_EDC_Export_Status] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Collection_McMullens_Col_Export_Status')
DROP INDEX [Collection_Details_Collection_McMullens_Col_Export_Status] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Collection_McMullens_Col_Export_Status] ON [dbo].[Collection_Details] 
(
	[Collection_McMullens_Col_Export_Status] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Collection_McMullens_EDC_Export_Status')
DROP INDEX [Collection_Details_Collection_McMullens_EDC_Export_Status] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Collection_McMullens_EDC_Export_Status] ON [dbo].[Collection_Details] 
(
	[Collection_McMullens_EDC_Export_Status] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Collection_Model_ID')
DROP INDEX [Collection_Details_Collection_Model_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Collection_Model_ID] ON [dbo].[Collection_Details] 
(
	[Collection_Model_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Collection_Secondary_Sub_Company_ID')
DROP INDEX [Collection_Details_Collection_Secondary_Sub_Company_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Collection_Secondary_Sub_Company_ID] ON [dbo].[Collection_Details] 
(
	[Collection_Secondary_Sub_Company_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Collection_Sub_Company_ID')
DROP INDEX [Collection_Details_Collection_Sub_Company_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Collection_Sub_Company_ID] ON [dbo].[Collection_Details] 
(
	[Collection_Sub_Company_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Collection_Supplier_Depot_ID')
DROP INDEX [Collection_Details_Collection_Supplier_Depot_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Collection_Supplier_Depot_ID] ON [dbo].[Collection_Details] 
(
	[Collection_Supplier_Depot_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Collection_Supplier_ID')
DROP INDEX [Collection_Details_Collection_Supplier_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Collection_Supplier_ID] ON [dbo].[Collection_Details] 
(
	[Collection_Supplier_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_EDI_Import_Log_ID')
DROP INDEX [Collection_Details_EDI_Import_Log_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_EDI_Import_Log_ID] ON [dbo].[Collection_Details] 
(
	[EDI_Import_Log_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Operator_Period_End_ID')
DROP INDEX [Collection_Details_Operator_Period_End_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Operator_Period_End_ID] ON [dbo].[Collection_Details] 
(
	[Operator_Period_End_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Operator_Period_ID')
DROP INDEX [Collection_Details_Operator_Period_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Operator_Period_ID] ON [dbo].[Collection_Details] 
(
	[Operator_Period_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Operator_Week_End_ID')
DROP INDEX [Collection_Details_Operator_Week_End_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Operator_Week_End_ID] ON [dbo].[Collection_Details] 
(
	[Operator_Week_End_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Operator_Week_ID')
DROP INDEX [Collection_Details_Operator_Week_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Operator_Week_ID] ON [dbo].[Collection_Details] 
(
	[Operator_Week_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Period_End_ID')
DROP INDEX [Collection_Details_Period_End_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Period_End_ID] ON [dbo].[Collection_Details] 
(
	[Period_End_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Period_ID')
DROP INDEX [Collection_Details_Period_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Period_ID] ON [dbo].[Collection_Details] 
(
	[Period_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Secondary_Sub_Company_Period_End_ID')
DROP INDEX [Collection_Details_Secondary_Sub_Company_Period_End_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Secondary_Sub_Company_Period_End_ID] ON [dbo].[Collection_Details] 
(
	[Secondary_Sub_Company_Period_End_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Secondary_Sub_Company_Period_ID')
DROP INDEX [Collection_Details_Secondary_Sub_Company_Period_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Secondary_Sub_Company_Period_ID] ON [dbo].[Collection_Details] 
(
	[Secondary_Sub_Company_Period_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Secondary_Sub_Company_Week_End_ID')
DROP INDEX [Collection_Details_Secondary_Sub_Company_Week_End_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Secondary_Sub_Company_Week_End_ID] ON [dbo].[Collection_Details] 
(
	[Secondary_Sub_Company_Week_End_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Secondary_Sub_Company_Week_ID')
DROP INDEX [Collection_Details_Secondary_Sub_Company_Week_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Secondary_Sub_Company_Week_ID] ON [dbo].[Collection_Details] 
(
	[Secondary_Sub_Company_Week_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Week_End_ID')
DROP INDEX [Collection_Details_Week_End_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Week_End_ID] ON [dbo].[Collection_Details] 
(
	[Week_End_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_Details_Week_ID')
DROP INDEX [Collection_Details_Week_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Details_Week_ID] ON [dbo].[Collection_Details] 
(
	[Week_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Details]') AND name = N'Collection_ID')
DROP INDEX [Collection_ID] ON [dbo].[Collection_Details] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Collection_ID] ON [dbo].[Collection_Details] 
(
	[Collection_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

