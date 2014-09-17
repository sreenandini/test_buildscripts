USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Maintenan__Event__7121B3AD]') AND parent_object_id = OBJECT_ID(N'[dbo].[MaintenanceHistory]'))

ALTER TABLE [dbo].[MaintenanceHistory]  WITH CHECK ADD  CONSTRAINT [FK__Maintenan__Event__7121B3AD] FOREIGN KEY([EventID])
REFERENCES [DoorEvent_lkp] ([EventID])
GO

