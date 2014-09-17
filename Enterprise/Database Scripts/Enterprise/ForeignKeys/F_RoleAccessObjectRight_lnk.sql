USE [Enterprise]
GO

--IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleAccessObjectRight_lnk_Object]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleAccessObjectRight_lnk]'))

--ALTER TABLE [dbo].[RoleAccessObjectRight_lnk]  WITH CHECK ADD  CONSTRAINT [FK_RoleAccessObjectRight_lnk_Object] FOREIGN KEY([SecurityObjectID])
--REFERENCES [Object] ([SecurityObjectID])
--GO

--IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleAccessObjectRight_lnk_Right]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleAccessObjectRight_lnk]'))

--ALTER TABLE [dbo].[RoleAccessObjectRight_lnk]  WITH CHECK ADD  CONSTRAINT [FK_RoleAccessObjectRight_lnk_Right] FOREIGN KEY([SecurityRightID])
--REFERENCES [Right] ([SecurityRightID])
--GO

--IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleAccessObjectRight_lnk_RoleAccess]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleAccessObjectRight_lnk]'))

--ALTER TABLE [dbo].[RoleAccessObjectRight_lnk]  WITH CHECK ADD  CONSTRAINT [FK_RoleAccessObjectRight_lnk_RoleAccess] FOREIGN KEY([RoleAccessID])
--REFERENCES [RoleAccess] ([RoleAccessID])
--GO

