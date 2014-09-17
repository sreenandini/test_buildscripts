USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserSiteMapping_SiteID]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserSiteMapping]'))

ALTER TABLE [dbo].[UserSiteMapping]  WITH CHECK ADD  CONSTRAINT [FK_UserSiteMapping_SiteID] FOREIGN KEY([SiteID])
REFERENCES [Site] ([Site_ID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserSiteMapping_UserID]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserSiteMapping]'))

ALTER TABLE [dbo].[UserSiteMapping]  WITH CHECK ADD  CONSTRAINT [FK_UserSiteMapping_UserID] FOREIGN KEY([UserID])
REFERENCES [Staff] ([Staff_ID])
GO

