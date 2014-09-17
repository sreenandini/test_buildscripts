USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Secondary_Sub_Company_Period_End]') AND name = N'Secondary_Sub_Company_Period_End_ID')
DROP INDEX [Secondary_Sub_Company_Period_End_ID] ON [dbo].[Secondary_Sub_Company_Period_End] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Secondary_Sub_Company_Period_End_ID] ON [dbo].[Secondary_Sub_Company_Period_End] 
(
	[Secondary_Sub_Company_Period_End_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

