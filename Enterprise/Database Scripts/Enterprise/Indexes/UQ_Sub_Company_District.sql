USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Sub_Company_District]') AND name = N'Sub_Company_District_ID')
DROP INDEX [Sub_Company_District_ID] ON [dbo].[Sub_Company_District] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Sub_Company_District_ID] ON [dbo].[Sub_Company_District] 
(
	[Sub_Company_District_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Sub_Company_District]') AND name = N'Sub_Company_District_Staff_ID')
DROP INDEX [Sub_Company_District_Staff_ID] ON [dbo].[Sub_Company_District] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Sub_Company_District_Staff_ID] ON [dbo].[Sub_Company_District] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Sub_Company_District]') AND name = N'Sub_Company_District_Sub_Company_Area_ID')
DROP INDEX [Sub_Company_District_Sub_Company_Area_ID] ON [dbo].[Sub_Company_District] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Sub_Company_District_Sub_Company_Area_ID] ON [dbo].[Sub_Company_District] 
(
	[Sub_Company_Area_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

