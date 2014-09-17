USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ExpenseShare_ExpenseShareGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[ExpenseShare]'))

ALTER TABLE [dbo].[ExpenseShare]  WITH CHECK ADD  CONSTRAINT [FK_ExpenseShare_ExpenseShareGroup] FOREIGN KEY([ExpenseShareGroupId])
REFERENCES [ExpenseShareGroup] ([ExpenseShareGroupId])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ExpenseShare_ShareHolders]') AND parent_object_id = OBJECT_ID(N'[dbo].[ExpenseShare]'))

ALTER TABLE [dbo].[ExpenseShare]  WITH CHECK ADD  CONSTRAINT [FK_ExpenseShare_ShareHolders] FOREIGN KEY([ShareHolderId])
REFERENCES [ShareHolders] ([ShareHolderId])
GO

