USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__0024001B]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__0024001B] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__00577397]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__00577397] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__01182454]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__01182454] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__053B58EE]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__053B58EE] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__062F7D27]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__062F7D27] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__08195C47]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__08195C47] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__090D8080]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__090D8080] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__0AB43B22]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__0AB43B22] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__0BA85F5B]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__0BA85F5B] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__0C533628]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__0C533628] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__0CBC7F25]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__0CBC7F25] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__0D475A61]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__0D475A61] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__0DB0A35E]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__0DB0A35E] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__0FC56B07]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__0FC56B07] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__10B98F40]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__10B98F40] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__180F0197]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__180F0197] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__18659E00]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__18659E00] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__18AEEB6C]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__18AEEB6C] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__190325D0]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__190325D0] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__1959C239]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__1959C239] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__19A30FA5]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__19A30FA5] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__1AC2E7BE]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__1AC2E7BE] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__1BB70BF7]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__1BB70BF7] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__1CFE9F3D]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__1CFE9F3D] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__1DF2C376]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__1DF2C376] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__2243429E]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__2243429E] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__224E2F96]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__224E2F96] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__233766D7]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__233766D7] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__234253CF]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__234253CF] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__247EFA1D]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__247EFA1D] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__25731E56]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__25731E56] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__2AC2AEAF]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__2AC2AEAF] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__2BB6D2E8]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__2BB6D2E8] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__2DCACF3A]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__2DCACF3A] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__2EBEF373]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__2EBEF373] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__34C04EDE]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__34C04EDE] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__35B47317]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__35B47317] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__36A00C5D]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__36A00C5D] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__37943096]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__37943096] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__37A7A881]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__37A7A881] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__389BCCBA]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__389BCCBA] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__3985CF52]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__3985CF52] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__3A592CA3]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__3A592CA3] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__3A79F38B]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__3A79F38B] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__3B4D50DC]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__3B4D50DC] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__3C7815ED]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__3C7815ED] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__3D614D2E]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__3D614D2E] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__3D6C3A26]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__3D6C3A26] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__3E1ED08F]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__3E1ED08F] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__3E557167]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__3E557167] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__3F12F4C8]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__3F12F4C8] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__3F92E30C]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__3F92E30C] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__40870745]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__40870745] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__47009158]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__47009158] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__47F4B591]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__47F4B591] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__501D8539]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__501D8539] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__505E47B2]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__505E47B2] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__5111A972]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__5111A972] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__51526BEB]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__51526BEB] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__56A1FC44]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__56A1FC44] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__5796207D]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__5796207D] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__59737BF7]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__59737BF7] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__5A67A030]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__5A67A030] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__5B50D771]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__5B50D771] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__5C44FBAA]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__5C44FBAA] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__5CFB8AC6]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__5CFB8AC6] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__5D6271BE]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__5D6271BE] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__5DEFAEFF]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__5DEFAEFF] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__5E5695F7]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__5E5695F7] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__5F422F3D]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__5F422F3D] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__60365376]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__60365376] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__6054B859]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__6054B859] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__6148DC92]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__6148DC92] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__61F2E808]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__61F2E808] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__62E70C41]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__62E70C41] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__65238F17]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__65238F17] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__6617B350]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__6617B350] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__67E26236]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__67E26236] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__68D6866F]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__68D6866F] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__696161AB]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__696161AB] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__6A5585E4]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__6A5585E4] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__6E6D022F]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__6E6D022F] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__6F612668]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__6F612668] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__71C006D4]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__71C006D4] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__72B42B0D]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__72B42B0D] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__732795AB]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__732795AB] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__741BB9E4]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__741BB9E4] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__7783CD22]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__7783CD22] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__7877F15B]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__7877F15B] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__7B33971E]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__7B33971E] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__7B4A3C65]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__7B4A3C65] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__7C27BB57]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__7C27BB57] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__7C3E609E]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__7C3E609E] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__7CE60A0F]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__7CE60A0F] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__7D0605A0]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__7D0605A0] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__7DDA2E48]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__7DDA2E48] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__7DFA29D9]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__7DFA29D9] FOREIGN KEY([SecurityRoleID])
REFERENCES [ROLE] ([SecurityRoleID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__UserRole___Secur__7F634F5E]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole_lnk]'))

ALTER TABLE [dbo].[UserRole_lnk]  WITH CHECK ADD  CONSTRAINT [FK__UserRole___Secur__7F634F5E] FOREIGN KEY([SecurityUserID])
REFERENCES [USER] ([SecurityUserID])
GO

