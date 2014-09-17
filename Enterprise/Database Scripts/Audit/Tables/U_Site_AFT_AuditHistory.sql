USE [Audit]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Site_AFT_AuditHistory]') AND type in (N'U'))

BEGIN

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[Site_AFT_AuditHistory](
	[AFT_Audit_ID] [bigint] NOT NULL,
	[AFT_InstallationNo] [int] NULL,
	[AFT_TransactionDate] [datetime] NULL,
	[AFT_TransactionType] [varchar](30) NULL,
	[CashableAmt] [float] NULL,
	[NonCashableAmt] [float] NULL,
	[WATAmt] [float] NULL,
	[AFT_PlayerID] [int] NULL,
	[AFT_FirstName] [varchar](50) NULL,
	[AFT_LastName] [varchar](50) NULL,
	[AFT_ECash_Enabled] [bit] NULL,
	[AFT_Error_Code] [int] NULL,
	[AFT_Error_Message] [varchar](100) NULL,
	[Site_ID] [int] NOT NULL
) ON [PRIMARY]

END
IF NOT EXISTS (SELECT * FROM sys.columns WHERE NAME ='AFT_TransactionID' AND OBJECT_ID = OBJECT_ID ('Site_AFT_AuditHistory'))
ALTER TABLE  [dbo].[Site_AFT_AuditHistory] ADD AFT_TransactionID INT NOT NULL
GO

