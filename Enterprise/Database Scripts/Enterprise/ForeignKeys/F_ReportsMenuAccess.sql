USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ReportsMenuAccess_ReportID]') AND parent_object_id = OBJECT_ID(N'[dbo].[ReportsMenuAccess]'))

ALTER TABLE [dbo].[ReportsMenuAccess]  WITH CHECK ADD  CONSTRAINT [FK_ReportsMenuAccess_ReportID] FOREIGN KEY([ReportID])
REFERENCES [ReportsMenu] ([ReportID])
GO

