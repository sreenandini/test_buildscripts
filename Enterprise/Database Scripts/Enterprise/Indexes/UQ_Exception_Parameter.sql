USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Exception_Parameter]') AND name = N'Exception_Parameter_ID')
DROP INDEX [Exception_Parameter_ID] ON [dbo].[Exception_Parameter] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Exception_Parameter_ID] ON [dbo].[Exception_Parameter] 
(
	[Exception_Parameter_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Exception_Parameter]') AND name = N'Exception_Parameter_Machine_Type_ID')
DROP INDEX [Exception_Parameter_Machine_Type_ID] ON [dbo].[Exception_Parameter] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Exception_Parameter_Machine_Type_ID] ON [dbo].[Exception_Parameter] 
(
	[Machine_Type_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Exception_Parameter]') AND name = N'Exception_Parameter_Sub_Company_ID')
DROP INDEX [Exception_Parameter_Sub_Company_ID] ON [dbo].[Exception_Parameter] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Exception_Parameter_Sub_Company_ID] ON [dbo].[Exception_Parameter] 
(
	[Sub_Company_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

