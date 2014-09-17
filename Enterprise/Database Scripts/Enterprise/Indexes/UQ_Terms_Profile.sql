USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Terms_Profile]') AND name = N'Terms_Profile_ID')
DROP INDEX [Terms_Profile_ID] ON [dbo].[Terms_Profile] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Terms_Profile_ID] ON [dbo].[Terms_Profile] 
(
	[Terms_Profile_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Terms_Profile]') AND name = N'Terms_Profile_Machine_Type_ID')
DROP INDEX [Terms_Profile_Machine_Type_ID] ON [dbo].[Terms_Profile] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Terms_Profile_Machine_Type_ID] ON [dbo].[Terms_Profile] 
(
	[Machine_Type_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Terms_Profile]') AND name = N'Terms_Profile_Terms_Group_ID')
DROP INDEX [Terms_Profile_Terms_Group_ID] ON [dbo].[Terms_Profile] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Terms_Profile_Terms_Group_ID] ON [dbo].[Terms_Profile] 
(
	[Terms_Group_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

