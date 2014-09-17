USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleAccessRole_lnk_ROLE]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleAccessRole_lnk]'))

ALTER TABLE [dbo].[RoleAccessRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK_RoleAccessRole_lnk_ROLE] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleAccessRole_lnk_RoleAccess]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleAccessRole_lnk]'))

ALTER TABLE [dbo].[RoleAccessRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK_RoleAccessRole_lnk_RoleAccess] FOREIGN KEY([RoleAccessID])
REFERENCES [RoleAccess] ([RoleAccessID])
GO

