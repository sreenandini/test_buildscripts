USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SL_LicenseInfo_ActivatedStaffID]') AND parent_object_id = OBJECT_ID(N'[dbo].[SL_LicenseInfo]'))

ALTER TABLE [dbo].[SL_LicenseInfo]  WITH CHECK ADD  CONSTRAINT [FK_SL_LicenseInfo_ActivatedStaffID] FOREIGN KEY([ActivatedStaffID])
REFERENCES [Staff] ([Staff_ID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SL_LicenseInfo_CancelledStaffID]') AND parent_object_id = OBJECT_ID(N'[dbo].[SL_LicenseInfo]'))

ALTER TABLE [dbo].[SL_LicenseInfo]  WITH CHECK ADD  CONSTRAINT [FK_SL_LicenseInfo_CancelledStaffID] FOREIGN KEY([CancelledStaffID])
REFERENCES [Staff] ([Staff_ID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SL_LicenseInfo_CreatedStaffID]') AND parent_object_id = OBJECT_ID(N'[dbo].[SL_LicenseInfo]'))

ALTER TABLE [dbo].[SL_LicenseInfo]  WITH CHECK ADD  CONSTRAINT [FK_SL_LicenseInfo_CreatedStaffID] FOREIGN KEY([CreatedStaffID])
REFERENCES [Staff] ([Staff_ID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SL_LicenseInfo_ModifiedStaffID]') AND parent_object_id = OBJECT_ID(N'[dbo].[SL_LicenseInfo]'))

ALTER TABLE [dbo].[SL_LicenseInfo]  WITH CHECK ADD  CONSTRAINT [FK_SL_LicenseInfo_ModifiedStaffID] FOREIGN KEY([ModifiedStaffID])
REFERENCES [Staff] ([Staff_ID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SL_LicenseInfo_Site]') AND parent_object_id = OBJECT_ID(N'[dbo].[SL_LicenseInfo]'))

ALTER TABLE [dbo].[SL_LicenseInfo]  WITH CHECK ADD  CONSTRAINT [FK_SL_LicenseInfo_Site] FOREIGN KEY([Site_ID])
REFERENCES [Site] ([Site_ID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SL_LicenseInfo_SL_KeyStatus]') AND parent_object_id = OBJECT_ID(N'[dbo].[SL_LicenseInfo]'))

ALTER TABLE [dbo].[SL_LicenseInfo]  WITH CHECK ADD  CONSTRAINT [FK_SL_LicenseInfo_SL_KeyStatus] FOREIGN KEY([KeyStatusID])
REFERENCES [SL_KeyStatus] ([KeyStatusID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SL_LicenseInfo_SL_Rules]') AND parent_object_id = OBJECT_ID(N'[dbo].[SL_LicenseInfo]'))

ALTER TABLE [dbo].[SL_LicenseInfo]  WITH CHECK ADD  CONSTRAINT [FK_SL_LicenseInfo_SL_Rules] FOREIGN KEY([RuleID])
REFERENCES [SL_Rules] ([RuleID])
GO

