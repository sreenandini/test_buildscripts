USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RDCColVar]') AND name = N'RDCColVar_ID')
DROP INDEX [RDCColVar_ID] ON [dbo].[RDCColVar] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [RDCColVar_ID] ON [dbo].[RDCColVar] 
(
	[RDCColVar_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RDCColVar]') AND name = N'RDCColVar_Machine_Type_ID')
DROP INDEX [RDCColVar_Machine_Type_ID] ON [dbo].[RDCColVar] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [RDCColVar_Machine_Type_ID] ON [dbo].[RDCColVar] 
(
	[Machine_Type_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RDCColVar]') AND name = N'RDCColVar_Sub_Company_ID')
DROP INDEX [RDCColVar_Sub_Company_ID] ON [dbo].[RDCColVar] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [RDCColVar_Sub_Company_ID] ON [dbo].[RDCColVar] 
(
	[Sub_Company_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

