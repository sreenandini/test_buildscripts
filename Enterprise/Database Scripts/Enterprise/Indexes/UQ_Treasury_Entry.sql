USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Treasury_Entry]') AND name = N'Treasury_Entry_Collection_ID')
DROP INDEX [Treasury_Entry_Collection_ID] ON [dbo].[Treasury_Entry] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Treasury_Entry_Collection_ID] ON [dbo].[Treasury_Entry] 
(
	[Collection_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Treasury_Entry]') AND name = N'Treasury_Entry_Installation_ID')
DROP INDEX [Treasury_Entry_Installation_ID] ON [dbo].[Treasury_Entry] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Treasury_Entry_Installation_ID] ON [dbo].[Treasury_Entry] 
(
	[Installation_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Treasury_Entry]') AND name = N'Treasury_Entry_Treasury_Type')
DROP INDEX [Treasury_Entry_Treasury_Type] ON [dbo].[Treasury_Entry] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Treasury_Entry_Treasury_Type] ON [dbo].[Treasury_Entry] 
(
	[Treasury_Type] ASC
)
INCLUDE ( [Collection_ID], [Treasury_Amount]) 
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Treasury_Entry]') AND name = N'IX_Treasury_Entry_Reason_Code')
DROP INDEX [IX_Treasury_Entry_Reason_Code] ON [dbo].[Treasury_Entry] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [IX_Treasury_Entry_Reason_Code]
ON [dbo].[Treasury_Entry] ([Treasury_Reason_Code],[Collection_ID],[Treasury_Type])
INCLUDE ([Installation_ID],[Treasury_Date],[Treasury_Time],[Treasury_Amount])
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Treasury_Entry]') AND name = N'IX_Treasury_Entry_CollectionID')
DROP INDEX [IX_Treasury_Entry_CollectionID] ON [dbo].[Treasury_Entry] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [IX_Treasury_Entry_CollectionID]
ON [dbo].[Treasury_Entry] ([Collection_ID],[Treasury_Reason],[Treasury_Type])
INCLUDE ([Installation_ID],[Treasury_Date],[Treasury_Time],[Treasury_Amount],[Treasury_Membership_No])
GO