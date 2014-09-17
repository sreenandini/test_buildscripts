USE [Enterprise]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__044734B5]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__044734B5] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__0725380E]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__0725380E] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__09C016E9]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__09C016E9] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__0B5F11EF]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__0B5F11EF] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__0BC85AEC]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__0BC85AEC] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__0ED146CE]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__0ED146CE] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__171ADD5E]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__171ADD5E] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__177179C7]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__177179C7] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__17BAC733]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__17BAC733] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__19CEC385]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__19CEC385] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__1C0A7B04]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__1C0A7B04] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__214F1E65]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__214F1E65] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__215A0B5D]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__215A0B5D] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__238AD5E4]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__238AD5E4] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__29CE8A76]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__29CE8A76] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__2CD6AB01]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__2CD6AB01] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__33CC2AA5]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__33CC2AA5] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__35ABE824]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__35ABE824] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__36B38448]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__36B38448] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__3891AB19]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__3891AB19] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__3965086A]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__3965086A] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__3B83F1B4]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__3B83F1B4] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__3C6D28F5]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__3C6D28F5] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__3D2AAC56]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__3D2AAC56] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__3E9EBED3]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__3E9EBED3] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__460C6D1F]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__460C6D1F] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__4F296100]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__4F296100] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__4F6A2379]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__4F6A2379] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__55ADD80B]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__55ADD80B] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__587F57BE]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__587F57BE] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__5A5CB338]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__5A5CB338] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__5C07668D]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__5C07668D] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__5C6E4D85]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__5C6E4D85] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__5E4E0B04]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__5E4E0B04] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__5F609420]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__5F609420] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__60FEC3CF]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__60FEC3CF] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__642F6ADE]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__642F6ADE] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__66EE3DFD]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__66EE3DFD] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__686D3D72]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__686D3D72] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__6D78DDF6]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__6D78DDF6] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__70CBE29B]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__70CBE29B] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__72337172]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__72337172] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__768FA8E9]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__768FA8E9] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__7A3F72E5]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__7A3F72E5] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__7A56182C]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__7A56182C] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__7BF1E5D6]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__7BF1E5D6] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__7C11E167]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__7C11E167] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__7E6F2B25]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__7E6F2B25] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Object__ObjectTy__7F2FDBE2]') AND parent_object_id = OBJECT_ID(N'[dbo].[Object]'))

ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK__Object__ObjectTy__7F2FDBE2] FOREIGN KEY([ObjectType])
REFERENCES [SecurityObjectType] ([SecurityObjectTypeID])
GO

