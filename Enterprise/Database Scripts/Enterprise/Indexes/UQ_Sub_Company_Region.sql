USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Sub_Company_Region]') AND name = N'Sub_Company_Region_ID')
DROP INDEX [Sub_Company_Region_ID] ON [dbo].[Sub_Company_Region] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Sub_Company_Region_ID] ON [dbo].[Sub_Company_Region] 
(
	[Sub_Company_Region_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Sub_Company_Region]') AND name = N'Sub_Company_Region_Staff_ID')
DROP INDEX [Sub_Company_Region_Staff_ID] ON [dbo].[Sub_Company_Region] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Sub_Company_Region_Staff_ID] ON [dbo].[Sub_Company_Region] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Sub_Company_Region]') AND name = N'Sub_Company_Region_Sub_Company_ID')
DROP INDEX [Sub_Company_Region_Sub_Company_ID] ON [dbo].[Sub_Company_Region] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Sub_Company_Region_Sub_Company_ID] ON [dbo].[Sub_Company_Region] 
(
	[Sub_Company_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

