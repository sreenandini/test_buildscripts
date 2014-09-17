USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Company_User_Group_Link]') AND name = N'Company_User_Group_Link_Company_ID')
DROP INDEX [Company_User_Group_Link_Company_ID] ON [dbo].[Company_User_Group_Link] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Company_User_Group_Link_Company_ID] ON [dbo].[Company_User_Group_Link] 
(
	[Company_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Company_User_Group_Link]') AND name = N'Company_User_Group_Link_ID')
DROP INDEX [Company_User_Group_Link_ID] ON [dbo].[Company_User_Group_Link] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Company_User_Group_Link_ID] ON [dbo].[Company_User_Group_Link] 
(
	[Company_User_Group_Link_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

