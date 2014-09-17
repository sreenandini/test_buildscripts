USE [Audit]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Site_Audit_History]') AND type in (N'U'))

BEGIN

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[Site_Audit_History](
	[Audit_ID] [bigint] NOT NULL,
	[Audit_Date] [datetime] NULL,
	[Audit_User_ID] [int] NULL,
	[Audit_User_Name] [varchar](50) NULL,
	[Audit_Module_ID] [int] NULL,
	[Audit_Module_Name] [varchar](50) NULL,
	[Audit_Screen_Name] [varchar](50) NULL,
	[Audit_Desc] [varchar](500) NULL,
	[Audit_Slot] [varchar](50) NULL,
	[Audit_Field] [varchar](100) NULL,
	[Audit_Old_Vl] [varchar](100) NULL,
	[Audit_New_Vl] [varchar](100) NULL,
	[Audit_Operation_Type] [varchar](25) NULL,
	[Site_ID] [int] NOT NULL
) ON [PRIMARY]

END
GO

IF EXISTS(SELECT 1 FROM SYS.COLUMNS WHERE NAME = 'Audit_Old_Vl' AND OBJECT_NAME(OBJECT_ID) = 'Site_Audit_History')
BEGIN
	ALTER TABLE dbo.Site_Audit_History ALTER COLUMN [Audit_Old_Vl] VARCHAR(500) NULL
END
GO

IF EXISTS(SELECT 1 FROM SYS.COLUMNS WHERE NAME = 'Audit_New_Vl' AND OBJECT_NAME(OBJECT_ID) = 'Site_Audit_History')
BEGIN
	ALTER TABLE dbo.Site_Audit_History ALTER COLUMN Audit_New_Vl VARCHAR(500) NULL
END
GO



