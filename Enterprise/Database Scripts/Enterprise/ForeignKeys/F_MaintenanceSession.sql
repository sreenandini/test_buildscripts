USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Maintenan__Insta__529D2C8D]') AND parent_object_id = OBJECT_ID(N'[dbo].[MaintenanceSession]'))

ALTER TABLE [dbo].[MaintenanceSession]  WITH CHECK ADD  CONSTRAINT [FK__Maintenan__Insta__529D2C8D] FOREIGN KEY([Installation_No])
REFERENCES [Installation] ([Installation_ID])
GO

