USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Site__NGA_Machin__4DAB2CE2]') AND parent_object_id = OBJECT_ID(N'[dbo].[Site]'))

ALTER TABLE [dbo].[Site]  WITH CHECK ADD  CONSTRAINT [FK__Site__NGA_Machin__4DAB2CE2] FOREIGN KEY([NGA_Machine_ID])
REFERENCES [Machine] ([Machine_ID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Site__Site_Setti__564072E3]') AND parent_object_id = OBJECT_ID(N'[dbo].[Site]'))

ALTER TABLE [dbo].[Site]  WITH CHECK ADD  CONSTRAINT [FK__Site__Site_Setti__564072E3] FOREIGN KEY([Site_Setting_Profile_ID])
REFERENCES [SettingsProfile] ([SettingsProfile_ID])
GO

