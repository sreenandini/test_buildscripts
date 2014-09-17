USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[HQ_User_Access]') AND name = N'HQ_User_Access_ID')
DROP INDEX [HQ_User_Access_ID] ON [dbo].[HQ_User_Access] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [HQ_User_Access_ID] ON [dbo].[HQ_User_Access] 
(
	[HQ_User_Access_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

