USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[User_Group]') AND name = N'User_Group_HQ_User_Access_ID')
DROP INDEX [User_Group_HQ_User_Access_ID] ON [dbo].[User_Group] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [User_Group_HQ_User_Access_ID] ON [dbo].[User_Group] 
(
	[HQ_User_Access_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[User_Group]') AND name = N'User_Group_ID')
DROP INDEX [User_Group_ID] ON [dbo].[User_Group] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [User_Group_ID] ON [dbo].[User_Group] 
(
	[User_Group_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

