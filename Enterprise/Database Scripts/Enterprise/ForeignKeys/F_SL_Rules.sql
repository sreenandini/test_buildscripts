USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SL_Rules_CreatedStaffID]') AND parent_object_id = OBJECT_ID(N'[dbo].[SL_Rules]'))

ALTER TABLE [dbo].[SL_Rules]  WITH CHECK ADD  CONSTRAINT [FK_SL_Rules_CreatedStaffID] FOREIGN KEY([CreatedStaffID])
REFERENCES [Staff] ([Staff_ID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SL_Rules_ModifiedStaffID]') AND parent_object_id = OBJECT_ID(N'[dbo].[SL_Rules]'))

ALTER TABLE [dbo].[SL_Rules]  WITH CHECK ADD  CONSTRAINT [FK_SL_Rules_ModifiedStaffID] FOREIGN KEY([ModifiedStaffID])
REFERENCES [Staff] ([Staff_ID])
GO

