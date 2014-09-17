USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TransactionKeys_SiteID]') AND parent_object_id = OBJECT_ID(N'[dbo].[TransactionKeys]'))

ALTER TABLE [dbo].[TransactionKeys]  WITH CHECK ADD  CONSTRAINT [FK_TransactionKeys_SiteID] FOREIGN KEY([SiteID])
REFERENCES [Site] ([Site_ID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TransactionKeys_TransactionFlagID]') AND parent_object_id = OBJECT_ID(N'[dbo].[TransactionKeys]'))

ALTER TABLE [dbo].[TransactionKeys]  WITH CHECK ADD  CONSTRAINT [FK_TransactionKeys_TransactionFlagID] FOREIGN KEY([TransactionFlagID])
REFERENCES [TransactionFlag] ([TransactionFlagID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TransactionKeys_UserID]') AND parent_object_id = OBJECT_ID(N'[dbo].[TransactionKeys]'))

ALTER TABLE [dbo].[TransactionKeys]  WITH CHECK ADD  CONSTRAINT [FK_TransactionKeys_UserID] FOREIGN KEY([UserID])
REFERENCES [Staff] ([Staff_ID])
GO

