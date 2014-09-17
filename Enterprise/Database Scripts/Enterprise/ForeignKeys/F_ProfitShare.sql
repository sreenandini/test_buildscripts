USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProfitShare_ProfitShareGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProfitShare]'))

ALTER TABLE [dbo].[ProfitShare]  WITH CHECK ADD  CONSTRAINT [FK_ProfitShare_ProfitShareGroup] FOREIGN KEY([ProfitShareGroupId])
REFERENCES [ProfitShareGroup] ([ProfitShareGroupId])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProfitShare_ShareHolders]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProfitShare]'))

ALTER TABLE [dbo].[ProfitShare]  WITH CHECK ADD  CONSTRAINT [FK_ProfitShare_ShareHolders] FOREIGN KEY([ShareHolderId])
REFERENCES [ShareHolders] ([ShareHolderId])
GO

