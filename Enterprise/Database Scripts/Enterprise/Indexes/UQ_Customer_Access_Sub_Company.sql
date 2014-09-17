USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer_Access_Sub_Company]') AND name = N'Customer_Access_Sub_Company_Customer_Access_ID')
DROP INDEX [Customer_Access_Sub_Company_Customer_Access_ID] ON [dbo].[Customer_Access_Sub_Company] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Customer_Access_Sub_Company_Customer_Access_ID] ON [dbo].[Customer_Access_Sub_Company] 
(
	[Customer_Access_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer_Access_Sub_Company]') AND name = N'Customer_Access_Sub_Company_ID')
DROP INDEX [Customer_Access_Sub_Company_ID] ON [dbo].[Customer_Access_Sub_Company] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Customer_Access_Sub_Company_ID] ON [dbo].[Customer_Access_Sub_Company] 
(
	[Customer_Access_Sub_Company_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer_Access_Sub_Company]') AND name = N'Customer_Access_Sub_Company_Sub_Company_ID')
DROP INDEX [Customer_Access_Sub_Company_Sub_Company_ID] ON [dbo].[Customer_Access_Sub_Company] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Customer_Access_Sub_Company_Sub_Company_ID] ON [dbo].[Customer_Access_Sub_Company] 
(
	[Sub_Company_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

