USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation]') AND name = N'Installation_Bar_Position_ID')
CREATE NONCLUSTERED INDEX [Installation_Bar_Position_ID] ON [dbo].[Installation] 
(
	[Bar_Position_ID] ASC
)INCLUDE([Machine_ID],
[Installation_Price_Per_Play])WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation]') AND name = N'Installation_CD_Program_ID')
CREATE NONCLUSTERED INDEX [Installation_CD_Program_ID] ON [dbo].[Installation] 
(
	[CD_Program_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation]') AND name = N'Installation_Datapak_ID')

CREATE NONCLUSTERED INDEX [Installation_Datapak_ID] ON [dbo].[Installation] 
(
	[Datapak_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation]') AND name = N'Installation_Depot_ID')

CREATE NONCLUSTERED INDEX [Installation_Depot_ID] ON [dbo].[Installation] 
(
	[Depot_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation]') AND name = N'Installation_Duty_ID')

CREATE NONCLUSTERED INDEX [Installation_Duty_ID] ON [dbo].[Installation] 
(
	[Duty_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation]') AND name = N'Installation_ID')

CREATE UNIQUE NONCLUSTERED INDEX [Installation_ID] ON [dbo].[Installation] 
(
	[Installation_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation]') AND name = N'Installation_Installation_Amedis_Export_In_Status')

CREATE NONCLUSTERED INDEX [Installation_Installation_Amedis_Export_In_Status] ON [dbo].[Installation] 
(
	[Installation_Amedis_Export_In_Status] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation]') AND name = N'Installation_Installation_Amedis_Export_Out_Status')

CREATE NONCLUSTERED INDEX [Installation_Installation_Amedis_Export_Out_Status] ON [dbo].[Installation] 
(
	[Installation_Amedis_Export_Out_Status] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation]') AND name = N'Installation_Installation_Amedis_Import_Log_ID')
CREATE NONCLUSTERED INDEX [Installation_Installation_Amedis_Import_Log_ID] ON [dbo].[Installation] 
(
	[Installation_Amedis_Import_Log_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation]') AND name = N'Installation_Installation_Amedis_Import_Log_Withdrawl_ID')
CREATE NONCLUSTERED INDEX [Installation_Installation_Amedis_Import_Log_Withdrawl_ID] ON [dbo].[Installation] 
(
	[Installation_Amedis_Import_Log_Withdrawl_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation]') AND name = N'Installation_Installation_Honeyframe_Export_In_Status')
CREATE NONCLUSTERED INDEX [Installation_Installation_Honeyframe_Export_In_Status] ON [dbo].[Installation] 
(
	[Installation_Honeyframe_Export_In_Status] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation]') AND name = N'Installation_Installation_Honeyframe_Export_Out_Status')
CREATE NONCLUSTERED INDEX [Installation_Installation_Honeyframe_Export_Out_Status] ON [dbo].[Installation] 
(
	[Installation_Honeyframe_Export_Out_Status] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation]') AND name = N'Installation_Installation_LeisureData_Export_In_Status')
CREATE NONCLUSTERED INDEX [Installation_Installation_LeisureData_Export_In_Status] ON [dbo].[Installation] 
(
	[Installation_LeisureData_Export_In_Status] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation]') AND name = N'Installation_Installation_LeisureData_Export_Out_Status')
CREATE NONCLUSTERED INDEX [Installation_Installation_LeisureData_Export_Out_Status] ON [dbo].[Installation] 
(
	[Installation_LeisureData_Export_Out_Status] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation]') AND name = N'Installation_Installation_McMullens_Export_In_Status')
CREATE NONCLUSTERED INDEX [Installation_Installation_McMullens_Export_In_Status] ON [dbo].[Installation] 
(
	[Installation_McMullens_Export_In_Status] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation]') AND name = N'Installation_Installation_McMullens_Export_Out_Status')
CREATE NONCLUSTERED INDEX [Installation_Installation_McMullens_Export_Out_Status] ON [dbo].[Installation] 
(
	[Installation_McMullens_Export_Out_Status] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation]') AND name = N'Installation_Machine_ID')
CREATE NONCLUSTERED INDEX [Installation_Machine_ID] ON [dbo].[Installation] 
(
	[Machine_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

/****** Object:  Index [IDX_Installation_End_Date]    Script Date: 12/10/2013 18:30:16 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation]') AND name = N'IDX_Installation_End_Date')
/****** Object:  Index [IDX_Installation_End_Date]    Script Date: 12/10/2013 18:30:16 ******/
CREATE NONCLUSTERED INDEX [IDX_Installation_End_Date] ON [dbo].[Installation] 
(
	[Installation_End_Date] ASC,
	[Installation_ID] ASC,
	[Bar_Position_ID] ASC,
	[Machine_ID] ASC
)
INCLUDE ( [Installation_Start_Date],
[Installation_Jackpot_Value],
[Installation_Price_Per_Play]) 
--WHERE ([Installation_End_Date] IS NULL)
WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation]') AND name = N'Installation_Installation_Price_Per_Play')
	DROP INDEX [Installation_Installation_Price_Per_Play] ON [dbo].[Installation] WITH ( ONLINE = OFF )
GO

CREATE NONCLUSTERED INDEX [Installation_Installation_Price_Per_Play] ON [dbo].[Installation] 
(
	[Installation_Price_Per_Play] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO