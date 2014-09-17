USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Route__User_id__2469202D]') AND parent_object_id = OBJECT_ID(N'[dbo].[Route]'))

ALTER TABLE [dbo].[Route]  WITH CHECK ADD  CONSTRAINT [FK__Route__User_id__2469202D] FOREIGN KEY([User_id])
REFERENCES [USER] ([SecurityUserID])
GO

