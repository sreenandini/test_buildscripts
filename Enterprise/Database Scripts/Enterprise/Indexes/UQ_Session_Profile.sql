USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Session_Profile]') AND name = N'Session_Profile_ID')
DROP INDEX [Session_Profile_ID] ON [dbo].[Session_Profile] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Session_Profile_ID] ON [dbo].[Session_Profile] 
(
	[Session_Profile_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Session_Profile]') AND name = N'Session_Profile_Sub_Company_ID')
DROP INDEX [Session_Profile_Sub_Company_ID] ON [dbo].[Session_Profile] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Session_Profile_Sub_Company_ID] ON [dbo].[Session_Profile] 
(
	[Sub_Company_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

