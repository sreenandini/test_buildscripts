USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__MGMD_Game]') AND parent_object_id = OBJECT_ID(N'[dbo].[MGMD_Installation]'))

ALTER TABLE [dbo].[MGMD_Installation]  WITH CHECK ADD  CONSTRAINT [FK__MGMD_Game] FOREIGN KEY([MGMD_Game_ID])
REFERENCES [Game_Library] ([MG_Game_ID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__MGMD_Install]') AND parent_object_id = OBJECT_ID(N'[dbo].[MGMD_Installation]'))

ALTER TABLE [dbo].[MGMD_Installation]  WITH CHECK ADD  CONSTRAINT [FK__MGMD_Install] FOREIGN KEY([MGMD_Installation_ID])
REFERENCES [Installation] ([Installation_ID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__MGMD_Paytable]') AND parent_object_id = OBJECT_ID(N'[dbo].[MGMD_Installation]'))

ALTER TABLE [dbo].[MGMD_Installation]  WITH CHECK ADD  CONSTRAINT [FK__MGMD_Paytable] FOREIGN KEY([MGMD_Paytable_ID])
REFERENCES [PayTable] ([Paytable_ID])
GO

