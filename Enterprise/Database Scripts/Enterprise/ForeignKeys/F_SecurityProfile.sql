USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__SecurityP__Custo__4362A899]') AND parent_object_id = OBJECT_ID(N'[dbo].[SecurityProfile]'))

ALTER TABLE [dbo].[SecurityProfile]  WITH CHECK ADD  CONSTRAINT [FK__SecurityP__Custo__4362A899] FOREIGN KEY([Customer_Access_ID])
REFERENCES [Customer_Access] ([Customer_Access_ID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__SecurityP__Secur__4456CCD2]') AND parent_object_id = OBJECT_ID(N'[dbo].[SecurityProfile]'))

ALTER TABLE [dbo].[SecurityProfile]  WITH CHECK ADD  CONSTRAINT [FK__SecurityP__Secur__4456CCD2] FOREIGN KEY([SecurityProfileType_ID])
REFERENCES [SecurityProfileType] ([SecurityProfileType_ID])
GO

