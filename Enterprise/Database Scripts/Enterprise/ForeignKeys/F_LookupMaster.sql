USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__LookupMast__Code__4FC0BFE2]') AND parent_object_id = OBJECT_ID(N'[dbo].[LookupMaster]'))

ALTER TABLE [dbo].[LookupMaster]  WITH CHECK ADD  CONSTRAINT [FK__LookupMast__Code__4FC0BFE2] FOREIGN KEY([Code])
REFERENCES [CodeMaster] ([Code])
GO

