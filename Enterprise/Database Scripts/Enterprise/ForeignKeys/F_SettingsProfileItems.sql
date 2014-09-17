USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__SettingsP__Setti__52F23248]') AND parent_object_id = OBJECT_ID(N'[dbo].[SettingsProfileItems]'))

ALTER TABLE [dbo].[SettingsProfileItems]  WITH CHECK ADD  CONSTRAINT [FK__SettingsP__Setti__52F23248] FOREIGN KEY([SettingsProfileItems_SettingsProfile_ID])
REFERENCES [SettingsProfile] ([SettingsProfile_ID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__SettingsP__Setti__53E65681]') AND parent_object_id = OBJECT_ID(N'[dbo].[SettingsProfileItems]'))

ALTER TABLE [dbo].[SettingsProfileItems]  WITH CHECK ADD  CONSTRAINT [FK__SettingsP__Setti__53E65681] FOREIGN KEY([SettingsProfileItems_SettingsMaster_ID])
REFERENCES [SettingsMaster] ([SettingsMaster_ID])
GO

