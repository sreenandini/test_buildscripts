USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_Master_Type]') AND name = N'Event_Master_Type_ID')
DROP INDEX [Event_Master_Type_ID] ON [dbo].[Event_Master_Type] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Event_Master_Type_ID] ON [dbo].[Event_Master_Type] 
(
	[Event_Master_Type_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

