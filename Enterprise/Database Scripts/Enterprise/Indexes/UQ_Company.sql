USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND name = N'Company_Access_Key_ID')
DROP INDEX [Company_Access_Key_ID] ON [dbo].[Company] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Company_Access_Key_ID] ON [dbo].[Company] 
(
	[Access_Key_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND name = N'Company_Company_AMEDIS_Code')
DROP INDEX [Company_Company_AMEDIS_Code] ON [dbo].[Company] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Company_Company_AMEDIS_Code] ON [dbo].[Company] 
(
	[Company_AMEDIS_Code] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND name = N'Company_Company_AMEDIS_Operational_Code')
DROP INDEX [Company_Company_AMEDIS_Operational_Code] ON [dbo].[Company] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Company_Company_AMEDIS_Operational_Code] ON [dbo].[Company] 
(
	[Company_AMEDIS_Operational_Code] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND name = N'Company_Company_Invoice_Postcode')
DROP INDEX [Company_Company_Invoice_Postcode] ON [dbo].[Company] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Company_Company_Invoice_Postcode] ON [dbo].[Company] 
(
	[Company_Invoice_Postcode] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND name = N'Company_Company_Model_Set_ID')
DROP INDEX [Company_Company_Model_Set_ID] ON [dbo].[Company] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Company_Company_Model_Set_ID] ON [dbo].[Company] 
(
	[Company_Model_Set_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND name = N'Company_Company_Postcode')
DROP INDEX [Company_Company_Postcode] ON [dbo].[Company] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Company_Company_Postcode] ON [dbo].[Company] 
(
	[Company_Postcode] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND name = N'Company_ID')
DROP INDEX [Company_ID] ON [dbo].[Company] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Company_ID] ON [dbo].[Company] 
(
	[Company_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND name = N'Company_Staff_ID')
DROP INDEX [Company_Staff_ID] ON [dbo].[Company] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Company_Staff_ID] ON [dbo].[Company] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND name = N'Company_Terms_Group_ID')
DROP INDEX [Company_Terms_Group_ID] ON [dbo].[Company] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Company_Terms_Group_ID] ON [dbo].[Company] 
(
	[Terms_Group_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

