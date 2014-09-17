USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Company_Model_Set]') AND name = N'Company_Model_Set_ID')
DROP INDEX [Company_Model_Set_ID] ON [dbo].[Company_Model_Set] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Company_Model_Set_ID] ON [dbo].[Company_Model_Set] 
(
	[Company_Model_Set_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

