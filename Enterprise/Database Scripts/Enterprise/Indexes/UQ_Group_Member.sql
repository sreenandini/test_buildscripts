USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Group_Member]') AND name = N'Group_Member_Bar_Position_ID')
DROP INDEX [Group_Member_Bar_Position_ID] ON [dbo].[Group_Member] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Group_Member_Bar_Position_ID] ON [dbo].[Group_Member] 
(
	[Bar_Position_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Group_Member]') AND name = N'Group_Member_Group_ID')
DROP INDEX [Group_Member_Group_ID] ON [dbo].[Group_Member] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Group_Member_Group_ID] ON [dbo].[Group_Member] 
(
	[Group_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Group_Member]') AND name = N'Group_Member_ID')
DROP INDEX [Group_Member_ID] ON [dbo].[Group_Member] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Group_Member_ID] ON [dbo].[Group_Member] 
(
	[Group_Member_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

