USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RoleAccess]') AND name = N'IX_RoleAccess')
DROP INDEX [IX_RoleAccess] ON [dbo].[RoleAccess] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_RoleAccess] ON [dbo].[RoleAccess] 
(
	[RoleAccessID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

